using Presentation.Dto.Category;
using Presentation.Dto.Product;using MediatR;
using Application.Services;
using System.Threading;
using System.Threading.Tasks;
using Application.Commands.Category;

public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, bool>
{
    private readonly ICategoryService _categoryService;
    public DeleteCategoryCommandHandler(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }
    public async Task<bool> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        return await _categoryService.DeleteAsync(request.Id);
    }
}