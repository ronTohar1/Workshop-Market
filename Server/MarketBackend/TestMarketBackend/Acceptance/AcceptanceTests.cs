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
using MarketBackend.BusinessLayer.System.ExternalServices;

namespace TestMarketBackend.Acceptance
{
    internal class AcceptanceTests
    {
        // system operator
        private SystemOperator systemOperator;

        // facades
        protected BuyerFacade buyerFacade;
        protected StoreManagementFacade storeManagementFacade;

        // users
        protected static int guest1Id;
        protected static int guest2Id;
        protected static int guest3Id;
        protected static int guest4Id;
        protected static int member1Id;
        protected static int member2Id;
        protected static int member3Id;
        protected static int member4Id;
        protected const string userName1 = "userName1";
        protected const string password1 = "password";
        protected const string userName2 = "userName2";
        protected const string password2 = "password2";
        protected const string userName3 = "userName3";
        protected const string password3 = "password3";
        protected const string userName4 = "userName4";
        protected const string password4 = "password4";

        //Admin
        protected const string adminUsername = "admin";
        protected const string adminPassword = "admin";

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

            Response<int> guest4IdResponse = buyerFacade.Enter();
            guest4Id = guest4IdResponse.Value;
            Response<int> member4IdResponse = buyerFacade.Register(userName4, password4);
            member4Id = member4IdResponse.Value;

            buyerFacade.Login(userName2, password2);
            buyerFacade.Login(userName3, password3);
        }

        public void SetUpStores()
        {
            // Opening a store whose owner is member2
            storeOwnerId = member2Id;
            Response<int> serviceStoreIdResponse = storeManagementFacade.OpenStore(storeOwnerId, storeName);
            storeId = serviceStoreIdResponse.Value;

            // the owner(member2) appoints member3 as a store owner
            Response<bool> response = storeManagementFacade.MakeCoOwner(member2Id, member3Id, storeId);

            // the owner(member2) appoints member4 as a store owner
            response = storeManagementFacade.MakeCoManager(member2Id, member4Id, storeId);
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
            systemOperator = new SystemOperator();
            Response<bool> response = systemOperator.OpenMarket(adminUsername,adminPassword);
            if (response.ErrorOccured())
                throw new Exception("Unexpected exception in acceptance setup");

            // "global" initialization here; Called before every test method.
            MembersController membersController = new MembersController();
            StoreController storeController = new StoreController(membersController);
            BuyersController bc = new BuyersController();
            MembersController mc = membersController;
            GuestsController gc = new GuestsController();
            bc.AddBuyersController(mc);
            bc.AddBuyersController(gc); 
            ExternalServicesController esc = new ExternalServicesController(new ExternalPaymentSystem(), new ExternalSupplySystem());
            PurchasesManager pm = new PurchasesManager(storeController, bc, esc);
            buyerFacade = new BuyerFacade(storeController, bc, mc, gc, pm, LogManager.GetCurrentClassLogger());
            storeManagementFacade = new StoreManagementFacade(storeController, LogManager.GetCurrentClassLogger());

            SetUpUsers();

            SetUpStores();

            SetUpStoresInventories();

        }
    }
}
