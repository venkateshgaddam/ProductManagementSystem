using InventoryManagement.Data.Entity;
using ProductManagement.Common.POCO;

namespace ProductManagementSystem.API.Biz
{
    public interface IProductBiz
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="addProduct"></param>
        /// <returns></returns>
        Task<Products> AddProduct(AddProductRequest addProduct);


        Task<Products> UpdateProduct(UpdateProductRequest addProduct);


        Task<List<Products>> SearchProducts(SearchProductRequest searchProductRequest);

    }
}
