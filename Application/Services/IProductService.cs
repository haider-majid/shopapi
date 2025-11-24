using Presentation.Dto.Category;
using Presentation.Dto.Product;

namespace Application.Services
{
    public interface IProductService
    {
        Task<IEnumerable<GetProductDto>> GetAllAsync();
        Task<GetProductDto?> GetByIdAsync(Guid id);
        Task<GetProductDto> CreateAsync(CreateProductDto dto);
        Task<bool> UpdateAsync(Guid id, UpdateProductDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
}