using Domain.Models.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    public class OrderWithPaymentIntentSpecification(string PaymentIntentId) 
                : BaseSpecifications<Order>(Order => Order.PaymentIntentId == PaymentIntentId)
    {
    }
}
