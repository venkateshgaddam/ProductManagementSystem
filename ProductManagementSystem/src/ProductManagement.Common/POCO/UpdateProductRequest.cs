using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Common.POCO
{
    public class UpdateProductRequest
    {
        public Guid ProductId { get; set; }

        public string ProductCode { get; set; }

        public bool IsImageUpdated { get; set; } = false;

        public string Name { get; set; }

        public string Description { get; set; } = string.Empty;

        public string Category { get; set; }

        public string Subcategory { get; set; }

        public IFormFile? Image { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }
    }
}
