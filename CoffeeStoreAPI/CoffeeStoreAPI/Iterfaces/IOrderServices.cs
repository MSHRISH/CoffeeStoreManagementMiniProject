using CoffeeStoreAPI.Models;
using CoffeeStoreAPI.Models.DTOs;

namespace CoffeeStoreAPI.Iterfaces
{
    public interface IOrderServices
    {
        public Task<OrderDetailsDTO> OpenAnOrder(int UserId);

        public Task<OrderDetailsDTO> AddToOrder(AddOrderItemDTO orderItemDTO,int userId);

        public Task<OrderDetailsDTO> GetOrderDetails(int OrderId);

        public Task<List<OrderDetailsDTO>> GetAllOrders();

        public Task<List<OrderDetailsDTO>> GetAllMyOrders(int UserID);

        public Task<OrderDetailsDTO> GetMyOrderDetails(int OrderId, int UserID);

        public Task<OrderDetailsDTO> CancelOrderItemByStore(CancelOrderItemDTO cancelOrderItemDTO);

        public Task<OrderDetailsDTO> CancelOrderItemByCustomer(CancelOrderItemDTO cancelOrderItemDTO,int userid);

        //  
        public Task<OrderItemDetailsDTO> ChangeOrderItemStatus(int orderItemId,int status);

        public Task<OrderDetailsDTO> CancelOrderByStore(int orderId);

        public Task<OrderDetailsDTO> CancellOrderByCustomer(int orderId,int userId);


    }
}
