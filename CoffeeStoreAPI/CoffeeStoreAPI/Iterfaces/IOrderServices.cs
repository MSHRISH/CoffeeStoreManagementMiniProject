using CoffeeStoreAPI.Models.DTOs;

namespace CoffeeStoreAPI.Iterfaces
{
    public interface IOrderServices
    {
        public Task<OrderDetailsDTO> OpenAnOrder(int UserId);

    }
}
