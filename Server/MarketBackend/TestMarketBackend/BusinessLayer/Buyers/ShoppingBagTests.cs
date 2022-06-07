using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Moq;
using MarketBackend.BusinessLayer.Market.StoreManagment;
using MarketBackend.BusinessLayer.Buyers;
using MarketBackend.BusinessLayer;

namespace TestMarketBackend.BusinessLayer.Market.StoreManagment
{
    public class ShoppingBagTests
    {
        private ShoppingBag bag;
        private static ProductInBag pInBag_1 = new ProductInBag(1, 1);
        private static ProductInBag pInBag_2 = new ProductInBag(2, 1);
        private static ProductInBag pNotInBag_3 = new ProductInBag(3, 3);

        // ----------- Setup helping functions -----------------------------

        

        [SetUp]
        public void Setup()
        {
            var products = new Dictionary<ProductInBag, int>();
            products.Add(pInBag_1, 1);
            products.Add(pInBag_2, 2);
            bag = new ShoppingBag(1, products);
        }

        // -------------------- Add product --------------------

        public static IEnumerable<TestCaseData> Data_TestAddProductToBag_Pass
        {
            get
            {
                yield return new TestCaseData(pInBag_1, 5, 6);
                yield return new TestCaseData(pInBag_2, 1, 3);
            }
        }
        [Test]
        [TestCaseSource("Data_TestAddProductToBag_Pass")]
        public void TestAddProductToBag_Pass(ProductInBag product, int amount, int expectedAmount)
        {
            Assert.DoesNotThrow(() => bag.AddProductToBag(product, amount));
            Assert.True(bag.ProductsAmounts.ContainsKey(product));
            Assert.AreEqual(bag.ProductsAmounts[product], expectedAmount);  
        }


        public static IEnumerable<TestCaseData> Data_TestAddProductToBag_Fail
        {
            get
            {
                yield return new TestCaseData(pInBag_1, -1);
                yield return new TestCaseData(null, 1);
                yield return new TestCaseData(pInBag_2, 0);
            }
        }
        [Test]
        [TestCaseSource("Data_TestAddProductToBag_Fail")]
        public void TestAddProductToBag_Fail(ProductInBag product, int amount)
        {
            Assert.Catch<Exception>(() => bag.AddProductToBag(product, amount));
        }


        // -------------------- Change product amount --------------------

        public static IEnumerable<TestCaseData> Data_ChangeProductAmount_Pass
        {
            get
            {
                yield return new TestCaseData(pInBag_1, 5);
            }
        }
        [Test]
        [TestCaseSource("Data_ChangeProductAmount_Pass")]
        public void TestChangeProductAmount_Pass(ProductInBag product, int amount)
        {
            bag.ChangeProductAmount(product, amount);

            Assert.AreEqual(bag.ProductsAmounts[product], amount);
        }

        public static IEnumerable<TestCaseData> Data_ChangeProductAmount_ToZero
        {
            get
            {
                yield return new TestCaseData(pInBag_1);
            }
        }
        [Test]
        [TestCaseSource("Data_ChangeProductAmount_ToZero")]
        public void TestChangeProductAmount_ToZeros(ProductInBag product)
        {
            Assert.Throws<MarketException>(() => bag.ChangeProductAmount(product, 0));
        }


        public static IEnumerable<TestCaseData> Data_ChangeProductAmount_Fail
        {
            get
            {
                yield return new TestCaseData(null, 1);
                yield return new TestCaseData(pInBag_1, -1);
                yield return new TestCaseData(pNotInBag_3, 5);
            }
        }
        [Test]
        [TestCaseSource("Data_ChangeProductAmount_Fail")]
        public void TestChangeProductAmount_Fail(ProductInBag product, int amount)
        {
            Assert.Catch<Exception>(() => bag.ChangeProductAmount(product, amount));
        }



        // -------------------- Remove product --------------------

        public static IEnumerable<TestCaseData> Data_RemoveProduct_Pass
        {
            get
            {
                yield return new TestCaseData(pInBag_1);
            }
        }
        [Test]
        [TestCaseSource("Data_RemoveProduct_Pass")]
        public void TestChangeProductAmount_Pass(ProductInBag product)
        {
            Assert.IsTrue(bag.ProductsAmounts.ContainsKey(product));
            bag.RemoveProduct(product);
            Assert.IsFalse(bag.ProductsAmounts.ContainsKey(product));
        }

        public static IEnumerable<TestCaseData> Data_RemoveProduct_Fail
        {
            get
            {
                yield return new TestCaseData(pNotInBag_3);
            }
        }
        [Test]
        [TestCaseSource("Data_RemoveProduct_Fail")]
        public void TestChangeProductAmount_Fail(ProductInBag product)
        {
            Assert.Throws<ArgumentException>(() => bag.RemoveProduct(product));
        }


    }
}