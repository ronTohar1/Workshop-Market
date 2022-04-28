using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Moq;
using MarketBackend.BusinessLayer.Market.StoreManagment;
using MarketBackend.BusinessLayer.Buyers.Members;
using MarketBackend.BusinessLayer.Market.StoreManagment;

namespace TestMarketBackend.BusinessLayer.Market.StoreManagment
{
    public class StoreTests
    {
        private Store store;

        private Member founder;
        private Mock<Member> founderMock;
        private const int founderMemberId = 0; 

        private Func<int, Member> memberGetter; 

        private const string storeName = "moreIsStore";

        // the comments in the following lines are relevant after running SetupStoreFull()
        private const int coOwnerId1 = 1;
        private const int coOwnerId2 = 2; 
        private const int managerId1 = 3; // all permissions
        private const int managerId2 = 4; // defualt permissions 
        private const int memberId1 = 5;
        private const int memberId2 = 6;
        private const int memberId3 = 7;
        private int[] membersIds = {coOwnerId1, coOwnerId2, managerId1, managerId2, memberId1, memberId2, memberId3};
        private const int notAMemberId1 = 11;
        private const int notAMemberId2 = 12;
        private const int notAMemberId3 = 13;

        private static IList<Permission> allPermissions = Enum.GetValues(typeof(Permission)).Cast<Permission>().ToList();

        // ----------- Setup helping functions -----------------------------

        private Member setupMcokedMember(int memberId)
        {
            Mock<Member> memberMock = new Mock<Member>(); // todo: add arguments to send to constructor

            memberMock.Setup(member =>
                member.GetId()).
                    Returns(memberId);

            return memberMock.Object;
        }

        private Member[] setupMembers(int[] membersIds)
        {
            Member[] members = new Member[membersIds.Length];

            for (int i = 0; i < membersIds.Length; i++)
            {
                members[i] = setupMcokedMember(membersIds[i]); 
            }

            return members; 
        }

        private void setupMcokedFounder()
        {
            founder = setupMcokedMember(founderMemberId); 
        }

        private void setupMemberGetter(int[] membersIds)
        {
            if (membersIds.Contains(founderMemberId))
                throw new ArgumentException("The id: " + founderMemberId + " is taken, its the founder meberId");
            if (membersIds.Length != membersIds.Distinct().Count()) // if contains duplicates from stackoverflow
                throw new ArgumentException("Members ids contain duplicates");

            Member[] members = setupMembers(membersIds);

            memberGetter = memberId =>
            {
                if (founder.GetId() == memberId)
                {
                    return founder;
                }
                int indexOfMember = Array.IndexOf(membersIds, memberId);
                if (indexOfMember < 0)
                {
                    return null;
                }
                return members[indexOfMember];
            };
        }

        private void SetupStoreNoRoles()
        {
            setupMcokedFounder();
            setupMemberGetter(membersIds);
            store = new Store(storeName, founder, memberGetter); 
        }

        private void SetupStoreNoPermissionsChange()
        {
            SetupStoreNoRoles();

            SetupMemberToCoOwner(coOwnerId1);
            SetupMemberToCoOwner(coOwnerId2);

            SetupMemberToManager(managerId1);
            SetupMemberToManager(managerId2);
        }

        private void SetupStoreFull()
        {
            SetupStoreNoPermissionsChange();

            SetupChangeMemberPermissions(managerId1, allPermissions);
            // managerId2 will be with the defualt permissions
        }

        private void SetupMemberToManagerWithAllPermissions(int memberId)
        {
            SetupMemberToManager(memberId, allPermissions); 
        }

        private void SetupMemberToManager(int memberId, IList<Permission> permissions)
        {
            store.MakeManager(founderMemberId, memberId);
            store.ChangeManagerPermissions(founderMemberId, memberId, permissions);
        }

        private void SetupChangeMemberPermissions(int managerMemberId, IList<Permission> newPermissions)
        {
            store.ChangeManagerPermissions(founderMemberId, managerMemberId, newPermissions);
        }

