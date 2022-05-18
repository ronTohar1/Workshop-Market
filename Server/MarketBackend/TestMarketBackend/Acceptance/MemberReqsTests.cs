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

            Assert.IsTrue(!response.ErrorOccured());
        }

        // r.3.2
        [Test]
        public void SuccessfulStoreCreation()
        {
            Response<int> response = storeManagementFacade.OpenStore(member3Id, "VeryNewStoreName");

            Assert.IsTrue(!response.ErrorOccured());
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

            Assert.IsTrue(response.ErrorOccured());
        }

        // r.3.2
        [Test]
        public void FailedCreationOfStoreThatAlreadyExists()
        {
            Response<int> response = storeManagementFacade.OpenStore(member3Id, storeName);

            Assert.IsTrue(response.ErrorOccured());
        }

        // r.3.3
        [Test]
        [TestCase("The quality is great. Shipment was very quick. Would recommend.")]
        public void SuccessfulProductReview(string review)
        {
            Response<bool> response = buyerFacade.AddProductReview(member3Id, storeId, iphoneProductId, review);

            Assert.IsTrue(!response.ErrorOccured());
        }

        // r.3.3
        [Test]
        [TestCase("")]
        [TestCase("   ")]
        public void FailedInvalidProductReview(string review)
        {
            Response<bool> response = buyerFacade.AddProductReview(member3Id, storeId, iphoneProductId, review);

            Assert.IsTrue(response.ErrorOccured());
        }

        // r.3.3
        [Test]
        [TestCase("The quality is great. Shipment was very quick. Would recommend.")]
        public void SuccessDoubleProductReview(string review)
        {
            Response<bool> firstResponse = buyerFacade.AddProductReview(member3Id, storeId, iphoneProductId, review);

            Response<bool> secondResponse = buyerFacade.AddProductReview(member3Id, storeId, iphoneProductId, review);

            Assert.IsTrue(!firstResponse.ErrorOccured() && !secondResponse.ErrorOccured());
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
                    Response<ServicePurchase> response = testsObject.buyerFacade.PurchaseCartContent(member3Id);
                    Assert.IsTrue(!response.ErrorOccured());
                });
                // shop of the store owner is closed 
                yield return new TestCaseData((MemberReqsTests testsObject) =>
                {
                    Response<bool> response = testsObject.storeManagementFacade.CloseStore(member2Id, storeId);
                    Assert.IsTrue(!response.ErrorOccured());
                });
                // store owner is removed from the store 
                yield return new TestCaseData((MemberReqsTests testsObject) =>
                {
                    Response<bool> response = testsObject.storeManagementFacade.RemoveCoOwner(member2Id, member5Id, storeId);
                    Assert.IsTrue(!response.ErrorOccured());
                });
            }
        }

        // r I 5
        [Test]
        [TestCaseSource("DataSuccessfulNotifications")]
        public void SuccessfulNotidications(Action<MemberReqsTests> action)
        {
            // member5 is a store owner in store of storeId

            // getting notifications before
            IList<string> notificationsBefore = member5Notifications.ToList();

            action(this); // performing an action for which member5 should be notified 

            IList<string> notificationsAfter = member5Notifications.ToList();

            // checking the new notifications with the the notifications that were before:
            IList<string> notificationsIntersection = notificationsBefore.Intersect(notificationsAfter).ToList();
            Assert.AreEqual(notificationsBefore.Count, notificationsIntersection.Count);
            Assert.IsTrue(notificationsBefore.Count < notificationsAfter.Count);
        }

        // todo: add tests for notifications on failed actions 
    }
}
