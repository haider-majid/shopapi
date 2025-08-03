using Domain.Common;

namespace Domain.Events
{
    public sealed record ProductCreatedEvent(
        Guid ProductId,
        string ProductName,
        decimal Price,
        int InitialStock,
        Guid CategoryId
    ) : DomainEvent;
}