using Presentation.Dto.Category;
using Presentation.Dto.Product;namespace Application.Commands.Product
{
    using MediatR;
    using System;
    using Presentation.Dto.Category;
using Presentation.Dto.Product;

    public class UpdateProductCommand : IRequest<GetProductDto?>
    {
        public Guid Id { get; set; }
        public UpdateProductDto Dto { get; set; }
        public UpdateProductCommand(Guid id, UpdateProductDto dto)
        {
            Id = id;
            Dto = dto;
        }
    }
}
