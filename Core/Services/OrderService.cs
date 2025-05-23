using AutoMapper;
using Domain.Contracs;
using Domain.Exceptions;
using Domain.Models.Baskets;
using Domain.Models.Orders;
using Domain.Models.Products;
using Services.Specifications;
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

            var Address = _mapper.Map<OrderAddress>(orderRequest.ShipToAddress);

            var SubTotal = items.Sum(i => i.Price * i.Quantity);

            var Order = new Order(email,items,Address,SubTotal,method);

            var orderRepo = _unitOfWork.GetRepository<Order, Guid>();
            orderRepo.Add(Order);
            await _unitOfWork.SaveChanges();

            await _basketRepository.DeleteAsync(orderRequest.BasketId);

            return _mapper.Map<OrderResponse>(Order);

        }

        public async Task<IEnumerable<OrderResponse>> GetAllAsync(string email)
        {
            var orders = await _unitOfWork.GetRepository<Order, Guid>()
                                          .GetAllAsync(new OrderSpecification(email));

            return _mapper.Map<IEnumerable<OrderResponse>>(orders);
        }

        public async Task<OrderResponse> GetByIdAsync(Guid orderId)
        {
            var order = await _unitOfWork.GetRepository<Order, Guid>()
                                          .GetByIdAsync(new OrderSpecification(orderId));

            return _mapper.Map<OrderResponse>(order);
        }

        public async Task<IEnumerable<DeliveryMethodResponse>> GetDeliveryMethodsAsync()
        {
            var DeliveryMethods = await _unitOfWork.GetRepository<DeliveryMethod, int>()
                                             .GetAllAsync();

            return _mapper.Map<IEnumerable<DeliveryMethodResponse>>(DeliveryMethods);

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
