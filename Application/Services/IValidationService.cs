using Application;

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