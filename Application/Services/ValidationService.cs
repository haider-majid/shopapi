using FluentValidation;
using Application;
using Presentation.Dto.Profile;

namespace Application.Services
{
    public class ValidationService : IValidationService
    {
        private readonly IValidator<CreateCategoryDto> _createCategoryValidator;
        private readonly IValidator<UpdateCategoryDto> _updateCategoryValidator;
        private readonly IValidator<CreateProductDto> _createProductValidator;
        private readonly IValidator<UpdateProductDto> _updateProductValidator;
        private readonly IValidator<CreateAccountDto> _createAccountValidator;
        private readonly IValidator<UpdateAccountDto> _updateAccountValidator;

        public ValidationService(
            IValidator<CreateCategoryDto> createCategoryValidator,
            IValidator<UpdateCategoryDto> updateCategoryValidator,
            IValidator<CreateProductDto> createProductValidator,
            IValidator<UpdateProductDto> updateProductValidator,
            IValidator<CreateAccountDto> createAccountValidator,
            IValidator<UpdateAccountDto> updateAccountValidator)
        {
            _createCategoryValidator = createCategoryValidator;
            _updateCategoryValidator = updateCategoryValidator;
            _createProductValidator = createProductValidator;
            _updateProductValidator = updateProductValidator;
            _createAccountValidator = createAccountValidator;
            _updateAccountValidator = updateAccountValidator;
        }

        public async Task ValidateAsync(CreateCategoryDto dto)
        {
            var result = await _createCategoryValidator.ValidateAsync(dto);
            if (!result.IsValid)
                throw new ValidationException(result.Errors);
        }

        public async Task ValidateAsync(UpdateCategoryDto dto)
        {
            var result = await _updateCategoryValidator.ValidateAsync(dto);
            if (!result.IsValid)
                throw new ValidationException(result.Errors);
        }

        public async Task ValidateAsync(CreateProductDto dto)
        {
            var result = await _createProductValidator.ValidateAsync(dto);
            if (!result.IsValid)
                throw new ValidationException(result.Errors);
        }

        public async Task ValidateAsync(UpdateProductDto dto)
        {
            var result = await _updateProductValidator.ValidateAsync(dto);
            if (!result.IsValid)
                throw new ValidationException(result.Errors);
        }

        public async Task ValidateAsync(CreateAccountDto dto)
        {
            var result = await _createAccountValidator.ValidateAsync(dto);
            if (!result.IsValid)
                throw new ValidationException(result.Errors);
        }

        public async Task ValidateAsync(UpdateAccountDto dto)
        {
            var result = await _updateAccountValidator.ValidateAsync(dto);
            if (!result.IsValid)
                throw new ValidationException(result.Errors);
        }

    }
}