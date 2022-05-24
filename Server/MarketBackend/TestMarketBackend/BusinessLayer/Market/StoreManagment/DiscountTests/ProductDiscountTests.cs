using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Moq;
using MarketBackend.BusinessLayer.Market.StoreManagment;
using MarketBackend.BusinessLayer.Buyers.Members;
using MarketBackend.BusinessLayer.Buyers;
using MarketBackend.BusinessLayer.Market.StoreManagment.Discounts.DiscountExpressions.ConditionalDiscounts;
using MarketBackend.BusinessLayer.Market.StoreManagment.Discounts.DiscountExpressions;

namespace TestMarketBackend.BusinessLayer.Market.StoreManagment.DiscountTests
{
    internal class ProductDiscountTests
    {
        int storeId;

        int pid1;
        int pid2;

        int price1;
        int price2;

        int amount1;
        int amount2;

        Mock<Store> store;
        Mock<ShoppingBag> sbag;

        int actualWorth;

        [SetUp]
        public void SetUp()
        {
            storeId = 1;
            pid1 = 1;
            pid2 = 2;

            price1 = 20;
            price2 = 40;

            amount1 = 3;
            amount2 = 2;

            actualWorth = 20 * 3 + 40 * 2;


            //string product_name, double pricePerUnit, string category
            Mock<Product> p1 = new Mock<Product>(It.IsAny<string>(), It.IsAny<double>(), It.IsAny<string>());
            Mock<Product> p2 = new Mock<Product>(It.IsAny<string>(), It.IsAny<double>(), It.IsAny<string>());

            p1.Setup(x => x.GetPrice()).Returns(price1);
            p2.Setup(x => x.GetPrice()).Returns(price2);

            IDictionary<int, Product> prods = new Dictionary<int, Product>() { { pid1, p1.Object }, { pid2, p2.Object } };

            ProductInBag pib1 = new ProductInBag(pid1, storeId);
            ProductInBag pib2 = new ProductInBag(pid2, storeId);

            IDictionary<ProductInBag, int> prodsAmount = new Dictionary<ProductInBag, int>() { { pib1, amount1 }, { pib2, amount2 } };


            //string storeName, Member founder, Func<int, Member> membersGetter
            store = new Mock<Store>();
            store.Setup(x => x.products).Returns(prods);


            //int storeId,IDictionary<ProductInBag, int> productsAmounts
            sbag = new Mock<ShoppingBag>(storeId, prodsAmount);
            sbag.Setup(x => x.productsAmounts).Returns(prodsAmount);


        }

        [Test]
        [TestCase(1, 10, ((20 * 3) * 10) / 100)]
        [TestCase(2, 20, ((40 * 2) * 20) / 100)]
        [TestCase(0, 0 ,0)]
        public void testProductDiscount(int pid, int discount, int result)
        {
            OneProductDiscount pred = new OneProductDiscount(pid, discount);
            Assert.AreEqual(pred.EvaluateDiscount(sbag.Object, store.Object), result);
        }
    }
}
