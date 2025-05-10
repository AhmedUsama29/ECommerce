using Domain.Models.Baskets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracs
{
    public interface IBasketRepository
    {

        Task<CustomerBasket?> GetAsync(string id);
        Task<CustomerBasket?> CreateOrUpdate(CustomerBasket basket, TimeSpan? TimeToLive = null);
        Task DeleteAsync(string id);



    }
}
