using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Repositories;


namespace Infrastructure.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly ShopDbContext _context;
    private IProductRepository? _productRepository;
    private ICategoryRepository? _categoryRepository;
    private IBrandRepository? _brandRepository;

    public UnitOfWork(ShopDbContext context)
    {
        _context = context;
    }

    public IProductRepository ProductRepository => _productRepository ??= new ProductRepository(_context);
    public ICategoryRepository CategoryRepository => _categoryRepository ??= new CategoryRepository(_context);
    public IBrandRepository BrandRepository => _brandRepository ??= new BrandRepository(_context);

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}