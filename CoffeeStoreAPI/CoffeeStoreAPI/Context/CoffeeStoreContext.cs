using CoffeeStoreAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoffeeStoreAPI.Context
{
    public class CoffeeStoreContext:DbContext
    {
        public CoffeeStoreContext(DbContextOptions options):base(options) { }

        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Authentication> Authentications { get; set; }
        public DbSet<RoleMapping> RoleMappings { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<ItemType> ItemTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(ConfigureUser);
            modelBuilder.Entity<Authentication>(ConfigureAuthentication);

            modelBuilder.Entity<Role>(ConfigureRole);
            modelBuilder.Entity<RoleMapping>(ConfigureRoleMapping);

            modelBuilder.Entity<Item>(ConfigureItem);
            modelBuilder.Entity<ItemType>(ConfigureItemType);

            modelBuilder.Entity<Order>(ConfigureOrder);
            modelBuilder.Entity<OrderItem>(ConfigureOrderItem);

            BuildItemTypes(modelBuilder);
            BuildItems(modelBuilder);
            BuildRoles(modelBuilder);
        }

        private void ConfigureUser(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(u => u.Authentication)
                .WithOne(a => a.User)
                .HasForeignKey<Authentication>(a => a.Id);
            
            builder.HasOne(u => u.RoleMapping)
                .WithOne(rm => rm.User)
                .HasForeignKey<RoleMapping>(rm => rm.UserId);
        }

        private void ConfigureAuthentication(EntityTypeBuilder<Authentication> builder)
        {
            builder.HasKey(a => a.Id);
        }
        private void ConfigureRole(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(r => r.RoleId);
        }
        private void ConfigureRoleMapping(EntityTypeBuilder<RoleMapping> builder)
        {
            builder.HasKey(rm => new { rm.RoleId, rm.UserId });
            
            builder.HasIndex(rm => rm.UserId).IsUnique();
            
            builder.HasOne(rm => rm.Role)
                .WithMany(r => r.RoleMappings)
                .HasForeignKey(rm => rm.RoleId);

            builder.HasOne(rm => rm.User)
                .WithOne(u => u.RoleMapping)
                .HasForeignKey<RoleMapping>(rm => rm.UserId);
        }

        private void ConfigureItem(EntityTypeBuilder<Item> builder) 
        {
            builder.HasKey(i => i.ItemId);
          
            builder.HasOne(i => i.ItemType)
                .WithMany(it=>it.Items)
                .HasForeignKey(i => i.ItemTypeId);
        }

        private void ConfigureItemType(EntityTypeBuilder<ItemType> builder)
        {
            builder.HasKey(it=>it.TypeId);
        }

        private void ConfigureOrder(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.OrderId);
            
            builder.HasOne(o=>o.User)
                .WithMany(u=>u.Orders)
                .HasForeignKey(o => o.UserId);
        }
        private void ConfigureOrderItem(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(oi=>new {oi.OrderId,oi.ItemId});
            
            builder.HasOne(oi => oi.Item)
                .WithMany(i => i.OrderItems)
                .HasForeignKey(oi => oi.ItemId);

            builder.HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId);
        }
        private void BuildItemTypes(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ItemType>().HasData(
                 new ItemType { TypeId = 1, TypeName = "Starters" },
                 new ItemType { TypeId=2, TypeName="Mains"},
                 new ItemType { TypeId=3, TypeName="Beverages"},
                 new ItemType { TypeId=4, TypeName="Deserts"}
                );
        }
        private void BuildItems(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>().HasData(
                new Item { ItemId=1, ItemName="Starter 1", ItemDescription="blahblah", IsAvailable=true, ItemTypeId=1, Price=200},
                new Item { ItemId=2, ItemName="Starter 2", ItemDescription= "blahblahblahblah", IsAvailable=true, ItemTypeId=1, Price=250},
                new Item { ItemId=3, ItemName="Main 1", ItemDescription= "blahblahMain", IsAvailable=true, ItemTypeId=2, Price=300 },
                new Item { ItemId=4, ItemName="Main 2", ItemDescription= "blahblahMain", IsAvailable=true, ItemTypeId=2, Price=300 },
                new Item { ItemId=5, ItemName="Beverage 1", ItemDescription= "blahblahBeverage", IsAvailable=true, ItemTypeId=3, Price=180 },
                new Item { ItemId=6, ItemName="Beverage 2", ItemDescription="blahblahBeverage", IsAvailable=true, ItemTypeId=3, Price=190},
                new Item { ItemId=7, ItemName="Desert 1", ItemDescription= "blahblahDesert", IsAvailable=true, ItemTypeId=4,Price=250 },
                new Item { ItemId=8,ItemName="Desert 2",ItemDescription= "blahblahDesert", IsAvailable=true,ItemTypeId=4, Price=230}
                );
        }
        private void BuildRoles(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
                new Role { RoleId=1, RoleName="Admin"},
                new Role { RoleId=2, RoleName="Manager"},
                new Role { RoleId=3, RoleName="Barista"},
                new Role { RoleId=4,RoleName="Customer"}
                );
        }
    }
}
