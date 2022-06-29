using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Moq;
using MarketBackend.BusinessLayer.Admins;
using MarketBackend.BusinessLayer.Market;
using MarketBackend.BusinessLayer.Buyers;
using MarketBackend.BusinessLayer.Buyers.Members;
using MarketBackend.BusinessLayer.Market.StoreManagment;
using MarketBackend.BusinessLayer;

namespace TestMarketBackend.BusinessLayer
{
    internal class AdminManagerTests
    {
        private const int adminId = 1;
        AdminManager adminManager;

        // profit checks
        double v1;
        double v2;

        int guest1Id = 2;
        int guest2Id = 3;
        int member1Id = 4; 
        int member2Id = 5;
        int manager1Id = 6;
        int manager2Id = 7;
        int coOwner1Id = 8;
        int coOwner2Id = 9;

        [SetUp]
        public void SetupAdmin()
        {
            DataManagersMock.InitMockDataManagers(); 

            v1 = 10;
            v2 = 20;

            Mock<Store> s1 = new Mock<Store>();
            Mock<Store> s2 = new Mock<Store>();

            s1.Setup(x => x.GetDailyProfit()).Returns(v1);
            s2.Setup(x => x.GetDailyProfit()).Returns(v2);

            IDictionary<int, Store> stores = new Dictionary<int, Store>();
            stores.Add(1, s1.Object);
            stores.Add(2, s2.Object);

            Mock<StoreController> ms = new Mock<StoreController>();
            ms.Setup(x => x.GetOpenStores()).Returns(stores);

            Mock<MembersController> mc = new Mock<MembersController>();
            Mock<Member> m = new Mock<Member>();
            mc.Setup(x => x.GetMember(It.IsAny<int>())).Returns(m.Object);

            Mock<AdminManager> adminManagerMock = new Mock<AdminManager>(ms.Object, It.IsAny<BuyersController>(), mc.Object);

            SetupRoles(adminManagerMock);

            adminManager = adminManagerMock.Object;
            adminManager.AddAdmin(adminId);
        }

        private void MockRole(Mock<AdminManager> adminManagerMock, int memberOfRoleId, Role role)
        {
            adminManagerMock.Setup(adminManager =>
               adminManager.HasRoleInMarket(It.Is<int>(memberId => memberId == memberOfRoleId), It.Is<Role>(argumentRole => argumentRole == role))).
                   Returns(true);
        }

        private void SetupRoles(Mock<AdminManager> adminManagerMock)
        {
            adminManagerMock.Setup(adminManager =>
               adminManager.HasRoleInMarket(It.IsAny<int>(), It.IsAny<Role>())).
                   Returns(false);
            MockRole(adminManagerMock, coOwner1Id, Role.Owner);
            MockRole(adminManagerMock, coOwner2Id, Role.Owner);
            MockRole(adminManagerMock, manager1Id, Role.Manager);
            MockRole(adminManagerMock, manager2Id, Role.Manager); 
        }

        [Test]
        public void TestGetSystemProfitValidAdmin()
        {
            Assert.AreEqual(v1 + v2, adminManager.GetSystemDailyProfit(adminId));
        }

        [Test]
        public void TestGetSystemProfitInValidAdmin()
        {
            Assert.Throws<MarketException>(() => adminManager.GetSystemDailyProfit(adminId + 1));
        }

        [Test]
        [TestCase(adminId + 1)]
        public void TestGetMarketStatisticsBetweenDatesNoPermissionFailed(int reqestingMemberId)
        {
            Assert.Throws<MarketException>(() => adminManager.GetMarketStatisticsBetweenDates(reqestingMemberId, DateOnly.FromDateTime(DateTime.Now.AddDays(-2).Date), DateOnly.FromDateTime(DateTime.Now.AddDays(1))));
        }

        [Test]
        public void TestGetMarketStatisticsBetweenDatesFromIsAfterToFailed()
        {
            Assert.Throws<MarketException>(() => adminManager.GetMarketStatisticsBetweenDates(adminId, DateOnly.FromDateTime(DateTime.Now.AddDays(2).Date), DateOnly.FromDateTime(DateTime.Now.AddDays(-1))));
        }

        [Test]
        public void TestGetMarketStatisticsBetweenDatesSuccessful()
        {
            adminManager.OnGuestEnter();
            adminManager.OnGuestEnter();
            adminManager.OnGuestEnter();
            adminManager.OnMemberLogin(member1Id);
            adminManager.OnMemberLogin(manager1Id);
            adminManager.OnMemberLogin(coOwner1Id);
            adminManager.OnMemberLogin(coOwner2Id);
            adminManager.OnMemberLogin(adminId);

            int[] expectedMarektStatistics = new int[]
            {
                1,  // admins 
                2, // coOwners
                1, // managers
                1, // members
                3 // guests
            };

            int[] marketStatistics = adminManager.GetMarketStatisticsBetweenDates(adminId, DateOnly.FromDateTime(DateTime.Now.AddDays(-2).Date), DateOnly.FromDateTime(DateTime.Now.AddDays(1)));

            CheckAreExpectedMarketStatistics(expectedMarektStatistics, marketStatistics);
        }

        private void CheckAreExpectedMarketStatistics(int[] expectedMarketStatistics, int[] marketStatistics)
        {
            Assert.IsTrue(marketStatistics != null);
            Assert.AreEqual(expectedMarketStatistics.Length, marketStatistics.Length);

            for (int i = 0; i < expectedMarketStatistics.Length; i++)
            {
                Assert.AreEqual(expectedMarketStatistics[i], marketStatistics[i]);
            }
        }
    }
}
