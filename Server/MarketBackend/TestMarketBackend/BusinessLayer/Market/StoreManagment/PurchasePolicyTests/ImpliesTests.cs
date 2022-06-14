using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Moq;
using MarketBackend.BusinessLayer.Buyers;
using MarketBackend.BusinessLayer.Market.StoreManagment.PurchasesPolicy.PurchaseInterfaces;
using MarketBackend.BusinessLayer.Market.StoreManagment.PurchasesPolicy.LogicalOperators;

namespace TestMarketBackend.BusinessLayer.Market.StoreManagment.PurchasePolicyTests
{
    internal class ImpliesTests
    {
        [SetUp]
        public void MockDataLayer()
        {
            DataManagersMock.InitMockDataManagers();
        }

        [Test]
        [TestCase(true, true, true)]
        [TestCase(false, true, true)]
        [TestCase(true, false, false)]
        [TestCase(false, false, true)]
        public void testAndPurcahseReturn(bool first, bool second, bool expected)
        {
            Mock<IPredicateExpression> condition = new Mock<IPredicateExpression>();
            Mock<IPredicateExpression> allowing = new Mock<IPredicateExpression>();

            condition.Setup(x => x.IsSatisfied(It.IsAny<ShoppingBag>())).Returns(first);
            allowing.Setup(x => x.IsSatisfied(It.IsAny<ShoppingBag>())).Returns(second);

            ImpliesExpression and = new ImpliesExpression(condition.Object, allowing.Object);
            Assert.IsTrue(and.IsSatisfied(It.IsAny<ShoppingBag>()) == expected);
        }
    }
}
