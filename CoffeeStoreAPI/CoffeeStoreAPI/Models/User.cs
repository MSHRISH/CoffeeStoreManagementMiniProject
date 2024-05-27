
namespace CoffeeStoreAPI.Models
{
    public class UserBase
    {

        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }   
    }
    public class User:UserBase
    {
        public int Id { get; set; }
        public string Status { get; set; } //Active, Disabled
        public RoleMapping RoleMapping { get; set; }
        public Authentication Authentication { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
