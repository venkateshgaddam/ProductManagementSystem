using Amazon.Lambda.Core;
using Amazon.Lambda.SQSEvents;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Data;
using Dapper;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace ProductManagement.ProductsProcessor;

public class Function
{
    /// <summary>
    /// Default constructor. This constructor is used by Lambda to construct the instance. When invoked in a Lambda environment
    /// the AWS credentials will come from the IAM role associated with the function and the AWS region will be set to the
    /// region the Lambda function is executed in.
    /// </summary>
    public Function()
    {

    }


    /// <summary>
    /// This method is called for every Lambda invocation. This method takes in an SQS event object and can be used 
    /// to respond to SQS messages.
    /// </summary>
    /// <param name="evnt"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public async Task FunctionHandler(SQSEvent evnt, ILambdaContext context)
    {
        foreach (var message in evnt.Records)
        {
            await ProcessMessageAsync(message, context);
        }
    }

    private async Task ProcessMessageAsync(SQSEvent.SQSMessage message, ILambdaContext context)
    {
        try
        {
            context.Logger.LogInformation($"Processed message {message.Body}");
            var addCategorizedProduct = JsonConvert.DeserializeObject<AddCategorizedProduct>(message.Body);
            string connectionstring = Environment.GetEnvironmentVariable("Connectionstring") ?? string.Empty;

            if (!string.IsNullOrEmpty(connectionstring))
            {
                IDbConnection dbConnection = new SqlConnection(connectionstring);

                using (dbConnection)
                {
                    dbConnection.Open();
                    string selectQuery = "SELECT * FROM CategorizedProducts WHERE Category = @Category AND Subcategory = @Subcategory";
                    var parameters = new { addCategorizedProduct.Category, addCategorizedProduct.Subcategory };
                    var existingResult = await dbConnection.QueryAsync<CategorizedProducts>(selectQuery, parameters);
                    if (existingResult.Any())
                    {
                        var newQuantity = existingResult.FirstOrDefault().Quantity + addCategorizedProduct.Quantity;
                        string updateQuery = "UPDATE CategorizedProducts SET Quantity = @NewQuantity WHERE Category = @Category AND Subcategory = @Subcategory";
                        var _updateparameters = new { NewQuantity = newQuantity, addCategorizedProduct.Category, addCategorizedProduct.Subcategory };
                        dbConnection.Execute(updateQuery, _updateparameters);

                        var result = await dbConnection.ExecuteAsync(selectQuery).ConfigureAwait(false);
                    }
                    else
                    {
                        string insertQuery = "INSERT INTO CategorizedProducts (Category, Subcategory, Quantity) VALUES ( @Category, @Subcategory, @Quantity)";
                        var insertResult = await dbConnection.ExecuteScalarAsync<CategorizedProducts>(insertQuery, addCategorizedProduct);
                        Console.WriteLine(insertResult);
                    }
                }
            }

            // TODO: Do interesting work based on the new message
            await Task.CompletedTask;
        }
        catch (Exception ex)
        {
            await Console.Out.WriteLineAsync($"Exception {ex.Message}");
            throw;
        }
    }
}