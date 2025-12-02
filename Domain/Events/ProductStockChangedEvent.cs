using Domain.Common;

namespace Domain.Events;
public sealed record ProductStockChangedEvent(
    Guid ProductId,
    int OldStock,
    int NewStock,
    string Reason
) : DomainEvent;
