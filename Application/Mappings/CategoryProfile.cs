using AutoMapper;

namespace Application
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<CreateCategoryDto, Category>();
            CreateMap<Category, GetCategoryDto>();
            CreateMap<UpdateCategoryDto, Category>();
        }
    }
}