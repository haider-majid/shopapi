using AutoMapper;
using Domain;

namespace Application
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<CreateProductDto, Product>();
            CreateMap<Product, GetProductDto>();
            CreateMap<UpdateProductDto, Product>();
        }
    }
}



