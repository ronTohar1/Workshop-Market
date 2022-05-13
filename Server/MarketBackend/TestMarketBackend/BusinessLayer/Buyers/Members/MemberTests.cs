using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Moq;
using System.Threading.Tasks;
using MarketBackend.BusinessLayer.Buyers.Members;
using MarketBackend.BusinessLayer;

namespace TestMarketBackend.BusinessLayer.Buyers.Members
{
    public class MemberTests
    {

        private string username1 = "Ron";
        private string username2 = "Nir";
        private readonly string password1 = "password1";
        private readonly string password2 = "password2";
        private Member member1;
        private Member member2;


        private Member SetUpMember(string username, string password)
        {
            Mock<Security> mock = new Mock<Security>();
            //Trying to figure how to replace a method with another method that depends on parameter.
            //mock.Setup(s => s.HashPassword(It.IsAny<string>())).Returns((string x) => x.GetHashCode());
            Security security = mock.Object;
            //Console.WriteLine(security.HashPassword("hello"));
            return new Member(username, password, security);

        }
        [SetUp]
        public void SetUp()
        {
            member1 = SetUpMember(username1, password1);
            member2 = SetUpMember(username2, password2);
        }

        [Test]
        public void TestLoginValid()
        {
            Assert.IsTrue(member1.Login(password1));
            Assert.IsTrue(member1.LoggedIn);
        }

        [Test]
        public void TestLoginWrongPassword()
        {
            Assert.IsFalse(member1.Login(password2));
            Assert.IsFalse(member1.LoggedIn);
        }

        [Test]
        public void TestLoginTwice()
        {
            Assert.IsTrue(member1.Login(password1));
            Assert.Throws<MarketException>(() => member1.Login(password1));
        }

        [Test]
        public void TestLogoutValid()
        {

            Assert.IsTrue(member1.Login(password1));
            Assert.DoesNotThrow(() => member1.Logout());

        }


        [Test]
        public void TestLogoutNoLogin()
        {
            Assert.Throws<Exception>(() => member1.Logout());
        }


        [Test]
        public void TestLogoutTwice()
        {
            Assert.IsTrue(member1.Login(password1));
            Assert.DoesNotThrow(() => member1.Logout());
            Assert.Throws<Exception>(() => member1.Logout());
        }

    }
}
