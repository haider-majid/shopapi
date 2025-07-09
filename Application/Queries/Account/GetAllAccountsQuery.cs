namespace Application.Queries.Account
{
    using MediatR;
    using System.Collections.Generic;
    using Presentation.Dto.Profile;
    public class GetAllAccountsQuery : IRequest<IEnumerable<GetAccountDto>>
    {
    }
}