using Shared.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects.Orders
{
    public class OrderResponse
    {
        public Guid Id { get; set; }

        public string BuyerEmail { get; set; } //

        public List<OrderItemDto> Items { get; set; } = [];

        public AddressDto ShipToAddress { get; set; }

        public string PaymentIntentId { get; set; }

        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }

        public DateTimeOffset OrderDate { get; set; } //

        public string Status { get; set; } //

        public decimal DeliveryCost { get; set; }

        public string DeliveryMethod { get; set; }

    }

    public class OrderItemDto
    {

        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public string ProductName { get; set; }

        public string PictureUrl { get; set; }
    }
}
