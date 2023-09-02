using InventoryManagement.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Common.Services
{
    public interface IProductService
    {
        Task<string> AddProduct(Products product);
    }
}