        // defualt permissions
        private void SetupMemberToManager(int memberId)
        {
            store.MakeManager(founderMemberId, memberId);
        }

        private void SetupMemberToCoOwner(int memberId)
        {
            store.MakeCoOwner(founderMemberId, memberId);
        }

        // ------- MakeCoOwner() ----------------------------------------

        // requesting not a member
        // requesting is member and not a manager
        // requesting is a manager with all permissions and not a coOwner

        // target is not a member
        // target is alerady a coOwner
        // target is already a manager
        // target is storeFounder

        // test that should pass with founder requesting
        // test that should pass


        // In these tests the set up is not done for all roles, just for the ones
        // needed for the tests because these are tests for the functions of appointing 
        // the roles

        [Test]
        [TestCase(notAMemberId1, memberId2)]
        [TestCase(memberId1, memberId2)]
        public void TestMakeCoOwnerByNoRole(int requestingMemberId, int newCoOwnerMemberId)
        {
            SetupStoreNoRoles();

            Assert.Throws<MarketException>(() => store.MakeCoOwner(requestingMemberId, newCoOwnerMemberId));
        }

        [Test]
        [TestCase(managerId1, memberId1)]
        [TestCase(managerId2, memberId1)]
        public void TestMakeCoOwnerByManager(int requestingMemberId, int newCoOwnerMemberId)
        {
            SetupStoreNoRoles();

            SetupMemberToManagerWithAllPermissions(requestingMemberId); 

            Assert.Throws<MarketException>(() => store.MakeCoOwner(requestingMemberId, newCoOwnerMemberId));
        }

        [Test]
        [TestCase(coOwnerId1, notAMemberId1)]
        [TestCase(coOwnerId1, coOwnerId2)]
        [TestCase(coOwnerId1, founderMemberId)]
        public void TestMakeCoOwnerTargetNotSuitable(int requestingMemberId, int newCoOwnerMemberId)
        {
            SetupStoreNoRoles();

            SetupMemberToCoOwner(coOwnerId1);
            SetupMemberToCoOwner(coOwnerId2);

            Assert.Throws<MarketException>(() => store.MakeCoOwner(requestingMemberId, newCoOwnerMemberId));
        }

        // doing this seperatly so from last test in order not to use MakeManager in the last
        // test's setup, so we can test the things there without using it
        [Test]
        [TestCase(coOwnerId1, managerId1)]
        public void TestMakeCoOwnerTargetIsManager(int requestingMemberId, int newCoOwnerMemberId)
        {
            SetupStoreNoRoles();

            SetupMemberToCoOwner(coOwnerId1);
            SetupMemberToCoOwner(coOwnerId2);
            SetupMemberToManagerWithAllPermissions(newCoOwnerMemberId); 

            Assert.Throws<MarketException>(() => store.MakeCoOwner(requestingMemberId, newCoOwnerMemberId));
        }

        [Test]
        [TestCase(coOwnerId1, memberId1)]
        public void TestMakeCoOwnerSholdPass(int requestingMemberId, int newCoOwnerMemberId)
        {
            SetupStoreNoRoles();

            store.MakeCoOwner(founderMemberId, requestingMemberId); // this is part of the testing
            Assert.IsTrue(store.IsCoOwner(requestingMemberId));
            
            store.MakeCoOwner(requestingMemberId, newCoOwnerMemberId);
            Assert.IsTrue(store.IsCoOwner(newCoOwnerMemberId));
        }

        // ------- MakeManager() ----------------------------------------

        // requesting not a member
        // requesting is member and not a manager
        // requesting is a manager with no permission for this

        // target is not a member
        // target is alerady a coOwner
        // target is already a manager
        // target is storeFounder

        // tests that should pass:
        //  founder requesting
        //  coOwner requesting
        //  manager with permission requesting


        // In these tests the set up is not done for all roles, just for the ones
        // needed for the tests because these are tests for the functions of appointing 
        // the roles

