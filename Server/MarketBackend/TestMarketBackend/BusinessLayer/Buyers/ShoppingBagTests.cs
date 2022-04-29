using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Moq;
using MarketBackend.BusinessLayer.Market.StoreManagment;
using MarketBackend.BusinessLayer.Buyers;

namespace TestMarketBackend.BusinessLayer.Market.StoreManagment
{
    public class ShoppingBagTests
    {
        private ShoppingBag bag;
        private static ProductInBag pInBag_1;
        private static ProductInBag pInBag_2;
        private static ProductInBag pNotInBag_3;

        // ----------- Setup helping functions -----------------------------

        

        [SetUp]
        public void Setup()
        {
            pInBag_1 = new ProductInBag(1, 1);
            pInBag_2 = new ProductInBag(2,2);
            pNotInBag_3 = new ProductInBag(3,3);

            var products = new Dictionary<ProductInBag, int>();
            products.Add(pInBag_1, 1);
            products.Add(pInBag_2, 2);
            bag = new ShoppingBag(products);
        }


        public static IEnumerable<TestCaseData> ProductsInBagTestCases
        {
            get
            {
                yield return new TestCaseData(pInBag_1, 5, 6);
                yield return new TestCaseData(pInBag_2, 1, 3);
                yield return new TestCaseData(pInBag_2, 0, 2);
                yield return new TestCaseData(pNotInBag_3, 1, 1);
            }
        }

        [Test]
        [TestCaseSource("ProductsInBagTestCases")]
        public void TestAddProductToBag_Pass(ProductInBag product, int amount, int expectedAmount)
        {
            //Console.WriteLine((new ProductInBag(1, 1)).Equals(pInBag_1));
        }
    }
}