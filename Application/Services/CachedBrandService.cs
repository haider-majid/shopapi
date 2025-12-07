using Presentation.Dto.Brand;

namespace Application.Services;

public class CachedBrandService : IBrandService
{
    private readonly IBrandService _innerService;
    private readonly ICacheService _cacheService;

    public CachedBrandService(IBrandService innerService, ICacheService cacheService)
    {
        _innerService = innerService;
        _cacheService = cacheService;
    }

    public async Task<IEnumerable<GetBrandDto>> GetAllAsync()
    {
        var cacheKey = _cacheService.GenerateKey("brand", "getall");

        var cachedBrands = await _cacheService.GetAsync<IEnumerable<GetBrandDto>>(cacheKey);
        if (cachedBrands != null)
            return cachedBrands;

        var brands = await _innerService.GetAllAsync();
        await _cacheService.SetAsync(cacheKey, brands);

        return brands;
    }

    public async Task<GetBrandDto?> GetByIdAsync(Guid id)
    {
        return await _innerService.GetByIdAsync(id);
    }

    public async Task<GetBrandDto> CreateAsync(CreateBrandDto dto)
    {
        var created = await _innerService.CreateAsync(dto);
        await InvalidateBrandCache();
        return created;
    }

    public async Task<bool> UpdateAsync(Guid id, UpdateBrandDto dto)
    {
        var updated = await _innerService.UpdateAsync(id, dto);
        if (updated)
        {
            await InvalidateBrandCache();
        }
        return updated;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var deleted = await _innerService.DeleteAsync(id);
        if (deleted)
        {
            await InvalidateBrandCache();
        }
        return deleted;
    }

    private async Task InvalidateBrandCache()
    {
        var cacheKey = _cacheService.GenerateKey("brand", "getall");
        await _cacheService.RemoveAsync(cacheKey);
    }
}
