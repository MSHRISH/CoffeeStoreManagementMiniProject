using CoffeeStoreAPI.Context;
using CoffeeStoreAPI.Execptions;
using CoffeeStoreAPI.Iterfaces;
using CoffeeStoreAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CoffeeStoreAPI.Repositories
{
    public class ItemRepository : IRepository<int, Item>
    {
        private readonly CoffeeStoreContext _context;

        public ItemRepository(CoffeeStoreContext context) 
        {
            _context=context;
        }
        public async Task<Item> Add(Item item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<Item> Delete(int key)
        {
            var item = await Get(key);
            if (item != null)
            {
                _context.Remove(item);
                await _context.SaveChangesAsync();
            }
            throw new NoSuchItemExecption();
        }

        public async Task<Item> Get(int key)
        {
            var item = await _context.Items.FirstOrDefaultAsync(e => e.ItemId == key);
            return item;
        }

        public async Task<IEnumerable<Item>> GetAll()
        {
            var items = await _context.Items.ToListAsync();
            return items;
        }

        public async Task<Item> Update(Item item)
        {
            item = await Get(item.ItemId);
            if (item != null)
            {
                _context.Update(item);
                await _context.SaveChangesAsync();
                return item;
            }
            throw new NoSuchItemExecption();
        }
    }
}
