

using Presentation.Dto.Profile;

namespace Application.Services
{
    public interface IAccountService
    {
        Task<IEnumerable<GetAccountDto>> GetAllAsync();
        Task<GetAccountDto> CreateAsync(CreateAccountDto dto);
        Task<bool> UpdateAsync(UpdateAccountDto dto);
        Task<bool> DeleteAsync(Guid id);
        Task<GetAccountDto> GetByIdAsync(Guid id);
    }
}