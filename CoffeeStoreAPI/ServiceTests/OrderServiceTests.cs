using CoffeeStoreAPI.Context;
using CoffeeStoreAPI.Iterfaces;
using CoffeeStoreAPI.Models;
using CoffeeStoreAPI.Repositories;
using CoffeeStoreAPI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceTests
{
    public class OrderServiceTests
    {
        private CoffeeStoreContext context;

        private IRepository<int, Item> itemRepository;
        private IRepository<int, ItemType> itemTypeRepository;
        private IItemServices itemServices;

        [SetUp]
        public void SetUp()
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder().UseInMemoryDatabase("dummyDB");
            context = new CoffeeStoreContext(optionsBuilder.Options);

            itemRepository = new ItemRepository(context);
            itemTypeRepository = new ItemTypeRepository(context);
            itemServices = new ItemServices(itemRepository, itemTypeRepository);
        }

    }
}
