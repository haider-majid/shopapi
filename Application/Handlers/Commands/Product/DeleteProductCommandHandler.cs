using MediatR;
using Application.Services;
using Application.Commands.Product;

namespace Application.Handlers.Commands.Product;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, bool>
{
    private readonly IProductService _productService;

    public DeleteProductCommandHandler(IProductService productService)
    {
        _productService = productService;
    }

    public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        return await _productService.DeleteAsync(request.Id);
    }
}
