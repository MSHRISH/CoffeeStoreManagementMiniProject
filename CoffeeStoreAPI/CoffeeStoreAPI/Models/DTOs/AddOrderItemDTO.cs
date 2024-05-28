namespace CoffeeStoreAPI.Models.DTOs
{
    public class AddOrderItemDTO
    {
        public int OrderId { get; set; }

        public int ItemId { get; set; }

        public int Quantity { get; set; }
    }
}
