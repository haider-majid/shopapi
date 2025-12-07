using MediatR;
using System.Collections.Generic;
using Presentation.Dto.Product;

namespace Application.Queries.Product;

public class GetAllProductsQuery : IRequest<IEnumerable<GetProductDto>>
{
}
