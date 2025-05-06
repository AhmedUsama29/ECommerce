using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Baskets
{
    public class CustomerBasket
    {

        public string Id { get; set; } // GUID , from lient side

        public IEnumerable<BasketItem> Items { get; set; } = [];

    }
}
