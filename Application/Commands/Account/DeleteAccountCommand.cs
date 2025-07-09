namespace Application.Commands.Account
{
    using MediatR;

    public class DeleteAccountCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public DeleteAccountCommand(Guid id)
        {
            Id = id;
        }
    }
}