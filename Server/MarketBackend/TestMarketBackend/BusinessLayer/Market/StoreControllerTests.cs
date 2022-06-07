using MarketBackend.BusinessLayer.Buyers.Members;
using MarketBackend.BusinessLayer.Market;
using MarketBackend.BusinessLayer.Market.StoreManagment;
using MarketBackend.BusinessLayer.Buyers;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketBackend.BusinessLayer;

namespace TestMarketBackend.BusinessLayer.Market
{
    public class StoreControllerTests
    {

        private StoreController storeController;
        private Mock<StoreController> storeControllerMock;

        private MembersController membersController;
        private Mock<MembersController> membersControllerMock;
        private Member member;
        private Mock<Member> memberMock;

        private const string storeName1 = "storeAndMore";
        private const string storeName2 = "sports things";
        private const int memberId1 = 1;
        private const int memberId2 = 123;
        private const int storeId1 = 2;
        private const int storeId2 = 29; 

        // ------- Setup helping functions -------------------------------------

        private void membersConrtollerMemberExistsSetup(int[] exsitingMembersIds)
        {
            membersControllerMock = new Mock<MembersController>();
            Mock<Security> securityMock = new Mock<Security>();

            foreach (int existingMemberId in exsitingMembersIds)
            {
                // should return a mock member for call with a right id
                memberMock = new Mock<Member>("user123", "12345678", securityMock.Object) { CallBase = true }; // todo: is this okay
                memberMock.Setup(member=>member.Id).Returns(existingMemberId);
                member = memberMock.Object;
                membersControllerMock.Setup(membersController =>
                    membersController.GetMember(It.Is<int>(id => id == existingMemberId))).
                        Returns(member);
            }

            membersController = membersControllerMock.Object;
        }

        private void membersConrtollerMemberExistsSetup(int exsitingMemberId)
        {
            membersConrtollerMemberExistsSetup(new int[] { exsitingMemberId });
        }

        private void membersControllerMemberDoesNotExistsSetup()
        {
            membersControllerMock = new Mock<MembersController>();

            // returns null for every given id
            membersControllerMock.Setup(membersController =>
                membersController.GetMember(It.IsAny<int>())).
                    Returns((Member)null);

            membersController = membersControllerMock.Object;
        }

        private void mockNoStores()
        {
            // returns false for every given id
            storeControllerMock.Setup(storeController =>
                storeController.StoreExists(It.IsAny<int>())).
                    Returns(false);
            // returns false for every given name
            storeControllerMock.Setup(storeController =>
                storeController.StoreExists(It.IsAny<string>())).
                    Returns(false);
        }

        private void mockStoreExists(int existingStoreId, string existingStoreName)
        {
            // returns null for every given id
            storeControllerMock.Setup(membersController =>
                membersController.StoreExists(It.IsAny<int>())).
                    Returns((int id) => id == existingStoreId);
            // returns null for every given name
            storeControllerMock.Setup(membersController =>
                membersController.StoreExists(It.IsAny<string>())).
                    Returns((string name) => existingStoreName.Equals(name));
        }

        // returns the id of the new store
        private int addOpenStore(int existingMemberId, string storeName)
        {
            return storeController.OpenNewStore(existingMemberId, storeName);
        }

        private void StoreControllerWithStoresSetup(string [] storesNames)
        {
            membersConrtollerMemberExistsSetup(memberId1);
            storeController = new StoreController(membersController);

            for (int i = 0; i < storesNames.Length; i++)
            {
                addOpenStore(memberId1, storesNames[i]);
            }
        }

        // open and closed store names should be distinct here
        private void StoreControllerWithStoresSetup(string[] openStoresNames, string[] closedStoresNames)
        {
            StoreControllerWithStoresSetup(openStoresNames);

            int currentStoreId; 
            for (int i = 0; i < closedStoresNames.Length; i++)
            {
                currentStoreId = addOpenStore(memberId1, closedStoresNames[i]);
                storeController.CloseStore(memberId1, currentStoreId); // memberId1 is the founder of the store so it supposed to have permission
            }
        }

        // ------- GetStoreIdByName() ----------------------------------------

        [Test]
        [TestCase(storeName1, new string[] { })]
        [TestCase(storeName2, new string[] { storeName1 })]
        public void TestGetStoreIdByNameStoreDoesNotExist(string storeName, string[] storeExtraExistingNames)
        {
            StoreControllerWithStoresSetup(storeExtraExistingNames); 

            Assert.Throws<MarketException>(() => storeController.GetStoreIdByName(storeName));
        }

        [Test]
        [TestCase(storeName1, new string[] { })]
        [TestCase(storeName2, new string[] { storeName1 })]
        public void TestGetStoreIdByNameStoreExists(string storeName, string[] storeExtraExistingNames)
        {
            StoreControllerWithStoresSetup(storeExtraExistingNames); 

            int storeId = addOpenStore(memberId1, storeName);

            Assert.AreEqual(storeId, storeController.GetStoreIdByName(storeName));
        }

