using Domain.Contracs;
using ServicesAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Services
{
    public class CacheService(ICacheRepository _cacheRepository) : ICacheService
    {
        public async Task<string?> GetAsync(string cacheKey)
            => await _cacheRepository.GetAsync(cacheKey);
        

        public Task SetAsync(string cacheKey, object value, TimeSpan lifeTime)
        {
            var option = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var json = JsonSerializer.Serialize(value, option);

            return _cacheRepository.SetAsync(cacheKey, json, lifeTime);
        }
    }
}
