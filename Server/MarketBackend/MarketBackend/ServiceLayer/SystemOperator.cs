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
using MarketBackend.BusinessLayer;
using NLog;
using MarketBackend.BusinessLayer.System.ExternalServices;
namespace MarketBackend.ServiceLayer
{
    internal class SystemOperator
    {

        private bool marketOpen;
        private BusiessSystemOperator bso;
        private AdminFacade adminFacade;
        private BuyerFacade buyerFacade;
        private ExternalSystemFacade externalSystemFacade;
        private StoreManagementFacade storeManagementFacade;

        private const string facadeErrorMsg = "Cannot give any facade when market is closed!";


        public SystemOperator()
        {
            marketOpen = false;
            bso = new BusiessSystemOperator();
            adminFacade = null;
            buyerFacade = null;
            externalSystemFacade = null;
            storeManagementFacade = null;
        }

        public Response<int> OpenMarket(string username, string password)
        {
            try { 
                int adminId = bso.OpenMarket(username, password);// will initialize the controllers if it's the first boot
                InitFacades(bso.membersController, bso.guestsController, bso.storeController, bso.buyersController, bso.adminManager, bso.purchasesManager);
                marketOpen = true;
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

        public Response<bool> CloseMarket()
        {
            try
            {
                bso.CloseMarket();
                InitFacades(bso.membersController, bso.guestsController, bso.storeController, bso.buyersController, bso.adminManager, bso.purchasesManager);
                bso.logger.Info("method CloseMarket was called");
                marketOpen = false;
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


    }
}
