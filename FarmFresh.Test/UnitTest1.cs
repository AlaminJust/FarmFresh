namespace FarmFresh.Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var obj = new
            {
                Name = "John",
                Age = 30,
                Address = new
                {
                    Street = "Main Street",
                    City = "New York"
                }
            };
            
            var obj1 = new
            {
                Name = "John",
                Age = 30,
                Address = new
                {
                    Street = "Main Street",
                    City = "New York"
                }
            };

            Assert.AreEqual(obj1, obj);
        }
    }
}