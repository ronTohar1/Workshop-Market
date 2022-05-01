using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Moq;
using System.Threading.Tasks;
using MarketBackend.BusinessLayer.Buyers;
using MarketBackend.BusinessLayer;
using MarketBackend.BusinessLayer.Buyers.Guests;

namespace TestMarketBackend.BusinessLayer.Buyers.Guests
{
    public class GuestsControllerTests
    {
        private GuestsController guestsController = new();
        private readonly int loggedInId = 1000;
        private readonly int loggedOutId = 2000;
        [SetUp]
        public void SetUp()
        {
            guestsController = new();
            Mock<Buyer> mockBuyer1 = new Mock<Buyer>();
            mockBuyer1.Setup(mockBuyer1 => mockBuyer1.Id).Returns(loggedInId);
            guestsController.Buyers.Add(loggedInId, mockBuyer1.Object);
        }

        [Test]
        public void TestGetBuyer_LoggedIn()
        {
            Buyer? buyer = guestsController.GetBuyer(loggedInId);
            Assert.IsNotNull(buyer);
            Assert.AreEqual(loggedInId, buyer.Id);
        }

        [Test]
        public void TestGetBuyer_NotLoggedIn()
        {
            Buyer? buyer = guestsController.GetBuyer(loggedOutId);
            Assert.IsNull(buyer);
        }

        [Test]
        public void TestEnter()
        {
            var oldBuyers = new Dictionary<int, Buyer>(guestsController.Buyers);
            int oldBuyersCount = oldBuyers.Count();
            int newId = guestsController.Enter();
            Assert.IsFalse(oldBuyers.ContainsKey(newId));
            Assert.IsTrue(guestsController.Buyers.ContainsKey(newId));
            Assert.IsTrue(guestsController.Buyers.Count == oldBuyersCount + 1);
        }

        [Test]
        public void TestLeave()
        { 
            Assert.IsTrue(guestsController.Buyers.ContainsKey(loggedInId));
            guestsController.Leave(loggedInId);
            Assert.IsFalse(guestsController.Buyers.ContainsKey(loggedInId));
        }

    }
}
