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

        public CategoryService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IValidationService validationService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validationService = validationService;
        }

        public async Task<IEnumerable<GetCategoryDto>> GetAllAsync()
        {
            var categories = await _unitOfWork.CategoryRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<GetCategoryDto>>(categories);
        }

        public async Task<GetCategoryDto> CreateAsync(CreateCategoryDto dto)
        {
            await _validationService.ValidateAsync(dto);

            var category = Category.Create(dto.Name);
            var created = await _unitOfWork.CategoryRepository.AddAsync(category);
            await _unitOfWork.SaveChangesAsync();

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

            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            await _unitOfWork.CategoryRepository.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}