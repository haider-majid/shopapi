

namespace Domain.Interfaces
{
    public interface IAccountRepository
    {
        Task<IEnumerable<Account>> GetAllAsync();
        Task<Account?> GetByIdAsync(Guid id);
        Task<Account> AddAsync(Account profile);
        Task UpdateAsync(Account profile);
        Task DeleteAsync(Guid id);
    }
}