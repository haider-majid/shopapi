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

        public CategoryService(
            ICategoryRepository repository,
            IMapper mapper,
            IValidationService validationService)
        {
            _repository = repository;
            _mapper = mapper;
            _validationService = validationService;
        }

        public async Task<IEnumerable<GetCategoryDto>> GetAllAsync()
        {
            var categories = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<GetCategoryDto>>(categories);
        }

        public async Task<GetCategoryDto> CreateAsync(CreateCategoryDto dto)
        {
            await _validationService.ValidateAsync(dto);

            var category = _mapper.Map<Category>(dto);
            await _repository.AddAsync(category);
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
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
            return true;
        }


    }
}