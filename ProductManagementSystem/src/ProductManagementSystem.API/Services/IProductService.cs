using InventoryManagement.Data.Entity;
using ProductManagement.Common.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.API.Services
{
    public interface IProductService
    {
        Task<Products> AddProduct(Products product);


        Task<Products> GetProductById(Guid productId);

        Task<Products> UpdateProduct(Products product);

        Task<List<Products>> SearchProducts(SearchProductRequest searchProductRequest);
    }
}
