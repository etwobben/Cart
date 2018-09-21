using AutoMapper;
using Domain.Entities;
using Models.ViewModels;

namespace CartWebApi.AutoMapper
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductViewModel>();
        }
    }
}
