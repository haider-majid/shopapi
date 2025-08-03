using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Application.Services;
using Application.Queries.Product;
using Presentation.Dto.Product;

namespace Application.Handlers.Queries.Product
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, GetProductDto?>
    {
        private readonly IProductService _productService;
        public GetProductByIdQueryHandler(IProductService productService)
        {
            _productService = productService;
        }
        public async Task<GetProductDto?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _productService.GetByIdAsync(request.Id);
            return product;
        }
    }
}