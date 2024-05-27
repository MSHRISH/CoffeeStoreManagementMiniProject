using CoffeeStoreAPI.Models.DTOs;

namespace CoffeeStoreAPI.Iterfaces
{
    public interface IUserService
    {
        public Task<UserDetails> RegisterUser(RegisterUserDTO registerUserDTO, int RoleId);
        public Task<TokenDTO> LoginUser(LoginDTO loginDTO);

        public Task<UserDetails> GetUserById(int id,int RoleId);
        public Task<UserDetails> GetManagerById(int id);
        public Task<UserDetails> GetBaristaById(int id);
        public Task<UserDetails> GetCustomerById(int id);

        public Task<List<UserDetails>> GetAllUsersByRole(int RoleId);
        public Task<List<UserDetails>> GetAllManagers();
        public Task<List<UserDetails>> GetAllBaristas();
        public Task<List<UserDetails>> GetAllCustomers();

    }
}
