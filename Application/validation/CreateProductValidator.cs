using Presentation.Dto.Category;
using Presentation.Dto.Product;using FluentValidation;

namespace Application;

public class CreateProductValidator : AbstractValidator<CreateProductDto>
{
    public CreateProductValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Description).MaximumLength(500);
        RuleFor(x => x.Price).NotEmpty().GreaterThanOrEqualTo(0);
        RuleFor(x => x.Stock).NotEmpty().GreaterThanOrEqualTo(0);
    }
}
