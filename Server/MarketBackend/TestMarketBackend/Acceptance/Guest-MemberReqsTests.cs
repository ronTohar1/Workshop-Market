using MarketBackend.ServiceLayer.ServiceDTO;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMarketBackend.Acceptance
{
    internal class Guest_MemberReqsTests : AcceptanceTests
    {

        // r.1.2
        [Test]
        public void GuestDisconnection()
        {
            Response<bool> response = buyerFacade.Leave(guest1Id);

            Assert.IsTrue(!response.ErrorOccured());

            Response<ServiceCart> cartResponse = buyerFacade.GetCart(guest1Id);

            Assert.IsTrue(cartResponse.ErrorOccured());
            Assert.IsNull(cartResponse.Value);
        }

        // r.1.2
        [Test]
        public void MemberDisconnection()
        {
            Response<ServiceCart> cartResponse = buyerFacade.GetCart(guest1Id);

            Response<bool> response = buyerFacade.Leave(member1Id);

            Assert.IsTrue(!response.ErrorOccured());

            Response<ServiceCart> cartAfterDisconnectionResponse = buyerFacade.GetCart(guest1Id);

            Assert.IsTrue(!cartResponse.ErrorOccured());
            Assert.IsTrue(cartResponse.Value.Equals(cartAfterDisconnectionResponse.Value));
        }

        // r.2.1
        [Test]
        public void GetInformationAboutStore()
        {
            // user1 is a guest
            Response<ServiceStore> response = buyerFacade.GetStoreInfo(guest1Id, serviceStoreId);

            Assert.IsTrue(!response.ErrorOccured());
            Assert.IsNotNull(response.Value);

            // user2 is a member
            response = buyerFacade.GetStoreInfo(member2Id, serviceStoreId);

            Assert.IsTrue(!response.ErrorOccured());
            Assert.IsNotNull(response.Value);
        }

        private bool IsSearchCorrect(IDictionary<int, IList<ServiceProduct>> result)
        {

        }

        // r.2.1
        [Test]
        [TestCase(storeName, null, null, null)]
        [TestCase(null, iphoneProductName, null, null)]
        public void GetInformationAboutProductInStore(string storeName, string productName, string category, string keyword)
        {
            Response<IDictionary<int, IList<ServiceProduct>>> response = 
                buyerFacade.ProductsSearch(storeName, productName, category, keyword);

            Assert.IsTrue(!response.ErrorOccured());
            Assert.IsNotNull(response.Value);
            Assert.IsTrue(IsSearchCorrect(response.Value));
        }

        // r.2.2
        [Test]
        [TestCase(Name)]
        [TestCase(Category)]
        [TestCase(keyWords)]
        [TestCase(metric)]
        [TestCase(ByName)]
        public void SuccessfulSearchProduct()
        {

        }

        // r.2.2
        [Test]
        public void FailedSearchProduct()
        {

        }

        // r.2.3
        [Test]
        public void SuccessfulProductKeeping()
        {

        }

        // r.2.3
        [Test]
        public void FailedProductKeeping()
        {

        }

        // r.2.4
        [Test]
        public void SuccessfulViewCart()
        {

        }



    }
}
