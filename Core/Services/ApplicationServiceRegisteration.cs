using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.MappingProfiles;
using ServicesAbstraction;
using Shared.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public static class ApplicationServiceRegisteration
    {

        public static IServiceCollection AddAplicationServices(this IServiceCollection services ,
                                                                IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(ProductProfile).Assembly);
            services.AddScoped<IServiceManager, ServiceManager>();

            services.Configure<JWTOptions>(configuration.GetSection("JWTOptions"));

            return services;
        }

    }
}
