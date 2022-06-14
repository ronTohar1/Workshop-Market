using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Moq;
using System.Threading.Tasks;
using MarketBackend.BusinessLayer.Buyers.Members;
using MarketBackend.BusinessLayer;
using MarketBackend.DataLayer.DataManagers.DataManagersInherentsForTesting;
using MarketBackend.DataLayer.DataDTOs.Buyers.Carts;
using MarketBackend.DataLayer.DataManagers;
using MarketBackend.DataLayer.DataDTOs.Buyers;
using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement;

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
            // database mocks
            Mock<ForTestingCartDataManager> c = new Mock<ForTestingCartDataManager>();
            Mock<ForTestingMemberDataManager> m = new Mock<ForTestingMemberDataManager>();
            Mock<ForTestingProductInBagDataManager> pib = new Mock<ForTestingProductInBagDataManager>();
            Mock<ForTestingShoppingBagDataManager> sb = new Mock<ForTestingShoppingBagDataManager>();
            Mock<ForTestingStoreDataManager> s = new Mock<ForTestingStoreDataManager>();

            c.Setup(x => x.Add(It.IsAny<DataCart>()));
            c.Setup(x => x.Remove(It.IsAny<int>()));
            c.Setup(x => x.Update(It.IsAny<int>(), It.IsAny<Action<DataCart>>()));
            c.Setup(x => x.Find(It.IsAny<int>())).Returns((DataCart)null);
            c.Setup(x => x.Save());
            CartDataManager.ForTestingSetInstance(c.Object);

            m.Setup(x => x.Add(It.IsAny<DataMember>()));
            m.Setup(x => x.Remove(It.IsAny<int>()));
            m.Setup(x => x.Update(It.IsAny<int>(), It.IsAny<Action<DataMember>>()));
            m.Setup(x => x.Find(It.IsAny<int>())).Returns((DataMember)null);
            m.Setup(x => x.Save());
            MemberDataManager.ForTestingSetInstance(m.Object);

            pib.Setup(x => x.Add(It.IsAny<DataProductInBag>()));
            pib.Setup(x => x.Remove(It.IsAny<int>()));
            pib.Setup(x => x.Update(It.IsAny<int>(), It.IsAny<Action<DataProductInBag>>()));
            pib.Setup(x => x.Find(It.IsAny<int>())).Returns((DataProductInBag)null);
            pib.Setup(x => x.Save());
            ProductInBagDataManager.ForTestingSetInstance(pib.Object);

            sb.Setup(x => x.Add(It.IsAny<DataShoppingBag>()));
            sb.Setup(x => x.Remove(It.IsAny<int>()));
            sb.Setup(x => x.Update(It.IsAny<int>(), It.IsAny<Action<DataShoppingBag>>()));
            sb.Setup(x => x.Find(It.IsAny<int>())).Returns((DataShoppingBag)null);
            sb.Setup(x => x.Save());
            ShoppingBagDataManager.ForTestingSetInstance(sb.Object);

            s.Setup(x => x.Add(It.IsAny<DataStore>()));
            s.Setup(x => x.Remove(It.IsAny<int>()));
            s.Setup(x => x.Update(It.IsAny<int>(), It.IsAny<Action<DataStore>>()));
            s.Setup(x => x.Find(It.IsAny<int>())).Returns((DataStore)null);
            s.Setup(x => x.Save());
            StoreDataManager.ForTestingSetInstance(s.Object);

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
