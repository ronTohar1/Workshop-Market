using MarketBackend.BusinessLayer.Buyers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Moq;

namespace TestMarketBackend.BusinessLayer.Buyers
{
    public class BuyersControllerTests
    {
        private BuyersController buyersController;
        private readonly Mock<Buyer> mockBuyer = new Mock<Buyer>();
        private readonly Mock<Cart> mockCart = new Mock<Cart>();

        [SetUp]
        public void SetUp()
        {
            Mock<IBuyersController> mock1 = new Mock<IBuyersController>();
            Mock<IBuyersController> mock2 = new Mock<IBuyersController>();

            mockBuyer.Setup(buyer => buyer.Cart)
                .Returns(mockCart.Object);

            mock1.Setup(cont => cont.GetBuyer(It.IsInRange<int>(1, 10, Moq.Range.Inclusive)))
                .Returns(mockBuyer.Object);

            mock2.Setup(cont => cont.GetBuyer(It.IsInRange<int>(11, 20, Moq.Range.Inclusive)))
                .Returns(mockBuyer.Object);


            buyersController = new BuyersController(
                new List<IBuyersController>(new IBuyersController[] { mock1.Object, mock2.Object }));


        }

        #region GetBuyer
        [Test]
        [TestCase(5)]
        [TestCase(15)]
        public void TestGetBuyer_Exist(int id)
        {
            Buyer? buyer = buyersController.GetBuyer(id);
            Assert.IsNotNull(buyer);
            Assert.AreEqual(mockBuyer.Object, buyer);
        }

        [Test]
        [TestCase(25)]
        public void TestGetBuyer_NotExist(int id)
        {
            Buyer? buyer = buyersController.GetBuyer(id);
            Assert.IsNull(buyer);
        }
        #endregion

        #region GetBuyer
        [Test]
        [TestCase(5)]
        [TestCase(15)]
        public void TestGetCart_Exist(int id)
        {
            Cart? cart = buyersController.GetCart(id);
            Assert.IsNotNull(cart);
            Assert.AreEqual(mockCart.Object, cart);
        }

        [Test]
        [TestCase(25)]
        public void TestGetCart_NotExist(int id)
        {
            Cart? cart = buyersController.GetCart(id);
            Assert.IsNull(cart);
        }
        #endregion

    }
}