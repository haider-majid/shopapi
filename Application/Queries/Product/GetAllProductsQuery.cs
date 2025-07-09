using MediatR;
using System.Collections.Generic;
using Application;

public class GetAllProductsQuery : IRequest<IEnumerable<GetProductDto>>
{
}