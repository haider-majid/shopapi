using Presentation.Dto.Category;
using Presentation.Dto.Product;using MediatR;
using Application.Services;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, IEnumerable<GetCategoryDto>>
{
    private readonly ICategoryService _categoryService;
    public GetAllCategoriesQueryHandler(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }
    public async Task<IEnumerable<GetCategoryDto>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        return await _categoryService.GetAllAsync();
    }
}