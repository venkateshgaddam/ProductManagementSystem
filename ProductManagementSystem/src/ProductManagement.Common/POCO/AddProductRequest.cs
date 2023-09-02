using Microsoft.AspNetCore.Http;

namespace ProductManagement.Common.POCO
{
    public class AddProductRequest
    {
        public string Name { get; set; }

        public string Description { get; set; } = string.Empty;

        public string Category { get; set; }

        public string Subcategory { get; set; }

        public IFormFile Image { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }
    }
}