        [Test]
        [TestCase(notAMemberId1, memberId2)]
        [TestCase(memberId1, memberId2)]
        public void TestMakeManagerByNoRole(int requestingMemberId, int newManagerMemberId)
        {
            SetupStoreNoRoles();

            Assert.Throws<MarketException>(() => store.MakeManager(requestingMemberId, newManagerMemberId));
        }

        [Test]
        [TestCase(managerId1, memberId1, new Permission[] {})]
        [TestCase(managerId1, memberId1, new Permission[] {Permission.RecieveInfo})]
        public void TestMakeManagerByManagerWithNoPermission(int requestingMemberId, int newManagerMemberId, Permission[] reqestingManagerPermissions)
        {
            SetupStoreNoRoles();

            SetupMemberToManager(requestingMemberId, reqestingManagerPermissions);

            Assert.Throws<MarketException>(() => store.MakeManager(requestingMemberId, newManagerMemberId));
        }

        [Test]
        [TestCase(coOwnerId1, notAMemberId1)]
        [TestCase(coOwnerId1, coOwnerId2)]
        [TestCase(coOwnerId1, founderMemberId)]
        public void TestMakeManagerTargetNotSuitable(int requestingMemberId, int newManagerMemberId)
        {
            SetupStoreNoRoles();

            SetupMemberToCoOwner(coOwnerId1);
            SetupMemberToCoOwner(coOwnerId2);

            Assert.Throws<MarketException>(() => store.MakeManager(requestingMemberId, newManagerMemberId));
        }

        // doing this seperatly so from last test in order not to use MakeManager in the last
        // test's setup, so we can test the things there without using it
        [Test]
        [TestCase(coOwnerId1, managerId1)]
        public void TestMakeManagerTargetIsManager(int requestingMemberId, int newManagerMemberId)
        {
            SetupStoreNoRoles();

            SetupMemberToCoOwner(coOwnerId1);
            SetupMemberToManagerWithAllPermissions(newManagerMemberId);

            Assert.Throws<MarketException>(() => store.MakeManager(requestingMemberId, newManagerMemberId));
        }

        [Test]
        [TestCase(founderMemberId, memberId1)]
        [TestCase(coOwnerId1, memberId1)]
        public void TestMakeManagerSholdPassNotManagerRequesting(int requestingMemberId, int newManagerMemberId)
        {
            SetupStoreNoRoles();

            SetupMemberToCoOwner(coOwnerId1); 

            store.MakeManager(requestingMemberId, newManagerMemberId);
            Assert.IsTrue(store.IsManager(newManagerMemberId));
            AssertStartingManagerPermissions(newManagerMemberId);
        }

        // seperaating this from last test in order not to call MakeManager in the setup 
        // of the last test
        [Test]
        [TestCase(managerId1, memberId1)]
        public void TestMakeManagerSholdPassManagerRequesting(int requestingMemberId, int newManagerMemberId)
        {
            SetupStoreNoRoles();

            store.MakeManager(founderMemberId, requestingMemberId); // this is part of the testing
            Assert.IsTrue(store.IsManager(requestingMemberId));
            AssertStartingManagerPermissions(requestingMemberId);

            store.ChangeManagerPermissions(founderMemberId, requestingMemberId, new Permission[] { Permission.MakeCoManager });

            store.MakeManager(requestingMemberId, newManagerMemberId);
            Assert.IsTrue(store.IsManager(newManagerMemberId));
            AssertStartingManagerPermissions(newManagerMemberId); 
        }

        private void AssertStartingManagerPermissions(int newManagerId)
        {
            IList<Permission> expectedManagerPermissions = new List<Permission>() { Permission.RecieveInfo };
            Assert.IsTrue(SameElements(store.GetManagerPermissions(founderMemberId, newManagerId), expectedManagerPermissions));
        }

        // ------- ChangeManagerPermissions() ----------------------------------------

        // requesting not a member
        // requesting is member and not a manager
        // requesting is a manager with no permission for this

        // target is not a member
        // target is a coOwner
        // target is storeFounder
        
        // manager asks to change permissions to someone not an ancesster of him which is not him
        // and maybe that he can change only to permissions that he has

