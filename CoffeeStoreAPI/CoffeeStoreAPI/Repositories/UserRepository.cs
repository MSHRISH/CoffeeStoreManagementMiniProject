using CoffeeStoreAPI.Context;
using CoffeeStoreAPI.Execptions;
using CoffeeStoreAPI.Iterfaces;
using CoffeeStoreAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CoffeeStoreAPI.Repositories
{
    public class UserRepository : IRepository<int, User>
    {
        private readonly CoffeeStoreContext _context;

        public UserRepository(CoffeeStoreContext context) 
        { 
            _context=context;
        }
        public async Task<User> Add(User item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<User> Delete(int key)
        {
            var user = await Get(key);
            if (user!= null)
            {
                _context.Remove(user);
                _context.SaveChangesAsync();
            }
            throw new NoSuchUserException();
        }

        public async Task<User> Get(int key)
        {
            var user = await _context.Users.FirstOrDefaultAsync(e=>e.Id==key);
            return user;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            var users = await _context.Users.ToListAsync();
            return users;
        }

        public async Task<User> Update(User item)
        {
            var user= await Get(item.Id);
            if (user!= null)
            {
                _context.Update(item);
                _context.SaveChangesAsync();
                return user;
            }
            throw new NoSuchUserException();
        }
    }
}
