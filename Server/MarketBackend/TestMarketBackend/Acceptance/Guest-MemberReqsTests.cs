using MarketBackend.ServiceLayer.ServiceDTO;
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
        //[SetUp]
        //public void DataManagerSetup()
        //{

        //} 

        // r.1.2
        [Test]
        public void GuestDisconnection()
        {
            Response<bool> response = buyerFacade.Leave(guest1Id);

            Assert.IsTrue(!response.IsErrorOccured());

            Response<ServiceCart> cartResponse = buyerFacade.GetCart(guest1Id);

            Assert.IsTrue(cartResponse.IsErrorOccured());
            Assert.IsNull(cartResponse.Value);
        }

        // r.1.2
        [Test]
        public void MemberDisconnection()
        {
            Response<ServiceCart> cartResponse = buyerFacade.GetCart(guest1Id);

            Response<bool> response = buyerFacade.Leave(member1Id);

            Assert.IsTrue(!response.IsErrorOccured());

            Response<ServiceCart> cartAfterDisconnectionResponse = buyerFacade.GetCart(guest1Id);

            Assert.IsTrue(!cartResponse.IsErrorOccured());
            Assert.IsTrue(cartResponse.Value.Equals(cartAfterDisconnectionResponse.Value));
        }

        // r.2.1
        [Test]
        public void GetInformationAboutStore()
        {
            Response<ServiceStore> response = buyerFacade.GetStoreInfo(storeId);

            Assert.IsTrue(!response.IsErrorOccured());
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

            Assert.IsTrue(!response.IsErrorOccured());
            Assert.IsNotNull(response.Value);
            Assert.IsTrue(IsSearchSuccessful(response.Value));
        }


        // WE ARE ALLOWING NULL -> WHEN NULL WE SEARCH WITHOUT FILTER!
        // r.2.2
        //[Test]
        //[TestCase("aaaaa", null, null, "iphone")]
        //[TestCase(null, null, null, null)]
        //[TestCase(null, null, "hmmm", "iphone")]
        //[TestCase(null, iphoneProductName, null, "0")]
        //[TestCase(null, iphoneProductName, "kitchen", "iphone")]
        //[TestCase("NoStore", iphoneProductName, mobileCategory, null)]
        //[TestCase("@!#!!@", iphoneProductName, mobileCategory, "iphone")]
        //public void FailedProductSearch(string storeName, string productName, string category, string keyword)
        //{
        //    Response<IDictionary<int, IList<ServiceProduct>>> response =
        //        buyerFacade.ProductsSearch(storeName, productName, category, keyword);

        //    Assert.IsTrue(!response.ErrorOccured());
        //    Assert.IsNotNull(response.Value);
        //    Assert.IsTrue(!IsSearchSuccessful(response.Value));
        //}

        private bool CartHasThisAmountOfProductFromStore(ServiceCart cart, int storeId, int productId, int amount)
        {
            // Verify that the product has been successfuly added to the cart
            ServiceShoppingBag shoppingBag = cart.ShoppingBags[storeId];

            foreach(int pid in shoppingBag.ProductsAmounts.Keys)
            {
                if (pid == productId && shoppingBag.ProductsAmounts[pid] == amount)
                    return true;
            }
            return false;

        }

        public static IEnumerable<TestCaseData> DataSuccessfulProductKeeping
        {
            get
            {
                yield return new TestCaseData(new Func<int>(() => guest1Id), new Func<int>(() => storeId), new Func<int>(() => iphoneProductId), 2);
                yield return new TestCaseData(new Func<int>(() => member2Id), new Func<int>(() => storeId), new Func<int>(() => iphoneProductId), 20);
            }
        }

        // r.2.3
        [Test]
        [TestCaseSource("DataSuccessfulProductKeeping")]
        public void SuccessfulProductKeeping(Func<int> userId, Func<int> storeId, Func<int> productId, int amount)
        {
            Response<bool> response = buyerFacade.AddProdcutToCart(userId(), storeId(), productId(), amount);
            Assert.IsTrue(!response.IsErrorOccured());

            // ReopenMarket();

            Response<ServiceCart> cartResponse = buyerFacade.GetCart(userId());

            Assert.IsTrue(!cartResponse.IsErrorOccured());
            Assert.IsNotNull(cartResponse);
            Assert.IsTrue(CartHasThisAmountOfProductFromStore(cartResponse.Value, storeId(), productId(), amount));
        }

        public static IEnumerable<TestCaseData> DataFailedProductKeeping
        {
            get
            {
                yield return new TestCaseData(new Func<int>(() => guest1Id), new Func<int>(() => storeId), new Func<int>(() => iphoneProductId), 51);
                yield return new TestCaseData(new Func<int> (() => guest1Id), new Func<int>(() => guest1Id), new Func<int>(() => calculatorProductId), 1);
                yield return new TestCaseData(new Func<int>(() => guest1Id), new Func<int>(() => guest1Id), new Func<int>(() => calculatorProductId), 20);
            }
        }

        // r.2.3
        [Test]
        [TestCaseSource("DataFailedProductKeeping")]
        public void FailedProductKeeping(Func<int> userId, Func<int> storeId, Func<int> productId, int amount)
        {
            Response<ServiceCart> cartResponseBefore = buyerFacade.GetCart(userId());
            Response<bool> response = buyerFacade.AddProdcutToCart(userId(), storeId(), productId(), amount);
            Assert.IsTrue(response.IsErrorOccured());

            // ReopenMarket();

            Response<ServiceCart> cartResponseAfter = buyerFacade.GetCart(userId());
            ServiceCart cartBefore = cartResponseBefore.Value;
            ServiceCart cartAfter = cartResponseAfter.Value;
            Assert.IsTrue(cartBefore.Equals(cartAfter));
        }

        public static IEnumerable<TestCaseData> DataSuccessfulViewCart
        {
            get
            {
                yield return new TestCaseData(new Func<int> (() => guest1Id));
                yield return new TestCaseData(new Func<int>(() => member2Id));
            }
        }

        // r.2.4
        [Test]
        [TestCaseSource("DataSuccessfulViewCart")]
        public void SuccessfulViewCart(Func<int> userId)
        {
            Response<ServiceCart> response = buyerFacade.GetCart(userId());

            Assert.IsTrue(!response.IsErrorOccured());
            Assert.IsNotNull(response.Value);
        }

        public static IEnumerable<TestCaseData> DataSuccessfulRemoveProductFromCart
        {
            get
            {
                yield return new TestCaseData(new Func<int> (() => guest1Id), new Func<int>(() => storeId), new Func<int>(() => iphoneProductId));
                yield return new TestCaseData(new Func<int>(() => member2Id), new Func<int>(() => storeId), new Func<int>(() => calculatorProductId));
            }
        }

        // r.2.4
        [Test]
        [TestCaseSource("DataSuccessfulRemoveProductFromCart")]
        public void SuccessfulRemoveProductFromCart(Func<int> userId, Func<int> storeId, Func<int> productId)
        {
            Response<ServiceCart> cartResponse = buyerFacade.GetCart(userId());
            Assert.IsFalse(cartResponse.IsErrorOccured());
            ServiceCart cartBefore = cartResponse.Value;

            Response<bool> additionResponse = buyerFacade.AddProdcutToCart(userId(), storeId(), productId(), 5);

            Response<bool> removeResponse = buyerFacade.RemoveProductFromCart(userId(), storeId(), productId());

            // is also on guests so not running the next line
            // ReopenMarket(); 

            cartResponse = buyerFacade.GetCart(userId());
            Assert.IsFalse(cartResponse.IsErrorOccured());
            ServiceCart cartAfter = cartResponse.Value;

            Assert.IsNotNull(cartBefore);
            Assert.IsNotNull(cartAfter);
            Assert.IsTrue(cartBefore.Equals(cartAfter));
        }

        public static IEnumerable<TestCaseData> DataSuccessfulRemoveAmountOfProductFromCart
        {
            get
            {
                yield return new TestCaseData(new Func<int>(() => guest1Id), new Func<int>(() => storeId), new Func<int>(() => iphoneProductId), 8);
                yield return new TestCaseData(new Func<int>(() => member2Id), new Func<int>(() => storeId), new Func<int>(() => calculatorProductId), 4);
            }
        }

        // r.2.4
        [Test]
        [TestCaseSource("DataSuccessfulRemoveAmountOfProductFromCart")]
        public void SuccessfulRemoveAmountOfProductFromCart(Func<int> userId, Func<int> storeId, Func<int> productId, int amount)
        {
            buyerFacade.AddProdcutToCart(userId(), storeId(), productId(), amount);

            buyerFacade.changeProductAmountInCart(userId(), storeId(), productId(), amount / 2);

            // ReopenMarket();

            Response<ServiceCart> cartResponse = buyerFacade.GetCart(userId());
            ServiceCart cart = cartResponse.Value;

            Assert.IsNotNull(cart);
            Assert.IsTrue(CartHasThisAmountOfProductFromStore(cart, storeId(), productId(),amount / 2));
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

            Assert.IsTrue(response.IsErrorOccured());
        }

        private void SetUpShoppingCarts()
        {
            buyerFacade.AddProdcutToCart(member3Id, storeId, iphoneProductId, 25);
            buyerFacade.AddProdcutToCart(member3Id, storeId, calculatorProductId, 5);

        }
        //r.2.5
        [Test]
        public void SuccessfulPurchase()
        {
            SetUpShoppingCarts();
            
            Response<ServicePurchase> response = buyerFacade.PurchaseCartContent(member3Id, paymentDetails, supplyDetails);

            Response<ServiceCart> cartResponse = buyerFacade.GetCart(member3Id);
            ServiceCart cart = cartResponse.Value;

            Assert.IsTrue(cart.IsEmpty());
            Assert.IsTrue(!response.IsErrorOccured());
        }

        //r.2.5, r I 5
        [Test]
        public void FailedPurchaseEmptyCart()
        {
            IList<string> notificationsBefore = member2Notifications.ToList(); 

            Response<ServicePurchase> response = buyerFacade.PurchaseCartContent(member3Id, paymentDetails, supplyDetails);
            Assert.IsTrue(response.IsErrorOccured());

            // member2 is logged in, checking that there wasn't a notification
            Assert.IsTrue(SameElements(notificationsBefore, member2Notifications.ToList()));
        }

        // r.2.5, r I 5
        [Test]
        public void FailedPurchaseProductsOutOfStock()
        {
            // A user purchases all iphones in the store
            buyerFacade.AddProdcutToCart(member2Id, storeId, iphoneProductId, iphoneProductAmount);
            buyerFacade.AddProdcutToCart(member3Id, storeId, iphoneProductId, iphoneProductAmount);
            Response<ServicePurchase> firstUserResponse = buyerFacade.PurchaseCartContent(member2Id, paymentDetails, supplyDetails);
            Assert.True(!firstUserResponse.IsErrorOccured());

            IList<string> notificationsBefore = member3Notifications.ToList();
            Response<ServicePurchase> secondUserResponse = buyerFacade.PurchaseCartContent(member3Id, paymentDetails, supplyDetails);

            Assert.IsTrue(secondUserResponse.IsErrorOccured() );

            // member3 is logged in, checking that there wasn't a notification
            Assert.IsTrue(SameElements(notificationsBefore, member3Notifications.ToList()));
        }

        public static IEnumerable<TestCaseData> DataLogoutLoginCartSaved
        {
            get
            {
                // cart with products
                yield return new TestCaseData(() => member3Id, userName3, password3);
                // cart with no products 
                yield return new TestCaseData(() => member2Id, userName2, password2);
            }
        }

        // r 3.1, r 2.3
        [Test]
        [TestCaseSource("DataLogoutLoginCartSaved")]
        public void LogoutLoginCartSaved(Func<int> getMemberId, string username, string password)
        {
            int memberId = getMemberId();

            SetUpShoppingCarts();

            Response<ServiceCart> response = buyerFacade.GetCart(memberId);
            Assert.IsTrue(!response.IsErrorOccured());
            ServiceCart cartBefore = response.Value;

            Response<bool> logoutResponse = buyerFacade.Logout(memberId);
            Assert.IsTrue(!logoutResponse.IsErrorOccured());

            Response<int> loginResponse = buyerFacade.Login(username, password, notifications => true);
            Assert.IsTrue(!loginResponse.IsErrorOccured());

            response = buyerFacade.GetCart(memberId);
            Assert.IsTrue(!response.IsErrorOccured());
            ServiceCart cartAfter = response.Value;

            Assert.AreEqual(cartBefore, cartAfter); 
        }



        /*
        // r.2.5
        [Test]
        [TestCase(2)]
        public void ConcurrentPurchaseProducts(int numberThreads)
        {
            int[] ids = GetFreshMembersIds(numberThreads);

            IDictionary<int, IList<Tuple<int, int>>>[] shoppingBags =
                Enumerable.Repeat(
                    new Dictionary<int, IList<Tuple<int, int>>>() {
                    [storeId] = new List<Tuple<int, int>>() { new Tuple<int, int>(iphoneProductId, (2/3)*iphoneProductAmount) }
                    },
                    numberThreads)
                .ToArray();

            Func<Response<ServicePurchaseAttempt>>[] jobs = new Func<Response<ServicePurchaseAttempt>>[numberThreads];
            for(int i = 0; i < numberThreads; i++)
            {
                int temp = i;
                jobs[i] = () => { return buyerFacade.PurchaseCartContent(ids[temp], shoppingBags[temp]); };
            }

            Response<ServicePurchaseAttempt>[] responses = GetResponsesFromThreads(jobs);

            Assert.IsTrue(Exactly1ResponseIsSuccessful(responses));

        }
        */

        // todo: when writing tests to failed adding product review, check that a
        // store owner did not receive notifications for the action 

    }
}
