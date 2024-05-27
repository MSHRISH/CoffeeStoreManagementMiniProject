using CoffeeStoreAPI.Context;
using CoffeeStoreAPI.Execptions;
using CoffeeStoreAPI.Iterfaces;
using CoffeeStoreAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CoffeeStoreAPI.Repositories
{
    public class OrderRepository : IRepository<int, Order>
    {
        private readonly CoffeeStoreContext _context;

        public OrderRepository(CoffeeStoreContext context) 
        { 
            _context = context; 
        }
        public async Task<Order> Add(Order item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<Order> Delete(int key)
        {
            var order = await Get(key);
            if (order!= null)
            {
                _context.Remove(order);
                _context.SaveChangesAsync();
            }
            throw new NoSuchOrderFoundExecption();
        }

        public async Task<Order> Get(int key)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(e => e.OrderId== key);
            return order;
        }

        public async Task<IEnumerable<Order>> GetAll()
        {
            var orders = await _context.Orders.ToListAsync();
            return orders;
        }

        public async Task<Order> Update(Order item)
        {
            var order = await Get(item.OrderId);
            if (order!= null)
            {
                _context.Update(item);
                _context.SaveChangesAsync();
                return order;
            }
            throw new NoSuchOrderFoundExecption();
        }
    }
}
