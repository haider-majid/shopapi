using Presentation.Dto.Category;
using Presentation.Dto.Product;using Presentation.Dto.Category;
using Presentation.Dto.Product;

namespace Application.Services
{
    public interface IValidationService
    {
        Task ValidateAsync(CreateCategoryDto dto);
        Task ValidateAsync(UpdateCategoryDto dto);
        Task ValidateAsync(CreateProductDto dto);
        Task ValidateAsync(UpdateProductDto dto);
    }
}

