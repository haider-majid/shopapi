using FluentValidation;
using Presentation.Dto.Brand;

namespace Application.Validation
{
    public class UpdateBrandValidator : AbstractValidator<UpdateBrandDto>
    {
        public UpdateBrandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(50).WithMessage("Name must not exceed 50 characters");

            RuleFor(x => x.Description)
                .MaximumLength(250).WithMessage("Description must not exceed 250 characters");
        }
    }
}
