using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Common.POCO
{
    public class SearchProductRequest
    {
        public string? category { get; set; }

        public string? subcategory { get; set; }
    }
}
