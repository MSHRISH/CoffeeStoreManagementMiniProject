namespace CoffeeStoreAPI.Models.DTOs
{
    public class UserDetails:UserBase
    {
        public int UserId { get; set; }
        public string? Role { get; set; }

        public string? Status { get; set; }
    }
}
