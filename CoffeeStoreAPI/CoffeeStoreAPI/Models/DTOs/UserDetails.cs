namespace CoffeeStoreAPI.Models.DTOs
{
    public class UserDetails
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int UserId { get; set; }
        public string? Role { get; set; }

        public string? Status { get; set; }
    }
}
