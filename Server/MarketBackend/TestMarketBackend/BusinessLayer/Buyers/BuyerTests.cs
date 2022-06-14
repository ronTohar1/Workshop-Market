using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using NUnit.Framework;
using Moq;
using System.Threading.Tasks;
using MarketBackend.BusinessLayer.Market.StoreManagment;
using MarketBackend.BusinessLayer.Buyers;

namespace TestMarketBackend.BusinessLayer.Buyers
{
    internal class BuyerTests
    {
        private Buyer buyer;
        private Purchase purchase;
        private DateTime purchaseDate = DateTime.MinValue;
        private double purchasePrice = 1;
        private string purhcaseDescription = "Regular buy";


        [SetUp]
        public void SetUp()
        {
            DataManagersMock.InitMockDataManagers();
            buyer = new Buyer();
            Mock<Purchase> purchaseMock = new Mock<Purchase>(buyer.Id, purchaseDate, purchasePrice,purhcaseDescription);
            purchase = purchaseMock.Object;
        }

        [Test]
        public void TestDifferentId()
        {
            Buyer buyer1 = new Buyer();
            Assert.AreNotEqual(buyer1.Id, buyer.Id);
        }

        [Test]
        public void TestAddPurchase()
        {
            Assert.DoesNotThrow(() => buyer.AddPurchase(purchase));
        }

        [Test]
        public void TestGetPurchase()
        {
            IEnumerable<Purchase> purchases = null;
            Assert.DoesNotThrow(() => buyer.AddPurchase(purchase));
            Assert.DoesNotThrow(() => { purchases = buyer.GetPurchaseHistory(); });
            Assert.NotNull(purchases);
            Assert.AreEqual(1, purchases.Count());

        }
    }
}
