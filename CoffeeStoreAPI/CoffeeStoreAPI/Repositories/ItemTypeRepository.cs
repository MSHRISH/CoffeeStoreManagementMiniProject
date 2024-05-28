using CoffeeStoreAPI.Context;
using CoffeeStoreAPI.Execptions;
using CoffeeStoreAPI.Iterfaces;
using CoffeeStoreAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CoffeeStoreAPI.Repositories
{
    public class ItemTypeRepository:IRepository<int,ItemType>
    {
        private readonly CoffeeStoreContext _context;

        public ItemTypeRepository(CoffeeStoreContext context) 
        { 
            _context=context;
        }

        public async Task<ItemType> Add(ItemType item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<ItemType> Delete(int key)
        {
            var itemType = await Get(key);
            if (itemType!= null)
            {
                _context.Remove(itemType);
                await _context.SaveChangesAsync();
            }
            throw new NoSuchItemTypeExecption();
        }

        public async Task<ItemType> Get(int key)
        {
            var itemType= await _context.ItemTypes.FirstOrDefaultAsync(e => e.TypeId == key);
            return itemType;
        }

        public async Task<IEnumerable<ItemType>> GetAll()
        {
            var itemTypes = await _context.ItemTypes.ToListAsync();
            return itemTypes;
        }

        public async Task<ItemType> Update(ItemType item)
        {
            var itemType = await Get(item.TypeId);
            if (itemType!= null)
            {
                _context.Update(item);
                await _context.SaveChangesAsync();
                return itemType;
            }
            throw new NoSuchItemTypeExecption();
        }
    }
}
