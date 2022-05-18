using MarketBackend.ServiceLayer.ServiceDTO;
using MarketBackend.BusinessLayer.Market.StoreManagment;
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
                yield return new TestCaseData(new Func<int>(() => storeOwnerId), new Func<int>(() => storeId), "Gaming Chair", 800, "Gaming", 20);
            }
        }

        // r.4.1
        [Test]
        [TestCaseSource("DataSuccessfulProductAdditionToStore")]
        public void SuccessfulProductAdditionToStore(
           Func<int> userId1, Func<int> storeId1, string productName, double price, string category, int amount)
        {
            int userId = userId1();
            int storeId = storeId1();
            // Adding the product
            Response<int> productIdResponse =
                storeManagementFacade.AddNewProduct(userId, storeId, productName, price, category);

            Assert.IsTrue(!productIdResponse.ErrorOccured());

            int productId = productIdResponse.Value;

            Response<bool> response =
                storeManagementFacade.AddProductToInventory(userId, storeId, productId, amount);

            Assert.IsTrue(!response.ErrorOccured());

            // Checking that it is available
            response = buyerFacade.AddProdcutToCart(userId, storeId, productId, 5);

            Assert.IsTrue(!response.ErrorOccured());
        }

        /*TODO
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
        */

        public static IEnumerable<TestCaseData> DataSuccessfulProductDecreaseInStore
        {
            get
            {
                yield return new TestCaseData(new Func<int>(() => storeOwnerId), new Func<int>(() => storeId), new Func<int>(() => iphoneProductId), new Func<int>(() => iphoneProductAmount / 2));
                yield return new TestCaseData(new Func<int>(() => storeOwnerId), new Func<int>(() => storeId), new Func<int>(() => calculatorProductId), new Func<int>(() => calculatorProductAmount / 2));
            }
        }

        // r.4.1
        [Test]
        [TestCaseSource("DataSuccessfulProductDecreaseInStore")]
        public void SuccessfulProductDecreaseInStore(Func<int> ownerId, Func<int> storeId, Func<int> productId, Func<int> amount)
        {
            Response<bool> response = storeManagementFacade.DecreaseProduct(ownerId(), storeId(), productId(), amount());

            Assert.IsTrue(!response.ErrorOccured());
        }

        public static IEnumerable<TestCaseData> DataFailedInvalidProductAdditionToStore
        {
            get
            {
                //yield return new TestCaseData(new Func<int>(() => storeOwnerId), new Func<int>(() => storeId), "124@#$!@$4444", 800, "Gaming");
                //yield return new TestCaseData(new Func<int>(() => storeOwnerId), new Func<int>(() => storeId), "Gaming Chair", 800, "!@#$eddddddd");
                yield return new TestCaseData(new Func<int>(() => storeOwnerId), new Func<int>(() => storeId), "Gaming Chair", -400, "Gaming");
            }
        }

        // r.4.1
        [Test]
        [TestCaseSource("DataFailedInvalidProductAdditionToStore")]
        public void FailedInvalidProductAdditionToStore(
            Func<int> userId1, Func<int> storeId1, string productName, double price, string category)
        {
            int userId = userId1();
            int storeId = storeId1();
            // Adding the product
            Response<int> productIdResponse =
                storeManagementFacade.AddNewProduct(userId, storeId, productName, price, category);

            Assert.IsTrue(productIdResponse.ErrorOccured());
        }

        public static IEnumerable<TestCaseData> DataFailedProductDecrease
        {
            get
            {
                yield return new TestCaseData(storeOwnerId, storeId, iphoneProductId, 99999);
                yield return new TestCaseData(storeOwnerId, storeId, 123, 1);
            }
        }

        // r.4.1
        [Test]
        [TestCaseSource("DataFailedProductDecrease")]
        public void FailedProductDecrease(int userId, int storeId, int productId, int amount)
        {
            Response<bool> response =
                storeManagementFacade.DecreaseProduct(userId, storeId, productId, amount);

            Assert.IsTrue(response.ErrorOccured());
        }

        private bool MemberIsRoleInStore(int storeOwnerId, int memberId, int storeId, Role role)
        {
            Response<IList<int>> ownersResponse =
                storeManagementFacade.GetMembersInRole(storeId, storeOwnerId, role);

            Assert.IsTrue(!ownersResponse.ErrorOccured());

            IList<int> ownersIds = ownersResponse.Value;
            return ownersIds.Contains(memberId);
        }

        // r.4.4
        [Test]
        public void SuccessfulStoreOwnerAppointment()
        {
            Response<bool> response = storeManagementFacade.MakeCoOwner(storeOwnerId, member1Id, storeId);

            Assert.IsTrue(!response.ErrorOccured());
            Assert.IsTrue(MemberIsRoleInStore(storeOwnerId, member1Id, storeId, Role.Owner));
        }

        // r.4.4
        [Test]
        public void FailedStoreOwnerAppointment()
        {
            Response<IList<int>> ownersBefore =
                storeManagementFacade.GetMembersInRole(storeId, storeOwnerId, Role.Owner);

            // Appointing a store owner as a store owner
            Response<bool> response = storeManagementFacade.MakeCoOwner(storeOwnerId, member3Id, storeId);

            Response<IList<int>> ownersAfter =
                storeManagementFacade.GetMembersInRole(storeId, storeOwnerId, Role.Owner);

            Assert.IsTrue(response.ErrorOccured());
            Assert.IsTrue(SameElements(ownersBefore.Value, ownersAfter.Value));
        }

        // r 4.4
        // r S 5
        [Test]
        [TestCase(2)]
        [TestCase(10)]
        [TestCase(50)]
        public void ConcurrentStoreOwnerAppointment(int threadsNumber)
        {
            // todo: maybe add more test cases on other things such as different members, different stores etc. 
            Response<IList<int>> ownersBefore =
                storeManagementFacade.GetMembersInRole(storeId, storeOwnerId, Role.Owner);

            // Appointing the same member by many threads 
            Func<Response<bool>>[] jobs =
                Enumerable.Repeat(() => storeManagementFacade.MakeCoOwner(storeOwnerId, member1Id, storeId), threadsNumber).ToArray();

            Response<bool>[] responses = GetResponsesFromThreads(jobs);

            Assert.IsTrue(Exactly1ResponseIsSuccessful(responses));

            Response<IList<int>> ownersAfter =
                storeManagementFacade.GetMembersInRole(storeId, storeOwnerId, Role.Owner);

            IList<int> expectedOwnersAfter = ownersBefore.Value;
            expectedOwnersAfter.Add(member1Id);

            Assert.IsTrue(SameElements(expectedOwnersAfter, ownersAfter.Value));
        }

        // succesful, need to check:

        //      the store owner removed is removed
        //      the store owners it appointed are removed 
        //      the store managers it appointed are removed

        // concurrent tests for removing the store owner 

        public static IEnumerable<TestCaseData> DataFailedRemoveCoOwnerAppointment
        {
            get
            {
                // remove not a buyer
                yield return new TestCaseData(() => member2Id, () => storeId, () => -1);
                // remove a guest
                yield return new TestCaseData(() => member2Id, () => storeId, () => guest1Id);
                // remove a member
                yield return new TestCaseData(() => member2Id, () => storeId, () => member1Id);
                // remove a manager (appointed by the requesting  co owner) 
                yield return new TestCaseData(() => member2Id, () => storeId, () => member4Id);
                // remove a store owner that it not appointed by the requsting co owner 
                yield return new TestCaseData(() => member3Id, () => storeId, () => member2Id);
                // not a storeId
                // nice to have to do sometime maybe, not doing this at the moment so we can check the roles in the store before and after the test
                // by not a buyer
                yield return new TestCaseData(() => -1, () => storeId, () => member2Id);
                // by a guest
                yield return new TestCaseData(() => guest1Id, () => storeId, () => member2Id);
                // by a member (that is not a co owner or manager in the store)
                yield return new TestCaseData(() => member1Id, () => storeId, () => member2Id);
                // by a manager
                yield return new TestCaseData(() => member4Id, () => storeId, () => member2Id);
            }
        }

        // r 4.5
        [Test]
        [TestCaseSource("DataFailedRemoveCoOwnerAppointment")]
        public void FailedRemoveCoOwnerAppointment(Func<int> requestingId, Func<int> storeId, Func<int> coOwnerToRemoveId)
        {
            // getting roles before to check the roles after the action 
            IDictionary<Role, IList<int>> rolesBefore = GetRolesInStore(storeId()); 

            Response<bool> response = storeManagementFacade.RemoveCoOwner(requestingId(), coOwnerToRemoveId(), storeId());

            Assert.IsTrue(response.ErrorOccured());

            // check that roles in the store remained as before 
            IDictionary<Role, IList<int>> roles = GetRolesInStore(storeId());

            Assert.IsTrue(SameDictionariesWithLists(rolesBefore, roles)); 
        }

        // r 4.5
        [Test]
        public void SuccessfulRemoveCoOwnerAppointment()
        {
            int requestingId = member2Id;
            // using storeId
            int coOwnerToRemoveId = member5Id; // appointed by 2, and appointed 6 and 7

            // getting roles before to check the roles after the action 
            IDictionary<Role, IList<int>> expectedRoles = GetRolesInStore(storeId);
            expectedRoles[Role.Owner].Remove(coOwnerToRemoveId);
            expectedRoles[Role.Owner].Remove(member6Id);
            expectedRoles[Role.Manager].Remove(member7Id);

            Response<bool> response = storeManagementFacade.RemoveCoOwner(requestingId, coOwnerToRemoveId, storeId);
            Assert.IsTrue(!response.ErrorOccured());

            // check that roles in the store where changed as needed 
            IDictionary<Role, IList<int>> roles = GetRolesInStore(storeId);

            Assert.IsTrue(SameDictionariesWithLists(expectedRoles, roles));
        }

        // r 4.5
        // r S 5
        [Test]
        [TestCase(2)]
        [TestCase(10)]
        [TestCase(50)]
        public void ConcurrentRemoveCoOwnerAppointment(int threadsNumber)
        {
            int requestingId = member2Id;
            // using storeId
            int coOwnerToRemoveId = member5Id; // appointed by 2, and appointed 6 and 7

            // getting roles before to check the roles after the action 
            IDictionary<Role, IList<int>> expectedRoles = GetRolesInStore(storeId);
            expectedRoles[Role.Owner].Remove(coOwnerToRemoveId);
            expectedRoles[Role.Owner].Remove(member6Id);
            expectedRoles[Role.Manager].Remove(member7Id);

            Func<Response<bool>>[] jobs =
                Enumerable.Repeat(() => storeManagementFacade.RemoveCoOwner(requestingId, coOwnerToRemoveId, storeId), threadsNumber).ToArray();
            Response<bool>[] responses = GetResponsesFromThreads(jobs);
            Assert.IsTrue(Exactly1ResponseIsSuccessful(responses));

            // check that roles in the store where changed as needed 
            IDictionary<Role, IList<int>> roles = GetRolesInStore(storeId);

            Assert.IsTrue(SameDictionariesWithLists(expectedRoles, roles));
        }

        // r.4.6
        [Test]
        public void SuccessfulStoreManagerAppointment()
        {
            Response<bool> response = storeManagementFacade.MakeCoManager(storeOwnerId, member1Id, storeId);

            Assert.IsTrue(!response.ErrorOccured());
            Assert.IsTrue(MemberIsRoleInStore(storeOwnerId, member1Id, storeId, Role.Manager));
        }

        // r.4.6
        [Test]
        public void FailedStoreManagerAppointment()
        {
            Response<IList<int>> managersBefore =
                storeManagementFacade.GetMembersInRole(storeId, storeOwnerId, Role.Manager);

            // Appointing a store manager as a store manager
            Response<bool> response = storeManagementFacade.MakeCoManager(storeOwnerId, member4Id, storeId);

            Response<IList<int>> managersAfter =
                storeManagementFacade.GetMembersInRole(storeId, storeOwnerId, Role.Manager);

            Assert.IsTrue(response.ErrorOccured());
            Assert.IsTrue(SameElements(managersBefore.Value, managersAfter.Value));
        }

        public static IEnumerable<TestCaseData> DataSuccessfulStoreManagerPermissionsAddition
        {
            get
            {
                yield return new TestCaseData(new List<Permission>()
                    { Permission.RecieveInfo, Permission.RecieiveRolesInfo });
                yield return new TestCaseData(new List<Permission>()
                    { Permission.RecieiveRolesInfo });
                yield return new TestCaseData(new List<Permission>()
                    { Permission.MakeCoOwner });
                yield return new TestCaseData(new List<Permission>()
                    { Permission.RemoveCoManager });
            }
        }

        // r.4.7
        [Test]
        [TestCaseSource("DataSuccessfulStoreManagerPermissionsAddition")]
        public void SuccessfulStoreManagerPermissionsAddition(IList<Permission> permissions)
        {
            Response<bool> response =
                storeManagementFacade.ChangeManagerPermission(storeOwnerId, member4Id, storeId, permissions);

            Response<IList<Permission>> newPermissionResponse =
                storeManagementFacade.GetManagerPermissions(storeId, storeOwnerId, member4Id);

            Assert.IsTrue(!response.ErrorOccured() && !newPermissionResponse.ErrorOccured());
            Assert.IsTrue(SameElements(newPermissionResponse.Value, permissions));
        }
        /*TODO
        // r.4.9
        public void SuccessfulStoreClosing()
        {
            Response<bool> response = storeManagementFacade.CloseStore(storeOwnerId, storeId);

            Assert.IsTrue(!response.ErrorOccured());

            // After closing, only owners and system managers can get any information about the store
            response = storeManagementFacade.GetPurchaseHistory(member4Id, storeId);

            Assert.IsTrue(response.ErrorOccured());
        }

        // r.4.9
        public void FailedClosedStoreClosing()
        {
            Response<bool> response = storeManagementFacade.CloseStore(storeOwnerId, storeId);

            Assert.IsTrue(!response.ErrorOccured());

            // closing an already closed store
            response = storeManagementFacade.CloseStore(storeOwnerId, storeId);

            Assert.IsTrue(response.ErrorOccured());
        }
        */
        // r.4.11

        [Test]
        public void SuccessfulGetStorePersonnelInformation()
        {
            Response<IList<int>> managers =
                storeManagementFacade.GetMembersInRole(storeId, storeOwnerId, Role.Manager);

            Assert.IsTrue(!managers.ErrorOccured());
            Assert.IsTrue(SameElements(managers.Value, new List<int>() { member4Id , member7Id}));

            Response<IList<int>> owners =
                storeManagementFacade.GetMembersInRole(storeId, storeOwnerId, Role.Owner);

            Assert.IsTrue(!owners.ErrorOccured());
            Assert.IsTrue(SameElements(owners.Value, new List<int>() { storeOwnerId, member3Id, member5Id, member6Id }));
        }

        [Test]
        // r.4.13
        public void SuccessfulGetStorePurchaseHistoryInformation()
        {
            Response<IList<Purchase>> purchaseHistory =
                storeManagementFacade.GetPurchaseHistory(storeOwnerId, storeId);

            Assert.IsTrue(!purchaseHistory.ErrorOccured());
        }

        private IDictionary<Role, IList<int>> GetRolesInStore(int storeId)
        {
            IDictionary<Role, IList<int>> roles = new Dictionary<Role, IList<int>>();
            foreach (Role role in Enum.GetValues(typeof(Role)))
            {
                Response<IList<int>> membersInRoleResponse = storeManagementFacade.GetMembersInRole(storeId, member2Id, role); // notice member2 is co owner in all stores 
                Assert.IsTrue(!membersInRoleResponse.ErrorOccured());
                roles[role] = membersInRoleResponse.Value;
            }
            return roles;
        }

    }
}
