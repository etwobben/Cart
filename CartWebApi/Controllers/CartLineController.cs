using System.Linq;
using System.Threading.Tasks;
using Models.DTOs;
using CartWebApi.FilterAttributes;
using CartWebApi.Services;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Models.ViewModels;

namespace CartWebApi.Controllers
{
    [Route("api/cart")]
    [ApiController]
    [ModelValidation]
    public class CartLineController : ControllerBase
    {
        private readonly CartLineResourceService _cartService;

        public CartLineController(CartLineResourceService cartService)
        {
            _cartService = cartService;
        }

        // GET api/cart
        [HttpGet]
        public async Task<ActionResult<CartLineViewModel>> Get()
        {
            return Ok(await _cartService.GetAllAsync());
        }


        // GET api/cart/5
        [HttpGet("{id}", Name = "GetLine")]
        public async Task<ActionResult<CartLineViewModel>> Get(int id)
        {
            var cart = await _cartService.GetByIdAsync(id);

            if (cart == null)
                return NotFound();

            return Ok(cart);
        }

        // POST api/cart
        [HttpPost]
        public async Task<ActionResult<CartLineViewModel>> CreateLine([BindRequired, FromBody] AddCartLineDTO line)
        {
            var cart = await _cartService.AddCartLine(line);
            return CreatedAtRoute("GetLine", new { id = cart.Id }, cart);
        }

        // PUT api/cart/5
        [HttpPut("{id}")]
        public async Task<ActionResult<CartLineViewModel>> UpdateLine([BindRequired, FromRoute] int id, [BindRequired, FromBody] UpdateCartLineDTO line)
        {
            if (id != line.LineId)
                return BadRequest();

            return Ok(await _cartService.UpdateCartLine(id, line));
        }

        // DELETE api/cart/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteLine([BindRequired, FromRoute] int id)
        {
            await _cartService.DeleteCartLine(id);
            return Ok();
        }
    }
}
