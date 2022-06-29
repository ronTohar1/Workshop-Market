using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Moq;
using MarketBackend.BusinessLayer.Buyers;
using MarketBackend.BusinessLayer.Market.StoreManagment.PurchasesPolicy.RestrictionPolicies;

namespace TestMarketBackend.BusinessLayer.Market.StoreManagment.PurchasePolicyTests
{
    internal class AfterHourProductTests
    {
        int storeId;

        int pid1;
        int pid2;

        int amount1;
        int amount2;

        Mock<ShoppingBag> sbag;

        [SetUp]
        public void SetUp()
        {
            DataManagersMock.InitMockDataManagers(); 

            storeId = 1;
            pid1 = 1;
            pid2 = 2;

            amount1 = 10;
            amount2 = 5;

            ProductInBag pib1 = new ProductInBag(pid1, storeId);
            ProductInBag pib2 = new ProductInBag(pid2, storeId);

            IDictionary<ProductInBag, int> prodsAmount = new Dictionary<ProductInBag, int>() { { pib1, amount1 }, { pib2, amount2 } };


            //int storeId,IDictionary<ProductInBag, int> productsAmounts
            sbag = new Mock<ShoppingBag>(storeId, prodsAmount);
            sbag.Setup(x => x.productsAmounts).Returns(prodsAmount);

        }

        [Test]
        public void TestAfterHourProduct()
        {
            int hour = DateTime.Now.Hour;
            AfterHourProductRestriction res = new AfterHourProductRestriction(hour, pid1, amount1);

            Assert.IsTrue(!res.IsSatisfied(sbag.Object));

            res.hour += 1;

            Assert.IsTrue(res.IsSatisfied(sbag.Object));

            res.hour -= 2;

            Assert.IsTrue(!res.IsSatisfied(sbag.Object));

            res.amount += 1;

            Assert.IsTrue(res.IsSatisfied(sbag.Object));

            res.amount -= 2;

            Assert.IsTrue(!res.IsSatisfied(sbag.Object));
        }
    }
}