        // ------- OpenNewStore() ----------------------------------------

        [Test]
        [TestCase(memberId1, storeName1)]
        [TestCase(memberId2, storeName2)]
        public void TestOpenNewStoreMemberExistsStoreExists(int memberId, string storeName)
        {
            int storeId = 1;

            membersConrtollerMemberExistsSetup(memberId);

            storeControllerMock = new Mock<StoreController>(membersController);
            mockStoreExists(storeId, storeName);

            storeController = storeControllerMock.Object;

            Assert.Throws<MarketException>(() => storeController.OpenNewStore(memberId, storeName));

            // todo: add a test for opening a store with an existing closed store name
        }

        [Test]
        [TestCase(memberId1, storeName1)]
        [TestCase(memberId2, storeName2)]
        public void TestOpenNewStoreMemberDoesNotExistExistsStoreDoesNotExist(int memberId, string storeName)
        {
            membersControllerMemberDoesNotExistsSetup();

            storeControllerMock = new Mock<StoreController>(membersController);
            mockNoStores();

            storeController = storeControllerMock.Object;

            Assert.Throws<MarketException>(() => storeController.OpenNewStore(memberId, storeName));
        }

        [Test]
        [TestCase(new int[] { memberId1 }, new string[] { storeName1 })]
        [TestCase(new int[] { memberId1, memberId2 }, new string[] { storeName1, storeName2 })]
        public void TestOpenNewStoreShouldPassMcokHelpingFunctions(int[] memberIds, string[] storeNames)
        {
            membersConrtollerMemberExistsSetup(memberIds);

            storeControllerMock = new Mock<StoreController>(membersController);
            mockNoStores();
            storeController = storeControllerMock.Object;
            // mock so in this test not checking using all helping function, checking this in the next test function

            List<int> storeIds = new List<int>();
            int currentId;

            for (int i = 0; i < memberIds.Length; i++)
            {
                currentId = storeController.OpenNewStore(memberIds[i], storeNames[i]);

                Assert.False(storeIds.Contains(currentId));

                storeIds.Add(currentId);
            }

            // todo: maybe add synchronization tests
        }

        [Test]
        [TestCase(new int[] { memberId1 }, new string[] { storeName1 })]
        [TestCase(new int[] { memberId1, memberId2 }, new string[] { storeName1, storeName2 })]
        public void TestOpenNewStoreShouldPassUseGetOpenStore(int[] memberIds, string[] storeNames)
        {
            membersConrtollerMemberExistsSetup(memberIds);

            storeController = new StoreController(membersController);
            // also using store exists bevause not mocking 

            List<int> storeIds = new List<int>();
            int currentId;

            for (int i = 0; i < memberIds.Length; i++)
            {
                currentId = storeController.OpenNewStore(memberIds[i], storeNames[i]);

                Assert.False(storeIds.Contains(currentId));

                storeIds.Add(currentId);
            }

            foreach (int storeId in storeIds)
            {
                Assert.NotNull(storeController.GetOpenStore(storeId));
            }

            // todo: maybe add synchronization tests
        }

        // ------- CloseStore() ----------------------------------------

        [Test]
        [TestCase(storeId1)]
        [TestCase(storeId2)]
        public void TestCloseStoreNoStores(int storeId)
        {

            membersConrtollerMemberExistsSetup(memberId1); 
            storeControllerMock = new Mock<StoreController>(membersController);
            mockNoStores(); // to make sure
            storeController = storeControllerMock.Object;

            Assert.Throws<MarketException>(() => storeController.CloseStore(memberId1, storeId)); 
        }

        [Test]
        [TestCase(storeName1, new string[] { })]
        [TestCase(storeName2, new string[] { storeName1 })]
        public void TestCloseStoreWhichIsClosed(string storeName, string[] storeExtraExistingNames)
        {
            StoreControllerWithStoresSetup(storeExtraExistingNames); // also sets up so that member1 exists in the system

            int storeId = addOpenStore(memberId1, storeName);

            storeController.CloseStore(memberId1, storeId); // should work

            Assert.Throws<MarketException>(() => storeController.CloseStore(memberId1, storeId));
        }

        // todo: should add tests for CloseStore in the Store testing

        [Test]
        [TestCase(storeName1, new string[] { })]
        [TestCase(storeName2, new string[] { storeName1 })]
        public void TestCloseStoreShouldPass(string storeName, string[] storeExtraExistingNames)
        {
            StoreControllerWithStoresSetup(storeExtraExistingNames); // also sets up so that member1 exists in the system

            int storeId = addOpenStore(memberId1, storeName);

            storeController.CloseStore(memberId1, storeId); // should work

            Assert.IsNull(storeController.GetOpenStore(storeId));
            Assert.NotNull(storeController.GetClosedStore(storeId));
        }

