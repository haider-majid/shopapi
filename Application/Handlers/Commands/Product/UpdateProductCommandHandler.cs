using Presentation.Dto.Category;
using Presentation.Dto.Product;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Application.Services;
using Application.Commands.Product;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, GetProductDto?>
{
    private readonly IProductService _productService;
    public UpdateProductCommandHandler(IProductService productService)
    {
        _productService = productService;
    }
    public async Task<GetProductDto?> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var success = await _productService.UpdateAsync(request.Id, request.Dto);
        if (!success)
        {
            return null;
        }
        var product = await _productService.GetByIdAsync(request.Id);
        return product;
    }
}
