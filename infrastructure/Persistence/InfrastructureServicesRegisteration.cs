using Domain.Contracs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Data;
using Persistence.Repositories;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public static class InfrastructureServicesRegisteration
    {

        public static IServiceCollection AddInfrastructureRegisteration(this IServiceCollection services,IConfiguration configuration)
        {

            services.AddDbContext<StoreDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IDbInitializer, DbInitializer>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddSingleton<IConnectionMultiplexer>(options =>
            {
                var redisConnection = configuration.GetConnectionString("RedisConnection");
                return ConnectionMultiplexer.Connect(redisConnection!);
            });

            services.AddScoped<IBasketRepository, BasketRepository>();

            return services;
        }

    }
}
