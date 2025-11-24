using Presentation.Dto.Category;
using Presentation.Dto.Product;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Application.Services;
using Application.Commands.Product;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, GetProductDto>
{
    private readonly IProductService _productService;
    public CreateProductCommandHandler(IProductService productService)
    {
        _productService = productService;
    }
    public async Task<GetProductDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var createdProduct = await _productService.CreateAsync(request.Dto);
        return createdProduct;
    }
}