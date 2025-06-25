using Application;

namespace Application.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<GetCategoryDto>> GetAllAsync();
        Task<GetCategoryDto> CreateAsync(CreateCategoryDto dto);
        Task<bool> UpdateAsync(UpdateCategoryDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
}