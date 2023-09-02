using System;

namespace InventoryManagement.Data.Entity
{
    public class Products
    {
        public Guid ProductId { get; set; }

        public string ProductName { get; set; }

        public string Category { get; set; }
        public string Subcategory { get; set; }

        public string ProductCode { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string ProductDescription { get; set; }
        public string ProductImage { get; set; }

        //public DateTimeOffset CreatedDate { get; set; }
        //public string CreatedBy { get; set; }
        //public string UpdatedBy { get; set; }
    }
}
