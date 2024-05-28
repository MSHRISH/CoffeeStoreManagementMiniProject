namespace CoffeeStoreAPI.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public string OrderStatus { get; set; } = "Open";//Open, Closed, CancelledByUser, CancelledByStore
        public double TotalAmount { get; set; }= 0;
        public DateTime OrderedOn { get; set; }=DateTime.Now;

        public User User { get; set; }
        public ICollection<OrderItem> OrderItems { get; set;}
    }
}
