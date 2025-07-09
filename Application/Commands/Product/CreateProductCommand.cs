namespace Application.Commands.Product
{
    using MediatR;
    using System;
    using Application;

    public class CreateProductCommand : IRequest<GetProductDto>
    {
        public CreateProductDto Dto { get; set; }
        public CreateProductCommand(CreateProductDto dto)
        {
            Dto = dto;
        }
    }
}