        // ------- SearchProductsInOpenStores() --------------------------------

        // no stores
        // some closed stores and no open stores
        // two open stores one closed store

        // no stores match
        // some stores
        // no products match in store - in this case should not return store

        [Test]
        [TestCase(new string[] { }, new string[] { }, "store", new string[] { })]
        [TestCase(new string[] { }, new string[] { "store3" }, "store", new string[] { })]
        [TestCase(new string[] { "store1", "store2" }, new string[] { "store3" }, "store", new string[] { })]
        [TestCase(new string[] { "store1", "store2" }, new string[] { "store3" }, "st123", new string[] { })]
        [TestCase(new string[] { "store1", "store2" }, new string[] { "store3" }, "re1", new string[] { })]
        [TestCase(new string[] { }, new string[] { }, "store", new string[] { }, false)]
        [TestCase(new string[] { "store1", "store2" }, new string[] { "store3" }, "re1", new string[] { "store1" }, false)]
        public void TestSerachProductsInOpenStoresNoProduts(string[] openStoresNames, string[] closedStoreNames, string nameInSearch, string[] expectedStoreNames, bool storesWithProductsThatPassedFilter = true)
        {
            StoreControllerWithStoresSetup(openStoresNames, closedStoreNames); // also sets up so that member1 exists in the system

            ProductsSearchFilter filter = new ProductsSearchFilter();
            filter.FilterStoreName(nameInSearch);

            IDictionary<int, IList<Product>> result = storeController.SearchProductsInOpenStores(filter, storesWithProductsThatPassedFilter);

            IList<string> resultStoresNames = result.Keys.Select(id => storeController.GetStore(id).GetName()).ToList();
        
            Assert.IsTrue(SameElements(expectedStoreNames, resultStoresNames));
        }

        //[Test]
        //[TestCase(new string[] { }, new string[] { }, "store", new string[] { })]
        //[TestCase(new string[] { }, new string[] { "store3" }, "store", new string[] { })]
        //[TestCase(new string[] { "store1", "store2" }, new string[] { "store3" }, "store", new string[] { "store1", "store2" })]
        //[TestCase(new string[] { "store1", "store2" }, new string[] { "store3" }, "st123", new string[] { })]
        //[TestCase(new string[] { "store1", "store2" }, new string[] { "store3" }, "re1", new string[] { "store1" })]
        //public void TestSerachProductsInOpenStores(string[] openStoresNames, string[] closedStoreNames, string nameInSearch, string[] expectedStoreNames)
        //{
        //    StoreControllerWithStoresSetup(openStoresNames, closedStoreNames); // also sets up so that member1 exists in the system

        //    ProductsSearchFilter filter = new ProductsSearchFilter();
        //    filter.FilterStoreName(nameInSearch);

        //    IDictionary<int, IList<Product>> result = storeController.SearchProductsInOpenStores(filter);

        //    IList<string> resultStoresNames = result.Keys.Select(id => storeController.getStore(id).GetName()).ToList();

        //    Assert.IsTrue(SameElements(expectedStoreNames, resultStoresNames));

        //    // todo: add products and test this (need to add many tests) 
        //}

        // ------- SearchOpenStores() ---------------------------------

        // these tests are similar to the ones of SearchProductsInOpenStores above, 
        // but one function does not use the other for efficiency so they are both 
        // tested on the implementation that they both have in their code 

        [Test]
        [TestCase(new string[] { }, new string[] { }, "store", new string[] { })]
        [TestCase(new string[] { }, new string[] { "store3" }, "store", new string[] { })]
        [TestCase(new string[] { "store1", "store2" }, new string[] { "store3" }, "store", new string[] { "store1", "store2" })]
        [TestCase(new string[] { "store1", "store2" }, new string[] { "store3" }, "st123", new string[] { })]
        [TestCase(new string[] { "store1", "store2" }, new string[] { "store3" }, "re1", new string[] { "store1" })]
        public void TestSerachOpenStores(string[] openStoresNames, string[] closedStoreNames, string nameInSearch, string[] expectedStoreNames)
        {
            StoreControllerWithStoresSetup(openStoresNames, closedStoreNames); // also sets up so that member1 exists in the system

            ProductsSearchFilter filter = new ProductsSearchFilter();
            filter.FilterStoreName(nameInSearch);

            IList<int> result = storeController.SearchOpenStores(filter);

            IList<string> resultStoresNames = result.Select(id => storeController.GetStore(id).GetName()).ToList();

            Assert.IsTrue(SameElements(expectedStoreNames, resultStoresNames));
        }

        private bool SameElements<T>(IList<T> list1, IList<T> list2)
        {
            if (list1.Count != list2.Count)
                return false;
            return list1.All(element => list2.Contains(element));

        }
    }
}
