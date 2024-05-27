using CoffeeStoreAPI.Context;
using CoffeeStoreAPI.Execptions;
using CoffeeStoreAPI.Iterfaces;
using CoffeeStoreAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CoffeeStoreAPI.Repositories
{
    public class AuthenticationRepository : IRepository<int, Authentication>
    {
        private readonly CoffeeStoreContext _context;

        public AuthenticationRepository(CoffeeStoreContext context) {
            _context = context;
        }
        public async Task<Authentication> Add(Authentication item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<Authentication> Delete(int key)
        {
            var user = await Get(key);
            if (user != null)
            {
                _context.Remove(user);
                _context.SaveChangesAsync();
            }
            throw new NoSuchUserException();
        }

        public async Task<Authentication> Get(int key)
        {
            var user = await _context.Authentications.FirstOrDefaultAsync(e => e.Id == key);
            return user;
        }

        public async Task<IEnumerable<Authentication>> GetAll()
        {
            var users = await _context.Authentications.ToListAsync();
            return users;
        }

        public async Task<Authentication> Update(Authentication item)
        {
            var user = await Get(item.Id);
            if (user != null)
            {
                _context.Update(item);
                _context.SaveChangesAsync();
                return user;
            }
            throw new NoSuchUserException();
        }
    }
}
