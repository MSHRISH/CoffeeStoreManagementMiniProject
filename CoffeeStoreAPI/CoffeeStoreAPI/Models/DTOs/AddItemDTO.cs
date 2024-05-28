namespace CoffeeStoreAPI.Models.DTOs
{
    public class AddItemDTO
    {
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public bool IsAvailable { get; set; }
        public double Price { get; set; }
        public string ItemType { get; set; }
    }
}
