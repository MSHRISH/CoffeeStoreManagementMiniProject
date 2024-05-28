using CoffeeStoreAPI.Models.DTOs;

namespace CoffeeStoreAPI.Iterfaces
{
    public interface IUserService
    {
        public Task<UserDetails> RegisterUser(RegisterUserDTO registerUserDTO, int RoleId);
        public Task<TokenDTO> LoginUser(LoginDTO loginDTO);

        public Task<UserDetails> GetUserById(int id,int ? RoleId=null);
        public Task<List<UserDetails>> GetAllUsersByRole(int RoleId);

        public Task<UserDetails> ChangeUserState(int id);


       

    }
}
