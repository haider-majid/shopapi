using Presentation.Dto.Category;
using Presentation.Dto.Product;
using AutoMapper;
using Domain.Entities;
using Presentation.Dto.Category;

namespace Application.Mappings
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, GetCategoryDto>();
        }
    }
}