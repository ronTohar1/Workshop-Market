using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Moq;
using MarketBackend.BusinessLayer.Market.StoreManagment;
using MarketBackend.BusinessLayer.Buyers;
using Autofac.Extras.Moq;
using MarketBackend.BusinessLayer;
using MarketBackend.DataLayer.DataManagers.DataManagersInherentsForTesting;
using MarketBackend.DataLayer.DataDTOs.Buyers.Carts;
using MarketBackend.DataLayer.DataManagers;
using MarketBackend.DataLayer.DataDTOs.Buyers;
using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement;

namespace TestMarketBackend.BusinessLayer.Market.StoreManagment
{
    internal class CartTests
    {
        private static Cart cart = new Cart();
        private static ShoppingBag bag1 = new ShoppingBag(1);
        private static ShoppingBag bag2 = new ShoppingBag(2);
        private static ProductInBag productNotInBag1 = new ProductInBag(1, 1);
        private static ProductInBag productInBag2    = new ProductInBag(2, 2);
        private static ProductInBag productNotInBag3 = new ProductInBag(3, 3);

        [SetUp]
        public void SetUp()
        {

            // database mocks
            Mock<ForTestingCartDataManager> c = new Mock<ForTestingCartDataManager>();
            Mock<ForTestingMemberDataManager> m = new Mock<ForTestingMemberDataManager>();
            Mock<ForTestingProductInBagDataManager> pib = new Mock<ForTestingProductInBagDataManager>();
            Mock<ForTestingShoppingBagDataManager> sb = new Mock<ForTestingShoppingBagDataManager>();
            Mock<ForTestingStoreDataManager> s = new Mock<ForTestingStoreDataManager>();

            c.Setup(x => x.Add(It.IsAny<DataCart>()));
            c.Setup(x => x.Remove(It.IsAny<int>()));
            c.Setup(x => x.Update(It.IsAny<int>(), It.IsAny<Action<DataCart>>()));
            c.Setup(x => x.Find(It.IsAny<int>())).Returns((DataCart)null);
            c.Setup(x => x.Save());
            CartDataManager.ForTestingSetInstance(c.Object);

            m.Setup(x => x.Add(It.IsAny<DataMember>()));
            m.Setup(x => x.Remove(It.IsAny<int>()));
            m.Setup(x => x.Update(It.IsAny<int>(), It.IsAny<Action<DataMember>>()));
            m.Setup(x => x.Find(It.IsAny<int>())).Returns((DataMember)null);
            m.Setup(x => x.Save());
            MemberDataManager.ForTestingSetInstance(m.Object);

            pib.Setup(x => x.Add(It.IsAny<DataProductInBag>()));
            pib.Setup(x => x.Remove(It.IsAny<int>()));
            pib.Setup(x => x.Update(It.IsAny<int>(), It.IsAny<Action<DataProductInBag>>()));
            pib.Setup(x => x.Find(It.IsAny<int>())).Returns((DataProductInBag)null);
            pib.Setup(x => x.Save());
            ProductInBagDataManager.ForTestingSetInstance(pib.Object);

            sb.Setup(x => x.Add(It.IsAny<DataShoppingBag>()));
            sb.Setup(x => x.Remove(It.IsAny<int>()));
            sb.Setup(x => x.Update(It.IsAny<int>(), It.IsAny<Action<DataShoppingBag>>()));
            sb.Setup(x => x.Find(It.IsAny<int>())).Returns((DataShoppingBag)null);
            sb.Setup(x => x.Save());
            ShoppingBagDataManager.ForTestingSetInstance(sb.Object);

            s.Setup(x => x.Add(It.IsAny<DataStore>()));
            s.Setup(x => x.Remove(It.IsAny<int>()));
            s.Setup(x => x.Update(It.IsAny<int>(), It.IsAny<Action<DataStore>>()));
            s.Setup(x => x.Find(It.IsAny<int>())).Returns((DataStore)null);
            s.Setup(x => x.Save());
            StoreDataManager.ForTestingSetInstance(s.Object);

            cart.ShoppingBags[1] = bag1;
            bag2.AddProductToBag(productInBag2, 2, 0, false, null, null);
            cart.ShoppingBags[2] = bag2;
        }

        [TearDown]
        public void TearDown()
        {
            bag2.ProductsAmounts.Clear();
            cart.ShoppingBags.Clear();
        }

        #region AddProductToCart
        public static IEnumerable<TestCaseData> Data_TestAddProductToCart
        {
            get
            {
                yield return new TestCaseData(productNotInBag1, 1, 10, 10);
                yield return new TestCaseData(productInBag2, 2, 10, 12);
                yield return new TestCaseData(productNotInBag3, 3, 10, 10);
            }
        }

        [Test]
        [TestCaseSource("Data_TestAddProductToCart")]
        public void TestAddProductToCart(ProductInBag product, int storeId, int amountToAdd, int expectedAmount)
        {
            cart.AddProductToCart(product, amountToAdd, 0, false);
            Assert.AreEqual(expectedAmount, cart.ShoppingBags[storeId].ProductsAmounts[product]);
        }
        #endregion


        #region RemoveProductFromCart
        public static IEnumerable<TestCaseData> Data_TestRemoveProductFromCart
        {
            get
            {
                yield return new TestCaseData(productInBag2);
            }
        }

        [Test]
        [TestCaseSource("Data_TestRemoveProductFromCart")]
        public void TestRemoveProductFromCart(ProductInBag product)
        {
            Assert.IsTrue(cart.ShoppingBags.Any(pair => pair.Value.ProductsAmounts.ContainsKey(product)));
            cart.RemoveProductFromCart(product, 0, false);
            Assert.IsFalse(cart.ShoppingBags.Any(pair => pair.Value.ProductsAmounts.ContainsKey(product)));
        }
        #endregion

        #region ChangeProductAmount
        public static IEnumerable<TestCaseData> Data_TestChangeProductAmount_ToNonZero
        {
            get
            {
                yield return new TestCaseData(productInBag2, 2, 5);
            }
        }

        //[Test]
        //[TestCaseSource("Data_TestChangeProductAmount_ToNonZero")]
        //public void TestChangeProductAmount_ToNonZero(ProductInBag product, int storeId, int newAmount)
        //{
        //    Assert.AreNotEqual(newAmount, cart.ShoppingBags[storeId].ProductsAmounts[product]);
        //    cart.ChangeProductAmount(product, newAmount);
        //    Assert.AreEqual(newAmount, cart.ShoppingBags[storeId].ProductsAmounts[product]);
        //}

        public static IEnumerable<TestCaseData> Data_TestChangeProductAmount_ToZero
        {
            get
            {
                yield return new TestCaseData(productInBag2, 2, 0);
            }
        }

        //[Test]
        //[TestCaseSource("Data_TestChangeProductAmount_ToZero")]
        //public void TestChangeProductAmount_ToZero(ProductInBag product, int storeId, int newAmount)
        //{
        //    Assert.AreNotEqual(newAmount, cart.ShoppingBags[storeId].ProductsAmounts[product]);
        //    Assert.Throws<MarketException>(() => cart.ChangeProductAmount(product, newAmount));
        //}
        #endregion
    }
}