using AutoMapper;
using Domain.Interfaces;
using Presentation.Dto.Profile;
using Presentation.Dto.Account;
using System.Security.Cryptography;
using System.Text;

namespace Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _repository;
        private readonly IMapper _mapper;
        private readonly IValidationService _validationService;
        private readonly TokenService _tokenService;
        private readonly ICacheService _cacheService;

        public AccountService(
            IAccountRepository repository,
            IMapper mapper,
            IValidationService validationService,
            TokenService tokenService,
            ICacheService cacheService)
        {
            _repository = repository;
            _mapper = mapper;
            _validationService = validationService;
            _tokenService = tokenService;
            _cacheService = cacheService;
        }

        public async Task<IEnumerable<GetAccountDto>> GetAllAsync()
        {
            var cacheKey = _cacheService.GenerateKey("account", "getall");

            // Try to get from cache first
            var cachedAccounts = await _cacheService.GetAsync<IEnumerable<GetAccountDto>>(cacheKey);
            if (cachedAccounts != null)
                return cachedAccounts;

            // If not in cache, get from database
            var accounts = await _repository.GetAllAsync();
            var accountDtos = _mapper.Map<IEnumerable<GetAccountDto>>(accounts);

            // Cache the result
            await _cacheService.SetAsync(cacheKey, accountDtos);

            return accountDtos;
        }

        public async Task<GetAccountDto> GetByIdAsync(Guid id)
        {
            var account = await _repository.GetByIdAsync(id);
            return _mapper.Map<GetAccountDto>(account);
        }

        public async Task<GetAccountDto> CreateAsync(CreateAccountDto dto)
        {
            await _validationService.ValidateAsync(dto);
            var account = _mapper.Map<Account>(dto);
            var created = await _repository.AddAsync(account);

            // Invalidate cache after creating new account
            await InvalidateAccountCache();

            return _mapper.Map<GetAccountDto>(created);
        }

        public async Task<bool> UpdateAsync(UpdateAccountDto dto)
        {
            await _validationService.ValidateAsync(dto);
            var account = _mapper.Map<Account>(dto);
            await _repository.UpdateAsync(account);

            // Invalidate cache after updating account
            await InvalidateAccountCache();

            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);

            // Invalidate cache after deleting account
            await InvalidateAccountCache();

            return true;
        }

        public async Task<GetAccountDto> RegisterAsync(RegisterDto dto)
        {
            // Hash the password
            using var sha256 = SHA256.Create();
            var passwordHash = Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(dto.Password)));
            var account = new Account
            {
                Name = dto.Name,
                Email = dto.Email,
                PasswordHash = passwordHash
            };
            var created = await _repository.AddAsync(account);

            // Invalidate cache after registering new account
            await InvalidateAccountCache();

            return _mapper.Map<GetAccountDto>(created);
        }

        public async Task<LoginResponseDto> LoginAsync(LoginDto dto)
        {
            var accounts = await _repository.GetAllAsync();
            var account = accounts.FirstOrDefault(a => a.Email == dto.Email);
            if (account == null)
                throw new Exception("Invalid credentials");
            using var sha256 = SHA256.Create();
            var passwordHash = Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(dto.Password)));
            if (account.PasswordHash != passwordHash)
                throw new Exception("Invalid credentials");
            // Generate JWT token
            var token = _tokenService.GenerateToken(account.Id, account.Email);
            return new LoginResponseDto
            {
                Token = token,
                Email = account.Email,
                Name = account.Name
            };
        }

        private async Task InvalidateAccountCache()
        {
            var cacheKey = _cacheService.GenerateKey("account", "getall");
            await _cacheService.RemoveAsync(cacheKey);
        }
    }
}