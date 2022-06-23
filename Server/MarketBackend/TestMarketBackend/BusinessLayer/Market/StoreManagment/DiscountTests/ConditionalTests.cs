
using NUnit.Framework;
using Moq;
using MarketBackend.BusinessLayer.Market.StoreManagment;
using MarketBackend.BusinessLayer.Buyers.Members;
using MarketBackend.BusinessLayer.Buyers;
using MarketBackend.BusinessLayer.Market.StoreManagment.Discounts.DiscountExpressions.ConditionalDiscounts;
using MarketBackend.BusinessLayer.Market.StoreManagment.Discounts;
using MarketBackend.BusinessLayer.Market.StoreManagment.Discounts.DiscountInterfaces;
using MarketBackend.BusinessLayer.Market.StoreManagment.Discounts.DiscountExpressions;

namespace TestMarketBackend.BusinessLayer.Market.StoreManagment.DiscountTests
{
    internal class ConditionalTests
    {
        Mock<IPredicateExpression> pred1;
        Mock<IPredicateExpression> pred2;

        Mock<IDiscountExpression> dis;

        int disAmount;
        
        [SetUp]
        public void SetUp()
        {
            DataManagersMock.InitMockDataManagers(); 

            disAmount = 10;

            pred1 = new Mock<IPredicateExpression>();
            pred1.Setup(x => x.EvaluatePredicate(It.IsAny<ShoppingBag>(), It.IsAny<Store>())).Returns(true);

            pred2 = new Mock<IPredicateExpression>();
            pred2.Setup(x => x.EvaluatePredicate(It.IsAny<ShoppingBag>(), It.IsAny<Store>())).Returns(false);

            dis = new Mock<IDiscountExpression>();
            dis.Setup(x => x.EvaluateDiscount(It.IsAny<ShoppingBag>(), It.IsAny<Store>())).Returns(disAmount);
        }

        [Test]
        public void testSucessCond()
        {
            ConditionDiscount cond1 = new ConditionDiscount(pred1.Object, dis.Object);

            Assert.AreEqual(cond1.EvaluateDiscount(It.IsAny<ShoppingBag>(), It.IsAny<Store>()), disAmount);
        }

        [Test]
        public void testFailCond()
        {
            ConditionDiscount cond2 = new ConditionDiscount(pred2.Object, dis.Object);

            Assert.AreEqual(cond2.EvaluateDiscount(It.IsAny<ShoppingBag>(), It.IsAny<Store>()), 0);
        }
    }
}
