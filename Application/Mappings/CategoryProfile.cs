using Presentation.Dto.Category;
using Presentation.Dto.Product;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<Category, GetCategoryDto>();
    }
}