using Domain.Contracs;
using ECommerce.Web.Factories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Shared.Authentication;
using System.Text;

namespace ECommerce.Web
{
    public static class Extentions
    {

        public static IServiceCollection AddWebApplicationServices(this IServiceCollection services,IConfiguration configuration)
        {


            services.AddControllers();
            services.AddSwaggerServices();
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = ApiResponseFactory.GenerateApiValidationResponse;
            });
            services.ConfigureJWT(configuration);

            return services;

        }
        private static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
        {
            var jwt = configuration.GetSection("JWTOptions").Get<JWTOptions>();
            services.AddAuthentication(config =>
            {
                config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(config =>
            {
                config.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwt.Issuer,
                    ValidateAudience = true,
                    ValidAudience = jwt.Audience,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.SecretKey)),
                };
            });
        }

        private static void AddSwaggerServices(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        public static async Task<WebApplication> InitializeDbAsync(this WebApplication app)
        {

            using var Scope = app.Services.CreateScope(); //BG Services
            var dbInitializer = Scope.ServiceProvider.GetRequiredService<IDbInitializer>();
            await dbInitializer.InitializeAsync();
            await dbInitializer.InitializeIdentityAsync();
            return app;

        }


    }
}
