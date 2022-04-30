using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using MarketBackend.ServiceLayer;
using MarketBackend.ServiceLayer.ServiceDTO;

namespace MarketBackened.Tests.Acceptance
{
    public class GuestReqsTests : IDisposable
    {
        private BuyerFacade buyerFacade;

        public GuestReqsTests()
        {
            // "global" initialization here; Called before every test method.
            //buyerFacade = new BuyerFacade();
        }

        public void Dispose()
        {
            // "global" teardown here; Called after every test method.
        }


        [Test]
        public void GuestEntrance()
        {
            Response<int> idResponse = buyerFacade.Enter();

            int id = idResponse.Value;

            Response<ServiceCart> cartResponse = buyerFacade.GetCart(id);
        }
    }
}
