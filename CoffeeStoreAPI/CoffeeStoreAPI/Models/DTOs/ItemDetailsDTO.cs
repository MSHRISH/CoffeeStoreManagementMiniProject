namespace CoffeeStoreAPI.Models.DTOs
{
    public class ItemDetailsDTO
    {
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public bool IsAvailable { get; set; }
        public double Price { get; set; }
        public int ItemId { get; set; }
        public string ItemType{ get; set; }
    }
}
