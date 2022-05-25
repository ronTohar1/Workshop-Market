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
        private string username3 = "David";
        private readonly string password1 = "password1";
        private readonly string password2 = "password2";
        private readonly string password3 = "password3";
        private Member member1;
        private Member member2;
        private bool notifyWasCalled = false;

        private readonly Func<string[], bool> dummyNotify = (string[] notifications) => true;
        private Func<string[], bool> failNotify;
        private Func<string[], bool> successNotify;
        private Member SetUpMember(string username, string password)
        {
            Mock<Security> mock = new Mock<Security>();
            //Trying to figure how to replace a method with another method that depends on parameter.
            //mock.Setup(s => s.HashPassword(It.IsAny<string>())).Returns((string x) => x.GetHashCode());
            Security security = mock.Object;
            //Console.WriteLine(security.HashPassword("hello"));
            return new Member(username, password, security);

        }
        private Func<string[], bool> CreateNotificationSender(bool notificationSucceeded) 
            =>  
            (string[] notifications) => {
                this.notifyWasCalled = true;
                return notificationSucceeded;
            };

        [SetUp]
        public void SetUp()
        {
            member1 = SetUpMember(username1, password1);
            member2 = SetUpMember(username2, password2);
            failNotify = CreateNotificationSender(false);
            successNotify = CreateNotificationSender(true);
        }

        [Test]
        public void TestLoginValid()
        {
            Assert.IsTrue(member1.Login(password1, dummyNotify));
            Assert.IsTrue(member1.LoggedIn);
        }

        [Test]
        public void TestLoginWrongPassword()
        {
            Assert.IsFalse(member1.Login(password2, dummyNotify));
            Assert.IsFalse(member1.LoggedIn);
        }

        [Test]
        public void TestLoginTwice()
        {
            Assert.IsTrue(member1.Login(password1, dummyNotify));
            Assert.Throws<MarketException>(() => member1.Login(password1, dummyNotify));
        }

        [Test]
        public void TestLogoutValid()
        {

            Assert.IsTrue(member1.Login(password1, dummyNotify));
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
            Assert.IsTrue(member1.Login(password1, dummyNotify));
            Assert.DoesNotThrow(() => member1.Logout());
            Assert.Throws<Exception>(() => member1.Logout());
        }

        
        [Test]
        [TestCase("you do not have such permissions")]
        [TestCase("you are no longer coManager")]
        public void TestNotifyMemberSuccedded(string notificationMessage) {
            notifyWasCalled = false;
            member2.Login(password2, successNotify);
            member2.Notify(notificationMessage);
            Assert.IsTrue(notifyWasCalled && member2.pendingNotifications.Count==0);
        }

        [Test]
        [TestCase("you've reached your bag limit")]
        [TestCase("you are no longer coManager")]
        public void TestNotifyMemberFailed(string notificationMessage)
        {
            notifyWasCalled = false;
            member2.Login(password2, failNotify);
            member2.Notify(notificationMessage);
            Assert.IsTrue(notifyWasCalled && member2.pendingNotifications.Contains(notificationMessage) );
        }

        [Test]
        [TestCase("you've reached your bag limit")]
        [TestCase("you do not have such permissions")]
        public void TestNotifyMemberLoggedOut(string notificationMessage)
        {
            notifyWasCalled = false;
            member2.Notify(notificationMessage);
            Assert.IsTrue(!notifyWasCalled && member2.pendingNotifications.Contains(notificationMessage));
        }

        [Test]
        [TestCase("you are no longer coManager")]
        [TestCase("you do not have such permissions")]
        public void TestNotifyMemberLoggedOutThenLoggedInWithNotifySuccess(string notificationMessage)
        {
            notifyWasCalled = false;
            member2.Notify(notificationMessage);
            Assert.IsTrue(!notifyWasCalled && member2.pendingNotifications.Contains(notificationMessage));
            member2.Login(password2, successNotify);
            Assert.IsTrue(notifyWasCalled && member2.pendingNotifications.Count==0);
        }

        [Test]
        [TestCase("you are no longer coManager")]
        [TestCase("you've reached your bag limit")]
        public void TestNotifyMemberLoggedOutThenLoggedInWithNotifyFail(string notificationMessage)
        {
            notifyWasCalled = false;
            member2.Notify(notificationMessage);
            Assert.IsTrue(!notifyWasCalled && member2.pendingNotifications.Contains(notificationMessage));
            member2.Login(password2, failNotify);
            Assert.IsTrue(notifyWasCalled && member2.pendingNotifications.Contains(notificationMessage));
        }
    }
}
