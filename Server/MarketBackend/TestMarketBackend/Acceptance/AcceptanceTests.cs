using MarketBackend.BusinessLayer.Buyers.Members;
using MarketBackend.BusinessLayer.Market;
using MarketBackend.ServiceLayer;
using MarketBackend.ServiceLayer.ServiceDTO;
using NUnit.Framework;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMarketBackend.Acceptance
{
    internal class AcceptanceTests
    {
        // facades
        protected BuyerFacade buyerFacade;
        protected StoreManagementFacade storeManagementFacade;

        // users
        protected int guest1Id;
        protected int guest2Id;
        protected int member1Id;
        protected int member2Id;
        protected const string userName1 = "userName1";
        protected const string password1 = "password";
        protected const string userName2 = "userName2";
        protected const string password2 = "password2";

        // stores
        protected int storeOwnerId;
        protected int serviceStoreId;
        protected const string storeName = "TheStore";

        // products
        protected const string iphoneProductName = "Iphone13";
        protected const int iphoneProductPrice = 4000;
        protected const int iphoneProductAmount = 50;
        protected const string mobileCategory = "Mobile";
        protected int iphoneProductId;

        private void SetUpUsers()
        {
            // Both guests register as members
            Response<int> guest1IdResponse = buyerFacade.Enter();
            guest1Id = guest1IdResponse.Value;
            Response<int> member1IdResponse = buyerFacade.Register(userName1, password1);
            member1Id = member1IdResponse.Value;
            Response<int> guest2IdResponse = buyerFacade.Enter();
            guest2Id = guest2IdResponse.Value;
            Response<int> member2IdResponse = buyerFacade.Register(userName2, password2);
            member2Id = member2IdResponse.Value;
            buyerFacade.Login(userName2, password2);
        }

        public void SetUpStores()
        {
            // Opening a store whose owner is member2
            storeOwnerId = member2Id;
            Response<int> serviceStoreIdResponse = storeManagementFacade.OpenStore(storeOwnerId, storeName);
            serviceStoreId = serviceStoreIdResponse.Value;
        }

        public void SetUpStoresInventories()
        {
            // Adding 50 Iphones to the store
            Response<int> iphoneProductIdResponse = storeManagementFacade.AddNewProduct(storeOwnerId, serviceStoreId, iphoneProductName, iphoneProductPrice, mobileCategory);
            iphoneProductId = iphoneProductIdResponse.Value;
            storeManagementFacade.AddProductToInventory(storeOwnerId, serviceStoreId, iphoneProductId, iphoneProductAmount);
        }

        [SetUp]
        public void SetUp()
        {
            // "global" initialization here; Called before every test method.
            buyerFacade = new BuyerFacade();
            StoreController storeController = new StoreController(new MembersController());
            storeManagementFacade = new StoreManagementFacade(storeController, LogManager.GetCurrentClassLogger());

            SetUpUsers();

            SetUpStores();

            SetUpStoresInventories();

        }
    }
}
