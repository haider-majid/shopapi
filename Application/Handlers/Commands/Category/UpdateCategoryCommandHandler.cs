using Presentation.Dto.Category;
using Presentation.Dto.Product;using MediatR;
using Application.Services;
using System.Threading;
using System.Threading.Tasks;
using Application.Commands.Category;

public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, GetCategoryDto?>
{
    private readonly ICategoryService _categoryService;
    public UpdateCategoryCommandHandler(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }
    public async Task<GetCategoryDto?> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var updated = await _categoryService.UpdateAsync(request.Id, request.Dto);
        if (!updated) return null;
        // Optionally fetch updated entity
        var all = await _categoryService.GetAllAsync();
        return all.FirstOrDefault(c => c.Id == request.Id);
    }
}