using MediatR;
using Application.Services;
using Presentation.Dto.Profile;
using System.Threading;
using System.Threading.Tasks;
using Application.Queries.Account;

public class GetAccountByIdQueryHandler : IRequestHandler<GetAccountByIdQuery, GetAccountDto?>
{
    private readonly IAccountService _accountService;
    public GetAccountByIdQueryHandler(IAccountService accountService)
    {
        _accountService = accountService;
    }
    public async Task<GetAccountDto?> Handle(GetAccountByIdQuery request, CancellationToken cancellationToken)
    {
        return await _accountService.GetByIdAsync(request.Id);
    }
}