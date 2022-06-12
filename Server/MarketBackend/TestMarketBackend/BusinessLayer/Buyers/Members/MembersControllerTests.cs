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
using MarketBackend.DataLayer.DataManagers.DataManagersInherentsForTesting;
using MarketBackend.DataLayer.DataDTOs.Buyers.Carts;
using MarketBackend.DataLayer.DataManagers;
using MarketBackend.DataLayer.DataDTOs.Buyers;
using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement;

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


        [Test]
        public void TestRemove() {
            int x = membersController.Register(validUsername, validPassword);
            Assert.NotNull(membersController.GetMember(x));
            membersController.RemoveMember(x);
            Assert.Null(membersController.GetMember(x));
        }



    }
}