        // tests that should pass:

        //  founder requesting
        //  coOwner requesting
        //  manager with permission requesting (if we allow/put this in manager permissions) 

        //  adding permissions
        //  removeing permissions
        //  etc. 

        [Test]
        [TestCase(notAMemberId1, managerId2)]
        [TestCase(memberId1, managerId2)]
        [TestCase(managerId1, managerId2)] // assuming it is not in the defualt permissions
        [TestCase(coOwnerId1, notAMemberId2)]
        [TestCase(coOwnerId1, coOwnerId2)]
        [TestCase(coOwnerId1, founderMemberId)]
        public void TestChangeManagerPermissionsShouldFail(int requestingMemberId, int managerId)
        {
            SetupStoreNoPermissionsChange();

            Assert.Throws<MarketException>(() => store.MakeManager(requestingMemberId, managerId));
        }

        // todo: myabe do the test described in the next lines:
        // manager asks to change permissions to someone not an ancesster of him which is not him
        // and maybe that he can change only to permissions that he has

        //// seperaating this from last test in order not to call ChangeManagerPermissions in the setup 
        //// of the last test
        //[Test]
        //[TestCase(managerId1, managerId2)]
        //public void TestChangeManagerPermissionsSholdPassManagerRequesting(int requestingMemberId, int managerId)
        //{
        //    SetupStoreNoRoles();

        //    store.ChangeManagerPermissions(founderMemberId, requestingMemberId); // this is part of the testing
        //    Assert.IsTrue(store.IsManager(requestingMemberId));
        //    AssertStartingManagerPermissions(requestingMemberId);

        //    store.ChangeManagerPermission(founderMemberId, requestingMemberId, new Permission[] { Permission.MakeCoManager });

        //    store.ChangeManagerPermissions(requestingMemberId, managerId);
        //    Assert.IsTrue(store.IsManager(managerId));
        //    AssertStartingManagerPermissions(managerId);
        //}

        [Test]
        [TestCase(founderMemberId, managerId2)]
        [TestCase(coOwnerId1, managerId2)]
        public void TestChangeManagerPermissionsSholdPass(int requestingMemberId, int managerId)
        {
            SetupStoreNoPermissionsChange();

            Action<IList<Permission>> test = (newPermissions) => TestSuccessfulChangeManagerPermissionsOnce(requestingMemberId, managerId, newPermissions);

            test(new List<Permission>() { Permission.RecieveInfo, Permission.MakeCoManager });
            test(new List<Permission>() { });
            test(new List<Permission>() { Permission.RecieveInfo});
            test(new List<Permission>() { Permission.MakeCoManager });
            test(allPermissions); 
        }

        private void TestSuccessfulChangeManagerPermissionsOnce(int requstingMemberId, int managerMemberId, IList<Permission> newPemissions)
        {
            store.ChangeManagerPermissions(requstingMemberId, managerMemberId, newPemissions); 
            Assert.IsTrue(SameElements(store.GetManagerPermissions(founderMemberId, managerMemberId), newPemissions));
        }

        // ------- GetMembersInRole() ----------------------------------------

        [Test]
        [TestCase(notAMemberId1, Role.Owner)]
        [TestCase(memberId1, Role.Manager)]
        [TestCase(managerId2, Role.Owner)] // not in defualt permissions
        public void TestGetMembersInRoleNoPermission(int requestingMemberId, Role role)
        {
            SetupStoreFull();

            Assert.Throws<MarketException>(() => store.GetMembersInRole(requestingMemberId, role));
        }

        [Test]
        [TestCase(coOwnerId1, Role.Manager, new int[] { managerId1, managerId2 })]
        [TestCase(managerId1, Role.Owner, new int[] { coOwnerId1, coOwnerId2, founderMemberId })]
        [TestCase(founderMemberId, Role.Manager, new int[] { managerId1, managerId2 })]
        public void TestGetMembersInRoleShouldPass(int requestingMemberId, Role role, int[] expectedMembersIds)
        {
            SetupStoreFull();

            Assert.IsTrue(SameElements(store.GetMembersInRole(requestingMemberId, role)
                , new List<int>(expectedMembersIds)));
        }

