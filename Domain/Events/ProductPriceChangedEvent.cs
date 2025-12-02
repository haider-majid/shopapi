using Domain.Common;

namespace Domain.Events;
public sealed record ProductPriceChangedEvent(
    Guid ProductId,
    decimal OldPrice,
    decimal NewPrice
) : DomainEvent;
