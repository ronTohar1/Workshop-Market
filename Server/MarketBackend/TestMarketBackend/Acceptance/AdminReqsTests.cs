using MarketBackend.BusinessLayer.Market.StoreManagment;
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

        public static IEnumerable<TestCaseData> DataFailedGetBuyerPurchaseHistory
        {
            get
            {
                yield return new TestCaseData(new Func<int>(() => -1), new Func<int>(() => member1Id));
                yield return new TestCaseData(new Func<int>(() => adminId), new Func<int>(() => -1));
            }
        }

        // r 6.4
        [Test]
        [TestCaseSource("DataFailedGetBuyerPurchaseHistory")]
        public void FailedGetBuyerPurchaseHistory(Func<int> adminId, Func<int> memberId)
        {
            Response<IReadOnlyCollection<ServicePurchase>> response = adminFacade.GetStorePurchaseHistory(adminId(), memberId());

            Assert.IsTrue(response.ErrorOccured());
        }

        // r 6.4
        [Test]
        public void SuccessfulGetBuyerPurchaseHistory()
        {

            // first check that current purchase history is empty
            Response<IReadOnlyCollection<ServicePurchase>> response = adminFacade.GetStorePurchaseHistory(adminId, member1Id);

            Assert.IsTrue(!response.ErrorOccured());
            Assert.IsEmpty(response.Value);

            // now check that after purchase that it is returned
            SetUpShoppingCarts();
            response = adminFacade.GetStorePurchaseHistory(adminId, member1Id);
            Assert.IsTrue(!response.ErrorOccured());
            Assert.AreEqual(1, response.Value.Count);
            ServicePurchase purchaseResult = response.Value.First();
            // checking the purchase was made at most a minute ago
            Assert.IsTrue(DateTime.Now - purchaseResult.purchaseDate < TimeSpan.FromMinutes(1));
            // price is supposed to be 8500
            Assert.AreEqual(8500, purchaseResult.purchasePrice);
        }

        public static IEnumerable<TestCaseData> DataFailedRemoveMember
        {
            get
            {
                // requesting is not an admin (and target is a storeOwner) 
                yield return new TestCaseData(new Func<int>(() => member3Id), new Func<int>(() => storeOwnerId));
                // target is not a member but is a guest
                yield return new TestCaseData(new Func<int>(() => adminId), new Func<int>(() => guest1Id));
                // target is not a member
                yield return new TestCaseData(new Func<int>(() => adminId), new Func<int>(() => -1));
                // target is the last admin in the system 
                yield return new TestCaseData(new Func<int>(() => adminId), new Func<int>(() => adminId));
                // target is the last storeOwner in a store
                yield return new TestCaseData(new Func<int>(() => adminId), new Func<int>(() => store2OwnerId)); 
            }
        }

        // r 6.2, r 4.5 // todo: move r 4.5 to the succesful tests functions
        // cc 2, cc 5
        [Test]
        [TestCaseSource("DataFailedRemoveMember")]
        public void FailedRemoveMember(Func<int> requestingId, Func<int> memberToRemoveId)
        {
            Response<bool> memberExistsResponse = adminFacade.MemberExists(memberToRemoveId());
            Assert.IsTrue(!memberExistsResponse.ErrorOccured());
            bool memberExsited = memberExistsResponse.Value;
            IDictionary<int, Role> rolesInStoresBefore = GetRolesInStores(memberToRemoveId()); 

            Response<bool> response = adminFacade.RemoveMember(requestingId(), memberToRemoveId()); 

            Assert.IsTrue(response.ErrorOccured());

            // checking that member still exists if existed before
            memberExistsResponse = adminFacade.MemberExists(memberToRemoveId());
            Assert.IsTrue(!memberExistsResponse.ErrorOccured());
            Assert.AreEqual(memberExsited, memberExistsResponse.Value);
            // checking that member still has the same roles
            IDictionary<int, Role> rolesInStores = GetRolesInStores(memberToRemoveId());
            foreach (int storeId in storesIds)
            {
                Assert.AreEqual(rolesInStoresBefore.ContainsKey(storeId), rolesInStores.ContainsKey(storeId));
                if (rolesInStoresBefore.ContainsKey(storeId))
                {
                    Assert.AreEqual(rolesInStoresBefore[storeId], rolesInStores[storeId]); 
                }
            }
        }

        private int GetStoreIdByName(string storeName)
        {
            Response<ServiceStore> response = buyerFacade.GetStoreInfo(storeName);
            if (response.ErrorOccured())
                throw new Exception(response.ErrorMessage);
            return response.Value.Id; 
        }

        private IDictionary<int, Role> GetRolesInStores(int memberId)
        {
            int storeOwnerId = member2Id; // should be a store owner in all stores 
            Array roles = Enum.GetValues(typeof(Role));

            IDictionary<int, Role> rolesinStores = new Dictionary<int, Role>();

            foreach (int storeId in storesIds)
            {
                foreach (Role role in roles)
                {
                    Response<IList<int>> response = storeManagementFacade.GetMembersInRole(storeId, storeOwnerId, role);
                    if (response.ErrorOccured())
                        throw new Exception(response.ErrorMessage);
                    if (response.Value.Contains(memberId))
                        rolesinStores[storeId] = role;
                }
            }

            return rolesinStores;
        }
    }
}
