using MarketBackend.DataLayer.DataDTOs.Buyers;
using MarketBackend.DataLayer.DataDTOs.Buyers.Carts;
using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement;
using MarketBackend.DataLayer.DataManagers;
using MarketBackend.DataLayer.DataManagers.DataManagersInherentsForTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMarketBackend
{
    public class DataManagersMock
    {
        public static void InitMockDataManagers()
        {
            // database mocks
            Mock<ForTestingCartDataManager> c = new Mock<ForTestingCartDataManager>();
            Mock<ForTestingMemberDataManager> m = new Mock<ForTestingMemberDataManager>();
            Mock<ForTestingProductInBagDataManager> pib = new Mock<ForTestingProductInBagDataManager>();
            Mock<ForTestingShoppingBagDataManager> sb = new Mock<ForTestingShoppingBagDataManager>();
            Mock<ForTestingStoreDataManager> s = new Mock<ForTestingStoreDataManager>();

            // Cart
            c.Setup(x => x.Add(It.IsAny<DataCart>()));
            c.Setup(x => x.Remove(It.IsAny<int>()));
            c.Setup(x => x.Update(It.IsAny<int>(), It.IsAny<Action<DataCart>>()));
            c.Setup(x => x.Find(It.IsAny<int>())).Returns((DataCart)null);
            c.Setup(x => x.Save());
            CartDataManager.ForTestingSetInstance(c.Object);

            // Member
            m.Setup(x => x.Add(It.IsAny<DataMember>()));
            m.Setup(x => x.Remove(It.IsAny<int>()));
            m.Setup(x => x.Update(It.IsAny<int>(), It.IsAny<Action<DataMember>>()));
            m.Setup(x => x.Find(It.IsAny<int>())).Returns((DataMember)null);
            m.Setup(x => x.GetNextId()).Returns(0);
            m.Setup(x => x.Save());
            MemberDataManager.ForTestingSetInstance(m.Object);

            // Product In Bag
            pib.Setup(x => x.Add(It.IsAny<DataProductInBag>()));
            pib.Setup(x => x.Remove(It.IsAny<int>()));
            pib.Setup(x => x.Update(It.IsAny<int>(), It.IsAny<Action<DataProductInBag>>()));
            pib.Setup(x => x.Find(It.IsAny<int>())).Returns((DataProductInBag)null);
            pib.Setup(x => x.Save());
            ProductInBagDataManager.ForTestingSetInstance(pib.Object);

            // Shopping Bag
            sb.Setup(x => x.Add(It.IsAny<DataShoppingBag>()));
            sb.Setup(x => x.Remove(It.IsAny<int>()));
            sb.Setup(x => x.Update(It.IsAny<int>(), It.IsAny<Action<DataShoppingBag>>()));
            sb.Setup(x => x.Find(It.IsAny<int>())).Returns((DataShoppingBag)null);
            sb.Setup(x => x.Save());
            ShoppingBagDataManager.ForTestingSetInstance(sb.Object);

            // Store
            s.Setup(x => x.Add(It.IsAny<DataStore>()));
            s.Setup(x => x.Remove(It.IsAny<int>()));
            s.Setup(x => x.Update(It.IsAny<int>(), It.IsAny<Action<DataStore>>()));
            s.Setup(x => x.Find(It.IsAny<int>())).Returns((DataStore)null);
            s.Setup(x => x.Save());
            StoreDataManager.ForTestingSetInstance(s.Object);
        }
    }
}
