using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Common.Models
{
    public class CategoryInfo
    {
        public string Category { get; set; }
        public List<string> SubCategories { get; set; }

    }
}
