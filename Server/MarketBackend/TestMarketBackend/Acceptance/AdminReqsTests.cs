using MarketBackend.ServiceLayer.ServiceDTO;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMarketBackend.Acceptance
{
    internal class AdminReqsTests : AcceptanceTests
    {

        public static IEnumerable<TestCaseData> DataFailedGetStorePurchaseHistory
        {
            get
            {
                yield return new TestCaseData(new Func<int>(() => -1), new Func<int>(() => storeId));
                yield return new TestCaseData(new Func<int>(() => adminId), new Func<int>(() => -1));
            }
        }

        // r 6.4
        [Test]
        [TestCaseSource("DataFailedGetStorePurchaseHistory")]
        public void FailedGetStorePurchaseHistory(Func<int> adminId, Func<int> storeId)
        {
            Response<IReadOnlyCollection<ServicePurchase>> response = adminFacade.GetStorePurchaseHistory(adminId(), storeId());

            Assert.IsTrue(response.ErrorOccured());
        }

        public static IEnumerable<TestCaseData> SuccessfulFailedGetStorePurchaseHistory
        {
            get
            {
                yield return new TestCaseData(new List<ServicePurchase>());
                yield return new TestCaseData(new List<ServicePurchase>() { new ServicePurchase(DateTime.Now, 10, "bouht milk") }); 
            }
        }
        // r 6.4
        [Test]
        public void SuccessfulGetStorePurchaseHistory()
        {

            // first check that current purchase history is empty
            Response<IReadOnlyCollection<ServicePurchase>> response = adminFacade.GetStorePurchaseHistory(adminId, storeId);

            Assert.IsTrue(!response.ErrorOccured());
            Assert.IsEmpty(response.Value);

            // now check that after purchase that it is returned
            SetUpShoppingCarts();
            response = adminFacade.GetStorePurchaseHistory(adminId, storeId);
            Assert.IsTrue(!response.ErrorOccured());
            Assert.AreEqual(1, response.Value.Count); 
            ServicePurchase purchaseResult = response.Value.First();
            // checking the purchase was made at most a minute ago
            Assert.IsTrue(DateTime.Now - purchaseResult.purchaseDate < TimeSpan.FromMinutes(1));
            // price is supposed to be 8500
            Assert.AreEqual(8500, purchaseResult.purchasePrice);
        }

        private int GetStoreIdByName(string storeName)
        {
            Response<ServiceStore> response = buyerFacade.GetStoreInfo(storeName);
            if (response.ErrorOccured())
                throw new Exception(response.ErrorMessage);
            return response.Value.Id; 
        }
    }
}
