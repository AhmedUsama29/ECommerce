using Domain.Contracs;
using Domain.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Data;
using Persistence.Identity;
using Persistence.Repositories;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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

            services.AddDbContext<StoreIdentityDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("IdentityStoreConnection")));

            services.AddScoped<IDbInitializer, DbInitializer>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddSingleton<IConnectionMultiplexer>(options =>
            {
                var redisConnection = configuration.GetConnectionString("RedisConnection");
                return ConnectionMultiplexer.Connect(redisConnection!);
            });

            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<ICacheRepository, CacheRepository>();

            services.RegisterIdentity();

            return services;
        }


        private static IServiceCollection RegisterIdentity(this IServiceCollection services)
        {

            //services.AddIdentity<ApplicationUser, IdentityRole>();

            services.AddIdentityCore<ApplicationUser>(config =>
            {
                config.User.RequireUniqueEmail = true;
                
            })
                    .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<StoreIdentityDbContext>();

            return services;
        }
    }
}
