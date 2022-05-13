using MarketBackend.ServiceLayer.ServiceDTO;
using NUnit.Framework;
using System.Threading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMarketBackend.Acceptance
{
    internal class GuestReqsTests : AcceptanceTests
    {

        // r.1.1
        [Test]
        public void GuestEntrance()
        {
            Response<int> idResponse = buyerFacade.Enter();

            int id = idResponse.Value;

            Response<ServiceCart> cartResponse = buyerFacade.GetCart(id);

            ServiceCart cart = cartResponse.Value;

            Assert.IsNotNull(cart);
            Assert.IsTrue(cart.IsEmpty());
        }

        // r.1.3
        [Test]
        [TestCase("ValidName", "ValidPass123")]
        public void SuccessfulGuestRegistration(string userName, string password)
        {
            Response<int> idResponse = buyerFacade.Enter();

            Response<int> response = buyerFacade.Register(userName, password);

            Assert.IsTrue(!response.ErrorOccured());
        }

        // r.1.3
        [Test]
        [TestCase("#@@#@#....InValidName", "----1.....@*InValidPass123")]
        public void InvalidDetailsGuestRegistration(string userName, string password)
        {
            Response<int> idResponse = buyerFacade.Enter();

            Response<int> response = buyerFacade.Register(userName, password);

            Assert.IsTrue(response.ErrorOccured());
        }

        // r.1.3
        [Test]
        [TestCase("ValidName", "ValidPass123")]
        public void DuplicateGuestRegistration(string userName, string password)
        {
            // first guest enters and registers
            Response<int> guest1 = buyerFacade.Enter();

            Response<int> response = buyerFacade.Register(userName, password);

            Assert.IsTrue(!response.ErrorOccured());

            buyerFacade.Leave(guest1.Value);

            // second guest enters and registers with the same details

            buyerFacade.Enter();

            response = buyerFacade.Register(userName, password);

            Assert.IsTrue(response.ErrorOccured());
        }

        [Test]
        [TestCase("userName", "password", 2)]
        [TestCase("userName", "password", 10)]
        [TestCase("userName", "password", 40)]
        public void ConcurrentRegistration(string userName, string password, int threadsNumber)
        {
            Func<Response<int>>[] jobs = 
                Enumerable.Repeat(() => buyerFacade.Register(userName, password), threadsNumber).ToArray();

            Response<int>[] responses = GetResponsesFromThreads(jobs);

            Assert.IsTrue(Exactly1ResponseIsSuccessful(responses));
        }

        // r.1.4
        [Test]
        [TestCase(userName1, password1)]
        public void SuccessfulLogin(string userName, string password)
        {
            Response<int> reponse = buyerFacade.Login(userName, password);

            Assert.IsTrue(!reponse.ErrorOccured());
        }

        [TestCase(userName1, password1)]
        public void FailureDoubleLogin(string username, string password)
        {
            buyerFacade.Login(username, password);
            Response<int> reponse = buyerFacade.Login(username, password);

            Assert.IsTrue(reponse.ErrorOccured());
        }

        [Test]
        [TestCase("user_name123", "pass12345")]
        public void LoginWithNonExistentDetails(string userName, string password)
        {
            Response<int> reponse = buyerFacade.Login(userName, password);

            Assert.IsTrue(reponse.ErrorOccured());
        }


    }
}
