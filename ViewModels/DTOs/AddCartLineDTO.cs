using System;
using System.ComponentModel.DataAnnotations;

namespace Models.DTOs
{
    public class AddCartLineDTO
    {
        public int ProductId { get; set; }
        [Range(1, Int32.MaxValue)]
        public int Amount { get; set; }
    }
}
