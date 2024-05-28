namespace CoffeeStoreAPI.Models
{
    public class OrderItem
    {
        public int ItemId { get; set; }
        public int OrderId { get; set; }
        public string ItemStatus { get; set; } //Accepted, Preparation, Deleivered
        public string CancellationStatus { get; set; } //CancelledByUser, CancelledByStore, NULL

        public int Quantity { get; set; }

        public Item Item { get; set; }
        public Order Order { get; set; }
    }
}
