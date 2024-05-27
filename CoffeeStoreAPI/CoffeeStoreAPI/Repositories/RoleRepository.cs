using CoffeeStoreAPI.Context;
using CoffeeStoreAPI.Execptions;
using CoffeeStoreAPI.Iterfaces;
using CoffeeStoreAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CoffeeStoreAPI.Repositories
{
    public class RoleRepository : IRepository<int, Role>
    {
        private readonly CoffeeStoreContext _context;

        public RoleRepository(CoffeeStoreContext context)
        {
            _context = context;
        }
        public async Task<Role> Add(Role item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<Role> Delete(int key)
        {
            var role = await Get(key);
            if (role != null)
            {
                _context.Remove(role);
                _context.SaveChangesAsync();
            }
            throw new NoSuchRoleFoundExecption();
        }

        public async Task<Role> Get(int key)
        {
            var role = await _context.Roles.FirstOrDefaultAsync(e => e.RoleId == key);
            return role;
        }

        public async Task<IEnumerable<Role>> GetAll()
        {
            var roles = await _context.Roles.ToListAsync();
            return roles;
        }

        public async Task<Role> Update(Role item)
        {
            var role = await Get(item.RoleId);
            if (role != null)
            {
                _context.Update(item);
                _context.SaveChangesAsync();
                return role;
            }
            throw new NoSuchRoleFoundExecption();
        }
    }
}
