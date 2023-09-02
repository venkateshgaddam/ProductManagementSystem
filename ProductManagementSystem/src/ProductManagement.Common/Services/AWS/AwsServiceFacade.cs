using Amazon.S3;
using Amazon.S3.Transfer;
using Amazon.SQS;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace ProductManagement.Common.Services.AWS
{
    public class AwsServiceFacade : IAwsServiceFacade
    {
        private readonly IAmazonS3 _amazonS3;
        private readonly IAmazonSQS _amazonSQS;
        private readonly IConfiguration _configuration;


        public AwsServiceFacade(IAmazonS3 amazonS3, IAmazonSQS amazonSQS, IConfiguration configuration)
        {
            _amazonS3 = amazonS3;
            _amazonSQS = amazonSQS;
            _configuration = configuration;
        }


        public async Task<string> SaveFiletoS3(IFormFile formFile)
        {
            try
            {
                if (formFile == null || formFile.Length == 0)
                {
                    return string.Empty;
                }
                string bucketName = _configuration[GlobalConstants.IMG_STORAGE_BUCKET] ?? string.Empty;
                string region = _amazonS3.Config.RegionEndpoint.SystemName;
                var s3Key = $"{Guid.NewGuid()}_{formFile.FileName}";

                using (var memoryStream = new MemoryStream())
                {
                    await formFile.CopyToAsync(memoryStream).ConfigureAwait(false);
                    // Upload the image to S3
                    var transferUtility = new TransferUtility(_amazonS3);
                    await transferUtility.UploadAsync(memoryStream, bucketName, formFile.FileName).ConfigureAwait(false);
                }
                return $"https://{bucketName}.s3.{region}.amazonaws.com/{formFile.FileName}";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "";
            }
        }


        public async Task PublishMessageToQueue()
        {

        }
    }
}
