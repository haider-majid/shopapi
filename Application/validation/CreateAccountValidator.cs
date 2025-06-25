using FluentValidation;
using Presentation.Dto.Profile;

namespace Application.Validation
{
    public class CreateAccountValidator : AbstractValidator<CreateAccountDto>
    {
        public CreateAccountValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty().MinimumLength(6);
        }
    }
}