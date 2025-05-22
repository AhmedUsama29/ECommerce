using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracs
{
    public interface ICacheRepository
    {

        Task<string?> GetAsync(string cacheKey);

        Task SetAsync(string cacheKey, string Value, TimeSpan lifeTime);

    }
}
