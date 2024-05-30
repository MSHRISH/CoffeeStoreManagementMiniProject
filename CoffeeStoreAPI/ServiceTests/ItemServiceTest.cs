using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CoffeeStoreAPI.Context;
using CoffeeStoreAPI.Iterfaces;
using CoffeeStoreAPI.Models;
using CoffeeStoreAPI.Models.DTOs;
using CoffeeStoreAPI.Repositories;
using CoffeeStoreAPI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;

namespace ServiceTests
{
    public class ItemServiceTests
    {
        private CoffeeStoreContext context;
        private IRepository<int,Item> itemRepository;
        private IRepository<int,ItemType> itemTypeRepository;
        private IItemServices itemServices;

        [SetUp]
        public void SetUp() 
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder().UseInMemoryDatabase("dummyDB");
            context = new CoffeeStoreContext(optionsBuilder.Options);

            itemRepository=new ItemRepository(context);
            itemTypeRepository=new ItemTypeRepository(context);

            itemServices=new ItemServices(itemRepository, itemTypeRepository);
        }

        [Test]
        public async Task AddItemTypeTest()
        {
            //Arrange
            AddItemTypeDTO addItemTypeDTO = new AddItemTypeDTO { TypeName="TestItemType2"};

            //AddItemDTO addItemDTO = new AddItemDTO { ItemName="item1", ItemType= "Deserts", ItemDescription="asasd", IsAvailable=true, Price=100};

            //Action
            var itemtype=await itemServices.AddItemType(addItemTypeDTO);
            //ItemDetailsDTO res =await itemServices.AddItem(addItemDTO);

            //Assert
            Assert.That(itemtype, Is.Not.Null);
        }

        [Test]
        public async Task AddItemTest()
        {
            //Arrange
            AddItemTypeDTO addItemTypeDTO = new AddItemTypeDTO { TypeName = "TestItemType" };

            AddItemDTO addItemDTO = new AddItemDTO { ItemName="item1", ItemType= "TestItemType", ItemDescription="asasd", IsAvailable=true, Price=100};

            //Action
            var itemtype = await itemServices.AddItemType(addItemTypeDTO);
            ItemDetailsDTO res =await itemServices.AddItem(addItemDTO);

            //Assert
            Assert.That(res, Is.Not.Null);
        }
        [Test]
        public async Task GetAllItems()
        {
            //Arrange
            AddItemDTO addItemDTO = new AddItemDTO { ItemName = "item1", ItemType = "TestItemType", ItemDescription = "asasd", IsAvailable = true, Price = 100 };

            //Action
            var items=await itemServices.GetAllItems();

            //Assert
            Assert.That(items.Count(), Is.EqualTo(2));
        }

        [Test]
        public async Task ChangeAvailabilityStatusTest()
        {
            //Arrange
            AddItemDTO addItemDTO = new AddItemDTO { ItemName = "item1", ItemType = "TestItemType", ItemDescription = "asasd", IsAvailable = true, Price = 100 };

            //Action
            ItemDetailsDTO item= await itemServices.AddItem(addItemDTO);
            var res = await itemServices.ChangeAvailabilityOfItem(item.ItemId);

            //Assert
            Assert.That(res.IsAvailable, Is.False);
        }

        [Test]
        public async Task GetItemByIdTest()
        {
            //Arrange
            AddItemDTO addItemDTO = new AddItemDTO { ItemName = "item1", ItemType = "TestItemType", ItemDescription = "asasd", IsAvailable = true, Price = 100 };

            //Action
            var item = await itemServices.AddItem(addItemDTO);
            var res=await itemServices.GetItemById(item.ItemId);

            //Assert
            Assert.That(res.ItemId,Is.EqualTo(item.ItemId));
        }

        [Test]
        public async Task GetItemTypesById()
        {
            //Arrange
            AddItemTypeDTO addItemTypeDTO = new AddItemTypeDTO { TypeName = "TestType" };

            
            //Action
            var itemtype = await itemServices.AddItemType(addItemTypeDTO);
            var res = await itemServices.GetItemTypeById(itemtype.Id);

            //Assert
            Assert.That(res.TypeName,Is.EqualTo(itemtype.TypeName));
        }

        [Test]
        public async Task GetItemsByRoleId()
        {
            //Arrange
            AddItemTypeDTO addItemTypeDTO = new AddItemTypeDTO { TypeName = "TestType2" };

            //Arrange
            AddItemDTO addItemDTO = new AddItemDTO { ItemName = "item1", ItemType = "TestType2", ItemDescription = "asasd", IsAvailable = true, Price = 100 };
            
            AddItemDTO addItemDTO2 = new AddItemDTO { ItemName = "item2", ItemType = "TestType2", ItemDescription = "asasd", IsAvailable = true, Price = 100 };

            //Action
            var itemtype = await itemServices.AddItemType(addItemTypeDTO);
            var item = await itemServices.AddItem(addItemDTO);
            item=await itemServices.AddItem(addItemDTO2);

            var res = await itemServices.GetItemsByTypeId(itemtype.Id);

            //Assert
            Assert.That(res.Count(),Is.EqualTo(2));
        }
    }
}
