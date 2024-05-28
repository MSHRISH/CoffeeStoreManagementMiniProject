using CoffeeStoreAPI.Execptions;
using CoffeeStoreAPI.Iterfaces;
using CoffeeStoreAPI.Models;
using CoffeeStoreAPI.Models.DTOs;

namespace CoffeeStoreAPI.Services
{
    public class ItemServices : IItemServices
    {
        private readonly IRepository<int, Item> _itemRepository;
        private readonly IRepository<int, ItemType> _itemTypeRepository;

        public ItemServices(IRepository<int,Item> itemRepository,IRepository<int,ItemType> itemTypeRepository) 
        {
            _itemRepository=itemRepository;
            _itemTypeRepository=itemTypeRepository;
        }

        public async Task<ItemDetailsDTO> AddItem(AddItemDTO itemDTO)
        {
            var itemTypes = (await _itemTypeRepository.GetAll()).ToList();
            var itemType = (from iT in itemTypes where iT.TypeName == itemDTO.ItemType 
                           select iT).ToList();
            if (itemType == null)
            {
                throw new NoSuchItemTypeExecption();
            }
            var item = MapDTOtoItem(itemDTO, itemType[0].TypeId);
            item = await _itemRepository.Add(item);
            var itemDetailsDTO = MapToItemDetailsDTO(item, itemDTO.ItemType);
            return itemDetailsDTO;
        }

        private Item MapDTOtoItem(AddItemDTO itemDetailsDTO,int ItemTypeId)
        {
            Item item = new Item();
            item.ItemName = itemDetailsDTO.ItemName;
            item.ItemDescription = itemDetailsDTO.ItemDescription;
            item.Price = itemDetailsDTO.Price;
            item.IsAvailable = itemDetailsDTO.IsAvailable;
            item.Price= itemDetailsDTO.Price;
            item.ItemTypeId=ItemTypeId;
            return item;
        }

        public async Task<ItemDetailsDTO> ChangeAvailabilityOfItem(int ItemId)
        {
            var item=await _itemRepository.Get(ItemId);
            var itemType=await _itemTypeRepository.Get(ItemId);
            if(item == null)
            {
                throw new NoSuchItemExecption();    
            }
            if(item.IsAvailable)
            {
                item.IsAvailable = false;
            }
            else
            {
                item.IsAvailable= true;
            }
            item = await _itemRepository.Update(item);
            var itemDetailsDTO=MapToItemDetailsDTO(item,itemType.TypeName);
            return itemDetailsDTO;
        }

        public async Task<List<ItemDetailsDTO>> GetAllItems(bool ? isAvailable = null)
        {
            var items=await _itemRepository.GetAll();
            var itemTypes = await _itemTypeRepository.GetAll();
                     
            var ItemsDetails = (from item in items
                               join itemtype in itemTypes on item.ItemTypeId equals itemtype.TypeId
                               select MapToItemDetailsDTO(item,itemtype.TypeName)).ToList();
            if (isAvailable.HasValue)
            {
                ItemsDetails = (from itd in ItemsDetails
                               where itd.IsAvailable == isAvailable.Value
                               select itd).ToList();
            }
            return ItemsDetails;
        }

        private ItemDetailsDTO MapToItemDetailsDTO(Item item, string itemType)
        {
            
            return new ItemDetailsDTO
            {
                ItemId = item.ItemId,
                ItemName = item.ItemName,
                ItemDescription = item.ItemDescription,
                IsAvailable = item.IsAvailable,
                Price = item.Price,
                ItemType = itemType
            };
        }
    }
}
