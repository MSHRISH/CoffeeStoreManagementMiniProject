namespace CoffeeStoreAPI.Models.DTOs
{
    public class ItemDetailsDTO:ItemBase
    {
        public int ItemId { get; set; }
        public string ItemType{ get; set; }
    }
}
