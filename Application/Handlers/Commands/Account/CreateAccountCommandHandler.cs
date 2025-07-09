using MediatR;
using Application.Services;
using Presentation.Dto.Profile;
using System.Threading;
using System.Threading.Tasks;
using Application.Commands.Account;

public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, GetAccountDto>
{
    private readonly IAccountService _accountService;
    public CreateAccountCommandHandler(IAccountService accountService)
    {
        _accountService = accountService;
    }
    public async Task<GetAccountDto> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
    {
        return await _accountService.CreateAsync(request.Dto);
    }
}