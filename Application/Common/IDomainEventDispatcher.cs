using Presentation.Dto.Category;
using Presentation.Dto.Product;using Domain.Common;

namespace Application.Common;

public interface IDomainEventDispatcher
{
    Task DispatchAsync(IEnumerable<IDomainEvent> domainEvents);
}
