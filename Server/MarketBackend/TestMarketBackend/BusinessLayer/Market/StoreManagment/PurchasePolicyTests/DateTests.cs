using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Moq;
using MarketBackend.BusinessLayer.Market.StoreManagment.PurchasesPolicy.RestrictionPolicies;
using MarketBackend.BusinessLayer.Buyers;

namespace TestMarketBackend.BusinessLayer.Market.StoreManagment.PurchasePolicyTests
{
    internal class DateTests
    {
        [Test]
        public void TestDateRestriction()
        {
            int year = DateTime.Now.Year;
            int month = DateTime.Now.Month;
            int day = DateTime.Now.Day;

            DateRestriction res = new DateRestriction(year, month, day);

            Assert.IsTrue(!res.IsSatisfied(It.IsAny<ShoppingBag>()));

            res.year = -1;
            Assert.IsTrue(!res.IsSatisfied(It.IsAny<ShoppingBag>()));

            res.month = -1;
            Assert.IsTrue(!res.IsSatisfied(It.IsAny<ShoppingBag>()));

            res.day = -1;
            Assert.IsTrue(!res.IsSatisfied(It.IsAny<ShoppingBag>()));

            res.year = 2020;
            Assert.IsTrue(res.IsSatisfied(It.IsAny<ShoppingBag>()));

            res.year = -1;
            res.month = (month + 1) % 12;
            Assert.IsTrue(res.IsSatisfied(It.IsAny<ShoppingBag>()));

            res.month = -1;
            res.day = (day + 1) % 31;
            Assert.IsTrue(res.IsSatisfied(It.IsAny<ShoppingBag>()));
        }
    }
}
