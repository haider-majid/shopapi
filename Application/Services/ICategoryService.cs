using Presentation.Dto.Category;
using Presentation.Dto.Product;

namespace Application.Services;

public interface ICategoryService
{
    Task<IEnumerable<GetCategoryDto>> GetAllAsync();
    Task<GetCategoryDto> CreateAsync(CreateCategoryDto dto);
    Task<bool> UpdateAsync(Guid id, UpdateCategoryDto dto);
    Task<bool> DeleteAsync(Guid id);
}