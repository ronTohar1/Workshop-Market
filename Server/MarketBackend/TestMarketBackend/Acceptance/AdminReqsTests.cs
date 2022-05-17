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

        // r 6.2
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
            // todo: maybe add a check that it should be an admin if it was before  
        }

        public static IEnumerable<TestCaseData> DataSuccessfulRemoveMember
        {
            get
            {
                // removing member that has no roles
                yield return new TestCaseData(new Func<int>(() => adminId), new Func<int>(() => member1Id));
                // removing store owner
                yield return new TestCaseData(new Func<int>(() => adminId), new Func<int>(() => member3Id));
                // removing a store owner that appointed others
                yield return new TestCaseData(new Func<int>(() => adminId), new Func<int>(() => member2Id));
                // removing manager
                yield return new TestCaseData(new Func<int>(() => adminId), new Func<int>(() => member4Id));
                // removing admin
                // todo: implement (there needs to be added another admin so it won't be the last)
            }
        }

        // r 6.2
        [Test]
        [TestCaseSource("DataSuccessfulRemoveMember")]
        public void SuccessfulRemoveMember(Func<int> requestingId, Func<int> memberToRemoveId)
        {
            Response<bool> response = adminFacade.RemoveMember(requestingId(), memberToRemoveId());

            Assert.IsTrue(!response.ErrorOccured());

            // checking that member does not exist
            Response<bool> memberExistsResponse = adminFacade.MemberExists(memberToRemoveId());
            Assert.IsTrue(!memberExistsResponse.ErrorOccured());
            Assert.IsTrue(!memberExistsResponse.Value);

            // checking that member does not have roles in stores 
            IDictionary<int, Role> rolesInStores = GetRolesInStores(memberToRemoveId());
            Assert.IsEmpty(rolesInStores.Keys); 
            // todo: maybe add a check that is not an admin 
        }

        // r 6.2, r 4.5
        [Test]
        public void SuccessfulRemoveMemberThatAppointedStoreOwners()
        {
            int requestingId = adminId;
            int memberToRemoveId = member2Id;
            int storeId = AcceptanceTests.storeId;
            int[] appointedStoreOwnersIds = { member3Id, member4Id }; 

            Response<bool> response = adminFacade.RemoveMember(requestingId, memberToRemoveId);

            Assert.IsTrue(!response.ErrorOccured());

            // checking that member does not exist
            Response<bool> memberExistsResponse = adminFacade.MemberExists(memberToRemoveId);
            Assert.IsTrue(!memberExistsResponse.ErrorOccured());
            Assert.IsTrue(!memberExistsResponse.Value);

            // checking that member does not have roles in stores 
            IDictionary<int, Role> rolesInStores = GetRolesInStores(memberToRemoveId);
            Assert.IsEmpty(rolesInStores.Keys);
            // todo: maybe add a check that is not an admin

            // checking that the appointed store owners were removed from store: 
            Response<IList<int>> respones = storeManagementFacade.GetMembersInRole(storeId, adminId, Role.Owner); // the admin id because request needs permissions 
            Assert.IsTrue(!respones.ErrorOccured());
            foreach (int appointedStoreOwnerId in appointedStoreOwnersIds)
            {
                Assert.IsTrue(!respones.Value.Contains(appointedStoreOwnerId)); 
            }
        }

        // r 6.2
        // r S 5
        [Test]
        [TestCase(2)]
        [TestCase(10)]
        [TestCase(50)]
        public void ConcurrentfulRemoveMember(int threadsNumber)
        {
            int requestingId = adminId;
            int memberToRemoveId = member2Id; // member2 is a store owner in multipule stores, and appointed some other store owners

            Func<Response<bool>>[] jobs =
                Enumerable.Repeat(() => adminFacade.RemoveMember(requestingId, memberToRemoveId), threadsNumber).ToArray();

            Response<bool>[] responses = GetResponsesFromThreads(jobs);

            Assert.IsTrue(Exactly1ResponseIsSuccessful(responses));

            // checking that member does not exist
            Response<bool> memberExistsResponse = adminFacade.MemberExists(memberToRemoveId);
            Assert.IsTrue(!memberExistsResponse.ErrorOccured());
            Assert.IsTrue(!memberExistsResponse.Value);

            // checking that member does not have roles in stores 
            IDictionary<int, Role> rolesInStores = GetRolesInStores(memberToRemoveId);
            Assert.IsEmpty(rolesInStores.Keys);
            // todo: maybe add a check that is not an admin 
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
