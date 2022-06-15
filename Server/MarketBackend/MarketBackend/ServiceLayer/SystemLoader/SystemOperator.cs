using MarketBackend.ServiceLayer.ServiceDTO;
using MarketBackend.BusinessLayer.Admins;
using MarketBackend.BusinessLayer.Market;
using MarketBackend.BusinessLayer.Buyers.Members;
using MarketBackend.BusinessLayer.Buyers.Guests;
using MarketBackend.BusinessLayer.Buyers;
using MarketBackend.BusinessLayer;
namespace MarketBackend.ServiceLayer
{
    public class SystemOperator : ISystemOperator
    {

        public bool MarketOpen { get; private set; }
        public int MarketOpenerAdminId { get => bso.MarketOpenerAdminId;  }
        private BusiessSystemOperator bso;
        private AdminFacade adminFacade;
        private BuyerFacade buyerFacade;
        private ExternalSystemFacade externalSystemFacade;
        private StoreManagementFacade storeManagementFacade;

        private const string facadeErrorMsg = "Cannot give any facade when market is closed!";

        // for init from config file
        public SystemOperator()
        {
            AppConfig appConfig = new AppConfig();
            if (!appConfig.RunInitFile)
                throw new Exception("You can use this c'tor only when system should be loaded using init file. Please recheck appconfig file.");

            SystemLoader systemLoader = new SystemLoader(appConfig.InitFilePath, this);
            bso = new BusiessSystemOperator(systemLoader.AdminUsername, systemLoader.AdminPassword, false); // will initialize the controllers if it's the first boot;
            OpenMarket(systemLoader.AdminUsername, systemLoader.AdminPassword);
            systemLoader.LoadSystem();
        }


        public SystemOperator(string username, string password, bool loadDatabase=true)
        {
            MarketOpen = false;
            bso = new BusiessSystemOperator(username, password, loadDatabase); // will initialize the controllers if it's the first boot;
            adminFacade = null;
            buyerFacade = null;
            externalSystemFacade = null;
            storeManagementFacade = null;
            Response<int> response = OpenMarket(username, password);
            if (response.IsErrorOccured())
                throw new Exception(response.ErrorMessage);
        }

        public Response<int> OpenMarket(string username, string password, bool loadDatabase=true)
        {
            try
            {
                if (MarketOpen)
                {
                    bso.logger.Error("Market is already open", $"method: OpenMarket, parameters: [username = {username}, can not reveal password]");
                    return new("Market is already open.");     
                }
                if (!bso.MarketOpen)
                {
                    if (loadDatabase)
                        bso.OpenMarketWithDatabase(username, password);
                    else
                        bso.OpenMarket(username, password);
                }
                int adminId = bso.MarketOpenerAdminId;
                InitFacades(bso.membersController, bso.guestsController, bso.storeController, bso.buyersController, bso.adminManager, bso.purchasesManager);
                MarketOpen = true;
                bso.logger.Info($"OpenMarket with parameters: [username = {username}, can not reveal password]");
                return new(adminId);
            }
            catch (MarketException mex)
            {
                bso.logger.Error(mex, $"method: OpenMarket, parameters: [username = {username}, can not reveal password]");
                return new Response<int>(mex.Message);
            }
            catch (Exception ex)
            {
                bso.logger.Error(ex, $"method: OpenMarket, parameters: [username = {username}, can not reveal password]");
                return new Response<int>("Sorry, an unexpected error occured. Please try again");
            }
        }

        public Response<bool> CloseMarket(bool clearDatabase=false)
        {
            try
            {
                bso.CloseMarket(clearDatabase);
                //InitFacades(bso.membersController, bso.guestsController, bso.storeController, bso.buyersController, bso.adminManager, bso.purchasesManager);
                bso.logger.Info("method CloseMarket was called");
                MarketOpen = false;
                RemoveFacades();
                return new(true);
            }
            catch (MarketException mex)
            {
                bso.logger.Error(mex, "method CloseMarket was called");
                return new Response<bool>(mex.Message);
            }
            catch (Exception ex)
            {
                bso.logger.Error(ex, "method CloseMarket was called");
                return new Response<bool>("Sorry, an unexpected error occured. Please try again");
            }
        }

        public Response<AdminFacade> GetAdminFacade()
        {
            if (!MarketOpen)
                return new(null, facadeErrorMsg);
            return new(adminFacade);

        }

        public Response<BuyerFacade> GetBuyerFacade()
        {
            if (!MarketOpen)
                return new(null, facadeErrorMsg);
            return new(buyerFacade);
        }

        public Response<ExternalSystemFacade> GetExternalSystemFacade()
        {
            if (!MarketOpen)
                return new(null, facadeErrorMsg);
            return new(externalSystemFacade);
        }

        public Response<StoreManagementFacade> GetStoreManagementFacade()
        {
            if (!MarketOpen)
                return new(null, facadeErrorMsg);
            return new(storeManagementFacade);
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
            adminFacade = new AdminFacade(am, bso.logger);
            buyerFacade = new BuyerFacade(sc, bc, mc, gc, pm, bso.logger);
            externalSystemFacade = new();
            storeManagementFacade = new(sc, bso.logger);

        }

        internal static void RemoveAllDatabaseContent()
        {
            BusiessSystemOperator.RemoveAllDatabaseContent(); 
        }
    }
}
