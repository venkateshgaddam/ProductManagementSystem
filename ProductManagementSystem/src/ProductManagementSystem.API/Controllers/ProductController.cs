using IM.Common.Repository.Sql;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProductManagement.Common.Models;
using ProductManagement.Common.POCO;
using ProductManagementSystem.API.Biz;
using ProductManagementSystem.CommonAPI.Controllers;

namespace ProductManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : BaseController
    {
        private readonly IProductBiz _productBiz;

        private readonly ILogger<ProductController> _logger;

        public ProductController(IProductBiz productBiz, ILogger<ProductController> logger)
        {
            _productBiz = productBiz;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct([FromForm] AddProductRequest addProductRequest)
        {
            await Console.Out.WriteLineAsync($"ProductName: {addProductRequest.Name}");
            var product = await _productBiz.AddProduct(addProductRequest);

            if (product == null) { return BadRequest(ModelState); }
            await Console.Out.WriteLineAsync($"result: {JsonConvert.SerializeObject(product)}");
            return PackageData(product, System.Net.HttpStatusCode.OK);
        }



        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromForm] UpdateProductRequest updateProductRequest)
        {
            await Console.Out.WriteLineAsync($"ProductName: {updateProductRequest.Name}");
            var product = await _productBiz.UpdateProduct(updateProductRequest);

            if (product == null) { return BadRequest(ModelState); }
            await Console.Out.WriteLineAsync($"result: {JsonConvert.SerializeObject(product)}");
            return PackageData(product, System.Net.HttpStatusCode.OK);
        }


        [HttpPost("SetProduct")]
        public async Task<IActionResult> SetProduct([FromBody] SearchProductRequest request)
        {
            await Console.Out.WriteLineAsync(request.category);
            return Ok();
        }

        [HttpGet]
        [Route("Search/{Category}/{SubCategory}")]
        public async Task<IActionResult> SearchProducts(string Category, string SubCategory)
        {
            SearchProductRequest searchProductRequest = new SearchProductRequest()
            {
                 category = Category,
                 subcategory = SubCategory
            };

            var result = await _productBiz.SearchProducts(searchProductRequest);
            if (result == null) { return NotFound(); }
            return PackageData(result, System.Net.HttpStatusCode.OK);
        }

    }
}
