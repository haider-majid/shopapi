

using AutoMapper;
using Domain.Interfaces;
using Presentation.Dto.Profile;

namespace Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _repository;
        private readonly IMapper _mapper;
        private readonly IValidationService _validationService;

        public AccountService(IAccountRepository repository, IMapper mapper, IValidationService validationService)
        {
            _repository = repository;
            _mapper = mapper;
            _validationService = validationService;
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
    }

}