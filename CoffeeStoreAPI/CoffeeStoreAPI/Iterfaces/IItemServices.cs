using CoffeeStoreAPI.Models.DTOs;

namespace CoffeeStoreAPI.Iterfaces
{
    public interface IItemServices
    {
        public Task<List<ItemDetailsDTO>> GetAllItems(bool ? isAvailable=null);

        public Task<ItemDetailsDTO> ChangeAvailabilityOfItem(int ItemId);

        public Task<ItemDetailsDTO> AddItem(AddItemDTO itemDTO);

        //
        public Task<ItemDetailsDTO> GetItemById(int ItemID);
        public Task<ItemTypeDetailsDTO> AddItemType(AddItemTypeDTO itemTypeDTO); 

        public Task<ItemTypeDetailsDTO> GetItemTypeById (int ItemTypeID);

        public Task<List<ItemTypeDetailsDTO>> GetAllItemTypes();

        public Task<List<ItemDetailsDTO>> GetItemsByTypeId(int ItemTypeId);
    }
}
