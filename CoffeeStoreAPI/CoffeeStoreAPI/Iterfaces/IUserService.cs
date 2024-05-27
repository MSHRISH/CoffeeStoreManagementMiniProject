using CoffeeStoreAPI.Models.DTOs;

namespace CoffeeStoreAPI.Iterfaces
{
    public interface IUserService
    {
        public Task<UserDetails> RegisterUser(RegisterUserDTO registerUserDTO, int RoleId);
        public Task<TokenDTO> LoginUser(LoginDTO loginDTO);
    }
}
