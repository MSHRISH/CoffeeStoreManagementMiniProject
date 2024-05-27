namespace CoffeeStoreAPI.Models.DTOs
{
    public class TokenDTO
    {
        public int UserId { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
    }
}
