using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Moq;
using MarketBackend.BusinessLayer.Market.StoreManagment;
using MarketBackend.BusinessLayer.Buyers.Members;
using MarketBackend.BusinessLayer.Market;
using System.Collections.Concurrent;
using MarketBackend.BusinessLayer;
using MarketBackend.BusinessLayer.Buyers;
using MarketBackend.BusinessLayer.Market.StoreManagment.Discounts;
using MarketBackend.BusinessLayer.Market.StoreManagment.PurchasesPolicy.PurchaseInterfaces;

namespace TestMarketBackend.BusinessLayer.Market.StoreManagment
{
    public class StoreTests
    {
        private Store store;
        private const int storeId = 0;
        private Member founder;
        private Mock<Member> founderMock;
        private const int founderMemberId = 0;

        private Purchase purchase = null;

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
        private int[] membersIds = { coOwnerId1, coOwnerId2, managerId1, managerId2, memberId1, memberId2, memberId3 };
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
        private const string category2 = "Dairy";
        private const string category3 = "Vegetables";

        private const int amount1 = 10;
        private const int amount2 = 6;
        private const int amount3 = 5;

        private int productId1;
        private int productId2;
        private int productId3;
        private const int illegalProductId = -1;

        private const double discountPercentage1 = 30;
        private const double discountPercentage2 = 45;
        private const double discountPercentage3 = 90;

        private const double purchasePrice = 5.5;
        private const string purchaseDescription = "eggs, 3X Milk carton, 2X peanut jar  ";

        private const string reviewMessage1 = "very nice";
        private const string reviewMessage2 = "wasn't what I expected";
        private const string reviewMessage3 = "excellent!";

        private IDictionary<int, int> productsAmount;
        private double correctTotal;
        private const int productIdAmount1 = 3;
        private const int productIdAmount2 = 4;
        private const int productIdAmount3 = 5;

        private IDictionary<int, bool> wasNotified;

        private ProductsSearchFilter filter;

        private IExpression expression1 = (new Mock<IExpression>()).Object;
        private IExpression expression2 = (new Mock<IExpression>()).Object;
        private IExpression expression3 = (new Mock<IExpression>()).Object;
        private IPurchasePolicy purchasePolicy1 = (new Mock<IPurchasePolicy>()).Object;
        private IPurchasePolicy purchasePolicy2 = (new Mock<IPurchasePolicy>()).Object;
        private const string description_purchase = "A banana can be bought in sets of 4 or more!";
        private const string description_discount = "A banana's discount will be given when bought in sets of 4 or more!";
        private Mock<ShoppingBag>? shoppingBagMock;
        private Mock<ProductInBag>? productInBagMock;
        // ----------- Setup helping functions -----------------------------

        private Member setupMcokedMember(int memberId)
        {
            Mock<Security> securityMock = new Mock<Security>();
            Mock<Member> memberMock = new Mock<Member>("user123", "12345678", securityMock.Object); // todo: make sure these arguments to the constructor are okay

            memberMock.Setup(member =>
                member.Id).
                    Returns(memberId);

            memberMock.Setup(member =>
               member.Notify(It.IsAny<string[]>())).
                   Callback(() => wasNotified[memberId] = true);

            return memberMock.Object;
        }

        private void setupMockedPurchase(int buyerId)
        {
            DateTime date = DateTime.Now;
            Mock<Purchase> purchaseMock = new Mock<Purchase>(buyerId, date, 0, "");
            purchaseMock.Setup(p => p.purchasePrice).Returns(0);
            purchaseMock.Setup(p => p.purchaseDescription).Returns(purchaseDescription);
            purchaseMock.Setup(p => p.purchaseDate).Returns(date);
            purchase = purchaseMock.Object;

        }

