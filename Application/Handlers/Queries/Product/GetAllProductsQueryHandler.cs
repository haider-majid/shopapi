using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Services;
using Application.Queries.Product;
using Presentation.Dto.Product;

namespace Application.Handlers.Queries.Product;

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
