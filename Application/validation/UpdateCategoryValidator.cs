using Presentation.Dto.Category;
using Presentation.Dto.Product;using FluentValidation;

namespace Application;

public class UpdateCategoryValidator : AbstractValidator<UpdateCategoryDto>
{
    public UpdateCategoryValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
    }
}
