using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MarketBackened.Tests.Acceptance
{
    public class GuestReqsTests : IDisposable
    {
        private BuyerFacade buyerFacade;

        public GuestReqsTests()
        {
            // "global" initialization here; Called before every test method.
        }

        public void Dispose()
        {
            // "global" teardown here; Called after every test method.
        }


        [Fact]
        public void GuestEntrance()
        {

        }
    }
}
