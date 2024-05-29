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

        //
        public async Task<ItemDetailsDTO> GetItemById(int ItemID)
        {
            var item = await _itemRepository.Get(ItemID);
            if (item == null)
            {
                throw new NoSuchItemExecption();
            }
            var itemType = await _itemTypeRepository.Get(item.ItemTypeId);
            return MapToItemDetailsDTO(item,itemType.TypeName);
        }
        

        public async Task<ItemTypeDetailsDTO> AddItemType(AddItemTypeDTO itemTypeDTO)
        {
            var itemTypes = await _itemTypeRepository.GetAll();
            bool hasItemType=itemTypes.Any(it=>it.TypeName==itemTypeDTO.TypeName);
            if(hasItemType)
            {
                throw new ItemTypeAlreadyExistsExecption();
            }
            ItemType itemType=MapDTOToItemType(itemTypeDTO);
            itemType=await _itemTypeRepository.Add(itemType);
            return MapToItemTypeDTO(itemType);
        }

        private ItemTypeDetailsDTO MapToItemTypeDTO(ItemType itemType)
        {
            ItemTypeDetailsDTO itemTypeDTO=new ItemTypeDetailsDTO();
            itemTypeDTO.TypeName = itemType.TypeName;
            itemTypeDTO.Id=itemType.TypeId;
            return itemTypeDTO;
        }

        private ItemType MapDTOToItemType(AddItemTypeDTO addItemTypeDTO)
        {
           ItemType itemType = new ItemType();
            itemType.TypeName = addItemTypeDTO.TypeName;
            return itemType;
        }

        public async Task<ItemTypeDetailsDTO> GetItemTypeById(int ItemTypeID)
        {
            var itemType=await _itemTypeRepository.Get(ItemTypeID);
            if (itemType == null)
            {
                throw new NoSuchItemTypeExecption();
            }
            return MapToItemTypeDTO(itemType);
        }

        public async Task<List<ItemTypeDetailsDTO>> GetAllItemTypes()
        {
            var itemTypes=await _itemTypeRepository.GetAll();
            var itemTypesDTO = from it in itemTypes
                               select MapToItemTypeDTO(it);
            return itemTypesDTO.ToList();
        }

        public async Task<List<ItemDetailsDTO>> GetItemsByTypeId(int ItemTypeId)
        {
            var itemType=await _itemTypeRepository.Get(ItemTypeId);
            if (itemType == null)
            {
                throw new NoSuchItemTypeExecption();
            }
            var items=await _itemRepository.GetAll();
            var itemTypes = await _itemTypeRepository.GetAll();
            var itemsDTO = from item in items
                           join itemtype in itemTypes on item.ItemTypeId equals itemtype.TypeId
                           where item.ItemTypeId == ItemTypeId
                           select MapToItemDetailsDTO(item,itemtype.TypeName);
            return itemsDTO.ToList();
        }
    }
}
