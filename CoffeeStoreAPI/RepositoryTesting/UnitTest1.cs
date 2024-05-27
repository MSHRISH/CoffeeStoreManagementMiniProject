using CoffeeStoreAPI.Context;
using CoffeeStoreAPI.Iterfaces;
using CoffeeStoreAPI.Models;

namespace RepositoryTesting
{
    public class Tests
    {
        private IRepository<int, RoleMapping> _roleMappingRepository;

        [SetUp]
        public void Setup()
        {
            _roleMappingRepository= new IRepository<int, RoleMapping>();
        }

        [Test]
        public void Test1()
        {
            var rolem=_roleMappingRepository.Get(1);

            Assert.IsNotNull(rolem);
        }
    }
}