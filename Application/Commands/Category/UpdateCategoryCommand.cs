using Presentation.Dto.Category;
using Presentation.Dto.Product;namespace Application.Commands.Category
{
    using MediatR;

    public class UpdateCategoryCommand : IRequest<GetCategoryDto?>
    {
        public Guid Id { get; set; }
        public UpdateCategoryDto Dto { get; set; }
        public UpdateCategoryCommand(Guid id, UpdateCategoryDto dto)
        {
            Id = id;
            Dto = dto;
        }
    }
}
