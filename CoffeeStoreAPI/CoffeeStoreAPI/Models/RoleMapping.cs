﻿namespace CoffeeStoreAPI.Models
{
    public class RoleMapping
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        
        public User User { get; set; }
        public Role Role { get; set; }
    }
}
