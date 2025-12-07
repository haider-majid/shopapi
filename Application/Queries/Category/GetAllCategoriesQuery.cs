using Presentation.Dto.Category;
using Presentation.Dto.Product;using MediatR;
using System.Collections.Generic;
public class GetAllCategoriesQuery : IRequest<IEnumerable<GetCategoryDto>>
{
}
