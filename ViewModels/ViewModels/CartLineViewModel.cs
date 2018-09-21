using System.ComponentModel.DataAnnotations;

namespace Models.ViewModels
{
    public class CartLineViewModel
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        [DisplayFormat(DataFormatString = "{0:c}")]
        public double TotalPrice { get; set; }
        public ProductViewModel Product { get; set; }
    }
}
