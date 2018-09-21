using AutoMapper;
using Domain.Entities;
using Models.ViewModels;

namespace CartWebApi.AutoMapper
{
    public class CartLineProfile : Profile
    {
        public CartLineProfile()
        {
            CreateMap<CartLine, CartLineViewModel>()
                .ForMember(vm => vm.TotalPrice, m => m.MapFrom(u => u.Product.Price * u.Amount));
        }
    }
}
