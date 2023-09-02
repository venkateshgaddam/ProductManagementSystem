using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagementSystem.Data.Entity
{
    public class CategorizedProducts
    {
        public Guid Id { get; set; }
        public string Category { get; set; }
        public string Subcategory { get; set; }
        public int Quantity { get; set; }

    }
}
