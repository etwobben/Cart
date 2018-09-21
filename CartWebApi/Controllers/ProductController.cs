using System.Collections.Generic;
using System.Threading.Tasks;
using CartWebApi.Services;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Models.ViewModels;

namespace CartWebApi.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductResourceService _productService;

        public ProductController(ProductResourceService productService)
        {
            _productService = productService;
        }

        // GET api/products
        [HttpGet]
        public async Task<ActionResult<List<ProductViewModel>>> Get()
        {
            return Ok(await _productService.GetAllAsync());
        }

        // GET api/products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductViewModel>> Get(int id)
        {
            var product = await _productService.GetByIdAsync(id);

            if (product == null)
                return NotFound(id);

            return Ok(product);
        }
    }
}
