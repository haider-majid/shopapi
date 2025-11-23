using Presentation.Dto.Category;
using Presentation.Dto.Product;

namespace Application.Services
{
    public class CachedProductService : IProductService
    {
        private readonly IProductService _innerService;
        private readonly ICacheService _cacheService;

        public CachedProductService(IProductService innerService, ICacheService cacheService)
        {
            _innerService = innerService;
            _cacheService = cacheService;
        }

        public async Task<IEnumerable<GetProductDto>> GetAllAsync()
        {
            var cacheKey = _cacheService.GenerateKey("product", "getall");

            var cachedProducts = await _cacheService.GetAsync<IEnumerable<GetProductDto>>(cacheKey);
            if (cachedProducts != null)
                return cachedProducts;

            var products = await _innerService.GetAllAsync();
            await _cacheService.SetAsync(cacheKey, products);

            return products;
        }

        public async Task<GetProductDto?> GetByIdAsync(Guid id)
        {
            // Optional: Cache individual products if needed, but for now we just delegate.
            // If we wanted to cache by ID, we could generate a key like "product:id:{id}"
            return await _innerService.GetByIdAsync(id);
        }

        public async Task<GetProductDto> CreateAsync(CreateProductDto dto)
        {
            var created = await _innerService.CreateAsync(dto);
            await InvalidateProductCache();
            return created;
        }

        public async Task<bool> UpdateAsync(Guid id, UpdateProductDto dto)
        {
            var updated = await _innerService.UpdateAsync(id, dto);
            if (updated)
            {
                await InvalidateProductCache();
            }
            return updated;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var deleted = await _innerService.DeleteAsync(id);
            if (deleted)
            {
                await InvalidateProductCache();
            }
            return deleted;
        }

        private async Task InvalidateProductCache()
        {
            var cacheKey = _cacheService.GenerateKey("product", "getall");
            await _cacheService.RemoveAsync(cacheKey);
        }
    }
}
