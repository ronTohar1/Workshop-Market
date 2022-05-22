﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Moq;
using MarketBackend.BusinessLayer.Market.StoreManagment.Discounts.DiscountInterfaces;
using MarketBackend.BusinessLayer.Buyers;
using MarketBackend.BusinessLayer.Market.StoreManagment;
using MarketBackend.BusinessLayer.Market.StoreManagment.Discounts.DiscountExpressions.LogicalOperators;

namespace TestMarketBackend.BusinessLayer.Market.StoreManagment.DiscountTests
{
    internal class XorTests
    {
        [Test]
        [TestCase(true, true, false)]
        [TestCase(false, true, true)]
        [TestCase(true, false, true)]
        [TestCase(false, false, false)]
        public void testReturn(bool first, bool second, bool expected)
        {
            Mock<IPredicateExpression> pred1 = new Mock<IPredicateExpression>();
            Mock<IPredicateExpression> pred2 = new Mock<IPredicateExpression>();

            pred1.Setup(x => x.EvaluatePredicate(It.IsAny<ShoppingBag>(), It.IsAny<Store>())).Returns(first);
            pred2.Setup(x => x.EvaluatePredicate(It.IsAny<ShoppingBag>(), It.IsAny<Store>())).Returns(second);

            XorExpression and = new XorExpression(pred1.Object, pred2.Object);
            Assert.IsTrue(and.EvaluatePredicate(It.IsAny<ShoppingBag>(), It.IsAny<Store>()) == expected);
        }
    }
}