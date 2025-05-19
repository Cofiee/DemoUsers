/*  Wszystkimi technologiami mo¿na zarz¹dzaæ w formie scentralizowanego rejestru, 
 *  w celu unikniêcia problemów z ich wersjonowaniem i zarz¹dzaniem nimi.
 *  https://learn.microsoft.com/en-us/nuget/consume-packages/central-package-management
 */

using System.Reflection;
using DemoUsers.Server.Users;
using Microsoft.AspNetCore.Mvc;

namespace DemoUsers.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();
            builder.Services.AddControllers();
            builder.Services.AddOpenApi();
            builder.Services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
            });
            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblyContaining<Program>();
            });
            builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
            builder.Services.AddUsersFeature();
            
            var app = builder.Build();

            app.UseDefaultFiles();
            app.MapStaticAssets();

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.Services.AddExampleUsers();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();

            app.MapControllers();
            app.MapFallbackToFile("/index.html");

            app.Run();
        }
    }
}