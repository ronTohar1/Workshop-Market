﻿using System;
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
    internal class AfterHourTests
    {
        [Test]
        public void TestAfterHour()
        {
            int hour = DateTime.Now.Hour;

            AfterHourRestriction res = new AfterHourRestriction(hour);

            Assert.IsTrue(res.IsSatisfied(It.IsAny<ShoppingBag>()));

            res.hour = hour - 1;
            Assert.IsTrue(!res.IsSatisfied(It.IsAny<ShoppingBag>()));

            res.hour = hour + 1;
            Assert.IsTrue(res.IsSatisfied(It.IsAny<ShoppingBag>()));
        }
    }
}