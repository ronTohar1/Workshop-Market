using NUnit.Framework;
using MarketBackend.BusinessLayer.Buyers.Guests;

namespace TestMarketBackend
{
    public class Tests
    {
        private Example? example;

        [SetUp]
        public void Setup()
        {
            example = new Example();
        }

        [Test]
        public void TestIsEven()
        {
            Assert.IsTrue(example.IsEven(2));
            Assert.IsFalse(example.IsEven(3));
        }
    }
}