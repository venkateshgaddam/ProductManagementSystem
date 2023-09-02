using Microsoft.AspNetCore.Http;

namespace ProductManagement.Common.Services.AWS
{
    public interface IAwsServiceFacade
    {
        Task<string> SaveFiletoS3(IFormFile formFile);
    }
}