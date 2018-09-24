using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CartWebApplication.Models
{
    public class ErrorViewModel
    {
        public ModelStateDictionary ModelState { get; set; }
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}