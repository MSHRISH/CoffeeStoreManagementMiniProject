using CoffeeStoreAPI.Context;
using CoffeeStoreAPI.Execptions;
using CoffeeStoreAPI.Iterfaces;
using CoffeeStoreAPI.Models;
using CoffeeStoreAPI.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace CoffeeStoreAPI.Repositories
{
    public class OrderItemsRepository : IRepository<OrderItemKeyDTO, OrderItem>
    {
        private readonly CoffeeStoreContext _context;

        public OrderItemsRepository(CoffeeStoreContext context)
        {
            _context = context;
        }

        public async Task<OrderItem> Add(OrderItem item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<OrderItem> Delete(OrderItemKeyDTO key)
        {
            var orderItem = await Get(key);
            if (orderItem != null)
            {
                _context.Remove(orderItem);
                _context.SaveChangesAsync();
            }
            throw new NoSuchOrderItemFoundExecption();
        }

        public async Task<OrderItem> Get(OrderItemKeyDTO key)
        {
            var orderItem = await _context.OrderItems.FirstOrDefaultAsync(e => e.OrderId== key.OrderId && e.ItemId==key.ItemId);
            return orderItem;
        }

        public async Task<IEnumerable<OrderItem>> GetAll()
        {
            var orderItems = await _context.OrderItems.ToListAsync();
            return orderItems;
        }

        public async Task<OrderItem> Update(OrderItem item)
        {
            OrderItemKeyDTO key=new OrderItemKeyDTO { ItemId = item.ItemId , OrderId=item.OrderId};
            var orderItem = await Get(key);
            if (orderItem != null)
            {
                _context.Update(item);
                _context.SaveChangesAsync();
                return orderItem;
            }
            throw new NoSuchOrderItemFoundExecption();
        }
    }
}
