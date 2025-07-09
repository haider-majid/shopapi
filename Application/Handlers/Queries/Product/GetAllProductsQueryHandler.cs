using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application;
using Application.Services;

public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<GetProductDto>>
{
    private readonly IProductService _productService;
    public GetAllProductsQueryHandler(IProductService productService)
    {
        _productService = productService;
    }
    public async Task<IEnumerable<GetProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await _productService.GetAllAsync();
        return products;
    }
}