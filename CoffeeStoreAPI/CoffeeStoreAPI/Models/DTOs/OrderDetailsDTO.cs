namespace CoffeeStoreAPI.Models.DTOs
{
    public class OrderDetailsDTO
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public string OrderStatus { get; set; }
        public double TotalAmount { get; set; }
        public DateTime OrderedOn { get; set; }

        public List<OrderItemDetailsDTO> OrderItems { get; set; }
    }
}
