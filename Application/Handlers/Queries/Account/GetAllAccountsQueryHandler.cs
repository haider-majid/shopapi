using MediatR;
using Application.Services;
using Presentation.Dto.Profile;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Queries.Account;

public class GetAllAccountsQueryHandler : IRequestHandler<GetAllAccountsQuery, IEnumerable<GetAccountDto>>
{
    private readonly IAccountService _accountService;
    public GetAllAccountsQueryHandler(IAccountService accountService)
    {
        _accountService = accountService;
    }
    public async Task<IEnumerable<GetAccountDto>> Handle(GetAllAccountsQuery request, CancellationToken cancellationToken)
    {
        return await _accountService.GetAllAsync();
    }
}