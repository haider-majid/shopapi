namespace Application.Commands.Account
{
    using MediatR;
    using Presentation.Dto.Profile;

    public class CreateAccountCommand : IRequest<GetAccountDto>
    {
        public CreateAccountDto Dto { get; set; }
        public CreateAccountCommand(CreateAccountDto dto)
        {
            Dto = dto;
        }
    }
}