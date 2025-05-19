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

        public string UserEmail { get; set; }

        public List<OrderItemDto> Items { get; set; } = [];

        public AddressDto Address { get; set; }

        public string PaymentIntentId { get; set; }

        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }

        public DateTimeOffset Date { get; set; }

        public string PaymentStatus { get; set; }

        public string DeliveryMethod { get; set; }

    }

    public class OrderItemDto
    {
        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public string ProductName { get; set; }

        public string PictureUrl { get; set; }
    }
}
