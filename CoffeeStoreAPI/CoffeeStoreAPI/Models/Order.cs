namespace CoffeeStoreAPI.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public string OrderStatus { get; set; } //Open, Closed, CancelledByUser, CancelledByStore
        public double TotalAmount { get; set; }
        public DateTime OrderedOn { get; set; }=DateTime.Now;

        public User User { get; set; }
        public ICollection<OrderItem> OrderItems { get; set;}
    }
}
