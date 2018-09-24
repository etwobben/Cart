using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models.DTOs;
using CartWebApplication.ApplicationSettings;
using Microsoft.Extensions.Options;
using Models.ViewModels;

namespace CartWebApplication.Clients
{
    public class CartLineApiClient : BaseApiClient<CartLineViewModel>
    {

        public CartLineApiClient(IOptions<ApiSettingsModel> settings) : base(settings, "cart")
        {

        }

        public async Task<List<CartLineViewModel>> GetCartLines()
        {
            var requestUrl = CreateRequestUri();
            return await GetAsync(requestUrl);
        }

        public async Task<CartLineViewModel> AddCartLine(AddCartLineDTO addCartLineDto)
        {
            var requestUrl = CreateRequestUri();
            return await PostAsync<AddCartLineDTO>(requestUrl, addCartLineDto);
        }

        public async Task<CartLineViewModel> UpdateCartLine(UpdateCartLineDTO updateCartLineDto)
        {
            var requestUrl = CreateItemRequestUri(updateCartLineDto.LineId);
            return await PutAsync<UpdateCartLineDTO>(requestUrl, updateCartLineDto);
        }

        public async Task DeleteCartLine(int lineId)
        {
            var requestUrl = CreateItemRequestUri(lineId);
            await DeleteAsync(requestUrl);
        }
    }
}
