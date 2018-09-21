using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Services;
using Models.ViewModels;

namespace CartWebApi.Services
{
    public class ProductResourceService
    {

        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductResourceService(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        public async Task<ProductViewModel> GetByIdAsync(int id)
        {
            return _mapper.Map<ProductViewModel>(await _productService.GetByIdAsync(id));
        }

        public async Task<List<ProductViewModel>> GetAllAsync()
        {
            var products = await _productService.GetAllAsync();
            return products.Select(p => _mapper.Map<ProductViewModel>(p)).ToList();
        }
    }
}
