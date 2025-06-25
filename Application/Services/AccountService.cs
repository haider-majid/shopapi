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

        public AccountService(IAccountRepository repository, IMapper mapper, IValidationService validationService, TokenService tokenService)
        {
            _repository = repository;
            _mapper = mapper;
            _validationService = validationService;
            _tokenService = tokenService;
        }

        public async Task<IEnumerable<GetAccountDto>> GetAllAsync()
        {
            var accounts = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<GetAccountDto>>(accounts);
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
            return _mapper.Map<GetAccountDto>(created);
        }

        public async Task<bool> UpdateAsync(UpdateAccountDto dto)
        {
            await _validationService.ValidateAsync(dto);
            var account = _mapper.Map<Account>(dto);
            await _repository.UpdateAsync(account);
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
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
    }
}