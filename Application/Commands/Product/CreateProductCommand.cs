using Presentation.Dto.Category;
using Presentation.Dto.Product;namespace Application.Commands.Product
{
    using MediatR;
    using System;
    using Presentation.Dto.Category;
using Presentation.Dto.Product;

    public class CreateProductCommand : IRequest<GetProductDto>
    {
        public CreateProductDto Dto { get; set; }
        public CreateProductCommand(CreateProductDto dto)
        {
            Dto = dto;
        }
    }
}