        private Member[] setupMembers(int[] membersIds)
        {
            Member[] members = new Member[membersIds.Length];
            wasNotified = new Dictionary<int, bool>();
            for (int i = 0; i < membersIds.Length; i++)
            {
                members[i] = setupMcokedMember(membersIds[i]);
                wasNotified[i] = false;
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
                if (founder.Id == memberId)
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

        [Test]
        [TestCase(managerId1)]
        public void TestMakeCoOwnerTargetIsManager(int newCoOwnerMemberId)
        {
            SetupStoreNoRoles();

            int requestingMemberId = founderMemberId; // needs to be the one that appoints it to be a manager

            SetupMemberToManagerWithAllPermissions(newCoOwnerMemberId);

            store.MakeCoOwner(requestingMemberId, newCoOwnerMemberId);
            Assert.IsTrue(store.IsCoOwner(requestingMemberId));
            Assert.IsFalse(store.IsManager(requestingMemberId));
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
        [TestCase(managerId1, memberId1, new Permission[] { })]
        [TestCase(managerId1, memberId1, new Permission[] { Permission.RecieveInfo })]
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
            test(new List<Permission>() { Permission.RecieveInfo });
            test(new List<Permission>() { Permission.MakeCoManager });
            test(allPermissions);
        }

        private void TestSuccessfulChangeManagerPermissionsOnce(int requstingMemberId, int managerMemberId, IList<Permission> newPemissions)
        {
            store.ChangeManagerPermissions(requstingMemberId, managerMemberId, newPemissions);
            Assert.IsTrue(SameElements(store.GetManagerPermissions(founderMemberId, managerMemberId), newPemissions));
        }


        // ----------------------- Products Tests ----------------------------
        private void SetUpSearchFilterName(string name)
        {
            filter = new ProductsSearchFilter();
            filter.FilterProductName(name);
        }
        [Test]
        [TestCase(coOwnerId1, productName1, productPrice1, category1)]
        [TestCase(coOwnerId2, productName2, productPrice2, category2)]
        [TestCase(founderMemberId, productName3, productPrice3, category3)]
        public void TestAddNewProductByCoOwnerOrMore(int memberId, string productName, double pricePerUnit, string category)
        {
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
            Assert.Throws<MarketException>(() => store.AddNewProduct(memberId, productName, pricePerUnit, category));
            Assert.True(store.SearchProducts(filter).Count == 0);
        }
        private void SetUpProductsIdInStore()
        {
            productId1 = store.AddNewProduct(founderMemberId, productName1, productPrice1, category1);
            productId2 = store.AddNewProduct(founderMemberId, productName2, productPrice2, category2);
            productId3 = store.AddNewProduct(founderMemberId, productName3, productPrice3, category3);
        }
        [Test]
        [TestCase(coOwnerId1, amount1)]
        [TestCase(coOwnerId2, amount2)]
        [TestCase(founderMemberId, amount3)]
        public void TestAddProductToInventoryWithProperPermissionSuccess(int memberId, int amountToAdd)
        {
            SetupStoreNoPermissionsChange();
            SetUpProductsIdInStore();
            int amountBefore = store.SearchProductByProductId(productId1).amountInInventory;
            store.AddProductToInventory(memberId, productId1, amountToAdd);
            int amountAfter = store.SearchProductByProductId(productId1).amountInInventory;
            Assert.AreEqual(amountBefore + amountToAdd, amountAfter);
        }
        [Test]
        [TestCase(notAMemberId1, amount1)]
        [TestCase(notAMemberId3, amount2)]
        public void TestAddProductToInventoryWithoutProperPermissionFail(int memberId, int amountToAdd)
        {
            SetupStoreNoRoles();
            SetUpProductsIdInStore();
            int amountBefore = store.SearchProductByProductId(productId2).amountInInventory;
            Assert.Throws<MarketException>(() => store.AddProductToInventory(memberId, productId2, amountToAdd));
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
            Assert.Throws<MarketException>(() => store.RemoveProduct(memberId, productId1));
            Assert.IsNotNull(store.SearchProductByProductId(productId1));
        }

        private void SetUpProductAmount()
        {
            SetUpProductsIdInStore();
            store.AddProductToInventory(founderMemberId, productId1, amount2);
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
            int amountBefore = store.SearchProductByProductId(productId1).amountInInventory;
            store.DecreaseProductAmountFromInventory(memberId, productId1, amountToReduce);
            int amountAfter = store.SearchProductByProductId(productId1).amountInInventory;
            Assert.AreEqual(amountBefore - amountToReduce, amountAfter);
        }
        [Test]
        [TestCase(notAMemberId1, amount2)]
        [TestCase(notAMemberId3, amount3)]
        public void TestDecreaseProductAmountFromInventoryWithoutProperPermissionFail(int memberId, int amountToReduce)
        {
            SetupStoreNoRoles();
            SetUpProductAmount();
            int amountBefore = store.SearchProductByProductId(productId1).amountInInventory;
            Assert.Throws<MarketException>(() => store.DecreaseProductAmountFromInventory(memberId, productId1, amountToReduce));
            int amountAfter = store.SearchProductByProductId(productId1).amountInInventory;
            Assert.AreEqual(amountBefore, amountAfter);
        }
        [Test]
        [TestCase(founderMemberId, amount1)]
        public void TestDecreaseProductAmountFromInventoryNotEnoughInInvFail(int memberId, int amountToReduce)
        {
            SetupStoreNoPermissionsChange();// we want to check inventory managment 
            SetUpProductAmount();
            int amountBefore = store.SearchProductByProductId(productId1).amountInInventory;
            Assert.Throws<MarketException>(() => store.DecreaseProductAmountFromInventory(memberId, productId1, amountToReduce));
            int amountAfter = store.SearchProductByProductId(productId1).amountInInventory;
            Assert.AreEqual(amountBefore, amountAfter);
        }

        //[Test]
        //[TestCase(coOwnerId1, PurchaseOption.Bid)]
        //[TestCase(coOwnerId2, PurchaseOption.Raffle)]
        //[TestCase(founderMemberId, PurchaseOption.Immediate)]
        //public void TestAddPurchaseOptionWithPermissionsSuccess(int memberId, PurchaseOption purchaseOptionToAdd)
        //{
        //    SetupStoreNoPermissionsChange();
        //    Assert.False(store.policy.ContainsPurchaseOption(purchaseOptionToAdd));
        //    store.AddPurchaseOption(memberId, purchaseOptionToAdd);
        //    Assert.True(store.policy.ContainsPurchaseOption(purchaseOptionToAdd));
        //}

        //[Test]
        //[TestCase(notAMemberId1, PurchaseOption.Public)]
        //[TestCase(coOwnerId2, PurchaseOption.Raffle)]
        //public void TestAddPurchaseOptionWithoutPermissionsFail(int memberId, PurchaseOption purchaseOptionToAdd)
        //{
        //    SetupStoreNoRoles();
        //    Assert.False(store.policy.ContainsPurchaseOption(purchaseOptionToAdd));
        //    Assert.Throws<MarketException>(() => store.AddPurchaseOption(memberId, purchaseOptionToAdd));
        //    Assert.False(store.policy.ContainsPurchaseOption(purchaseOptionToAdd));
        //}

        //private void SetUpStoreWithPurchaseOption()
        //{
        //    SetUpProductsIdInStore();
        //    store.AddPurchaseOption(founderMemberId, PurchaseOption.Public);
        //    store.AddPurchaseOption(founderMemberId, PurchaseOption.Immediate);
        //}

        //[Test]
        //[TestCase(coOwnerId1, PurchaseOption.Public)]
        //[TestCase(coOwnerId2, PurchaseOption.Immediate)]
        //[TestCase(founderMemberId, PurchaseOption.Immediate)]
        //public void TestAddProductPurchaseOptionWithPermissionsSuccess(int memberId, PurchaseOption purchaseOptionToAdd)
        //{
        //    SetupStoreNoPermissionsChange();
        //    SetUpStoreWithPurchaseOption();
        //    Assert.False(store.SearchProductByProductId(productId1).ContainsPurchasePolicy(purchaseOptionToAdd));
        //    store.AddProductPurchaseOption(memberId, productId1, purchaseOptionToAdd);
        //    Assert.True(store.SearchProductByProductId(productId1).ContainsPurchasePolicy(purchaseOptionToAdd));
        //}

        //[Test]
        //[TestCase(notAMemberId1, PurchaseOption.Public)]
        //[TestCase(coOwnerId2, PurchaseOption.Immediate)]
        //public void TestAddProductPurchaseOptionWithPermissionsFail(int memberId, PurchaseOption purchaseOptionToAdd)
        //{
        //    SetupStoreNoRoles();
        //    SetUpStoreWithPurchaseOption();
        //    Assert.False(store.SearchProductByProductId(productId2).ContainsPurchasePolicy(purchaseOptionToAdd));
        //    Assert.Throws<MarketException>(() => store.AddProductPurchaseOption(memberId, productId2, purchaseOptionToAdd));
        //    Assert.False(store.SearchProductByProductId(productId2).ContainsPurchasePolicy(purchaseOptionToAdd));
        //}

        //[Test]
        //[TestCase(coOwnerId2, PurchaseOption.Bid)]
        //[TestCase(founderMemberId, PurchaseOption.Raffle)]
        //public void TestAddProductPurchaseThatStoreDoesNotHaveFail(int memberId, PurchaseOption purchaseOptionToAdd)
        //{
        //    SetupStoreNoPermissionsChange();
        //    SetUpStoreWithPurchaseOption();
        //    Assert.False(store.SearchProductByProductId(productId3).ContainsPurchasePolicy(purchaseOptionToAdd));
        //    Assert.Throws<MarketException>(() => store.AddProductPurchaseOption(memberId, productId3, purchaseOptionToAdd));
        //    Assert.False(store.SearchProductByProductId(productId3).ContainsPurchasePolicy(purchaseOptionToAdd));
        //}

        [Test]
        [TestCase(coOwnerId1, productPrice2)]
        [TestCase(coOwnerId2, productPrice3)]
        [TestCase(founderMemberId, productPrice3)]
        public void TestSetProductPriceWithPermissionsSuccess(int memberId, double price)
        {
            SetupStoreNoPermissionsChange();
            SetUpProductsIdInStore();
            Assert.False(store.SearchProductByProductId(productId1).pricePerUnit == price);
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

        [Test]
        [TestCase(coOwnerId1)]
        [TestCase(founderMemberId)]
        [TestCase(managerId1)]
        public void TestSetPurchasePolicyWithPermissionsSuccess(int memberId)
        {
            SetupStorePermissionsChangePolicies();
            SetUpProductsIdInStore();
            Assert.True(store.purchaseManager.purchases.Count == 0);
            int pid = store.AddPurchasePolicy(purchasePolicy1, description_purchase, memberId);
            Assert.True(store.purchaseManager.purchases.Count == 1 && store.purchaseManager.purchases.ContainsKey(pid));

        }

        [Test]
        [TestCase(notAMemberId1)]
        [TestCase(memberId2)]
        [TestCase(managerId2)]
        public void TestSetPurchasePolicyWithPermissionsFail(int memberId)
        {
            SetupStorePermissionsChangePolicies();
            SetUpProductsIdInStore();
            Assert.True(store.purchaseManager.purchases.Count == 0);
            Assert.Throws<MarketException>(() => store.AddPurchasePolicy(purchasePolicy1, description_purchase, memberId));
            Assert.True(store.purchaseManager.purchases.Count == 0);
        }

        [Test]
        [TestCase(coOwnerId1)]
        [TestCase(founderMemberId)]
        [TestCase(managerId1)]
        public void TestRemovePurchasePolicyWithPermissionsSuccess(int memberId)
        {
            SetupStorePermissionsChangePolicies();
            SetUpProductsIdInStore();
            Assert.True(store.purchaseManager.purchases.Count == 0);
            int pid = store.AddPurchasePolicy(purchasePolicy1, description_purchase, founderMemberId);
            Assert.True(store.purchaseManager.purchases.Count == 1 && store.purchaseManager.purchases.ContainsKey(pid));
            store.RemovePurchasePolicy(pid, memberId);
            Assert.True(store.purchaseManager.purchases.Count == 0);

        }

        [Test]
        [TestCase(notAMemberId1)]
        [TestCase(memberId2)]
        [TestCase(managerId2)]
        public void TestRemovePurchasePolicyWithPermissionsFail(int memberId)
        {
            SetupStorePermissionsChangePolicies();
            SetUpProductsIdInStore();
            Assert.True(store.purchaseManager.purchases.Count == 0);
            int pid = store.AddPurchasePolicy(purchasePolicy1, description_purchase, founderMemberId);
            Assert.True(store.purchaseManager.purchases.Count == 1 && store.purchaseManager.purchases.ContainsKey(pid));
            Assert.Throws<MarketException>(() => store.RemovePurchasePolicy(pid, memberId));
            Assert.True(store.purchaseManager.purchases.Count == 1 && store.purchaseManager.purchases.ContainsKey(pid));
        }

        [Test]
        [TestCase(coOwnerId1, discountPercentage1)]
        [TestCase(coOwnerId2, discountPercentage2)]
        [TestCase(founderMemberId, discountPercentage3)]
        public void TestSetProductDiscountPercentageWithPermissionsSuccess(int memberId, double discountPercentage)
        {
            SetupStoreNoPermissionsChange();
            SetUpProductsIdInStore();
            Assert.False(store.SearchProductByProductId(productId1).productdicount == (discountPercentage / 100));
            store.SetProductDiscountPercentage(memberId, productId1, discountPercentage);
            Assert.True(store.SearchProductByProductId(productId1).productdicount == (discountPercentage / 100));
        }

        [Test]
        [TestCase(notAMemberId1, productPrice2)]
        [TestCase(notAMemberId2, productPrice3)]
        public void TestSetProductDiscountPercentageWithoutPermissionsFail(int memberId, double discountPercentage)
        {
            SetupStoreNoRoles();
            SetUpProductsIdInStore();
            Assert.False(store.SearchProductByProductId(productId1).productdicount == (discountPercentage / 100));
            Assert.Throws<MarketException>(() => store.SetProductDiscountPercentage(memberId, productId3, discountPercentage));
            Assert.False(store.SearchProductByProductId(productId1).productdicount == (discountPercentage / 100));
        }

        [Test]
        [TestCase(coOwnerId1, category2)]
        [TestCase(coOwnerId2, category2)]
        [TestCase(founderMemberId, category3)]
        public void TestSetProductCategoryWithPermissionsSuccess(int memberId, string category)
        {
            SetupStoreNoPermissionsChange();
            SetUpProductsIdInStore();
            Assert.False(store.SearchProductByProductId(productId1).category == category);
            store.SetProductCategory(memberId, productId1, category);
            Assert.True(store.SearchProductByProductId(productId1).category == category);
        }

        [Test]
        [TestCase(notAMemberId1, category2)]
        [TestCase(notAMemberId2, category3)]
        public void TestSetProductCategoryWithoutPermissionsFail(int memberId, string category)
        {
            SetupStoreNoRoles();
            SetUpProductsIdInStore();
            Assert.False(store.SearchProductByProductId(productId1).category == category);
            Assert.Throws<MarketException>(() => store.SetProductCategory(memberId, productId3, category));
            Assert.False(store.SearchProductByProductId(productId1).category == category);
        }

        [Test]
        [TestCase(coOwnerId1)]
        [TestCase(coOwnerId2)]
        [TestCase(founderMemberId)]
        public void TestAddPurchaseRecordWithPermissionSuccess(int memberId)
        {
            SetupStoreNoPermissionsChange();
            DateTime date = DateTime.Now;
            store.AddPurchaseRecord(memberId, purchase);
            Assert.True(store.findPurchasesByDate(purchase.purchaseDate).Count == 1);
        }
        [Test]
        [TestCase(notAMemberId1)]
        [TestCase(notAMemberId2)]
        public void TestAddPurchaseRecordWithoutPermissionFail(int memberId)
        {
            SetupStoreNoRoles();
            DateTime date = DateTime.Now;
            purchase = new Purchase(memberId, date, 9.9, "great!");
            Assert.Throws<MarketException>(() => store.AddPurchaseRecord(memberId, purchase));
            Assert.True(store.findPurchasesByDate(purchase.purchaseDate).Count == 0);
        }

        private void SetUpPurchasesInStore()
           => store.AddPurchaseRecord(founderMemberId, purchase);

        [Test]
        [TestCase(coOwnerId1)]
        [TestCase(coOwnerId2)]
        [TestCase(founderMemberId)]
        public void TestGetPurchaseHistoryWithPermissionSuccess(int memberId)
        {
            SetupStoreNoPermissionsChange();
            setupMockedPurchase(memberId);
            SetUpPurchasesInStore();
            IList<Purchase> purchases = store.GetPurchaseHistory(memberId);
            Assert.True(purchases.Count == 1);
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
            Assert.True(store.GetProductReviews(productId1).Count == 0);
            store.AddProductReview(memberId, productId1, reviewMessage);
            IDictionary<Member, IList<string>> reviews = store.GetProductReviews(productId1);
            Assert.True(reviews.Count == 1);
        }
        [Test]
        [TestCase(memberId1, reviewMessage1)]
        [TestCase(memberId2, reviewMessage2)]
        [TestCase(memberId3, reviewMessage3)]
        public void TestAddProductReviewMemberExistsProductDoesNotFail(int memberId, string reviewMessage)
        {
            SetupStoreNoRoles();
            Assert.Throws<MarketException>(() => store.AddProductReview(memberId, productId1, reviewMessage));
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

        private void SetupStorePermissionsChangePolicies()
        {
            SetupStoreNoPermissionsChange();
            store.ChangeManagerPermissions(founder.Id, managerId1, new List<Permission> { Permission.purchasePolicyManagement });
            store.ChangeManagerPermissions(founder.Id, managerId2, new List<Permission> { Permission.DiscountPolicyManagement });
        }

        [Test]
        [TestCase(managerId2)]
        [TestCase(coOwnerId2)]
        [TestCase(founderMemberId)]
        public void TestAddDiscountPolicySuccess(int memeberId)
        {
            SetupStorePermissionsChangePolicies();

            Assert.True(store.discountManager.discounts.Count == 0);
            int xid = store.AddDiscountPolicy(expression1, description_discount, memeberId);
            Assert.True(store.discountManager.discounts.Count == 1 && store.discountManager.discounts.ContainsKey(xid));
        }
        [Test]
        [TestCase(managerId1)]
        [TestCase(memberId1)]
        [TestCase(notAMemberId2)]
        public void TestAddDiscountPolicyFail(int memeberId)
        {
            SetupStorePermissionsChangePolicies();
            Assert.True(store.discountManager.discounts.Count == 0);
            Assert.Throws<MarketException>(() => store.AddDiscountPolicy(expression1, description_discount, memeberId));
            Assert.True(store.discountManager.discounts.Count == 0);
        }

        [Test]
        [TestCase(managerId2)]
        [TestCase(coOwnerId2)]
        [TestCase(founderMemberId)]
        public void TestRemoveDiscountPolicySuccess(int memeberId)
        {
            SetupStorePermissionsChangePolicies();

            Assert.True(store.discountManager.discounts.Count == 0);
            int xid = store.AddDiscountPolicy(expression1, description_discount, memeberId);
            Assert.True(store.discountManager.discounts.Count == 1 && store.discountManager.discounts.ContainsKey(xid));
            store.RemoveDiscountPolicy(xid, memeberId);
            Assert.True(store.discountManager.discounts.Count == 0);
        }
        [Test]
        [TestCase(managerId1)]
        [TestCase(memberId1)]
        [TestCase(notAMemberId2)]
        public void TestRemoveDiscountPolicyFail(int memeberId)
        {
            SetupStorePermissionsChangePolicies();
            Assert.True(store.discountManager.discounts.Count == 0);
            int xid = store.AddDiscountPolicy(expression1, description_discount, founderMemberId);
            Assert.True(store.discountManager.discounts.Count == 1 && store.discountManager.discounts.ContainsKey(xid));
            Assert.Throws<MarketException>(() => store.RemoveDiscountPolicy(xid, memeberId));
            Assert.True(store.discountManager.discounts.Count == 1 && store.discountManager.discounts.ContainsKey(xid));
        }

        private ShoppingBag getShoppingBagMock(int[] productsId, int[] appropriateAmounts)
        {
            ConcurrentDictionary<ProductInBag, int> productInShoppingBag = new ConcurrentDictionary<ProductInBag, int>();
            for (int i = 0; i < productsId.Length; i++)
            {
                productInBagMock = new Mock<ProductInBag>(productsId[i], storeId) { CallBase = true };
                productInShoppingBag.TryAdd(productInBagMock.Object, appropriateAmounts[i]);
            }
            shoppingBagMock = new Mock<ShoppingBag>() { CallBase = true };
            shoppingBagMock.Setup(shoppingBag => shoppingBag.productsAmounts).Returns(productInShoppingBag);
            shoppingBagMock.Setup(shoppingBag => shoppingBag.StoreId).Returns(storeId);
            return shoppingBagMock.Object;
        }



        [Test]
        public void TestGetTotalBagCostSuccess()
        {
            SetupStoreNoPermissionsChange();
            SetUpProductsIdInStore();
            ShoppingBag shoppingBag = getShoppingBagMock(new int[] { productId1, productId2 }, new int[] { amount1, amount2 });
            Tuple<double, double> total = store.GetTotalBagCost(shoppingBag);
            Assert.True(total.Item1 == amount1 * productPrice1 + amount2 * productPrice2);
            Assert.True(total.Item2 == 0);
        }

        [Test]
        public void TestGetTotalBagCostWithoutProductIdFail()
        {
            SetupStoreNoPermissionsChange();
            ShoppingBag shoppingBag = getShoppingBagMock(new int[] { productId1, illegalProductId }, new int[] { amount1, amount2 });
            Assert.Throws<MarketException>(() => store.GetTotalBagCost(shoppingBag));
        }

        [Test]
        [TestCase(productName1)]
        [TestCase(productName2)]
        [TestCase(productName3)]
        public void TestSearchProductsByNameFilter(string name)
        {
            SetupStoreNoRoles();
            SetUpProductsIdInStore();
            SetUpSearchFilterName(name);
            IList<Product> productsWithName = store.SearchProducts(filter);
            Assert.True(productsWithName.Count == 1 && productsWithName.First().name == name);
        }

        private void SetUpSearchFilterCategory(string category)
        {
            filter = new ProductsSearchFilter();
            filter.FilterProductCategory(category);
        }

        [Test]
        [TestCase(category1, productName1)]
        [TestCase(category2, productName2)]
        [TestCase(category3, productName3)]
        public void TestSearchProductsByCategoryFilter(string category, string name)
        {
            SetupStoreNoRoles();
            SetUpProductsIdInStore();
            SetUpSearchFilterCategory(category);
            IList<Product> productsWithCategory = store.SearchProducts(filter);
            Assert.True(productsWithCategory.Count == 1 && productsWithCategory.First().name == name);
        }

        private void SetUpSearchFilterKeyword(string keyword)
        {
            filter = new ProductsSearchFilter();
            filter.FilterProductKeyword(keyword);
        }

        [Test]
        [TestCase(category1, productName1)]
        [TestCase(productName1, productName1)]
        [TestCase(category2, productName2)]
        public void TestSearchProductsByKeywordFilter(string keyword, string name)
        {
            SetupStoreNoRoles();
            SetUpProductsIdInStore();
            SetUpSearchFilterKeyword(keyword);
            IList<Product> productsWithKeyword = store.SearchProducts(filter);
            Assert.True(productsWithKeyword.Count == 1 && productsWithKeyword.First().name == name);
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


        [TestCase(managerId1)]
        [TestCase(founderMemberId)]
        public void TestGetFounderShouldPass(int requestingMemberId)
        {
            SetupStoreFull();

            Assert.AreEqual(founderMemberId, store.GetFounder(requestingMemberId).Id);
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
        [TestCase(coOwnerId1, managerId1, new Permission[] { Permission.MakeCoManager, Permission.RecieiveRolesInfo })]
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

        // --------------- notification tests - from v2 -------------------
        [Test]
        [TestCase(reviewMessage1)]
        [TestCase(reviewMessage2)]
        [TestCase(reviewMessage3)]
        public void TestNotifyCoOwners(string notifications)
        {
            SetupStoreNoPermissionsChange();
            store.notifyAllStoreOwners(notifications);
            foreach (int memberId in wasNotified.Keys)
            {
                if (memberId == coOwnerId1 || memberId == coOwnerId2 || memberId == founder.Id)
                    Assert.True(wasNotified[memberId]);
                else
                    Assert.False(wasNotified[memberId]);
            }
        }

        [Test]
        [TestCase(reviewMessage1)]
        [TestCase(reviewMessage2)]
        [TestCase(reviewMessage3)]
        public void TestNotifyCoManagers(string notifications)
        {
            SetupStoreNoPermissionsChange();
            store.notifyAllStoreManagers(notifications);
            foreach (int memberId in wasNotified.Keys)
            {
                if (memberId == managerId1 || memberId == managerId2)
                    Assert.True(wasNotified[memberId]);
                else
                    Assert.False(wasNotified[memberId]);
            }
        }
        [Test]
        public void TestCloseStoreSuccess()
        {
            SetupStoreNoPermissionsChange();
            store.CloseStore(founder.Id);
            foreach (int memberId in wasNotified.Keys)
            {
                if (memberId == managerId1 || memberId == managerId2 || memberId == coOwnerId1 || memberId == coOwnerId2 || memberId == founder.Id)
                    Assert.True(wasNotified[memberId]);
                else
                    Assert.False(wasNotified[memberId]);
            }
            Assert.False(store.isOpen);
        }
        [Test]
        public void TestCloseStoreTwiceFail()
        {
            SetupStoreNoPermissionsChange();
            store.CloseStore(founder.Id);

            foreach (int memberId in wasNotified.Keys)//clean the notifications 
                wasNotified[memberId] = false;
            Assert.Throws<MarketException>(() => store.CloseStore(founder.Id));
            foreach (int memberId in wasNotified.Keys)//check that no one was notified
                Assert.False(wasNotified[memberId]);
            Assert.False(store.isOpen);
        }
        [Test]
        [TestCase(coOwnerId1)]
        [TestCase(managerId1)]
        [TestCase(memberId3)]
        [TestCase(notAMemberId1)]
        public void TestCloseStoreByNonFounder(int id)
        {
            SetupStoreNoPermissionsChange();
            Assert.Throws<MarketException>(() => store.CloseStore(id));
            foreach (int memberId in wasNotified.Keys)//check that no one was notified
                Assert.False(wasNotified[memberId]);
            Assert.True(store.isOpen);
        }
        [Test]
        public void TestGetInformationAfterStoreClosed()
        {
            SetupStoreNoPermissionsChange();
            store.CloseStore(founder.Id);
            Assert.Throws<MarketException>(() => store.GetMembersInRole(founder.Id, Role.Owner));
            Assert.Throws<MarketException>(() => store.GetManagerPermissions(founder.Id, managerId1));
        }

        private void SetupStoreCoOwnerChain()
        {
            SetupStoreNoRoles();

            store.MakeCoOwner(founderMemberId, coOwnerId1);
            store.MakeCoOwner(coOwnerId1, coOwnerId2);

            store.MakeManager(founderMemberId, managerId1);
            store.MakeManager(coOwnerId2, managerId2);
        }

        [Test]
        [TestCase(founderMemberId, coOwnerId2)]
        [TestCase(coOwnerId1, coOwnerId2)]
        public void TestRemoveCoOwnerFromStoreSimpleCaseSuccess(int requestingMemberId, int coOwnerToRemoveMemberId)
        {
            SetupStoreCoOwnerChain();
            Assert.True(store.IsCoOwner(coOwnerToRemoveMemberId));
            store.RemoveCoOwner(requestingMemberId, coOwnerToRemoveMemberId);
            Assert.False(store.IsCoOwner(coOwnerToRemoveMemberId));
            foreach (int memberId in wasNotified.Keys)
            {
                if (memberId == coOwnerId2 || memberId == managerId2)
                    Assert.True(wasNotified[memberId]);
                else
                    Assert.False(wasNotified[memberId]);
            }
        }
        [Test]
        public void TestRemoveCoOwnerFromStoreComplexCaseSuccess()
        {
            SetupStoreCoOwnerChain();
            Assert.True(store.IsCoOwner(coOwnerId1) && store.IsCoOwner(coOwnerId2));
            store.RemoveCoOwner(founder.Id, coOwnerId1);
            Assert.False(store.IsCoOwner(coOwnerId1) || store.IsCoOwner(coOwnerId2));
            foreach (int memberId in wasNotified.Keys)
            {
                if (memberId == coOwnerId1 || memberId == coOwnerId2 || memberId == managerId2)
                    Assert.True(wasNotified[memberId]);
                else
                    Assert.False(wasNotified[memberId]);
            }
        }
        [Test]
        [TestCase(founderMemberId, managerId1)]
        [TestCase(coOwnerId1, memberId2)]
        [TestCase(managerId1, coOwnerId1)]
        [TestCase(memberId2, coOwnerId2)]
        public void TestRemoveCoOwnerFromStoreNotOwner(int requestingMemberId, int coOwnerToRemoveMemberId)
        {
            SetupStoreCoOwnerChain();

            Assert.Throws<MarketException>(() => store.RemoveCoOwner(requestingMemberId, coOwnerToRemoveMemberId));
            foreach (int memberId in wasNotified.Keys)
                Assert.False(wasNotified[memberId]);
        }
        [Test]
        [TestCase(coOwnerId2, coOwnerId1)]
        [TestCase(coOwnerId2, founderMemberId)]
        [TestCase(coOwnerId1, founderMemberId)]
        public void TestRemoveCoOwnerFromStoreIllegalHieirarchyRequest(int requestingMemberId, int coOwnerToRemoveMemberId)
        {
            SetupStoreCoOwnerChain();

            Assert.Throws<MarketException>(() => store.RemoveCoOwner(requestingMemberId, coOwnerToRemoveMemberId));
            foreach (int memberId in wasNotified.Keys)
                Assert.False(wasNotified[memberId]);
        }
        private void SetupProductsWithAmounts()
        {
            SetupStoreNoPermissionsChange();
            SetUpProductsIdInStore();
            store.AddProductToInventory(founderMemberId, productId1, amount1);
            store.AddProductToInventory(founderMemberId, productId2, amount2);
            store.AddProductToInventory(founderMemberId, productId3, amount3);
        }
        [Test]
        [TestCase(memberId1, new int[] { amount1, amount2, amount3 })]
        [TestCase(memberId2, new int[] { amount1, 0, amount3 })]
        [TestCase(memberId3, new int[] { 0, 0, amount3 })]
        [TestCase(memberId2, new int[] { amount1 - 1, 0, amount3 - 3 })]
        [TestCase(memberId3, new int[] { 0, amount2, 0 })]

        public void TestReserveProductsSuccessfuly(int buyerId, int[] amounts)
        {
            SetupProductsWithAmounts();
            IDictionary<int, int> productsAmount = new Dictionary<int, int>()
            {
                [productId1] = amounts[0],
                [productId2] = amounts[1],
                [productId3] = amounts[2]
            };
            Assert.True(store.products[productId1].amountInInventory == amount1);
            Assert.True(store.products[productId2].amountInInventory == amount2);
            Assert.True(store.products[productId3].amountInInventory == amount3);
            int transactionId = -1;
            Assert.Null(store.ReserveProducts(buyerId, productsAmount, out transactionId));
            Assert.True(store.products[productId1].amountInInventory == amount1 - amounts[0]);
            Assert.True(store.products[productId2].amountInInventory == amount2 - amounts[1]);
            Assert.True(store.products[productId3].amountInInventory == amount3 - amounts[2]);
            Assert.AreNotEqual(transactionId, -1);

        }
        [Test]
        [TestCase(memberId1, new int[] { amount1, amount2+1, amount3 })]
        [TestCase(memberId2, new int[] { amount1, 0, amount3+1 })]
        [TestCase(memberId3, new int[] { 0, 0, amount3+1 })]
        public void TestReserveProductsAmountIsTooBigFail(int buyerId, int[] amounts)
        {
            SetupProductsWithAmounts();
            IDictionary<int, int> productsAmount = new Dictionary<int, int>()
            {
                [productId1] = amounts[0],
                [productId2] = amounts[1],
                [productId3] = amounts[2]
            };
            Assert.True(store.products[productId1].amountInInventory == amount1);
            Assert.True(store.products[productId2].amountInInventory == amount2);
            Assert.True(store.products[productId3].amountInInventory == amount3);
            int transactionId = -1;
            Assert.NotNull(store.ReserveProducts(buyerId, productsAmount, out transactionId));
            Assert.True(store.products[productId1].amountInInventory == amount1);
            Assert.True(store.products[productId2].amountInInventory == amount2);
            Assert.True(store.products[productId3].amountInInventory == amount3);
            Assert.AreEqual(transactionId, -1);
        }
        [Test]
        [TestCase(memberId1, new int[] { amount1, amount2, amount3 })]
        [TestCase(memberId2, new int[] { amount1, 0, amount3 })]
        [TestCase(memberId3, new int[] { 0, 0, amount3})]
        public void TestRollbackTransactionSuccess(int buyerId, int[] amounts) {
            SetupProductsWithAmounts();
            IDictionary<int, int> productsAmount = new Dictionary<int, int>()
            {
                [productId1] = amounts[0],
                [productId2] = amounts[1],
                [productId3] = amounts[2]
            };
            int transactionId = -1;
            Assert.Null(store.ReserveProducts(buyerId, productsAmount, out transactionId));
            Assert.True(store.RollbackTransaction(transactionId));
            Assert.True(store.products[productId1].amountInInventory == amount1);
            Assert.True(store.products[productId2].amountInInventory == amount2);
            Assert.True(store.products[productId3].amountInInventory == amount3);
        }
        [Test]
        [TestCase(-2)]
        [TestCase(-3)]
        [TestCase(-4)]
        public void TestRollbackTransactionDoesNotExistFail(int transactionId)
        {
            SetupProductsWithAmounts();
            Assert.False(store.RollbackTransaction(transactionId));
        }
        [Test]
        [TestCase(memberId1, new int[] { amount1, amount2, amount3 })]
        [TestCase(memberId2, new int[] { amount1, 0, amount3 })]
        [TestCase(memberId3, new int[] { 0, 0, amount3 })]
        public void TestCommitTransactionSuccess(int buyerId, int[] amounts)
        {
            SetupProductsWithAmounts();
            IDictionary<int, int> productsAmount = new Dictionary<int, int>()
            {
                [productId1] = amounts[0],
                [productId2] = amounts[1],
                [productId3] = amounts[2]
            };
            int transactionId = -1;
            Assert.Null(store.ReserveProducts(buyerId, productsAmount, out transactionId));
            Assert.True(store.CommitTransaction(transactionId));
            Assert.True(store.products[productId1].amountInInventory == amount1 - amounts[0]);
            Assert.True(store.products[productId2].amountInInventory == amount2 - amounts[1]);
            Assert.True(store.products[productId3].amountInInventory == amount3 - amounts[2]);
        }
        [Test]
        [TestCase(-2)]
        [TestCase(-3)]
        [TestCase(-4)]
        public void TestCommitTransactionDoesNotExistFail(int transactionId)
        {
            SetupProductsWithAmounts();
            Assert.False(store.RollbackTransaction(transactionId));
            Assert.True(store.products[productId1].amountInInventory == amount1);
            Assert.True(store.products[productId2].amountInInventory == amount2);
            Assert.True(store.products[productId3].amountInInventory == amount3);


        }

        // -------------------- Daily Profit tests -------------------

        [Test]
        [TestCase(10, 20)]
        [TestCase(0, 100)]
        public void TestDailyProfitOnlyFromDaySuccess(double v1, double v2)
        {
            SetupStoreNoRoles();
            DateTime now = DateTime.Now;
            Mock<Purchase> p1 = new Mock<Purchase>(1, now, v1, "Description1") { CallBase = true };
            Mock<Purchase> p2 = new Mock<Purchase>(2, now, v2, "Description2") { CallBase = true };
            store.AddPurchaseRecord(founder.Id, p1.Object);
            store.AddPurchaseRecord(founder.Id, p2.Object);
            Assert.AreEqual(store.GetDailyProfit(), v1 + v2);
        }

        [Test]
        [TestCase(10, 20)]
        [TestCase(0, 100)]
        public void TestDailyProfitOnlyFromTwoDaySuccess(double v1, double v2)
        {
            SetupStoreNoRoles();
            DateTime now = DateTime.Now;
            DateTime tomorrow = new DateTime(2000, 5, 20);
            Mock<Purchase> p1 = new Mock<Purchase>(1, now, v1, "Description1") { CallBase = true };
            Mock<Purchase> p2 = new Mock<Purchase>(2, tomorrow, v2, "Description2") { CallBase = true };
            store.AddPurchaseRecord(founder.Id, p1.Object);
            store.AddPurchaseRecord(founder.Id, p2.Object);
            Assert.AreEqual(store.GetDailyProfit(), v1);
        }

        [Test]
        [TestCase(10, 20)]
        [TestCase(0, 100)]
        public void TestDailyProfitNoPermission(double v1, double v2)
        {
            SetupStoreNoRoles();
            DateTime now = DateTime.Now;
            DateTime tomorrow = new DateTime(2000, 5, 20);
            Mock<Purchase> p1 = new Mock<Purchase>(1, now, v1, "Description1") { CallBase = true };
            Mock<Purchase> p2 = new Mock<Purchase>(2, tomorrow, v2, "Description2") { CallBase = true };
            store.AddPurchaseRecord(founder.Id, p1.Object);
            store.AddPurchaseRecord(founder.Id, p2.Object);
            Assert.Throws<MarketException>(()=>store.GetDailyProfit(founder.Id+1));
        }
    }
}