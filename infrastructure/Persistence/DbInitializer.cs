using Domain.Contracs;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistence
{
    public class DbInitializer(StoreDbContext _storeDbContext) : IDbInitializer
    {
        public async Task InitializeAsync()
        {
            //In Deployment
            //if((await _storeDbContext.Database.GetPendingMigrationsAsync()).Any())
            //{
            //    await _storeDbContext.Database.MigrateAsync();
            //}

            //In Development

            try
            {
                if (!_storeDbContext.Set<ProductBrand>().Any())
                {
                    var data = await File.ReadAllTextAsync(@"..\infrastructure\Persistence\Data\Seeding\brands.json");
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(data);
                    if (brands is not null && brands.Any())
                    {
                        _storeDbContext.Set<ProductBrand>().AddRange(brands);
                        await _storeDbContext.SaveChangesAsync();
                    }
                }
                if (!_storeDbContext.Set<ProductType>().Any())
                {
                    var data = await File.ReadAllTextAsync(@"..\infrastructure\Persistence\Data\Seeding\types.json");
                    var types = JsonSerializer.Deserialize<List<ProductType>>(data);
                    if (types is not null && types.Any())
                    {
                        _storeDbContext.Set<ProductType>().AddRange(types);
                        await _storeDbContext.SaveChangesAsync();
                    }
                }
                if (!_storeDbContext.Set<Product>().Any())
                {
                    var data = await File.ReadAllTextAsync(@"..\infrastructure\Persistence\Data\Seeding\products.json");
                    var products = JsonSerializer.Deserialize<List<Product>>(data);
                    if (products is not null && products.Any())
                    {
                        _storeDbContext.Set<Product>().AddRange(products);
                        await _storeDbContext.SaveChangesAsync();
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }

        }
    }
}
