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

    }
}
