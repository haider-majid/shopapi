using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class BrandRepository : IBrandRepository
{
    private readonly ShopDbContext _context;

    public BrandRepository(ShopDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Brand>> GetAllAsync()
    {
        return await _context.Brands.ToListAsync();
    }

    public async Task<Brand?> GetByIdAsync(Guid id)
    {
        return await _context.Brands.FindAsync(id);
    }

    public async Task<Brand> AddAsync(Brand brand)
    {
        await _context.Brands.AddAsync(brand);
        return brand;
    }

    public Task UpdateAsync(Brand brand)
    {
        _context.Brands.Update(brand);
        return Task.CompletedTask;
    }

    public async Task DeleteAsync(Guid id)
    {
        var brand = await _context.Brands.FindAsync(id);
        if (brand != null)
        {
            _context.Brands.Remove(brand);
        }
    }
}