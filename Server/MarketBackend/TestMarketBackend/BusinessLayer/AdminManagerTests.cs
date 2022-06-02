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
        int adminId;
        AdminManager adminManager;

        // profit checks
        double v1;
        double v2;

        [SetUp]
        public void SetupAdmin()
        {
            adminId = 1;

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
            ms.Setup(x => x.openStores).Returns(stores);

            adminManager = new AdminManager(ms.Object, It.IsAny<BuyersController>(), It.IsAny<MembersController>());
            adminManager.AddAdmin(adminId);

        }

        [Test]
        public void TestGetSystemProfitValidAdmin()
        {
            Assert.AreEqual(v1 + v2, adminManager.GetSystemDailyProfit(adminId));
        }

        [Test]
        public void TestGetSystemProfitInValidAdmin()
        {
            Assert.Throws<MarketException>(() => adminManager.GetSystemDailyProfit(adminId+1));
        }
    }
}
