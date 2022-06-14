using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Moq;
using MarketBackend.BusinessLayer.Market.StoreManagment;
using MarketBackend.BusinessLayer;
using System.Threading;

namespace TestMarketBackend.BusinessLayer.Market.StoreManagment
{
    internal class BidTests
    {
        int pid1;

        int mid1;

        int sid1;

        double bid1;

        Bid b1;

        [SetUp]
        public void Setup()
        {
            pid1 = 1;

            mid1 = 1;

            sid1 = 1;

            bid1 = 100;

            b1 = new Bid(pid1, mid1, sid1, bid1);
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(5)]
        public void TestApproveBids(int howMany)
        {
            for (int i = 0; i < howMany; i++)
                b1.approveBid(i, new Action(() => Thread.Sleep(0)));
            IList<int> a = b1.aprovingIds;
            for (int i = 0; i < howMany; i++)
                Assert.IsTrue(a.Contains(i));
        }

        [Test]
        [TestCase(50)]
        [TestCase(0)]
        public void TestCounterOffer(int sum)
        {
            b1.CounterOffer(sum);
            Assert.IsTrue(b1.counterOffer);
        }

        [Test]
        [TestCase(50)]
        [TestCase(0)]
        public void TestApproveCounterOfferSuccess(int sum)
        {
            b1.CounterOffer(sum);
            b1.approveCounterOffer(new Action(() => Thread.Sleep(0)));
            Assert.IsTrue(!b1.counterOffer);
        }

        [Test]
        public void TestApproveCounterOfferFail()
        {
            Assert.Throws<MarketException>(() => b1.approveCounterOffer(new Action(() => Thread.Sleep(0))));
        }

    }
}
