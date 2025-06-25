using Application;
using Presentation.Dto.Profile;

namespace Application.Services
{
    public interface IValidationService
    {
        Task ValidateAsync(CreateCategoryDto dto);
        Task ValidateAsync(UpdateCategoryDto dto);
        Task ValidateAsync(CreateProductDto dto);
        Task ValidateAsync(UpdateProductDto dto);
        Task ValidateAsync(CreateAccountDto dto);
        Task ValidateAsync(UpdateAccountDto dto);
    }
}

