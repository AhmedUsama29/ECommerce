using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Orders
{
    public enum PaymentStatus
    {
        Pending = 0,
        PaymentReceived = 1,
        PaymentFailed = 2,
    }

    public class Order : BaseEntity<Guid>
    {

        public Order()
        {
            
        }

        public Order(string userEmail, List<OrderItem> items, OrderAddress address, decimal subTotal, DeliveryMethod deliveryMethod)
        {
            UserEmail = userEmail;
            Items = items;
            Address = address;
            SubTotal = subTotal;
            DeliveryMethod = deliveryMethod;
        }

        public string UserEmail { get; set; }

        public List<OrderItem> Items { get; set; } = [];

        public OrderAddress Address { get; set; }

        public string PaymentIntentId { get; set; } = string.Empty;

        public decimal SubTotal { get; set; }

        public DateTimeOffset Date { get; set; } = DateTimeOffset.Now;

        public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Pending;

        public DeliveryMethod DeliveryMethod { get; set; }

        public int DeliveryMethodId { get; set; }

    }
}
