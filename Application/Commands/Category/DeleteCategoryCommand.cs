using Presentation.Dto.Category;
using Presentation.Dto.Product;namespace Application.Commands.Category
{
    using MediatR;

    public class DeleteCategoryCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public DeleteCategoryCommand(Guid id)
        {
            Id = id;
        }
    }
}