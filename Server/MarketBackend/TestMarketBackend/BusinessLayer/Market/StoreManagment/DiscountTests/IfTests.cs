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
    internal class IfTests
    {
        Mock<IPredicateExpression> pred1;
        Mock<IPredicateExpression> pred2;

        Mock<IDiscountExpression> dis1;
        Mock<IDiscountExpression> dis2;

        int disAmount1;
        int disAmount2;

        [SetUp]
        public void SetUp()
        {
            DataManagersMock.InitMockDataManagers(); 

            disAmount1 = 10;
            disAmount2 = 20;

            pred1 = new Mock<IPredicateExpression>();
            pred1.Setup(x => x.EvaluatePredicate(It.IsAny<ShoppingBag>(), It.IsAny<Store>())).Returns(true);

            pred2 = new Mock<IPredicateExpression>();
            pred2.Setup(x => x.EvaluatePredicate(It.IsAny<ShoppingBag>(), It.IsAny<Store>())).Returns(false);

            dis1 = new Mock<IDiscountExpression>();
            dis1.Setup(x => x.EvaluateDiscount(It.IsAny<ShoppingBag>(), It.IsAny<Store>())).Returns(disAmount1);
            dis2 = new Mock<IDiscountExpression>();
            dis2.Setup(x => x.EvaluateDiscount(It.IsAny<ShoppingBag>(), It.IsAny<Store>())).Returns(disAmount2);
        }

        [Test]
        public void testSucessIf()
        {
            IfDiscount ifexp = new IfDiscount(pred1.Object, dis1.Object, dis2.Object);

            Assert.AreEqual(ifexp.EvaluateDiscount(It.IsAny<ShoppingBag>(), It.IsAny<Store>()), disAmount1);
        }

        [Test]
        public void testFailIf()
        {
            IfDiscount ifexp = new IfDiscount(pred2.Object, dis1.Object, dis2.Object);

            Assert.AreEqual(ifexp.EvaluateDiscount(It.IsAny<ShoppingBag>(), It.IsAny<Store>()), disAmount2);
        }
    }
}
