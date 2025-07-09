using MediatR;
using Application.Services;
using Presentation.Dto.Profile;
using System.Threading;
using System.Threading.Tasks;
using Application.Commands.Account;

public class UpdateAccountCommandHandler : IRequestHandler<UpdateAccountCommand, GetAccountDto?>
{
    private readonly IAccountService _accountService;
    public UpdateAccountCommandHandler(IAccountService accountService)
    {
        _accountService = accountService;
    }
    public async Task<GetAccountDto?> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
    {
        var updated = await _accountService.UpdateAsync(request.Dto);
        if (!updated) return null;
        // Optionally fetch updated entity
        return await _accountService.GetByIdAsync(request.Id);
    }
}