using MarketBackend.ServiceLayer.ServiceDTO;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMarketBackend.Acceptance
{
    internal class StoreOwner_ManagerReqsTests : AcceptanceTests
    {

        public static IEnumerable<TestCaseData> DataSuccessfulProductAdditionToStore
        {
            get
            {
                yield return new TestCaseData(member2Id, storeId, "Gaming Chair", 800, "Gaming", 20);
            }
        }

        // r.4.1
        [Test]
        [TestCaseSource("DataSuccessfulProductAdditionToStore")]
        public void SuccessfulProductAdditionToStore(
            int userId, int storeId, string productName, double price, string category, int amount)
        {
            // Adding the product
            Response<int> productIdResponse = storeManagementFacade.AddNewProduct(userId, storeId, productName, price, category);

            Assert.IsTrue(!productIdResponse.ErrorOccured());

            int productId = productIdResponse.Value;

            Response<bool> response = storeManagementFacade.AddProductToInventory(userId, storeId, productId, amount);

            Assert.IsTrue(!response.ErrorOccured());

            // Checking that it is available
            response = buyerFacade.AddProdcutToCart(userId, storeId, productId, 5);

            Assert.IsTrue(!response.ErrorOccured());
        }

        // r.4.1
        [Test]
        public void SuccessfulProductDetailsUpdate()
        {

        }

        // r.4.1
        [Test]
        public void SuccessfulProductRemovalFromStore()
        {

        }

        public static IEnumerable<TestCaseData> DataFailedInvalidProductAdditionToStore
        {
            get
            {
                yield return new TestCaseData(member2Id, storeId, "124@#$!@$4444", 800, "Gaming");
                yield return new TestCaseData(member2Id, storeId, "Gaming Chair", 800, "!@#$eddddddd");
                yield return new TestCaseData(member2Id, storeId, "Gaming Chair", -400, "Gaming");
            }
        }

        // r.4.1
        [Test]
        [TestCaseSource("DataFailedInvalidProductAdditionToStore")]
        public void FailedInvalidProductAdditionToStore(
            int userId, int storeId, string productName, double price, string category)
        {
            // Adding the product
            Response<int> productIdResponse = storeManagementFacade.AddNewProduct(userId, storeId, productName, price, category);

            Assert.IsTrue(productIdResponse.ErrorOccured());
        }

        public static IEnumerable<TestCaseData> DataFailedProductDecrease
        {
            get
            {
                yield return new TestCaseData(member2Id, storeId, iphoneProductId, 99999);
                yield return new TestCaseData(member2Id, storeId, 123, 1);
            }
        }

        // r.4.1
        [Test]
        [TestCaseSource("DataFailedProductDecrease")]
        public void FailedProductDecrease(int userId, int storeId, int productId, int amount)
        {
            Response<bool> response = storeManagementFacade.DecreaseProduct(userId, storeId, productId, amount);

            Assert.IsTrue(response.ErrorOccured());
        }

        // r.4.4
        [Test]
        public void SuccessfulStoreOwnerAppointment()
        {

        }

        // r.4.4
        [Test]
        public void FailedStoreOwnerAppointment()
        {

        }

        // r.4.6
        [Test]
        public void SuccessfulStoreManagerAppointment()
        {

        }

        // r.4.6
        [Test]
        public void FailedStoreManagerAppointment()
        {

        }

        // r.4.7
        [Test]
        public void SuccessfulStoreManagerPermissionsAddition()
        {

        }

        // r.4.7
        [Test]
        public void FailedStoreManagerPermissionsAddition()
        {

        }

        // r.4.7
        [Test]
        public void FailedStoreManagerPermissionsAdditionWhenHeAlreadyHasThem()
        {

        }

        // r.4.7
        [Test]
        public void FailedStoreManagerPermissionsRemovalWhenHeDoesntHaveThem()
        {

        }

        // r.4.9
        public void SuccessfulStoreClosing()
        {

        }

        // r.4.9
        public void FailedClosedStoreClosing()
        {

        }

        // r.4.11
        public void SuccessfulGetStorePersonnelInformation()
        {

        }

        // r.4.13
        public void SuccessfulGetStorePurchaseHistoryInformation()
        {

        }
    }
}
