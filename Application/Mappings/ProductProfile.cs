using AutoMapper;
using Domain.Entities;
using Presentation.Dto.Product;

namespace Application.Mappings;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, GetProductDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.Value))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price.Amount));
    }
}
