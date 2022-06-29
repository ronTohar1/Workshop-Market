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
            Response<int> response = storeManagementFacade.OpenNewStore(member3Id, "VeryNewStoreName");

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
                jobs[i] = () => { return storeManagementFacade.OpenNewStore(ids[temp], storeName); };
            }

            Response<int>[] responses = GetResponsesFromThreads(jobs);

            Assert.IsTrue(Exactly1ResponseIsSuccessful(responses));
        }

        // r.3.2
        [Test]
        [TestCase("")]
        public void FailedCreationOfStoreWithInvalidDetails(string storeName)
        {
            Response<int> response = storeManagementFacade.OpenNewStore(member3Id, storeName);

            Assert.IsTrue(response.IsErrorOccured());
        }

        // r.3.2
        [Test]
        public void FailedCreationOfStoreThatAlreadyExists()
        {
            Response<int> response = storeManagementFacade.OpenNewStore(member3Id, storeName);

            Assert.IsTrue(response.IsErrorOccured());
        }

        // r.3.3
        [Test]
        [TestCase("The quality is great. Shipment was very quick. Would recommend.")]
        public void SuccessfulProductReview(string review)
        {
            int reviewsAmount = storeManagementFacade.GetProductReviews(storeId, iphoneProductId).Value.Count();

            Response<bool> response = buyerFacade.AddProductReview(member3Id, storeId, iphoneProductId, review);
            Assert.IsTrue(!response.IsErrorOccured());

            ReopenMarket();

            Assert.AreEqual(reviewsAmount + 1, storeManagementFacade.GetProductReviews(storeId, iphoneProductId).Value.Count());
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
            IDictionary<string, IList<string>> reviewsByUsernames = storeManagementFacade.GetProductReviews(storeId, iphoneProductId).Value;
            int reviewsAmount;
            if (!reviewsByUsernames.ContainsKey(userName3))
                reviewsAmount = 0;
            else
                reviewsAmount = reviewsByUsernames[userName3].Count(); 

            Response<bool> firstResponse = buyerFacade.AddProductReview(member3Id, storeId, iphoneProductId, review);

            Response<bool> secondResponse = buyerFacade.AddProductReview(member3Id, storeId, iphoneProductId, review);

            Assert.IsTrue(!firstResponse.IsErrorOccured() && !secondResponse.IsErrorOccured());

            ReopenMarket();

            Assert.AreEqual(reviewsAmount + 2, storeManagementFacade.GetProductReviews(storeId, iphoneProductId).Value[userName3].Count());
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


        public static IEnumerable<TestCaseData> DataBID
        {
            get
            {
                yield return new TestCaseData(new Func<int>(() => storeOwnerId), new Func<int>(() => storeId), new Func<int>(() => iphoneProductId), new Func<int>(() => member5Id ), new Func<int>(() => member3Id));
            }
        }


        [Test]
        [TestCaseSource("DataBID")]
        public void SuccessfulyBidRemovedCO_OWNER(Func<int> storeOwner, Func<int> storeId, Func<int> productToBid, Func<int> coOwner, Func<int> bidderId )
        {
            // logging out so we can use the same data as in the SuccessfulNotidicationsLoggedIn tests 
            Response<int> response = storeManagementFacade.AddBid(storeId(),productToBid(),bidderId(),10);
            Assert.IsTrue(!response.IsErrorOccured());

            // getting notifications before
            IList<string> notificationsBefore = member3Notifications.ToList();
            Response<bool> approved = storeManagementFacade.ApproveBid(storeId(), storeOwner(), response.Value);
            Assert.IsFalse(approved.IsErrorOccured());
            Assert.IsTrue(approved.Value);

            //Response<bool> removed1 = storeManagementFacade.RemoveCoOwner(storeOwner(), member6Id, storeId());
            //Assert.IsFalse(removed1.IsErrorOccured());
            //Assert.IsTrue(removed1.Value);
            Response<bool> removed2 = storeManagementFacade.RemoveCoOwner(storeOwner(), member3Id, storeId());
            Assert.IsFalse(removed2.IsErrorOccured());
            Assert.IsTrue(removed2.Value);
            Response<bool> removed = storeManagementFacade.RemoveCoOwner(storeOwner(), coOwner(), storeId());

            Assert.IsFalse(removed.IsErrorOccured());
            Assert.IsTrue(removed.Value);
            // first checking that there is not a notification when not logged in 
            IList<string> notificationsAfter = member3Notifications.ToList();
            Assert.IsFalse(SameElements(notificationsBefore, notificationsAfter));

            //// second checking there is a notification after loggin in
            //bool notificationArrived = false;
            //buyerFacade.Login(userName5, password5, notification => { notificationArrived = true; return true; });
            //// checking the new notifications with the the notifications that were before:
            //Assert.IsTrue(notificationArrived);
        }
    }
}
