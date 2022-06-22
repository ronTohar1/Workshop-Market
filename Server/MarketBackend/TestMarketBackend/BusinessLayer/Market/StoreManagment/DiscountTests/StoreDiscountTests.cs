using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Moq;
using MarketBackend.BusinessLayer.Market.StoreManagment.Discounts.DiscountExpressions.BasicDiscounts;
using MarketBackend.BusinessLayer.Market.StoreManagment;
using MarketBackend.BusinessLayer.Buyers;

namespace TestMarketBackend.BusinessLayer.Market.StoreManagment.DiscountTests
{
    internal class StoreDiscountTests
    {
        int discount1;
        int discount2;
        [SetUp]
        public void StoreDiscountSetUp()
        {
            DataManagersMock.InitMockDataManagers(); 

            discount1 = 20;
            discount2 = 50;
        }

        [Test]
        [TestCase(100)]
        [TestCase(200)]
        public void testStoreDiscount(int sum)
        {
            Mock<StoreDiscount> dis1 = new Mock<StoreDiscount>(discount1) { CallBase = true };
            Mock<StoreDiscount> dis2 = new Mock<StoreDiscount>(discount2) { CallBase = true };

            dis1.Setup(x => x.sumOfCart(It.IsAny<ShoppingBag>(), It.IsAny<Store>())).Returns(sum);
            dis2.Setup(x => x.sumOfCart(It.IsAny<ShoppingBag>(), It.IsAny<Store>())).Returns(sum);

            Assert.IsTrue(dis1.Object.EvaluateDiscount(It.IsAny<ShoppingBag>(), It.IsAny<Store>()) == (sum * discount1) / 100);
            Assert.IsTrue(dis2.Object.EvaluateDiscount(It.IsAny<ShoppingBag>(), It.IsAny<Store>()) == (sum * discount2) / 100);
        }
    }
}
