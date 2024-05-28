namespace CoffeeStoreAPI.Models
{
    public class ItemBase
    {
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public bool IsAvailable { get; set; }
        public double Price { get; set; }
    }
    public class Item:ItemBase
    {
        public int ItemId { get; set; }
        public int ItemTypeId { get; set; }

        public ItemType ItemType { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }

    }
}
