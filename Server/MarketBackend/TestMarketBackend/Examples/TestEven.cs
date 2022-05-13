using NUnit.Framework;
using Moq;
using TestMarketBackend.Examples;

namespace TestMarketBackend
{
    public class Tests
    {
        private Mock<Calculator> calcMock;
        private Calculator calc;
        private Calculator calcFake;

        [SetUp]
        public void Setup()
        {
            calc = new Calculator();
            calcMock = new Mock<Calculator>();

            // Returns 5 always, as long as first integer is between 1 and 10 (including 10).
            // IMPORTANT: 'Add' must be virtual method (everything mocked must be overridable).
            calcMock.Setup(p => p.Add(It.IsInRange<int>(1,10,Range.Inclusive), It.IsAny<int>())).Returns(5);
            //calcMock.Setup(p => p.Add(It.IsInRange<int>(1, 10, Range.Inclusive), It.IsAny<int>())).Returns((int a, int b) => a + b);

            calcFake = calcMock.Object;
        }

        [Test]
        public void TestMock()
        {
            Assert.IsTrue(calcFake.Add(4, 6) == 5);
            Assert.IsFalse(calcFake.Add(0, 76) == 5);
        }
        [Test]
        public void TestRegular()
        {
            Assert.IsTrue(calc.Add(4, 6) == 10);
            Assert.IsFalse(calc.Add(3, 6) == 10);

        }

    }
}