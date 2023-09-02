using DapperExtensions;
using IM.Common.Repository.Sql;
using InventoryManagement.Data.Entity;
using ProductManagement.Common.POCO;

namespace ProductManagement.API.Services
{
    public class ProductService : IProductService
    {
        private readonly IGenericRepository<Products> _repository;

        public ProductService(IGenericRepository<Products> genericRepository)
        {
            _repository = genericRepository;
        }

        public async Task<Products> AddProduct(Products product)
        {
            var result = await _repository.AddAsync(product, "dbo");
            return result;
        }

        public async Task<Products> GetProductById(Guid productId)
        {
            var result = await _repository.GetAsync("ProductId", productId, "dbo");
            return result;
        }

        public async Task<List<Products>> SearchProducts(SearchProductRequest searchProductRequest)
        {
            var result = await _repository.SP_GetListByFilterAsync(new Dictionary<object, dynamic>()
            {
                { "Category",searchProductRequest.category } ,
                { "SubCategory",searchProductRequest.subcategory }
            }, "dbo", "sp_GetProducts");
            return result.ToList();
        }

        public async Task<Products> UpdateProduct(Products product)
        {
            var idPredicate = Predicates.Field<Products>(p => p.ProductId, Operator.Eq, product.ProductId);


            var existsPredicate = Predicates.Exists<Products>(idPredicate);

            var result = await _repository.UpdateAsync(product, idPredicate, existsPredicate, "dbo");
            return result;
        }
    }
}
