namespace Application.Queries.Account
{
    using MediatR;
    using Presentation.Dto.Profile;
    public class GetAccountByIdQuery : IRequest<GetAccountDto?>
    {
        public Guid Id { get; set; }
        public GetAccountByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}