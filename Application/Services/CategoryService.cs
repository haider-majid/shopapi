using AutoMapper;
using Domain;
using Application;

namespace Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;
        private readonly IValidationService _validationService;
        private readonly ICacheService _cacheService;

        public CategoryService(
            ICategoryRepository repository,
            IMapper mapper,
            IValidationService validationService,
            ICacheService cacheService)
        {
            _repository = repository;
            _mapper = mapper;
            _validationService = validationService;
            _cacheService = cacheService;
        }

        public async Task<IEnumerable<GetCategoryDto>> GetAllAsync()
        {
            var cacheKey = _cacheService.GenerateKey("category", "getall");

            // Try to get from cache first
            var cachedCategories = await _cacheService.GetAsync<IEnumerable<GetCategoryDto>>(cacheKey);
            if (cachedCategories != null)
                return cachedCategories;

            // If not in cache, get from database
            var categories = await _repository.GetAllAsync();
            var categoryDtos = _mapper.Map<IEnumerable<GetCategoryDto>>(categories);

            // Cache the result
            await _cacheService.SetAsync(cacheKey, categoryDtos);

            return categoryDtos;
        }

        public async Task<GetCategoryDto> CreateAsync(CreateCategoryDto dto)
        {
            await _validationService.ValidateAsync(dto);

            var category = _mapper.Map<Category>(dto);
            await _repository.AddAsync(category);

            // Invalidate cache after creating new category
            await InvalidateCategoryCache();

            return _mapper.Map<GetCategoryDto>(category);
        }

        public async Task<bool> UpdateAsync(Guid id, UpdateCategoryDto dto)
        {
            await _validationService.ValidateAsync(dto);

            var category = await _repository.GetByIdAsync(id);
            if (category == null)
                return false;

            category.Name = dto.Name;

            await _repository.UpdateAsync(category);

            // Invalidate cache after updating category
            await InvalidateCategoryCache();

            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);

            // Invalidate cache after deleting category
            await InvalidateCategoryCache();

            return true;
        }

        private async Task InvalidateCategoryCache()
        {
            var cacheKey = _cacheService.GenerateKey("category", "getall");
            await _cacheService.RemoveAsync(cacheKey);
        }
    }
}