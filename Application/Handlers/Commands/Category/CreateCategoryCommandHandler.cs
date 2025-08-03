using Presentation.Dto.Category;
using Presentation.Dto.Product;using MediatR;
using Application.Services;
using System.Threading;
using System.Threading.Tasks;
using Application.Commands.Category;

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, GetCategoryDto>
{
    private readonly ICategoryService _categoryService;
    public CreateCategoryCommandHandler(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }
    public async Task<GetCategoryDto> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        return await _categoryService.CreateAsync(request.Dto);
    }
}