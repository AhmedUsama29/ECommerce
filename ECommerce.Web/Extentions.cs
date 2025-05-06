using Domain.Contracs;
using ECommerce.Web.Factories;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Web
{
    public static class Extentions
    {

        public static IServiceCollection AddWebApplicationServices(this IServiceCollection services)
        {


            services.AddControllers();
            services.AddSwaggerServices();
            services.Configure<ApiBehaviorOptions>(options =>
            {

                options.InvalidModelStateResponseFactory = ApiResponseFactory.GenerateApiValidationResponse;
            });

            return services;

        }

        private static void AddSwaggerServices(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        public static async Task InitializeDbAsync(this WebApplication app)
        {

            using var Scope = app.Services.CreateScope(); //BG Services
            var dbInitializer = Scope.ServiceProvider.GetRequiredService<IDbInitializer>();
            await dbInitializer.InitializeAsync();

        }


    }
}
