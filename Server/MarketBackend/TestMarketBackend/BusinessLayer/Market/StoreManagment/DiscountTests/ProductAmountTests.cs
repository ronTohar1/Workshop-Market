using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Moq;
using MarketBackend.BusinessLayer.Buyers;
using System.Collections.Concurrent;
using MarketBackend.BusinessLayer.Market.StoreManagment.Discounts.DiscountExpressions.ConditionalDiscounts;
using MarketBackend.BusinessLayer.Market.StoreManagment;

namespace TestMarketBackend.BusinessLayer.Market.StoreManagment.DiscountTests
{
    internal class ProductAmountTests
    {
        int pid1;
        int pid2;

        int sid1;

        int amount;

        Mock<ProductInBag> mockPib;
        Mock<ShoppingBag> mockSb;

        [SetUp]
        public void SetUp()
        {
            DataManagersMock.InitMockDataManagers(); 

            pid1 = 1;
            pid2 = 2;

            sid1 = 1;

            amount = 2;

            mockPib = new Mock<ProductInBag>(pid1, sid1) { CallBase = true };

            ConcurrentDictionary<ProductInBag, int> dic = new ConcurrentDictionary<ProductInBag, int>();
            dic.TryAdd(mockPib.Object, amount);
            mockSb = new Mock<ShoppingBag>(sid1, dic) { CallBase = true};
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void testProductAmount(int amount)
        {
            ProductAmountPredicate pred1 = new ProductAmountPredicate(pid1, amount);
            ProductAmountPredicate pred2 = new ProductAmountPredicate(pid2, amount);

            Assert.IsTrue(pred1.EvaluatePredicate(mockSb.Object, It.IsAny<Store>()) == (amount <= this.amount));
            Assert.IsTrue(!pred2.EvaluatePredicate(mockSb.Object, It.IsAny<Store>()));
        }
    }
}
