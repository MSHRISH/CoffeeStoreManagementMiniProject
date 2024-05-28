﻿// <auto-generated />
using System;
using CoffeeStoreAPI.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CoffeeStoreAPI.Migrations
{
    [DbContext(typeof(CoffeeStoreContext))]
    [Migration("20240528091505_EditOrderItem")]
    partial class EditOrderItem
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.30")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("CoffeeStoreAPI.Models.Authentication", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordHashKey")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.HasKey("Id");

                    b.ToTable("Authentications");
                });

            modelBuilder.Entity("CoffeeStoreAPI.Models.Item", b =>
                {
                    b.Property<int>("ItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ItemId"), 1L, 1);

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("bit");

                    b.Property<string>("ItemDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ItemName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ItemTypeId")
                        .HasColumnType("int");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.HasKey("ItemId");

                    b.HasIndex("ItemTypeId");

                    b.ToTable("Items");

                    b.HasData(
                        new
                        {
                            ItemId = 1,
                            IsAvailable = true,
                            ItemDescription = "blahblah",
                            ItemName = "Starter 1",
                            ItemTypeId = 1,
                            Price = 200.0
                        },
                        new
                        {
                            ItemId = 2,
                            IsAvailable = true,
                            ItemDescription = "blahblahblahblah",
                            ItemName = "Starter 2",
                            ItemTypeId = 1,
                            Price = 250.0
                        },
                        new
                        {
                            ItemId = 3,
                            IsAvailable = true,
                            ItemDescription = "blahblahMain",
                            ItemName = "Main 1",
                            ItemTypeId = 2,
                            Price = 300.0
                        },
                        new
                        {
                            ItemId = 4,
                            IsAvailable = true,
                            ItemDescription = "blahblahMain",
                            ItemName = "Main 2",
                            ItemTypeId = 2,
                            Price = 300.0
                        },
                        new
                        {
                            ItemId = 5,
                            IsAvailable = true,
                            ItemDescription = "blahblahBeverage",
                            ItemName = "Beverage 1",
                            ItemTypeId = 3,
                            Price = 180.0
                        },
                        new
                        {
                            ItemId = 6,
                            IsAvailable = true,
                            ItemDescription = "blahblahBeverage",
                            ItemName = "Beverage 2",
                            ItemTypeId = 3,
                            Price = 190.0
                        },
                        new
                        {
                            ItemId = 7,
                            IsAvailable = true,
                            ItemDescription = "blahblahDesert",
                            ItemName = "Desert 1",
                            ItemTypeId = 4,
                            Price = 250.0
                        },
                        new
                        {
                            ItemId = 8,
                            IsAvailable = true,
                            ItemDescription = "blahblahDesert",
                            ItemName = "Desert 2",
                            ItemTypeId = 4,
                            Price = 230.0
                        });
                });

            modelBuilder.Entity("CoffeeStoreAPI.Models.ItemType", b =>
                {
                    b.Property<int>("TypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TypeId"), 1L, 1);

                    b.Property<string>("TypeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TypeId");

                    b.ToTable("ItemTypes");

                    b.HasData(
                        new
                        {
                            TypeId = 1,
                            TypeName = "Starters"
                        },
                        new
                        {
                            TypeId = 2,
                            TypeName = "Mains"
                        },
                        new
                        {
                            TypeId = 3,
                            TypeName = "Beverages"
                        },
                        new
                        {
                            TypeId = 4,
                            TypeName = "Deserts"
                        });
                });

            modelBuilder.Entity("CoffeeStoreAPI.Models.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderId"), 1L, 1);

                    b.Property<string>("OrderStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("OrderedOn")
                        .HasColumnType("datetime2");

                    b.Property<double>("TotalAmount")
                        .HasColumnType("float");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("OrderId");

                    b.HasIndex("UserId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("CoffeeStoreAPI.Models.OrderItem", b =>
                {
                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("ItemId")
                        .HasColumnType("int");

                    b.Property<string>("CancellationStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ItemStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("OrderId", "ItemId");

                    b.HasIndex("ItemId");

                    b.ToTable("OrderItems");
                });

            modelBuilder.Entity("CoffeeStoreAPI.Models.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoleId"), 1L, 1);

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RoleId");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            RoleId = 1,
                            RoleName = "Admin"
                        },
                        new
                        {
                            RoleId = 2,
                            RoleName = "Manager"
                        },
                        new
                        {
                            RoleId = 3,
                            RoleName = "Barista"
                        },
                        new
                        {
                            RoleId = 4,
                            RoleName = "Customer"
                        });
                });

            modelBuilder.Entity("CoffeeStoreAPI.Models.RoleMapping", b =>
                {
                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("RoleId", "UserId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("RoleMappings");
                });

            modelBuilder.Entity("CoffeeStoreAPI.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CoffeeStoreAPI.Models.Authentication", b =>
                {
                    b.HasOne("CoffeeStoreAPI.Models.User", "User")
                        .WithOne("Authentication")
                        .HasForeignKey("CoffeeStoreAPI.Models.Authentication", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("CoffeeStoreAPI.Models.Item", b =>
                {
                    b.HasOne("CoffeeStoreAPI.Models.ItemType", "ItemType")
                        .WithMany("Items")
                        .HasForeignKey("ItemTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ItemType");
                });

            modelBuilder.Entity("CoffeeStoreAPI.Models.Order", b =>
                {
                    b.HasOne("CoffeeStoreAPI.Models.User", "User")
                        .WithMany("Orders")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("CoffeeStoreAPI.Models.OrderItem", b =>
                {
                    b.HasOne("CoffeeStoreAPI.Models.Item", "Item")
                        .WithMany("OrderItems")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CoffeeStoreAPI.Models.Order", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Item");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("CoffeeStoreAPI.Models.RoleMapping", b =>
                {
                    b.HasOne("CoffeeStoreAPI.Models.Role", "Role")
                        .WithMany("RoleMappings")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CoffeeStoreAPI.Models.User", "User")
                        .WithOne("RoleMapping")
                        .HasForeignKey("CoffeeStoreAPI.Models.RoleMapping", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CoffeeStoreAPI.Models.Item", b =>
                {
                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("CoffeeStoreAPI.Models.ItemType", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("CoffeeStoreAPI.Models.Order", b =>
                {
                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("CoffeeStoreAPI.Models.Role", b =>
                {
                    b.Navigation("RoleMappings");
                });

            modelBuilder.Entity("CoffeeStoreAPI.Models.User", b =>
                {
                    b.Navigation("Authentication")
                        .IsRequired();

                    b.Navigation("Orders");

                    b.Navigation("RoleMapping")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
