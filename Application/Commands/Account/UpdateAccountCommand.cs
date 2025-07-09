namespace Application.Commands.Account
{
    using MediatR;
    using Presentation.Dto.Profile;

    public class UpdateAccountCommand : IRequest<GetAccountDto?>
    {
        public Guid Id { get; set; }
        public UpdateAccountDto Dto { get; set; }
        public UpdateAccountCommand(Guid id, UpdateAccountDto dto)
        {
            Id = id;
            Dto = dto;
        }
    }
}