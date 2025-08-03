using AutoMapper;
using Domain.Interfaces;
using Domain.Entities;
using Presentation.Dto.Category;
using Presentation.Dto.Product;

namespace Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidationService _validationService;
        private readonly ICacheService _cacheService;

        public CategoryService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IValidationService validationService,
            ICacheService cacheService)
        {
            _unitOfWork = unitOfWork;
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
            var categories = await _unitOfWork.CategoryRepository.GetAllAsync();
            var categoryDtos = _mapper.Map<IEnumerable<GetCategoryDto>>(categories);

            // Cache the result
            await _cacheService.SetAsync(cacheKey, categoryDtos);

            return categoryDtos;
        }

        public async Task<GetCategoryDto> CreateAsync(CreateCategoryDto dto)
        {
            await _validationService.ValidateAsync(dto);

            var category = Category.Create(dto.Name);
            var created = await _unitOfWork.CategoryRepository.AddAsync(category);
            await _unitOfWork.SaveChangesAsync();

            // Invalidate cache after creating new category
            await InvalidateCategoryCache();

            return _mapper.Map<GetCategoryDto>(created);
        }

        public async Task<bool> UpdateAsync(Guid id, UpdateCategoryDto dto)
        {
            await _validationService.ValidateAsync(dto);

            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(id);
            if (category == null)
                return false;

            category.UpdateName(dto.Name);

            await _unitOfWork.CategoryRepository.UpdateAsync(category);
            await _unitOfWork.SaveChangesAsync();

            // Invalidate cache after updating category
            await InvalidateCategoryCache();

            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            await _unitOfWork.CategoryRepository.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();

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