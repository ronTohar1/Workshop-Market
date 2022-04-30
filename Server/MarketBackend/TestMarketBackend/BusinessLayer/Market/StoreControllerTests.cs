using MarketBackend.BusinessLayer.Buyers.Members;
using MarketBackend.BusinessLayer.Buyers;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            memberMock = new Mock<Member>();
            member = memberMock.Object;

            foreach (int existingMemberId in exsitingMembersIds)
            {
                // should return a mock member for call with a right id
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

        // ------- GetStoreIdByName() ----------------------------------------

        [Test]
        [TestCase(storeName1, new string[] { })]
        [TestCase(storeName2, new string[] { storeName1 })]
        public void TestGetStoreIdByNameStoreDoesNotExist(string storeName, string[] storeExtraExistingNames)
        {
            StoreControllerWithStoresSetup(storeExtraExistingNames); 

            Assert.Throws<ArgumentException>(() => storeController.GetStoreIdByName(storeName));
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

            Assert.Throws<ArgumentException>(() => storeController.OpenNewStore(memberId, storeName));

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

            Assert.Throws<ArgumentException>(() => storeController.OpenNewStore(memberId, storeName));
        }

        [Test]
        [TestCase(new int[] { memberId1 }, new string[] { storeName1 })]
        [TestCase(new int[] {memberId1, memberId2 }, new string[] {storeName1, storeName2 })]
        public void TestOpenNewStoreShouldPass(int[] memberIds, string[] storeNames)
        {
            membersConrtollerMemberExistsSetup(memberIds);

            storeControllerMock = new Mock<StoreController>(membersController);
            mockNoStores();

            storeController = storeControllerMock.Object;

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

            Assert.Throws<ArgumentException>(() => storeController.CloseStore(memberId1, storeId)); 
        }

        [Test]
        [TestCase(storeName1, new string[] { })]
        [TestCase(storeName2, new string[] { storeName1 })]
        public void TestCloseStoreWhichIsClosed(string storeName, string[] storeExtraExistingNames)
        {
            StoreControllerWithStoresSetup(storeExtraExistingNames); // also sets up so that member1 exists in the system

            int storeId = addOpenStore(memberId1, storeName);

            storeController.CloseStore(memberId1, storeId); // should work

            Assert.Throws<ArgumentException>(() => storeController.CloseStore(memberId1, storeId));
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
    }
}
