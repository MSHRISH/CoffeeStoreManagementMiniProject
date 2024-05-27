namespace CoffeeStoreAPI.Models
{
    public class Role
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; } //Admin, Manager, Barista, Customer

        public ICollection<RoleMapping> RoleMappings { get; set;}
    }
}
