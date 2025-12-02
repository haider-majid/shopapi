using Presentation.Dto.Category;
using Presentation.Dto.Product;

namespace Application.Services;

public class CachedCategoryService : ICategoryService
{
    private readonly ICategoryService _innerService;
    private readonly ICacheService _cacheService;

    public CachedCategoryService(ICategoryService innerService, ICacheService cacheService)
    {
        _innerService = innerService;
        _cacheService = cacheService;
    }

    public async Task<IEnumerable<GetCategoryDto>> GetAllAsync()
    {
        var cacheKey = _cacheService.GenerateKey("category", "getall");

        var cachedCategories = await _cacheService.GetAsync<IEnumerable<GetCategoryDto>>(cacheKey);
        if (cachedCategories != null)
            return cachedCategories;

        var categories = await _innerService.GetAllAsync();
        await _cacheService.SetAsync(cacheKey, categories);

        return categories;
    }

    public async Task<GetCategoryDto> CreateAsync(CreateCategoryDto dto)
    {
        var created = await _innerService.CreateAsync(dto);
        await InvalidateCategoryCache();
        return created;
    }

    public async Task<bool> UpdateAsync(Guid id, UpdateCategoryDto dto)
    {
        var updated = await _innerService.UpdateAsync(id, dto);
        if (updated)
        {
            await InvalidateCategoryCache();
        }
        return updated;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var deleted = await _innerService.DeleteAsync(id);
        if (deleted)
        {
            await InvalidateCategoryCache();
        }
        return deleted;
    }

    private async Task InvalidateCategoryCache()
    {
        var cacheKey = _cacheService.GenerateKey("category", "getall");
        await _cacheService.RemoveAsync(cacheKey);
    }
}