using MediatR;
using System.Collections.Generic;
public class GetAllCategoriesQuery : IRequest<IEnumerable<GetCategoryDto>>
{
}