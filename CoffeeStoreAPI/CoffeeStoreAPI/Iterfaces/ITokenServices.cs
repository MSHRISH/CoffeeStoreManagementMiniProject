using CoffeeStoreAPI.Models;

namespace CoffeeStoreAPI.Iterfaces
{
    public interface ITokenServices
    {
        public string GenerateToken(int UserId,string Role);
    }
}
