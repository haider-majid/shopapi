using MediatR;
using System;
using Application;

public class GetProductByIdQuery : IRequest<GetProductDto?>
{
    public Guid Id { get; set; }
    public GetProductByIdQuery(Guid id)
    {
        Id = id;
    }
}