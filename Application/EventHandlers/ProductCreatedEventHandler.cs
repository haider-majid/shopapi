using Presentation.Dto.Category;
using Presentation.Dto.Product;using Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.EventHandlers;

public class ProductCreatedEventHandler : INotificationHandler<ProductCreatedEvent>
{
    private readonly ILogger<ProductCreatedEventHandler> _logger;

    public ProductCreatedEventHandler(ILogger<ProductCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public async Task Handle(ProductCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Product created: {ProductId} - {ProductName} at price {Price} with stock {Stock} in category {CategoryId}",
            notification.ProductId,
            notification.ProductName,
            notification.Price,
            notification.InitialStock,
            notification.CategoryId);

        // Here you could add additional business logic like:
        // - Send notifications
        // - Update analytics
        // - Trigger integrations
        // - etc.

        await Task.CompletedTask;
    }
}
