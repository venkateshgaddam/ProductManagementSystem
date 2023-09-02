using InventoryManagement.Data.Entity;
using ProductManagement.API.Services;
using ProductManagement.Common.POCO;
using ProductManagement.Common.Services.AWS;

namespace ProductManagementSystem.API.Biz
{
    public class ProductBiz : IProductBiz
    {
        private readonly IAwsServiceFacade _awsServiceFacade;

        private readonly IProductService _productService;

        private readonly IUtilityBiz _utilityBiz;

        public ProductBiz(IAwsServiceFacade awsServiceFacade, IProductService productService, IUtilityBiz utilityBiz)
        {
            _awsServiceFacade = awsServiceFacade;
            _productService = productService;
            _utilityBiz = utilityBiz;
        }

        public async Task<Products> AddProduct(AddProductRequest addProduct)
        {
            //1 Create Product Code
            string productCode = GetProductCode(addProduct.Category, addProduct.Subcategory);

            //2 Create ProductDTO
            Products product = PrepareProductDTO(addProduct, productCode);

            //3 Save Image to S3
            var imageUrl = await _awsServiceFacade.SaveFiletoS3(addProduct.Image);
            product.ProductImage = imageUrl;

            //4 Save it in DB

            var result = await _productService.AddProduct(product);

            return product;

        }

        public async Task<Products> UpdateProduct(UpdateProductRequest updateProductRequest)
        {
            var product = await _productService.GetProductById(updateProductRequest.ProductId);

            if (product == null)
            {
                throw new Exception($"No Product is available in the DB for Id {updateProductRequest.ProductId}");
            }

            Products updatedProduct = PrepareProductDTOAfterUpdate(updateProductRequest, product);

            if (updateProductRequest.IsImageUpdated)
            {
                var imageUrl = await _awsServiceFacade.SaveFiletoS3(updateProductRequest.Image);
                updatedProduct.ProductImage = imageUrl;
            }

            var result = await _productService.UpdateProduct(updatedProduct);
            await Console.Out.WriteLineAsync(result.ProductId.ToString());

            return updatedProduct;

        }

        public async Task<List<Products>> SearchProducts(SearchProductRequest searchProductRequest)
        {
            var categories = await _utilityBiz.GetCategories();
            var result = categories.FirstOrDefault(a => a.Category.ToString() == searchProductRequest.category);
            if (result != null && result.SubCategories.Contains(searchProductRequest.subcategory))
            {
                var resultSet = await _productService.SearchProducts(searchProductRequest);
                return resultSet;
            }
            else
            {
                throw new Exception("");
            }

        }

        #region PrivateMethods

        private static string GetProductCode(string category, string subcategoy)
        {
            Random generator = new();
            string code = generator.Next(0, 1000000).ToString("D6");
            return category.ToUpper()[..2] + subcategoy.ToUpper()[..2] + code;
        }

        private static Products PrepareProductDTO(AddProductRequest addProduct, string productCode)
        {
            Products product = new()
            {
                ProductId = Guid.NewGuid(),
                ProductCode = productCode,
                ProductDescription = addProduct.Description,
                ProductName = addProduct.Name,
                Category = addProduct.Category,
                Subcategory = addProduct.Subcategory,
                Quantity = addProduct.Quantity,
                Price = addProduct.Price
            };
            return product;
        }

        private static Products PrepareProductDTOAfterUpdate(UpdateProductRequest updateRequest, Products existingProduct)
        {
            existingProduct.ProductId = updateRequest.ProductId;
            existingProduct.ProductCode = updateRequest.ProductCode;
            existingProduct.ProductDescription = updateRequest.Description;
            existingProduct.ProductName = updateRequest.Name;
            existingProduct.Category = updateRequest.Category;
            existingProduct.Subcategory = updateRequest.Subcategory;
            existingProduct.Quantity += updateRequest.Quantity;
            existingProduct.Price = updateRequest.Price;

            return existingProduct;
        }



        #endregion

    }
}
