namespace CoffeeStoreAPI.Models.DTOs
{
    public class OrderItemDetailsDTO
    {
        public int ItemId { get; set; }
        public string ItemStatus { get; set; }
        public string CancellationStatus { get; set; }
        public int Quantity { get; set; }
    }
}
