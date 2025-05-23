using AutoMapper;
using Domain.Contracs;
using Domain.Exceptions;
using Domain.Models.Orders;
using Microsoft.Extensions.Configuration;
using Services.Specifications;
using ServicesAbstraction;
using Shared.DataTransferObjects.Basket;
using Stripe;
using Stripe.Forwarding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Product = Domain.Models.Products.Product;

namespace Services
{
    public class PaymentService(IConfiguration _configuration,
                                IBasketRepository _basketRepository,
                                IUnitOfWork _unitOfWork,
                                IMapper _mapper) : IPaymentService
    {

        public async Task<BasketDto> CreateOrUpdatePaymentIntent(string basketId)
        {
            StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];

            var basket = await _basketRepository.GetAsync(basketId)
                ?? throw new BasketNotFoundException(basketId);


            foreach (var item in basket.Items)
            {
                var OriginalProduct = await _unitOfWork.GetRepository<Product, int>()
                                                        .GetByIdAsync(item.Id)
                                                        ?? throw new ProductNotFoundException(item.Id);

                item.Price = OriginalProduct.Price;
            }

            ArgumentNullException.ThrowIfNull(basket.DeliveryMethodId);

            var deliveryMethod = await _unitOfWork.GetRepository<DeliveryMethod, int>()
                                                  .GetByIdAsync(basket.DeliveryMethodId)
                ?? throw new DeliveryMethodNotFoundException(basket.DeliveryMethodId);

            basket.ShippingPrice = deliveryMethod.Cost;

            var amount = (basket.Items.Sum(i => i.Price * i.Quantity) + deliveryMethod.Cost) * 100;

            var service = new PaymentIntentService();

            if (string.IsNullOrWhiteSpace(basket.PaymentIntentId)) // Create
            {
                var options = new PaymentIntentCreateOptions()
                {
                    Amount = (long)amount,
                    Currency = "usd",
                    PaymentMethodTypes = ["card"]
                };

                var PaymentIntetnt = await service.CreateAsync(options);

                basket.PaymentIntentId = PaymentIntetnt.Id;
                basket.ClientSecret = PaymentIntetnt.ClientSecret;
            }
            else //Update
            {

                var options = new PaymentIntentUpdateOptions()
                {
                    Amount = (long)amount,
                };

                await service.UpdateAsync(basket.PaymentIntentId, options);

            }

            await _basketRepository.CreateOrUpdate(basket);

            return _mapper.Map<BasketDto>(basket);

        }

        public async Task UpdateOrderPaymentStatus(string jsonRequest, string stripeHeader)
        {
            var stripeEvent = EventUtility.ConstructEvent(jsonRequest,
                    stripeHeader, _configuration["Stripe:EndPointSecret"]);

            var PaymentIntetn = stripeEvent.Data.Object as PaymentIntent;

            if (stripeEvent.Type == EventTypes.PaymentIntentPaymentFailed)
            {
                await UpdatePaymentFailedAsync(PaymentIntetn.Id);
            }
            else if (stripeEvent.Type == EventTypes.PaymentIntentSucceeded)
            {
                await UpdatePaymentRecivedAsync(PaymentIntetn.Id);
            }
            else
            {
                Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);
            }

        }

        private async Task UpdatePaymentRecivedAsync(string PaymentIntetnId)
        {

            var orderRepo = _unitOfWork.GetRepository<Order, Guid>();

            var order = await orderRepo.GetByIdAsync(new OrderWithPaymentIntentSpecification(PaymentIntetnId));

            order.Status = PaymentStatus.PaymentReceived;

            orderRepo.Update(order);
            await _unitOfWork.SaveChanges();
        }

        private async Task UpdatePaymentFailedAsync(string PaymentIntetnId)
        {

            var orderRepo = _unitOfWork.GetRepository<Order, Guid>();

            var order = await orderRepo.GetByIdAsync(new OrderWithPaymentIntentSpecification(PaymentIntetnId));

            order.Status = PaymentStatus.PaymentFailed;

            orderRepo.Update(order);
            await _unitOfWork.SaveChanges();
        }
    }
}
