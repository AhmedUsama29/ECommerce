using AutoMapper;
using Domain.Contracs;
using Domain.Exceptions;
using Domain.Models.Baskets;
using Domain.Models.Orders;
using Domain.Models.Products;
using ServicesAbstraction;
using Shared.DataTransferObjects.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class OrderService(IBasketRepository _basketRepository,
                              IUnitOfWork _unitOfWork,
                              IMapper _mapper) : IOrderService
    {
        public async Task<OrderResponse> CreateAsync(OrderRequest orderRequest, string email)
        {
            var basket = await _basketRepository.GetAsync(orderRequest.BasketId)
                ?? throw new BasketNotFoundException(orderRequest.BasketId);

            List<OrderItem> items = [];

            foreach (var item in basket.Items)
            {
                var OriginalProduct = await _unitOfWork.GetRepository<Product,int>()
                                                        .GetByIdAsync(item.Id) 
                                                        ?? throw new ProductNotFoundException(item.Id);



                items.Add(CreateOrderItem(OriginalProduct,item));

            }

            var method = await _unitOfWork.GetRepository<DeliveryMethod, int>()
                                          .GetByIdAsync(orderRequest.DeliveryMethodId)
                                          ?? throw new DeliveryMethodNotFoundException(orderRequest.DeliveryMethodId);

            var Address = _mapper.Map<OrderAddress>(orderRequest.Address);

            var SubTotal = items.Sum(i => i.Price * i.Quantity);

            var Order = new Order(email,items,Address,SubTotal,method);

            var orderRepo = _unitOfWork.GetRepository<Order, Guid>();
            orderRepo.Add(Order);
            await _unitOfWork.SaveChanges();

            await _basketRepository.DeleteAsync(orderRequest.BasketId);

            return _mapper.Map<OrderResponse>(Order);

        }

        public Task<IEnumerable<OrderResponse>> GetAllAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<OrderResponse> GetByIdAsync(Guid orderId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<DeliveryMethodResponse>> GetDeliveryMethodsAsync()
        {
            throw new NotImplementedException();
        }

        private OrderItem CreateOrderItem(Product originalProduct, BasketItem item)
        {
            return new OrderItem()
            {
                ProductName = originalProduct.Name,
                PictureUrl = originalProduct.PictureUrl,
                Price = originalProduct.Price,
                Quantity = item.Quantity,
                ProductId = originalProduct.Id
            };
        }


    }
}
