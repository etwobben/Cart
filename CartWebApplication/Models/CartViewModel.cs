using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Models.ViewModels;

namespace CartWebApplication.Models
{
    public class CartViewModel
    {
        public List<CartLineViewModel> CartLines { get; set; } = new List<CartLineViewModel>();
        public List<ProductViewModel> AllProducts { get; set; } = new List<ProductViewModel>();

        [DisplayFormat(DataFormatString = "{0:c}")]
        public double TotalPrice { get; set; }
    }
}
