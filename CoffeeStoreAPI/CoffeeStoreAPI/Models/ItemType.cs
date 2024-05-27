namespace CoffeeStoreAPI.Models
{
    public class ItemType
    {
        public int TypeId { get; set; }
        public string TypeName { get; set; }

        public ICollection<Item> Items { get; set; }
    }
}
