using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Moq;
using System.Threading.Tasks;
using MarketBackend.BusinessLayer.Buyers.Members;
using MarketBackend.BusinessLayer.Buyers;
using MarketBackend.BusinessLayer;

namespace TestMarketBackend.BusinessLayer.Buyers.Members
{
    public class MembersControllerTests
    {
        private MembersController membersController = new MembersController();

        private readonly string validUsername = "Ron";
        private readonly string validPassword = "pass";

        [SetUp]
        public void SetUp()
        {
            membersController = new MembersController();
        }


        [Test]
        public void TestRegister()
        {
            int x =  -1;
            Assert.DoesNotThrow(() => { x = membersController.Register(validUsername, validPassword); });
            Member addedMember = membersController.GetMember(x);
            Assert.IsNotNull(addedMember);
        }


        [Test]
        public void TestRegisterExistingMember()
        {
            membersController.Register(validUsername , validPassword);
            Assert.Throws<MarketException>(() => membersController.Register(validUsername, validPassword));
        }

        [Test]
        public void TestGetMemberByUsername()
        {
            int x = membersController.Register(validUsername, validPassword);
            Member addedMember = membersController.GetMember(validUsername);
            Assert.IsNotNull(addedMember);
            Assert.AreEqual(validUsername, addedMember.Username);
            Assert.AreEqual(x, addedMember.Id);
        }

        [Test]
        public void TestGetMemberById()
        {
            int x = membersController.Register(validUsername, validPassword);
            Member addedMember = membersController.GetMember(x);
            Assert.IsNotNull(addedMember);
            Assert.AreEqual(validUsername, addedMember.Username);
            Assert.AreEqual(x, addedMember.Id);
        }
        
        [Test]
        public void TestGetBuyer()
        {
            int x = membersController.Register(validUsername, validPassword);
            Buyer addedBuyer = membersController.GetBuyer(x);
            Assert.IsNotNull(addedBuyer);
            Assert.AreEqual(x, addedBuyer.Id);
        }



        /*[Test]
        public void TestRegisterUnvalidUsername()
        {
        }

        [Test]
        public void TestRegisterUnvalidPassword()
        {
        }*/



    }
}
