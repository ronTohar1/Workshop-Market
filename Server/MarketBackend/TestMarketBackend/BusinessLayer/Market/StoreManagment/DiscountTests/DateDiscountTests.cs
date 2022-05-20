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
    internal class DateDiscountTests
    {
        Mock<DateDiscount> d1;
        Mock<DateDiscount> d2;
        Mock<DateDiscount> d3;
        double storeSum;
        int discount1;
        int discount2;
        int discount3;

        [SetUp]
        public void setDates()
        {
            DateTime date = DateTime.Now;

            discount1 = 20;
            discount2 = 50;
            discount3 = 70;
            d1 = new Mock<DateDiscount>(discount1, 2001, 3, 29) { CallBase = true };
            d2 = new Mock<DateDiscount>(discount2, date.Year, date.Month, date.Day) { CallBase = true };
            d3 = new Mock<DateDiscount>(discount3, -1, date.Month, date.Day) { CallBase = true };

            storeSum = 100;

            d1.Setup(x => x.GetSum(It.IsAny<ShoppingBag>(), It.IsAny<Store>())).Returns(storeSum);
            d2.Setup(x => x.GetSum(It.IsAny<ShoppingBag>(), It.IsAny<Store>())).Returns(storeSum);
            d3.Setup(x => x.GetSum(It.IsAny<ShoppingBag>(), It.IsAny<Store>())).Returns(storeSum);
        }

        [Test]
        public void testDate()
        {
            //21 years ago so no discount
            Assert.IsTrue(d1.Object.EvaluateDiscount(It.IsAny<ShoppingBag>(), It.IsAny<Store>()) == 0);
            //today so discount
            Assert.IsTrue(d2.Object.EvaluateDiscount(It.IsAny<ShoppingBag>(), It.IsAny<Store>()) == ((storeSum * discount2) / 100));
            //annually today so discount
            Assert.IsTrue(d3.Object.EvaluateDiscount(It.IsAny<ShoppingBag>(), It.IsAny<Store>()) == ((storeSum * discount3) / 100));
        }
    }
}
