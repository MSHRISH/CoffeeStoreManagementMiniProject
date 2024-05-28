using CoffeeStoreAPI.Models.DTOs;

namespace CoffeeStoreAPI.Iterfaces
{
    public interface IItemServices
    {
        public Task<List<ItemDetailsDTO>> GetAllItems(bool ? isAvailable=null);

        public Task<ItemDetailsDTO> ChangeAvailabilityOfItem(int ItemId);

        public Task<ItemDetailsDTO> AddItem(AddItemDTO itemDTO);
    }
}
