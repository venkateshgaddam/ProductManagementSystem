using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Common.Models
{
    public class Products
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public int SubcategoryId { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string ProductDescription { get; set; }
        public string ProductImage { get; set; }

    }
}
