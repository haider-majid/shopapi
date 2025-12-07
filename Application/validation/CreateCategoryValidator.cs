using Presentation.Dto.Category;
using Presentation.Dto.Product;using FluentValidation;

namespace Application;

public class CreateCategoryValidator : AbstractValidator<CreateCategoryDto>
{
    public CreateCategoryValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
    }
}
