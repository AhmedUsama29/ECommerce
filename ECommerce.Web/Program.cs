using Domain.Contracs;
using ECommerce.Web.Factories;
using ECommerce.Web.Middelwares;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Data;
using Persistence.Repositories;
using Services;
using Services.MappingProfiles;
using ServicesAbstraction;
using Shared.ErrorModels;

namespace ECommerce.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddInfrastructureRegisteration(builder.Configuration);
            builder.Services.AddAplicationServices();
            builder.Services.AddWebApplicationServices();

            var app = builder.Build();

            await app.InitializeDbAsync();

            app.UseMiddleware<CustomExceptionHandlerMiddleware>();


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseStaticFiles();
            app.UseHttpsRedirection();

            //app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
        

    }
}
