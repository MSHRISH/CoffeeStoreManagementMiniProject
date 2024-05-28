using CoffeeStoreAPI.Execptions;
using CoffeeStoreAPI.Iterfaces;
using CoffeeStoreAPI.Models;
using CoffeeStoreAPI.Models.DTOs;

namespace CoffeeStoreAPI.Services
{
    public class OrderServices : IOrderServices
    {
        private readonly IRepository<int, Order> _orderRepository;

        public OrderServices(IRepository<int,Order> orderRepository) 
        {
            _orderRepository=orderRepository;
        }
        public async Task<OrderDetailsDTO> OpenAnOrder(int UserId)
        {
            Order order=new Order();
            order.UserId=UserId;
            order=await _orderRepository.Add(order);
            if (order==null)
            {
                throw new FailedToOpenOrderExecption();
            }
            var orderDTO=MapToOrderDetailsDTO(order);
            return orderDTO;
        }

        private OrderDetailsDTO MapToOrderDetailsDTO(Order order)
        {
            OrderDetailsDTO orderDTO=new OrderDetailsDTO();
            orderDTO.UserId=order.UserId;
            orderDTO.OrderId=order.OrderId;
            orderDTO.OrderStatus=order.OrderStatus;
            orderDTO.OrderedOn=order.OrderedOn;
            orderDTO.TotalAmount=order.TotalAmount;
            orderDTO.OrderItems = new List<OrderItem>();
            return orderDTO;
        }
    }
}
