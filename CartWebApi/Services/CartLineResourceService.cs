using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities;
using Domain.Services;
using Models.DTOs;
using Models.ViewModels;

namespace CartWebApi.Services
{
    public class CartLineResourceService
    {
        private readonly ICartLineService _cartLineService;
        private readonly IMapper _mapper;

        public CartLineResourceService(ICartLineService cartLineService, IMapper mapper)
        {
            _cartLineService = cartLineService;
            _mapper = mapper;
        }

        public async Task<CartLineViewModel> GetByIdAsync(int id)
        {
            return _mapper.Map<CartLineViewModel>(await _cartLineService.GetByIdAsync(id));
        }

        public async Task<List<CartLineViewModel>> GetAllAsync()
        {
            var products = await _cartLineService.GetAllAsync();
            return products.Select(p => _mapper.Map<CartLineViewModel>(p)).ToList();
        }


        public async Task<CartLineViewModel> UpdateCartLine(int lineId, UpdateCartLineDTO updateCartLineDto)
        {
            return _mapper.Map<CartLineViewModel>(
                await _cartLineService.UpdateCartLine(lineId, updateCartLineDto.Amount));
        }

        public async Task<CartLineViewModel> AddCartLine(AddCartLineDTO addCartLineDto)
        {
            return _mapper.Map<CartLineViewModel>(await _cartLineService.AddCartLine(addCartLineDto.ProductId, addCartLineDto.Amount));
        }

        public async Task DeleteCartLine(int lineId)
        {
            await _cartLineService.DeleteCartLine(lineId);
        }
    }
}
