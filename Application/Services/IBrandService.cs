using Presentation.Dto.Brand;

namespace Application.Services;

public interface IBrandService
{
    Task<IEnumerable<GetBrandDto>> GetAllAsync();
    Task<GetBrandDto?> GetByIdAsync(Guid id);
    Task<GetBrandDto> CreateAsync(CreateBrandDto dto);
    Task<bool> UpdateAsync(Guid id, UpdateBrandDto dto);
    Task<bool> DeleteAsync(Guid id);
}