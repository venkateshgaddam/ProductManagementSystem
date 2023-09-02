using Microsoft.AspNetCore.Builder;
using Microsoft.Data.SqlClient;
using ProductManagement.Common;
using ProductManagementSystem.CommonAPI.Extensions;
using System.Data;

namespace ProductManagementSystem.CommonAPI;

public class BaseStartup
{
    public BaseStartup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container
    public virtual void ConfigureServices(IServiceCollection services)
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
        services.AddSwaggerGen();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
    public virtual void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

    }
}
