using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using CartWebApplication.Clients;
using Microsoft.AspNetCore.Mvc;
using CartWebApplication.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Models.DTOs;
using Models.ViewModels;

namespace CartWebApplication.Controllers
{
    public class CartController : Controller
    {
        private readonly CartLineApiClient _cartLineApiClient;
        private readonly ProductApiClient _productApiClient;

        public CartController(CartLineApiClient cartLineApiClient, ProductApiClient productApiClient)
        {
            _cartLineApiClient = cartLineApiClient;
            _productApiClient = productApiClient;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var cart = new CartViewModel
            {
                CartLines = await _cartLineApiClient.GetCartLines(),
                AllProducts = await _productApiClient.GetProducts()
            };
            cart.TotalPrice = cart.CartLines.Sum(cl => cl.TotalPrice);
            return View(cart);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int lineId)
        {
            await _cartLineApiClient.DeleteCartLine(lineId);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCartLine([BindRequired, FromForm] UpdateCartLineDTO updateCartLineDto)
        {
            if (!ModelState.IsValid)
                return Error();

            await _cartLineApiClient.UpdateCartLine(updateCartLineDto);
            return RedirectToAction("Index", "Cart");
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart([BindRequired, FromForm] AddCartLineDTO addCartLineDto)
        {
            if (!ModelState.IsValid)
                return Error();

            await _cartLineApiClient.AddCartLine(addCartLineDto);
            return RedirectToAction("Index", "Cart");
        }
    }
}
