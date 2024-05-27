using CoffeeStoreAPI.Models.DTOs;

namespace CoffeeStoreAPI.Iterfaces
{
    public interface IUserService
    {
        public Task<RegisterUserDTO> RegisterUser(RegisterUserDTO registerUserDTO);
    }
}
