using MediatR;
using Application.Services;
using System.Threading;
using System.Threading.Tasks;
using Application.Commands.Account;

public class DeleteAccountCommandHandler : IRequestHandler<DeleteAccountCommand, bool>
{
    private readonly IAccountService _accountService;
    public DeleteAccountCommandHandler(IAccountService accountService)
    {
        _accountService = accountService;
    }
    public async Task<bool> Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
    {
        return await _accountService.DeleteAsync(request.Id);
    }
}