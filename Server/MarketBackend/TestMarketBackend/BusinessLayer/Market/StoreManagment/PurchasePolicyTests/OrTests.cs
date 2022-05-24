using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Moq;
using MarketBackend.BusinessLayer.Market.StoreManagment.PurchasesPolicy.PurchaseInterfaces;
using MarketBackend.BusinessLayer.Buyers;
using MarketBackend.BusinessLayer.Market.StoreManagment.PurchasesPolicy.logicalOperators;

namespace TestMarketBackend.BusinessLayer.Market.StoreManagment.PurchasePolicyTests
{
    internal class OrTests
    {
        [Test]
        [TestCase(true, true, true)]
        [TestCase(false, true, true)]
        [TestCase(true, false, true)]
        [TestCase(false, false, false)]
        public void testOrPurchaseReturn(bool first, bool second, bool expected)
        {
            Mock<IRestrictionExpression> pred1 = new Mock<IRestrictionExpression>();
            Mock<IRestrictionExpression> pred2 = new Mock<IRestrictionExpression>();

            pred1.Setup(x => x.IsSatisfied(It.IsAny<ShoppingBag>())).Returns(first);
            pred2.Setup(x => x.IsSatisfied(It.IsAny<ShoppingBag>())).Returns(second);

            OrExpression and = new OrExpression(pred1.Object, pred2.Object);
            Assert.IsTrue(and.IsSatisfied(It.IsAny<ShoppingBag>()) == expected);
        }
    }
}