        // ------- GetFounder() ----------------------------------------

        [Test]
        [TestCase(notAMemberId1)]
        [TestCase(memberId1)]
        [TestCase(managerId2)] // not in defualt permissions
        public void TestGetFounderNoPermission(int requestingMemberId)
        {
            SetupStoreFull();

            Assert.Throws<MarketException>(() => store.GetFounder(requestingMemberId));
        }

        [Test]
        [TestCase(coOwnerId1)]
        [TestCase(managerId1)]
        [TestCase(founderMemberId)]
        public void TestGetFounderShouldPass(int requestingMemberId)
        {
            SetupStoreFull();

            Assert.AreEqual(founderMemberId, store.GetFounder(requestingMemberId).GetId());
        }

        // ------- GetManagerPermossions() ----------------------------------------

        [Test]
        [TestCase(notAMemberId1, managerId1)]
        [TestCase(memberId1, managerId1)]
        [TestCase(managerId2, managerId1)] // not in defualt permissions
        [TestCase(coOwnerId1, notAMemberId1)]
        [TestCase(coOwnerId1, memberId1)]
        [TestCase(coOwnerId1, coOwnerId1)]
        [TestCase(coOwnerId1, founderMemberId)]
        public void TestGetManagerPermissionShouldFail(int requestingMemberId, int managerMemberId)
        {
            SetupStoreFull();

            Assert.Throws<MarketException>(() => store.GetManagerPermissions(requestingMemberId, managerMemberId));
        }

        [Test]
        [TestCase(coOwnerId1, managerId1, new Permission[] { Permission.MakeCoManager, Permission.RecieiveRolesInfo})]
        [TestCase(managerId1, managerId2, new Permission[] { Permission.RecieveInfo })]
        [TestCase(founderMemberId, managerId1, new Permission[] { Permission.RecieveInfo })]
        public void TestGetManagerPremissionsShouldPass(int requestingMemberId, int managerMemberId, Permission[] excpectedPermossions)
        {
            SetupStoreFull();

            SetupChangeMemberPermissions(managerMemberId, excpectedPermossions); 

            Assert.IsTrue(SameElements(store.GetManagerPermissions(requestingMemberId, managerMemberId)
                , new List<Permission>(excpectedPermossions)));
        }

        // ------- IsFounder() ----------------------------------------

        [TestCase(notAMemberId1, false)]
        [TestCase(memberId1, false)]
        [TestCase(managerId1, false)]
        [TestCase(coOwnerId1, false)]
        [TestCase(founderMemberId, true)]
        public void TestIsFounder(int memberId, bool expected)
        {
            SetupStoreNoPermissionsChange();

            Assert.AreEqual(expected, store.IsFounder(memberId));
        }

        // ------- IsCoOwner() ----------------------------------------

        [TestCase(notAMemberId1, false)]
        [TestCase(memberId1, false)]
        [TestCase(managerId1, false)]
        [TestCase(coOwnerId1, true)]
        [TestCase(founderMemberId, true)]
        public void TestIsCoOwner(int memberId, bool expected)
        {
            SetupStoreNoPermissionsChange();

            Assert.AreEqual(expected, store.IsCoOwner(memberId));
        }

        // ------- IsManager() ----------------------------------------

        [TestCase(notAMemberId1, false)]
        [TestCase(memberId1, false)]
        [TestCase(managerId1, true)]
        [TestCase(coOwnerId1, false)]
        [TestCase(founderMemberId, false)]
        public void TestIsManager(int memberId, bool expected)
        {
            SetupStoreNoPermissionsChange();

            Assert.AreEqual(expected, store.IsManager(memberId));
        }


        private bool SameElements<T>(IList<T> list1, IList<T> list2)
        {
            if (list1.Count != list2.Count)
                return false;
            return list1.All(element => list2.Contains(element)); 
        }

    }
}