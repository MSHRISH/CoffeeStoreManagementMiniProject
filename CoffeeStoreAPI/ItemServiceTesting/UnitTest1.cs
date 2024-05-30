using CoffeeStoreAPI.Context;
using CoffeeStoreAPI.Iterfaces;
using CoffeeStoreAPI.Models;
using CoffeeStoreAPI.Models.DTOs;
using CoffeeStoreAPI.Repositories;
using CoffeeStoreAPI.Services;
using Microsoft.EntityFrameworkCore;

namespace ItemServiceTesting
{
    public class Tests
    {
        private CoffeeStoreContext context;
        private IRepository<int,Item> itemRepo;
        private IRepository<int,ItemType> itemTypeRepo;
        private IItemServices itemServices;

        [SetUp]
        public void Setup()
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder().UseInMemoryDatabase("dummyDB");
            context = new CoffeeStoreContext(optionsBuilder.Options);

            itemRepo=new ItemRepository(context);
            itemTypeRepo=new ItemTypeRepository(context);

            itemServices = new ItemServices(itemRepo, itemTypeRepo);
        }

        [Test]
        public async Task AddItemTest()
        {
            Assert.Pass();
    }
}