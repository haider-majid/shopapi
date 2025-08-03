using Presentation.Dto.Category;
using Presentation.Dto.Product;namespace Application.Commands.Category
{
    using MediatR;

    public class CreateCategoryCommand : IRequest<GetCategoryDto>
    {
        public CreateCategoryDto Dto { get; set; }
        public CreateCategoryCommand(CreateCategoryDto dto)
        {
            Dto = dto;
        }
    }
}