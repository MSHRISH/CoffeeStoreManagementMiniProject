using CoffeeStoreAPI.Models;
using CoffeeStoreAPI.Models.DTOs;

namespace CoffeeStoreAPI.Iterfaces
{
    public interface IOrderServices
    {
        public Task<OrderDetailsDTO> OpenAnOrder(int UserId);

        public Task<OrderDetailsDTO> AddToOrder(AddOrderItemDTO orderItemDTO,int userId);

        public Task<OrderDetailsDTO> GetOrderDetails(int OrderId);

        public Task<OrderDetailsDTO> GetMyOrderDetails(int OrderId, int UserID);

        public Task<OrderDetailsDTO> CancelOrderItemByStore(CancelOrderItemDTO cancelOrderItemDTO);

        public Task<OrderDetailsDTO> CancelOrderItemByCustomer(CancelOrderItemDTO cancelOrderItemDTO,int userid);

        //  
        public Task<OrderItemDetailsDTO> ChangeOrderItemStatus(int orderItemId,int status);


    }
}
