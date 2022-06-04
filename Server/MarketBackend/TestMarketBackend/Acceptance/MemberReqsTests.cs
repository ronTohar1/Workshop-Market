using MarketBackend.ServiceLayer.ServiceDTO;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMarketBackend.Acceptance
{
    internal class MemberReqsTests : AcceptanceTests
    {
        // r.3.1
        [Test]
        public void SuccessfulLogout()
        {
            Response<bool> response = buyerFacade.Logout(member2Id);

            Assert.IsTrue(!response.IsErrorOccured());
        }

        // r.3.2
        [Test]
        public void SuccessfulStoreCreation()
        {
            Response<int> response = storeManagementFacade.OpenStore(member3Id, "VeryNewStoreName");

            Assert.IsTrue(!response.IsErrorOccured());
        }

        // r.3.2
        [Test]
        [TestCase("BestStoreEver", 2)]
        [TestCase("BestStoreEver", 10)]
        [TestCase("BestStoreEver", 30)]
        public void ConcurrentStoreCreation(string storeName, int threadsNumber)
        {
            int[] ids = GetFreshMembersIds(threadsNumber);

            Func<Response<int>>[] jobs = new Func<Response<int>>[threadsNumber];
            for(int i = 0; i < threadsNumber; i++)
            {
                int temp = i;
                jobs[i] = () => { return storeManagementFacade.OpenStore(ids[temp], storeName); };
            }

            Response<int>[] responses = GetResponsesFromThreads(jobs);

            Assert.IsTrue(Exactly1ResponseIsSuccessful(responses));
        }

        // r.3.2
        [Test]
        [TestCase("")]
        public void FailedCreationOfStoreWithInvalidDetails(string storeName)
        {
            Response<int> response = storeManagementFacade.OpenStore(member3Id, storeName);

            Assert.IsTrue(response.IsErrorOccured());
        }

        // r.3.2
        [Test]
        public void FailedCreationOfStoreThatAlreadyExists()
        {
            Response<int> response = storeManagementFacade.OpenStore(member3Id, storeName);

            Assert.IsTrue(response.IsErrorOccured());
        }

        // r.3.3
        [Test]
        [TestCase("The quality is great. Shipment was very quick. Would recommend.")]
        public void SuccessfulProductReview(string review)
        {
            Response<bool> response = buyerFacade.AddProductReview(member3Id, storeId, iphoneProductId, review);

            Assert.IsTrue(!response.IsErrorOccured());
        }

        // r.3.3
        [Test]
        [TestCase("")]
        [TestCase("   ")]
        public void FailedInvalidProductReview(string review)
        {
            Response<bool> response = buyerFacade.AddProductReview(member3Id, storeId, iphoneProductId, review);

            Assert.IsTrue(response.IsErrorOccured());
        }

        // r.3.3
        [Test]
        [TestCase("The quality is great. Shipment was very quick. Would recommend.")]
        public void SuccessDoubleProductReview(string review)
        {
            Response<bool> firstResponse = buyerFacade.AddProductReview(member3Id, storeId, iphoneProductId, review);

            Response<bool> secondResponse = buyerFacade.AddProductReview(member3Id, storeId, iphoneProductId, review);

            Assert.IsTrue(!firstResponse.IsErrorOccured() && !secondResponse.IsErrorOccured());
        }

        // testing member notifications 

        public static IEnumerable<TestCaseData> DataSuccessfulNotifications
        {
            get
            {
                // these are events that after which member5 should be notified 

                // purchase in the store of the store owner 
                yield return new TestCaseData((MemberReqsTests testsObject) =>
                {
                    testsObject.SetUpShoppingCarts();
                    Response<ServicePurchase> response = testsObject.buyerFacade.PurchaseCartContent(member3Id, paymentDetails, supplyDetails);
                    Assert.IsTrue(!response.IsErrorOccured());
                });
                // shop of the store owner is closed 
                yield return new TestCaseData((MemberReqsTests testsObject) =>
                {
                    Response<bool> response = testsObject.storeManagementFacade.CloseStore(member2Id, storeId);
                    Assert.IsTrue(!response.IsErrorOccured());
                });
                // store owner is removed from the store 
                yield return new TestCaseData((MemberReqsTests testsObject) =>
                {
                    Response<bool> response = testsObject.storeManagementFacade.RemoveCoOwner(member2Id, member5Id, storeId);
                    Assert.IsTrue(!response.IsErrorOccured());
                });
                // a member writes a review on a product in the store of the store owner 
                yield return new TestCaseData((MemberReqsTests testsObject) =>
                {
                    Response<bool> response = testsObject.buyerFacade.AddProductReview(member1Id, storeId, calculatorProductId, "The product is great");
                    Assert.IsTrue(!response.IsErrorOccured());
                });
            }
        }

        // r I 5
        [Test]
        [TestCaseSource("DataSuccessfulNotifications")]
        public void SuccessfulNotidicationsLoggedIn(Action<MemberReqsTests> action)
        {
            // getting notifications before
            IList<string> notificationsBefore = member5Notifications.ToList();

            action(this); // performing an action for which member5 should be notified 

            IList<string> notificationsAfter = member5Notifications.ToList();

            // checking the new notifications with the the notifications that were before:
            IList<string> notificationsIntersection = notificationsBefore.Intersect(notificationsAfter).ToList();
            Assert.AreEqual(notificationsBefore.Count, notificationsIntersection.Count);
            Assert.IsTrue(notificationsBefore.Count < notificationsAfter.Count);
        }

        // tests on failed actions that should not be followed by notifications 
        // are in the tests of the different actions 

        // r I 6
        [Test]
        [TestCaseSource("DataSuccessfulNotifications")]
        public void SuccessfulNotidicationsNotLoggedIn(Action<MemberReqsTests> action)
        {
            // logging out so we can use the same data as in the SuccessfulNotidicationsLoggedIn tests 
            Response<bool> response = buyerFacade.Logout(member5Id);
            Assert.IsTrue(!response.IsErrorOccured()); 

            // getting notifications before
            IList<string> notificationsBefore = member5Notifications.ToList();

            action(this); // performing an action for which member5 should be notified 

            // first checking that there is not a notification when not logged in 
            IList<string> notificationsAfter = member5Notifications.ToList();
            SameElements(notificationsBefore, notificationsAfter);

            // second checking there is a notification after loggin in
            bool notificationArrived = false;
            buyerFacade.Login(userName5, password5, notification => { notificationArrived = true; return true; });
            // checking the new notifications with the the notifications that were before:
            Assert.IsTrue(notificationArrived); 
        }
    }
}
