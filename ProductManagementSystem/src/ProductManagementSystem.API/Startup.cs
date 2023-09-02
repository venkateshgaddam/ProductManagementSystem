using Microsoft.OpenApi.Models;
using ProductManagementSystem.API.Biz;
using ProductManagementSystem.CommonAPI;
using IM.Common.Repository.Sql;
using ProductManagement.Common.Services.AWS;
using ProductManagement.API.Services;
using Amazon.S3;
using Amazon.SQS;
using ProductManagement.Common;
using ProductManagementSystem.CommonAPI.Extensions;
using System.Data;
using Microsoft.Data.SqlClient;

namespace ProductManagementSystem.API;

public class Startup  
{
    public Startup(IConfiguration configuration)  
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddAmazonSystemsManager();

        services.AddLogging(options =>
        {
            options.AddConsole();
            options.AddDebug();
        });
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });
        services.AddTransient<IDbConnection>(a =>
        {
            var _connString = Configuration[GlobalConstants.CONNECTION_STRING];
            return new SqlConnection(_connString);
        });
        services.ConfigurePollyPolicies();
        services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });

        // Register the generic repository
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IUtilityBiz, UtilityBiz>();
        services.AddScoped<IProductBiz,ProductBiz>();
        services.AddScoped<IAwsServiceFacade, AwsServiceFacade>();
        services.AddScoped<IProductService, ProductService>();
        services.AddAWSService<IAmazonS3>();
        services.AddAWSService<IAmazonSQS>();
         
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "ProductManagement System", Version = "v1" });
        });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseHttpsRedirection();

        app.UseRouting();
        app.UseCors();
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            //c.SwaggerEndpoint("/swagger/v1/swagger.json", "FarmerMarketApi v1");
            //c.RoutePrefix = "";
            c.SwaggerEndpoint("/Prod/swagger/v1/swagger.json", "Product Management System v1");
            c.RoutePrefix = "swagger/ui"; // Set Swagger UI at the root URL
        });

        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapGet("/", async context =>
            {
                await context.Response.WriteAsync("Welcome to running ASP.NET Core on AWS Lambda");
            });
        });
    }
}