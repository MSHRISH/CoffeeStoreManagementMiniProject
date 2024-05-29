using Microsoft.EntityFrameworkCore;

namespace ServiceTestings
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder()
                                                       .UseInMemoryDatabase("dummyDB");

        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}