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
    public class Tests
    {
        private CoffeeStoreContext context;
        private IRepository<int,User> userRepo;
        private IRepository<int,Role> roleRepo;
        private  IRepository<int,RoleMapping> roleMappingRepo;
        private IRepository<int,Authentication> authenticationRepo;

        private ITokenServices tokenServices;
        private IUserService userService;

        [SetUp]
        public void Setup()
        {
            DbContextOptionsBuilder optionsBuilder= new DbContextOptionsBuilder().UseInMemoryDatabase("dummyDB");
            context = new CoffeeStoreContext(optionsBuilder.Options);
            userRepo = new UserRepository(context);
            roleRepo=new RoleRepository(context);
            roleMappingRepo=new RoleMappingsRepository(context);
            authenticationRepo=new AuthenticationRepository(context);

            Mock<IConfigurationSection> configurationJWTSection = new Mock<IConfigurationSection>();
            configurationJWTSection.Setup(x => x.Value).Returns("This is the dummy key which has to be a bit long for the 512. which should be even more longer for the passing");
            Mock<IConfigurationSection> congigTokenSection = new Mock<IConfigurationSection>();
            congigTokenSection.Setup(x => x.GetSection("JWT")).Returns(configurationJWTSection.Object);
            Mock<IConfiguration> mockConfig = new Mock<IConfiguration>();
            mockConfig.Setup(x => x.GetSection("TokenKey")).Returns(congigTokenSection.Object);

            tokenServices = new TokenServices(mockConfig.Object);
            userService = new UserServices(userRepo,roleRepo,authenticationRepo,roleMappingRepo,tokenServices);
        }

        [Test]
        public async Task AddUserTest()
        {
            //Arrange
            RegisterUserDTO userDTO = new RegisterUserDTO { Name = "testUserCustomer1", Email = "string", Phone = "1231231233", Password = "string" };
            
            //Action
            //Customer
            var res=await userService.RegisterUser(userDTO,4);
            
            //Assert
            Assert.That(res, Is.Not.Null);
        }

        [Test]
        public async Task LoginTest()
        {
            //Arrange
            RegisterUserDTO userDTO = new RegisterUserDTO { Name = "testUserCustomer1", Email = "string", Phone = "1231231233", Password = "string" };

            //Action
            //Customer
            var res = await userService.RegisterUser(userDTO, 4);
            var auth = userService.LoginUser(new LoginDTO { UserId = res.UserId, Password = "string" });

            //Assert
            Assert.That(auth, Is.Not.Null);
        }

        [Test]
        public async Task GetUserById()
        {
            //Arrange
            RegisterUserDTO userDTO = new RegisterUserDTO { Name = "testUserCustomer1", Email = "string", Phone = "1231231233", Password = "string" };
            RegisterUserDTO userDTO2 = new RegisterUserDTO { Name = "testUserCustomer2", Email = "string", Phone = "1231231233", Password = "string" };

            //Action
            //Customer
            var res = await userService.RegisterUser(userDTO, 4);
            //Manager
            res = await userService.RegisterUser(userDTO2, 2);

            var result=await userService.GetUserById(res.UserId);

            //Assert
            Assert.That(userDTO2.Name, Is.EqualTo(result.Name));
        }
        [Test]
        public async Task GetAllUsers()
        {
            //Arrange
            RegisterUserDTO userDTO = new RegisterUserDTO { Name = "testUserCustomer1", Email = "string", Phone = "1231231233", Password = "string" };
            RegisterUserDTO userDTO2 = new RegisterUserDTO { Name = "testUserCustomer2", Email = "string", Phone = "1231231233", Password = "string" };

            //Action
            //Customer
            var res = await userService.RegisterUser(userDTO, 4);
            //Manager
            res = await userService.RegisterUser(userDTO2, 2);

            var result = await userService.GetAllUsersByRole(2);

            Assert.That(result.Count() , Is.EqualTo(1));
        }
        [Test]
        public async Task ChangeUserStatus()
        {
            //Arrange
            RegisterUserDTO userDTO = new RegisterUserDTO { Name = "testUserCustomer1", Email = "string", Phone = "1231231233", Password = "string" };

            //Action
            //Customer
            var res = await userService.RegisterUser(userDTO, 4);

            var result=await userService.ChangeUserState(res.UserId);

            //Assert
            Assert.That(result.Status, Is.EqualTo("Disabled"));
        }
    }
}