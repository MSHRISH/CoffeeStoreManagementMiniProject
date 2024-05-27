namespace CoffeeStoreAPI.Models
{
    public class Authentication
    {
        public int Id { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordHashKey { get; set; }

        public User User { get; set; }
    }
}
