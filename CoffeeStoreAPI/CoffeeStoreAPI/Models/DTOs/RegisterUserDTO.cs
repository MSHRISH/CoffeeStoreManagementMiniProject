namespace CoffeeStoreAPI.Models.DTOs
{
    public class RegisterUserDTO:User
    {
        public string Password { get; set; }
        public int RoleId { get; set; }
    }
}
