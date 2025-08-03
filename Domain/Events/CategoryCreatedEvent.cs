using Domain.Common;

namespace Domain.Events
{
    public sealed record CategoryCreatedEvent(
        Guid CategoryId,
        string CategoryName
    ) : DomainEvent;
}