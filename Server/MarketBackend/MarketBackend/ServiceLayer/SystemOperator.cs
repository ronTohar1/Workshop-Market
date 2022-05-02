using MarketBackend.ServiceLayer.ServiceDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketBackend.BusinessLayer.Admins;
using MarketBackend.BusinessLayer.Market;
using MarketBackend.BusinessLayer.Buyers.Members;
using MarketBackend.BusinessLayer.Buyers.Guests;
using MarketBackend.BusinessLayer.Buyers;
using SystemLog;
using NLog;
using MarketBackend.BusinessLayer.System.ExternalServices;
namespace MarketBackend.ServiceLayer
{
    internal class SystemOperator
    {

        private bool marketOpen;
        private AdminFacade adminFacade;
        private BuyerFacade buyerFacade;
        private ExternalSystemFacade externalSystemFacade;
        private StoreManagementFacade storeManagementFacade;
        private Logger logger;

        private const string facadeErrorMsg = "Cannot give any facade when market is closed!";


        public SystemOperator()
        {
            marketOpen = false;

        }

        public Response<bool> OpenMarket(string username, string password)
        {

            if (!VerifyAdmin(username, password))
                return new(false, $"User with username: {username} does not have permission to open the market!");

            //Init controllers
            MembersController membersController = new();
            GuestsController guestsController = new();
            StoreController storeController = new(membersController);
            BuyersController buyersController = new(new List<IBuyersController> { guestsController, membersController });
            ExternalServicesController externalServicesController = new(new ExternalPaymentSystem(), new ExternalSupplySystem());

            PurchasesManager purchasesManager = new(storeController, buyersController, externalServicesController);

            AdminManager adminManager = new(storeController, buyersController);
            InitLogger();
            InitFacades(membersController, guestsController, storeController, buyersController, adminManager, purchasesManager);

            marketOpen = true;
            return new(true);

        }

        public Response<bool> CloseMarket()
        {
            if (!marketOpen)
                return new(false, "Market already closed!");
            marketOpen = false;
            RemoveFacades();
            return new(true);
        }

        public Response<AdminFacade> GetAdminFacade()
        {
            if (!marketOpen)
                return new(null, facadeErrorMsg);
            return new(adminFacade);

        }

        public Response<BuyerFacade> GetBuyerFacade()
        {
            if (!marketOpen)
                return new(null, facadeErrorMsg);
            return new(buyerFacade);
        }

        public Response<ExternalSystemFacade> GetExternalSystemFacade()
        {
            if (!marketOpen)
                return new(null, facadeErrorMsg);
            return new(externalSystemFacade);
        }

        public Response<StoreManagementFacade> GetStoreManagementFacade()
        {
            if (!marketOpen)
                return new(null, facadeErrorMsg);
            return new(storeManagementFacade);
        }


        private void InitLogger()
        {
            logger = SystemLogger.getLogger();
        }

        private void RemoveFacades()
        {
            adminFacade = null;
            buyerFacade = null;
            externalSystemFacade = null;
            storeManagementFacade = null;
        }

        private void InitFacades(MembersController mc, GuestsController gc, StoreController sc, BuyersController bc, AdminManager am, PurchasesManager pm)
        {
            adminFacade = new AdminFacade(am, logger);
            buyerFacade = new BuyerFacade(sc, bc, mc, gc, pm, logger);
            externalSystemFacade = new();
            storeManagementFacade = new(sc, logger);

        }

        private bool VerifyAdmin(string username, string password)
        {
            return true;
        }

    }
}
