using Microsoft.AspNetCore.Mvc;

namespace ProductManagementSystem.Common.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : Controller
    {
        protected JsonResult PackageData<T>(T data, System.Net.HttpStatusCode httpStatusCode)
        {
            var result = Json(data);
            result.StatusCode = (int)httpStatusCode;
            return result;
        }
    }
}
