

using FluentValidation;
using Presentation.Dto.Profile;

namespace Application.Validation
{
    public class UpdateAccountValidator : AbstractValidator<UpdateAccountDto>
    {
        public UpdateAccountValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
        }
    }
}