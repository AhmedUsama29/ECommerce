using Shared.DataTransferObjects.Basket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesAbstraction
{
    public interface IPaymentService
    {

        Task<BasketDto> CreateOrUpdatePaymentIntent(string basketId);

        Task UpdateOrderPaymentStatus(string jsonRequest, string stripeHeader);

    }
}
