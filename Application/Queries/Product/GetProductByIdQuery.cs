using MediatR;
using System;
using Presentation.Dto.Product;

namespace Application.Queries.Product;

public class GetProductByIdQuery : IRequest<GetProductDto?>
{
    public Guid Id { get; set; }
    public GetProductByIdQuery(Guid id)
    {
        Id = id;
    }
}