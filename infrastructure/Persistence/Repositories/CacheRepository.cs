using Domain.Contracs;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class CacheRepository(IConnectionMultiplexer connectionMultiplexer) : ICacheRepository
    {

        private readonly IDatabase _database = connectionMultiplexer.GetDatabase();
        public async Task<string?> GetAsync(string cacheKey)
        {
            var value = await _database.StringGetAsync(cacheKey);

            return value.IsNullOrEmpty ? null : value.ToString();
        }

        public async Task SetAsync(string cacheKey, string Value, TimeSpan lifeTime)
        {
            await _database.StringSetAsync(cacheKey, Value, lifeTime);
        }
    }
}
