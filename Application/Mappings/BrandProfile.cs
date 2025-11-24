using AutoMapper;
using Domain.Entities;
using Presentation.Dto.Brand;

namespace Application.Mappings
{
    public class BrandProfile : Profile
    {
        public BrandProfile()
        {
            CreateMap<Brand, GetBrandDto>();
        }
    }
}
