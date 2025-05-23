using Shared.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects.Orders
{
    public class OrderRequest
    {

        public string BasketId { get; set; }

        public AddressDto ShipToAddress { get; set; }

        public int DeliveryMethodId { get; set; }



    }
}
