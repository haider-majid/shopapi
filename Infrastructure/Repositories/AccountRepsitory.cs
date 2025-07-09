using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ShopDbContext _context;

        public AccountRepository(ShopDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Account>> GetAllAsync()
        {
            return await _context.Accounts.ToListAsync();
        }

        public async Task<Account?> GetByIdAsync(Guid id)
        {
            return await _context.Accounts.FindAsync(id);
        }

        public async Task<Account> AddAsync(Account account)
        {
            _context.Accounts.Add(account);
            return account;
        }

        public async Task UpdateAsync(Account account)
        {
            _context.Accounts.Update(account);
        }

        public async Task DeleteAsync(Guid id)
        {
            var account = await _context.Accounts.FindAsync(id);
            if (account != null)
            {
                _context.Accounts.Remove(account);
            }
        }
    }
}