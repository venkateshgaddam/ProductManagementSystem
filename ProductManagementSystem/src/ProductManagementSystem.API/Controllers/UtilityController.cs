using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.Common.Models;
using ProductManagementSystem.API.Biz;
using ProductManagementSystem.CommonAPI.Controllers;

namespace ProductManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UtilityController : BaseController
    {
        public IUtilityBiz UtilityBiz { get; set; }

        public UtilityController(IUtilityBiz utilityBiz)
        {
            UtilityBiz = utilityBiz;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var result = await UtilityBiz.GetCategories();
            return PackageData<List<CategoryInfo>>(result, System.Net.HttpStatusCode.OK);
        }
    }
}
