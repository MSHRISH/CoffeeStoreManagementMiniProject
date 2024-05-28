using CoffeeStoreAPI.Context;
using CoffeeStoreAPI.Execptions;
using CoffeeStoreAPI.Iterfaces;
using CoffeeStoreAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CoffeeStoreAPI.Repositories
{
    public class RoleMappingsRepository : IRepository<int, RoleMapping>
    {
        private readonly CoffeeStoreContext _context;

        public RoleMappingsRepository(CoffeeStoreContext context) 
        {
            _context=context;
        }
        public async Task<RoleMapping> Add(RoleMapping item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<RoleMapping> Delete(int key)
        {
            var roleMapping = await Get(key);
            if (roleMapping!= null)
            {
                _context.Remove(roleMapping);
                await _context.SaveChangesAsync();
            }
            throw new UnableToAssignRoleExecption();
        }

        public async Task<RoleMapping> Get(int key)
        {
            var roleMapping= await _context.RoleMappings.FirstOrDefaultAsync(e => e.UserId== key);
            return roleMapping;
        }

        public  async Task<IEnumerable<RoleMapping>> GetAll()
        {
            var roleMappings= await _context.RoleMappings.ToListAsync();
            return roleMappings;
        }

        public async Task<RoleMapping> Update(RoleMapping item)
        {
            var roleMapping= await Get(item.UserId);
            if (roleMapping!= null)
            {
                _context.Update(item);
                await _context.SaveChangesAsync();
                return roleMapping;
            }
            throw new UnableToAssignRoleExecption();
        }
    }
}
