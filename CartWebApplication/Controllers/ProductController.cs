using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CartWebApplication.Clients;
using CartWebApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Models.DTOs;
using Models.ViewModels;

namespace CartWebApplication.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductApiClient _productApiClient;
        public ProductController(ProductApiClient productApiClient)
        {
            _productApiClient = productApiClient;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _productApiClient.GetProducts());
        }
    }
}