using CoffeeStoreAPI.Execptions;
using CoffeeStoreAPI.Iterfaces;
using CoffeeStoreAPI.Models;
using CoffeeStoreAPI.Models.DTOs;
using CoffeeStoreAPI.Repositories;
using System.ComponentModel.DataAnnotations;

namespace CoffeeStoreAPI.Services
{
    public class OrderServices : IOrderServices
    {
        private readonly IRepository<int, Order> _orderRepository;
        private readonly IRepository<int, OrderItem> _orderItemRepository;
        private readonly IRepository<int, Item> _itemRepository;

        public OrderServices(IRepository<int,Order> orderRepository, IRepository<int,OrderItem> orderItemRepository, IRepository<int,Item> itemRepository) 
        {
            _orderRepository=orderRepository;
            _orderItemRepository=orderItemRepository;
            _itemRepository=itemRepository;
        }

        public async Task<OrderDetailsDTO> AddToOrder(AddOrderItemDTO orderItemDTO,int userId)
        {
            if (orderItemDTO.Quantity == 0)
            {
                throw new QuantityIsZeroExecption();
            }
            //Check if the order exist
            var order=await _orderRepository.Get(orderItemDTO.OrderId);
            if(order==null || order.UserId!=userId)
            {
                throw new NoSuchOrderFoundExecption();
            }
            
            //Check if the item exist and is available
            var item=await _itemRepository.Get(orderItemDTO.ItemId);
            if (item == null)
            {
                throw new NoSuchItemExecption();
            }
            if(item.IsAvailable==false) 
            {
                throw new ItemNotAvailableExecption();
            }

            //Check If the item is already ordered, if yes then update quantity or add a new orderitem
            var orderItems = await _orderItemRepository.GetAll();
            var orderItem = orderItems.FirstOrDefault(oi => oi.OrderId == orderItemDTO.OrderId && oi.ItemId==orderItemDTO.ItemId && oi.CancellationStatus=="NULL");

            if(orderItem!=null)
            {
                orderItem.Quantity += orderItemDTO.Quantity;
                orderItem=await _orderItemRepository.Update(orderItem);
            }
            else
            {
                //MapDTO to OrderITem
                OrderItem orderitem = MapDTOToOrderItem(orderItemDTO);
                orderitem = await _orderItemRepository.Add(orderitem);
            }
            

            //Update Bill amount 
            order.TotalAmount += item.Price * orderItemDTO.Quantity;
            order=await _orderRepository.Update(order);
            var orderDetailsDTO = await MapToOrderDetailsDTO(order);

            return orderDetailsDTO;
                
        }

        private OrderItem MapDTOToOrderItem(AddOrderItemDTO orderItemDTO)
        {
            OrderItem orderitem = new OrderItem();
            orderitem.OrderId = orderItemDTO.OrderId;
            orderitem.ItemId = orderItemDTO.ItemId;
            orderitem.Quantity = orderItemDTO.Quantity;
            orderitem.ItemStatus = "Accepted";
            orderitem.CancellationStatus = "NULL";
            return orderitem;
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
            var orderDTO=await MapToOrderDetailsDTO(order);
            return orderDTO;
        }

        private async Task<List<OrderItemDetailsDTO>> GetAllOrderItems(int orderid)
        {
            var orderitems=await _orderItemRepository.GetAll();
            
            var orderItemsDTO=from orderitem in orderitems
                              where orderitem.OrderId==orderid
                              select MapToOrderItemDetailsDTO(orderitem);
            return orderItemsDTO.ToList();
        }
        private OrderItemDetailsDTO MapToOrderItemDetailsDTO(OrderItem orderItem)
        {
            OrderItemDetailsDTO orderItemDTO=new OrderItemDetailsDTO();
            orderItemDTO.OrderItemId = orderItem.OrderItemId;
            orderItemDTO.ItemId = orderItem.ItemId;
            orderItemDTO.Quantity = orderItem.Quantity;
            orderItemDTO.ItemStatus = orderItem.ItemStatus;
            orderItemDTO.CancellationStatus = orderItem.CancellationStatus;
            return orderItemDTO;

        }
        private async Task<OrderDetailsDTO> MapToOrderDetailsDTO(Order order)
        {
            OrderDetailsDTO orderDTO=new OrderDetailsDTO();
            orderDTO.UserId=order.UserId;
            orderDTO.OrderId=order.OrderId;
            orderDTO.OrderStatus=order.OrderStatus;
            orderDTO.OrderedOn=order.OrderedOn;
            orderDTO.TotalAmount=order.TotalAmount;
            orderDTO.OrderItems = await GetAllOrderItems(order.OrderId);
            return orderDTO;
        }

        public async Task<OrderDetailsDTO> GetOrderDetails(int OrderId)
        {
            var order=await _orderRepository.Get(OrderId);
            if(order == null)
            {
                throw new NoSuchOrderFoundExecption();
            }
            return await MapToOrderDetailsDTO(order);
        }

        public async Task<OrderDetailsDTO> GetMyOrderDetails(int OrderId, int UserID)
        {
            var order=await _orderRepository.Get(OrderId);
            if (order == null)
            {
                throw new NoSuchOrderFoundExecption();
            }
            if (order.UserId == UserID)
            {
                return await MapToOrderDetailsDTO(order);
            }
            throw new NoSuchOrderFoundExecption();
        }

        public async Task<OrderDetailsDTO> CancelOrderItemByStore(CancelOrderItemDTO cancelOrderItemDTO)
        {
            var orderItem = await _orderItemRepository.Get(cancelOrderItemDTO.OrderItemId);
            if(orderItem == null)
            {
                throw new NoSuchOrderItemFoundExecption();
            }
            if (orderItem.ItemStatus != "Accepted")
            {
                throw new PreparationStartedExecption(); //Store cant cancel the items that are started preparation or delivered.
            }
            orderItem.CancellationStatus = "CancelledByStore";
            orderItem=await _orderItemRepository.Update(orderItem);

            var order=await _orderRepository.Get(orderItem.OrderId);
            var item=await _itemRepository.Get(orderItem.ItemId);
            order.TotalAmount -= (item.Price * orderItem.Quantity);

            order=await _orderRepository.Update(order);
            return await MapToOrderDetailsDTO(order);
        }

        public async Task<OrderDetailsDTO> CancelOrderItemByCustomer(CancelOrderItemDTO cancelOrderItemDTO, int userid)
        {
            var orderitem=await _orderItemRepository.Get(cancelOrderItemDTO.OrderItemId);
            if(orderitem == null)
            {
                throw new NoSuchOrderItemFoundExecption();
            }
            if (orderitem.ItemStatus == "Deleivered")
            {
                throw new ItemDeleiveredExecption();
            }
            var order=await _orderRepository.Get(orderitem.OrderId);
            if (order.UserId != userid)
            {
                throw new NoSuchOrderItemFoundExecption();
            }
            
            orderitem.CancellationStatus = "CancelledByCustomer";
            orderitem=await _orderItemRepository.Update(orderitem);

            var item = await _itemRepository.Get(orderitem.ItemId);
            if (orderitem.ItemStatus == "Accepted")
            {
                order.TotalAmount -= (item.Price * orderitem.Quantity);
            }
            order=await _orderRepository.Update(order);
            return await MapToOrderDetailsDTO(order);
        }
    }
}
