using System;
using System.ComponentModel.DataAnnotations;

namespace Models.DTOs
{
    public class UpdateCartLineDTO
    {

        public int LineId { get; set; }
        [Range(1, Int32.MaxValue)]
        public int Amount { get; set; }
    }
}
