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
using MarketBackend.BusinessLayer.Buyers;
using MarketBackend.BusinessLayer.Buyers.Guests;

namespace TestMarketBackend.Acceptance
{
    internal class AcceptanceTests
    {
        // facades
        protected BuyerFacade buyerFacade;
        protected StoreManagementFacade storeManagementFacade;

        // users
        protected static int guest1Id;
        protected static int guest2Id;
        protected static int guest3Id;
        protected static int member1Id;
        protected static int member2Id;
        protected static int member3Id;
        protected const string userName1 = "userName1";
        protected const string password1 = "password";
        protected const string userName2 = "userName2";
        protected const string password2 = "password2";
        protected const string userName3 = "userName3";
        protected const string password3 = "password3";

        // stores
        protected static int storeOwnerId;
        protected static int storeId;
        protected const string storeName = "TheStore";

        // products
        protected const string iphoneProductName = "Iphone13";
        protected const int iphoneProductPrice = 4000;
        protected const int iphoneProductAmount = 50;
        protected const string mobileCategory = "Mobile";
        protected static int iphoneProductId;

        protected const string calculatorProductName = "991ES Calculator";
        protected const int calculatorProductPrice = 100;
        protected const int calculatorProductAmount = 200;
        protected const string officeCategory = "Office";
        protected static int calculatorProductId;

        private void SetUpUsers()
        {
            // Both guests register as members
            // guest1 stays in the market as a guest
            // member1 logs in
            Response<int> guest1IdResponse = buyerFacade.Enter();
            guest1Id = guest1IdResponse.Value;
            Response<int> member1IdResponse = buyerFacade.Register(userName1, password1);
            member1Id = member1IdResponse.Value;

            Response<int> guest2IdResponse = buyerFacade.Enter();
            guest2Id = guest2IdResponse.Value;
            Response<int> member2IdResponse = buyerFacade.Register(userName2, password2);
            member2Id = member2IdResponse.Value;

            Response<int> guest3IdResponse = buyerFacade.Enter();
            guest3Id = guest3IdResponse.Value;
            Response<int> member3IdResponse = buyerFacade.Register(userName3, password3);
            member3Id = member3IdResponse.Value;

            buyerFacade.Login(userName2, password2);
            buyerFacade.Login(userName3, password3);
        }

        public void SetUpStores()
        {
            // Opening a store whose owner is member2
            storeOwnerId = member2Id;
            Response<int> serviceStoreIdResponse = storeManagementFacade.OpenStore(storeOwnerId, storeName);
            storeId = serviceStoreIdResponse.Value;
        }

        public void SetUpStoresInventories()
        {
            // Adding Iphones to the store
            Response<int> iphoneProductIdResponse = storeManagementFacade.AddNewProduct(storeOwnerId, storeId, iphoneProductName, iphoneProductPrice, mobileCategory);
            iphoneProductId = iphoneProductIdResponse.Value;
            storeManagementFacade.AddProductToInventory(storeOwnerId, storeId, iphoneProductId, iphoneProductAmount);

            // Adding calculators to the store but there arent any in the stock
            Response<int> calculatorProductIdResponse = storeManagementFacade.AddNewProduct(storeOwnerId, storeId, calculatorProductName, calculatorProductPrice, officeCategory);
            calculatorProductId = calculatorProductIdResponse.Value;
        }

        public void SetUpShoppingCarts()
        {
            buyerFacade.AddProdcutToCart(member3Id, storeId, iphoneProductId, 2);
            buyerFacade.AddProdcutToCart(member3Id, storeId, calculatorProductId, 5);
        }

        [SetUp]
        public void SetUp()
        {
            // "global" initialization here; Called before every test method.
            StoreController storeController = new StoreController(new MembersController());
            BuyersController bc = new BuyersController();
            MembersController mc = new MembersController();
            GuestsController gc = new GuestsController();
            buyerFacade = new BuyerFacade(storeController, bc, mc, gc, LogManager.GetCurrentClassLogger());
            storeManagementFacade = new StoreManagementFacade(storeController, LogManager.GetCurrentClassLogger());

            SetUpUsers();

            SetUpStores();

            SetUpStoresInventories();

        }
    }
}
