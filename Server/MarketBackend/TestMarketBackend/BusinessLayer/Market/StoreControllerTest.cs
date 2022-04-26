using MarketBackend.BusinessLayer.Buyers.Members;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMarketBackend.BusinessLayer.Market
{
    public class StoreControllerTest
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
            storeControllerMock.Setup(membersController =>
                membersController.StoreExists(It.IsAny<int>())).
                    Returns(false);
            // returns false for every given name
            storeControllerMock.Setup(membersController =>
                membersController.StoreExists(It.IsAny<string>())).
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

            // todo: maybe add synchronization tests
        }
    }
}
