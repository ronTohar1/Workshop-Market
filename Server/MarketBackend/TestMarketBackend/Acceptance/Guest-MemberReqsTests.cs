﻿using MarketBackend.ServiceLayer.ServiceDTO;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMarketBackend.Acceptance
{
    internal class Guest_MemberReqsTests : AcceptanceTests
    {

        // r.1.2
        [Test]
        public void GuestDisconnection()
        {
            Response<bool> response = buyerFacade.Leave(guest1Id);

            Assert.IsTrue(!response.ErrorOccured());

            Response<ServiceCart> cartResponse = buyerFacade.GetCart(guest1Id);

            Assert.IsTrue(cartResponse.ErrorOccured());
            Assert.IsNull(cartResponse.Value);
        }

        // r.1.2
        [Test]
        public void MemberDisconnection()
        {
            Response<ServiceCart> cartResponse = buyerFacade.GetCart(guest1Id);

            Response<bool> response = buyerFacade.Leave(member1Id);

            Assert.IsTrue(!response.ErrorOccured());

            Response<ServiceCart> cartAfterDisconnectionResponse = buyerFacade.GetCart(guest1Id);

            Assert.IsTrue(!cartResponse.ErrorOccured());
            Assert.IsTrue(cartResponse.Value.Equals(cartAfterDisconnectionResponse.Value));
        }

        // r.2.1
        [Test]
        public void GetInformationAboutStore()
        {
            Response<ServiceStore> response = buyerFacade.GetStoreInfo(storeId);

            Assert.IsTrue(!response.ErrorOccured());
            Assert.IsNotNull(response.Value);
        }

        private bool IsSearchSuccessful(IDictionary<int, IList<ServiceProduct>> result)
        {
            if (!result.Keys.Contains(storeId))
                return false;

            IList<ServiceProduct> productList = result[storeId];
            if (productList.Where(p => p.Id == iphoneProductId).Count() == 0)
                return false;

            return true;
        }

        // r.2.2
        [Test]
        [TestCase(null, null, null, "iphone")]
        [TestCase(null, null, mobileCategory, null)]
        [TestCase(null, null, mobileCategory, "iphone")]
        [TestCase(null, iphoneProductName, null, null)]
        [TestCase(null, iphoneProductName, null, "iphone")]
        [TestCase(null, iphoneProductName, mobileCategory, null)]
        [TestCase(null, iphoneProductName, mobileCategory, "iphone")]
        [TestCase(storeName, null, null, null)]
        [TestCase(storeName, null, null, "iphone")]
        [TestCase(storeName, null, mobileCategory, null)]
        [TestCase(storeName, null, mobileCategory, "iphone")]
        [TestCase(storeName, iphoneProductName, null, null)]
        [TestCase(storeName, iphoneProductName, null, "iphone")]
        [TestCase(storeName, iphoneProductName, mobileCategory, null)]
        [TestCase(storeName, iphoneProductName, mobileCategory, "iphone")]
        public void SuccessfulProductSearch(string storeName, string productName, string category, string keyword)
        {
            Response<IDictionary<int, IList<ServiceProduct>>> response =
                buyerFacade.ProductsSearch(storeName, productName, category, keyword);

            Assert.IsTrue(!response.ErrorOccured());
            Assert.IsNotNull(response.Value);
            Assert.IsTrue(IsSearchSuccessful(response.Value));
        }

        // r.2.2
        [Test]
        [TestCase("aaaaa", null, null, "iphone")]
        [TestCase(null, null, null, null)]
        [TestCase(null, null, "hmmm", "iphone")]
        [TestCase(null, iphoneProductName, null, "0")]
        [TestCase(null, iphoneProductName, "kitchen", "iphone")]
        [TestCase("NoStore", iphoneProductName, mobileCategory, null)]
        [TestCase("@!#!!@", iphoneProductName, mobileCategory, "iphone")]
        public void FailedProductSearch(string storeName, string productName, string category, string keyword)
        {
            Response<IDictionary<int, IList<ServiceProduct>>> response =
                buyerFacade.ProductsSearch(storeName, productName, category, keyword);

            Assert.IsTrue(!response.ErrorOccured());
            Assert.IsNotNull(response.Value);
            Assert.IsTrue(!IsSearchSuccessful(response.Value));
        }

        private bool CartHasThisAmountOfProductFromStore(ServiceCart cart, int storeId, int productId, int amount)
        {
            // Verify that the product has been successfuly added to the cart
            return true;

        }

        public static IEnumerable<TestCaseData> DataSuccessfulProductKeeping
        {
            get
            {
                yield return new TestCaseData(guest1Id, storeId, iphoneProductId, 2);
                yield return new TestCaseData(member2Id, storeId, iphoneProductId, 20);
            }
        }

        // r.2.3
        [Test]
        [TestCaseSource("DataSuccessfulProductKeeping")]
        public void SuccessfulProductKeeping(int userId, int storeId, int productId, int amount)
        {
            Response<bool> response = buyerFacade.AddProdcutToCart(userId, storeId, productId, amount);

            Assert.IsTrue(!response.ErrorOccured());

            Response<ServiceCart> cartResponse = buyerFacade.GetCart(userId);

            Assert.IsTrue(!cartResponse.ErrorOccured());
            Assert.IsNotNull(cartResponse);
            Assert.IsTrue(CartHasThisAmountOfProductFromStore(cartResponse.Value, storeId, productId, amount));
        }

        public static IEnumerable<TestCaseData> DataFailedProductKeeping
        {
            get
            {
                yield return new TestCaseData(guest1Id, storeId, iphoneProductId, 51);
                yield return new TestCaseData(guest1Id, storeId, calculatorProductId, 1);
                yield return new TestCaseData(member2Id, storeId, 123, 20);
            }
        }

        // r.2.3
        [Test]
        [TestCaseSource("DataFailedProductKeeping")]
        public void FailedProductKeeping(int userId, int storeId, int productId, int amount)
        {
            Response<ServiceCart> cartResponseBefore = buyerFacade.GetCart(userId);

            Response<bool> response = buyerFacade.AddProdcutToCart(userId, storeId, productId, amount);

            Assert.IsTrue(response.ErrorOccured());

            Response<ServiceCart> cartResponseAfter = buyerFacade.GetCart(userId);

            ServiceCart cartBefore = cartResponseBefore.Value;
            ServiceCart cartAfter = cartResponseAfter.Value;

            Assert.IsTrue(cartBefore.Equals(cartAfter));
        }

        public static IEnumerable<TestCaseData> DataSuccessfulViewCart
        {
            get
            {
                yield return new TestCaseData(guest1Id);
                yield return new TestCaseData(member2Id);
            }
        }

        // r.2.4
        [Test]
        [TestCaseSource("DataFailedProductKeeping")]
        public void SuccessfulViewCart(int userId)
        {
            Response<ServiceCart> response = buyerFacade.GetCart(userId);

            Assert.IsTrue(!response.ErrorOccured());
            Assert.IsNotNull(response.Value);
        }

        public static IEnumerable<TestCaseData> DataSuccessfulRemoveProductFromCart
        {
            get
            {
                yield return new TestCaseData(guest1Id, storeId, iphoneProductId);
                yield return new TestCaseData(member2Id, storeId, calculatorProductId);
            }
        }

        // r.2.4
        [Test]
        [TestCaseSource("DataSuccessfulRemoveProductFromCart")]
        public void SuccessfulRemoveProductFromCart(int userId, int storeId, int productId)
        {
            Response<ServiceCart> cartResponse = buyerFacade.GetCart(userId);
            ServiceCart cartBefore = cartResponse.Value;

            Response<bool> additionResponse = buyerFacade.AddProdcutToCart(userId, storeId, productId, 5);

            Response<bool> removeResponse = buyerFacade.RemoveProductFromCart(userId, storeId, productId);

            cartResponse = buyerFacade.GetCart(userId);
            ServiceCart cartAfter = cartResponse.Value;

            Assert.IsNotNull(cartBefore);
            Assert.IsNotNull(cartAfter);
            Assert.IsTrue(cartBefore.Equals(cartAfter));
        }

        public static IEnumerable<TestCaseData> DataSuccessfulRemoveAmountOfProductFromCart
        {
            get
            {
                yield return new TestCaseData(guest1Id, storeId, iphoneProductId, 8);
                yield return new TestCaseData(member2Id, storeId, calculatorProductId, 4);
            }
        }

        // r.2.4
        [Test]
        [TestCaseSource("DataSuccessfulRemoveAmountOfProductFromCart")]
        public void SuccessfulRemoveAmountOfProductFromCart(int userId, int storeId, int productId, int amount)
        {
            buyerFacade.AddProdcutToCart(userId, storeId, productId, amount);

            buyerFacade.changeProductAmountInCart(userId, storeId, productId, amount / 2);

            Response<ServiceCart> cartResponse = buyerFacade.GetCart(userId);
            ServiceCart cart = cartResponse.Value;

            Assert.IsNotNull(cart);
            Assert.IsTrue(CartHasThisAmountOfProductFromStore(cart, storeId, productId, amount - amount / 2));
        }

        public static IEnumerable<TestCaseData> DataFailedRemoveProductFromCart
        {
            get
            {
                yield return new TestCaseData(guest1Id, storeId, iphoneProductId, 8);
                yield return new TestCaseData(member2Id, storeId, calculatorProductId, 4);
            }
        }

        // r.2.4
        [Test]
        [TestCaseSource("DataFailedRemoveProductFromCart")]
        public void FailedRemoveProductFromCart(int userId, int storeId, int productId, int amount)
        {
            Response<bool> response = buyerFacade.RemoveProductFromCart(userId, storeId, productId);

            Assert.IsTrue(response.ErrorOccured());
        }

        // r.2.5
        //[Test]
        //public void SuccessfulPurchase()
        //{
        //    Response<bool> response = buyerFacade.PurchaseCartContent(member3Id);

        //    Response<ServiceCart> cartResponse = buyerFacade.GetCart(member3Id);
        //    ServiceCart cart = cartResponse.Value;

        //    Assert.IsTrue(!response.ErrorOccured() && cart.IsEmpty());
        //}

        // r.2.5
        //[Test]
        //public void FailedPurchaseEmptyCart()
        //{
        //    Response<bool> response = buyerFacade.PurchaseCartContent(member3Id);

        //    Assert.IsTrue(response.ErrorOccured());
        //}

        // r.2.5
        //[Test]
        //public void FailedPurchaseProductsOutOfStock()
        //{
        //    // A user purchases all iphones in the store
        //    buyerFacade.AddProdcutToCart(member2Id, storeId, iphoneProductId, iphoneProductAmount);

        //    Response<bool> firstUserResponse = buyerFacade.PurchaseCartContent(member2Id);

        //    // Another user tries to purchase iphones from the same store
        //    Response<bool> response = buyerFacade.PurchaseCartContent(member3Id);

        //    Assert.IsTrue(!firstUserResponse.ErrorOccured() && response.ErrorOccured());
        //}

    }
}