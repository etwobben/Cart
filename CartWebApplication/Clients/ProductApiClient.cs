using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CartWebApplication.ApplicationSettings;
using Microsoft.Extensions.Options;
using Models.ViewModels;

namespace CartWebApplication.Clients
{
    public class ProductApiClient : BaseApiClient<ProductViewModel>
    {

        public ProductApiClient(IOptions<ApiSettingsModel> settings) : base(settings, "product")
        {

        }

        public async Task<List<ProductViewModel>> GetProducts()
        {
            var requestUrl = CreateRequestUri();
            return await GetAsync(requestUrl);
        }
    }
}
