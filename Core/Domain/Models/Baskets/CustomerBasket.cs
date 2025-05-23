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

        public string? ClientSecret { get; set; }
        public string? PaymentIntentId { get; set; }
        public int DeliveryMethodId { get; set; }
        public decimal ShippingPrice { get; set; }

    }
}
