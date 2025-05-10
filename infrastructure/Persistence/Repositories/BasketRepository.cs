using Domain.Contracs;
using Domain.Models.Baskets;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class BasketRepository(IConnectionMultiplexer _connection) : IBasketRepository
    {

        private readonly IDatabase _database = _connection.GetDatabase();


        public async Task DeleteAsync(string id)
        {
            await _database.KeyDeleteAsync(id);
        }

        public async Task<CustomerBasket?> GetAsync(string id)
        {
            var basket = await _database.StringGetAsync(id);

            return string.IsNullOrEmpty(basket) ? null : JsonSerializer.Deserialize<CustomerBasket>(basket!);
        }


        public async Task<CustomerBasket?> CreateOrUpdate(CustomerBasket basket, TimeSpan? TimeToLive = null)
        {
            var JsonBasket = JsonSerializer.Serialize(basket);

            var isCreatedOrUpdated = await _database.StringSetAsync(basket.Id, JsonBasket, TimeToLive ?? TimeSpan.FromDays(7));

            return isCreatedOrUpdated ? await GetAsync(basket.Id) : null;
        }

        
    }
}
