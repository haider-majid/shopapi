

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ShopDbContext _context;

        public CategoryRepository(ShopDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> AddAsync(Category category)
        {
            _context.Categories.Add(category);
            return category;
        }

        public async Task UpdateAsync(Category category)
        {
            _context.Categories.Update(category);
        }

        public async Task DeleteAsync(Guid id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
            }
        }

        public async Task<Category> GetByIdAsync(Guid id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
                return null;
            return category;
        }
    }
}