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

        private const string productName1 = "Apple";
        private const string productName2 = "Milk";
        private const string productName3 = "Tommato";
        private const double productPrice1 = 3.3;
        private const double productPrice2 = 4.4;
        private const double productPrice3 = 5.5;
        private const string category1 = "Fruits";
        private const string category2= "Dairy";
        private const string category3= "Vegetables";
        private const int amount1 = 10;
        private const int amount2 = 6;
        private const int amount3 = 5;
        private int productId1;
        private int productId2;
        private int productId3;
        private const double purchasePrice = 5.5;
        private const string purchaseDescription = "eggs, 3X Milk carton, 2X peanut jar  ";
        private const string reviewMessage1 = "very nice";
        private const string reviewMessage2 = "wasn't what I expected";
        private const string reviewMessage3 = "excellent!";
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
            Assert.AreEqual(store.GetManagersPermissions(founderMemberId)[newManagerId], expectedManagerPermissions);
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
            Assert.AreEqual(store.GetManagersPermissions(founderMemberId)[managerMemberId], newPemissions);
        }


        // ----------------------- Products Tests ----------------------------
        [Test]
        [TestCase(coOwnerId1, productName1, productPrice1,category1)]
        [TestCase(coOwnerId2, productName2, productPrice2, category2)]
        [TestCase(founderMemberId, productName3, productPrice3, category3)]
        public void TestAddNewProductByCoOwnerOrMore(int memberId, string productName, double pricePerUnit, string category) {
            SetupStoreNoPermissionsChange();
            int productId = store.AddNewProduct(memberId, productName, pricePerUnit, category);
            Assert.True(store.ContainProductInStock(productId));
        }
        
        [Test]
        [TestCase(coOwnerId1, productName1, productPrice1, category1)]
        [TestCase(notAMemberId1, productName2, productPrice2, category2)]
        public void TestAddNewProductByLessThanShouldFail(int memberId, string productName, double pricePerUnit, string category)
        {
            SetupStoreNoRoles();
            Assert.Throws<MarketException>(()=>store.AddNewProduct(memberId, productName, pricePerUnit, category));
            Assert.IsNull(store.SearchProductByName(productName).FirstOrDefault());
        }
        private void SetUpProductsIdInStore() {
            productId1 = store.AddNewProduct(founderMemberId, productName1, productPrice1, category1);
            productId2 = store.AddNewProduct(founderMemberId, productName2, productPrice2,category2);
            productId3 = store.AddNewProduct(founderMemberId, productName3, productPrice3,category3);
        }
        [Test]
        [TestCase(coOwnerId1,amount1)]
        [TestCase(coOwnerId2,amount2)]
        [TestCase(founderMemberId, amount3)]
        public void TestAddProductToInventoryWithProperPermissionSuccess(int memberId, int amountToAdd)
        {
            SetupStoreNoPermissionsChange();
            SetUpProductsIdInStore();
            int amountBefore = store.SearchProductByProductId(productId1).amountInInventory;
            store.AddProductToInventory(memberId, productId1, amountToAdd);
            int amountAfter = store.SearchProductByProductId(productId1).amountInInventory;
            Assert.AreEqual(amountBefore+amountToAdd, amountAfter); 
        }
        [Test]
        [TestCase(notAMemberId1, amount1)]
        [TestCase(notAMemberId3, amount2)]
        public void TestAddProductToInventoryWithoutProperPermissionFail(int memberId, int amountToAdd)
        {
            SetupStoreNoRoles();
            SetUpProductsIdInStore();
            int amountBefore = store.SearchProductByProductId(productId2).amountInInventory;
            Assert.Throws<MarketException>(()=> store.AddProductToInventory(memberId, productId2, amountToAdd));
            int amountAfter = store.SearchProductByProductId(productId2).amountInInventory;
            Assert.AreEqual(amountBefore, amountAfter);
        }


        [Test]
        [TestCase(coOwnerId1)]
        [TestCase(coOwnerId2)]
        [TestCase(founderMemberId)]
        public void RemoveAProductByCoOwnerOrMore(int memberId)
        {
            SetupStoreNoPermissionsChange();
            SetUpProductsIdInStore();
            Assert.IsNotNull(store.SearchProductByProductId(productId3));
            store.RemoveProduct(memberId, productId3);
            Assert.IsNull(store.SearchProductByProductId(productId3));
        }

        [Test]
        [TestCase(coOwnerId1)]
        [TestCase(notAMemberId1)]
        public void TestRemoveAProductByLessThanShouldFail(int memberId)
        {
            SetupStoreNoRoles();
            SetUpProductsIdInStore();
            Assert.Throws<MarketException>(() => store.RemoveProduct(memberId,productId1));
            Assert.IsNotNull(store.SearchProductByProductId(productId1));
        }

        private void SetUpProductAmount() {
            SetUpProductsIdInStore();
            store.AddProductToInventory(founderMemberId,productId1,amount2);
            store.AddProductToInventory(founderMemberId, productId2, amount2);
            store.AddProductToInventory(founderMemberId, productId3, amount2);
        }
        [Test]
        [TestCase(coOwnerId1, amount2)]
        [TestCase(founderMemberId, amount3)]
        public void TestDecreaseProductAmountFromInventoryWithProperPermissionSuccess(int memberId, int amountToReduce)
        {
            SetupStoreNoPermissionsChange();
            SetUpProductAmount();
            int amountBefore = store.SearchProductByName(productName1).First().amountInInventory;
            store.DecreaseProductAmountFromInventory(memberId, productId1, amountToReduce);
            int amountAfter = store.SearchProductByName(productName1).First().amountInInventory;
            Assert.AreEqual(amountBefore - amountToReduce, amountAfter);
        }
        [Test]
        [TestCase(notAMemberId1, amount2)]
        [TestCase(notAMemberId3, amount3)]
        public void TestDecreaseProductAmountFromInventoryWithoutProperPermissionFail(int memberId, int amountToReduce)
        {
            SetupStoreNoRoles();
            SetUpProductAmount();
            int amountBefore = store.SearchProductByName(productName1).First().amountInInventory;
            Assert.Throws<MarketException>(()=>store.DecreaseProductAmountFromInventory(memberId, productId1, amountToReduce));
            int amountAfter = store.SearchProductByName(productName1).First().amountInInventory;
            Assert.AreEqual(amountBefore, amountAfter);
        }
        [Test]
        [TestCase(founderMemberId, amount1)]
        public void TestDecreaseProductAmountFromInventoryNotEnoughInInvFail(int memberId, int amountToReduce)
        {
            SetupStoreNoPermissionsChange();// we want to check inventory managment 
            SetUpProductAmount();
            int amountBefore = store.SearchProductByName(productName1).First().amountInInventory;
            Assert.Throws<MarketException>(() => store.DecreaseProductAmountFromInventory(memberId, productId1, amountToReduce));
            int amountAfter = store.SearchProductByName(productName1).First().amountInInventory;
            Assert.AreEqual(amountBefore, amountAfter);
        }

        [Test]
        [TestCase(coOwnerId1,PurchaseOption.Bid)]
        [TestCase(coOwnerId2, PurchaseOption.Raffle)]
        [TestCase(founderMemberId, PurchaseOption.Immediate)]
        public void TestAddPurchaseOptionWithPermissionsSuccess(int memberId, PurchaseOption purchaseOptionToAdd)
        {
            SetupStoreNoPermissionsChange();
            Assert.False(store.policy.ContainsPurchaseOption(purchaseOptionToAdd));
            store.AddPurchaseOption(memberId, purchaseOptionToAdd);
            Assert.True(store.policy.ContainsPurchaseOption(purchaseOptionToAdd));
        }
        
        [Test]
        [TestCase(notAMemberId1, PurchaseOption.Public)]
        [TestCase(coOwnerId2, PurchaseOption.Raffle)]
        public void TestAddPurchaseOptionWithoutPermissionsFail(int memberId, PurchaseOption purchaseOptionToAdd)
        {
            SetupStoreNoRoles();
            Assert.False(store.policy.ContainsPurchaseOption(purchaseOptionToAdd));
            Assert.Throws<MarketException>(() => store.AddPurchaseOption(memberId, purchaseOptionToAdd));
            Assert.False(store.policy.ContainsPurchaseOption(purchaseOptionToAdd));
        }

        private void SetUpStoreWithPurchaseOption() {
            SetUpProductsIdInStore();
            store.AddPurchaseOption(founderMemberId,PurchaseOption.Public);
            store.AddPurchaseOption(founderMemberId, PurchaseOption.Immediate);
        }

        [Test]
        [TestCase(coOwnerId1, PurchaseOption.Public)]
        [TestCase(coOwnerId2, PurchaseOption.Immediate)]
        [TestCase(founderMemberId, PurchaseOption.Immediate)]
        public void TestAddProductPurchaseOptionWithPermissionsSuccess(int memberId, PurchaseOption purchaseOptionToAdd)
        {
            SetupStoreNoPermissionsChange();
            SetUpStoreWithPurchaseOption();
            Assert.False(store.SearchProductByProductId(productId1).ContainsPurchasePolicy(purchaseOptionToAdd));
            store.AddProductPurchaseOption(memberId,productId1, purchaseOptionToAdd);
            Assert.True(store.SearchProductByProductId(productId1).ContainsPurchasePolicy(purchaseOptionToAdd));
        }

        [Test]
        [TestCase(notAMemberId1, PurchaseOption.Public)]
        [TestCase(coOwnerId2, PurchaseOption.Immediate)]
        public void TestAddProductPurchaseOptionWithPermissionsFail(int memberId, PurchaseOption purchaseOptionToAdd)
        {
            SetupStoreNoRoles();
            SetUpStoreWithPurchaseOption();
            Assert.False(store.SearchProductByProductId(productId2).ContainsPurchasePolicy(purchaseOptionToAdd));
            Assert.Throws<MarketException>(() => store.AddProductPurchaseOption(memberId, productId2, purchaseOptionToAdd));
            Assert.False(store.SearchProductByProductId(productId2).ContainsPurchasePolicy(purchaseOptionToAdd));
        }

        [Test]
        [TestCase(coOwnerId2, PurchaseOption.Bid)]
        [TestCase(founderMemberId, PurchaseOption.Raffle)]
        public void TestAddProductPurchaseThatStoreDoesNotHaveFail(int memberId, PurchaseOption purchaseOptionToAdd)
        {
            SetupStoreNoPermissionsChange();
            SetUpStoreWithPurchaseOption();
            Assert.False(store.SearchProductByProductId(productId3).ContainsPurchasePolicy(purchaseOptionToAdd));
            Assert.Throws<MarketException>(() => store.AddProductPurchaseOption(memberId, productId3, purchaseOptionToAdd));
            Assert.False(store.SearchProductByProductId(productId3).ContainsPurchasePolicy(purchaseOptionToAdd));
        }

        [Test]
        [TestCase(coOwnerId1, productPrice2)]
        [TestCase(coOwnerId2, productPrice3)]
        [TestCase(founderMemberId, productPrice3)]
        public void TestSetProductPriceWithPermissionsSuccess(int memberId, double price)
        {
            SetupStoreNoPermissionsChange();
            SetUpProductsIdInStore();
            Assert.False(store.SearchProductByProductId(productId1).pricePerUnit==price);
            store.SetProductPrice(memberId, productId1, price);
            Assert.True(store.SearchProductByProductId(productId1).pricePerUnit == price);
        }
        
        [Test]
        [TestCase(notAMemberId1, productPrice2)]
        [TestCase(notAMemberId2, productPrice3)]
        public void TestSetProductPriceWithoutPermissionsFail(int memberId, double price)
        {
            SetupStoreNoRoles();
            SetUpProductsIdInStore();
            Assert.False(store.SearchProductByProductId(productId1).pricePerUnit == price);
            Assert.Throws<MarketException>(() => store.SetProductPrice(memberId, productId3, price));
            Assert.False(store.SearchProductByProductId(productId1).pricePerUnit == price);
        }

        private Purchase SetUpMockPurchases()
        {
            Mock<Purchase> purchaseMock = new Mock<Purchase>();

            purchaseMock.Setup(purchase =>
               purchase.GetPurchaseDescription()).
                   Returns(purchaseDescription);

            return purchaseMock.Object;
        }

        [Test]
        [TestCase(coOwnerId1)]
        [TestCase(coOwnerId2)]
        [TestCase(founderMemberId)]
        public void TestAddPurchaseRecordWithPermissionSuccess(int memberId)
        {
            SetupStoreNoPermissionsChange();
            DateTime date = DateTime.Now;
            store.AddPurchaseRecord(memberId, date, purchasePrice, purchaseDescription);
            Assert.True(store.findPurchasesByDate(date).Count == 1);
        }
        [Test]
        [TestCase(notAMemberId1)]
        [TestCase(notAMemberId2)]
        public void TestAddPurchaseRecordWithoutPermissionFail(int memberId)
        {
            SetupStoreNoRoles();
            DateTime date = DateTime.Now; 
            Assert.Throws<MarketException>(() => store.AddPurchaseRecord(memberId, date, purchasePrice, purchaseDescription));
            Assert.True(store.findPurchasesByDate(date).Count==0);
        }

        private void SetUpPurchasesInStore()
           => store.AddPurchaseRecord(founderMemberId, DateTime.Now, purchasePrice, purchaseDescription);

        [Test]
        [TestCase(coOwnerId1)]
        [TestCase(coOwnerId2)]
        [TestCase(founderMemberId)]
        public void TestGetPurchaseHistoryWithPermissionSuccess(int memberId)
        {
            SetupStoreNoPermissionsChange();
            SetUpPurchasesInStore();
            IList<Purchase> purchases =  store.GetPurchaseHistory(memberId);
            Assert.True(purchases.Count==1 && purchases.First().GetPurchaseDescription()==purchaseDescription);
        }
        
        [Test]
        [TestCase(notAMemberId1)]
        [TestCase(notAMemberId2)]
        public void TestGetPurchaseHistoryWithotPermissionFail(int memberId)
        {
            SetupStoreNoRoles();
            SetUpPurchasesInStore();
            Assert.Throws<MarketException>(() => store.GetPurchaseHistory(memberId));
        }
        
        [Test]
        [TestCase(memberId1, reviewMessage1)]
        [TestCase(memberId2, reviewMessage2)]
        [TestCase(memberId3, reviewMessage3)]
        public void TestAddProductReviewMemberExistsSuccess(int memberId, string reviewMessage)
        {
            SetupStoreNoRoles();
            SetUpProductsIdInStore();
            Assert.True(store.GetProductReviews(productId1).Count==0);
            store.AddProductReview(memberId, productId1, reviewMessage);
            IList<string> reviews = store.GetProductReviews(productId1);
            Assert.True(reviews.Count == 1 && reviews.First().Contains(reviewMessage));
        }
        [Test]
        [TestCase(memberId1, reviewMessage1)]
        [TestCase(memberId2, reviewMessage2)]
        [TestCase(memberId3, reviewMessage3)]
        public void TestAddProductReviewMemberExistsProductDoesNotFail(int memberId, string reviewMessage)
        {
            SetupStoreNoRoles();
            Assert.Throws<MarketException>(() => store.AddProductReview(memberId,productId1,reviewMessage));
        }
        [Test]
        [TestCase(notAMemberId1, reviewMessage1)]
        [TestCase(notAMemberId2, reviewMessage2)]
        [TestCase(notAMemberId3, reviewMessage3)]
        public void TestAddProductReviewMemberDoesNotExistsFail(int memberId, string reviewMessage)
        {
            SetupStoreNoRoles();
            SetUpProductsIdInStore();
            Assert.True(store.GetProductReviews(productId1).Count == 0);
            Assert.Throws<MarketException>(() => store.AddProductReview(memberId, productId2, reviewMessage));
            Assert.True(store.GetProductReviews(productId1).Count == 0);
        }

    }
}