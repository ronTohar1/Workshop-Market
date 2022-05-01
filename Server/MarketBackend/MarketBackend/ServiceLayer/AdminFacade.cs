using MarketBackend.BusinessLayer;
using MarketBackend.BusinessLayer.Admins;
using MarketBackend.BusinessLayer.Market.StoreManagment;
using MarketBackend.ServiceLayer.ServiceDTO;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.ServiceLayer
{
    internal class AdminFacade
    {
        private AdminManager adminManager;
        private Logger logger;

        public AdminFacade(AdminManager adminManager, Logger logger)
        {
            this.adminManager = adminManager; 
            this.logger = logger;
        }

        public Response<IReadOnlyCollection<Purchase>> GetBuyerPurchaseHistory(int currUserId, int buyerId)
        {
            try
            {
                IReadOnlyCollection<Purchase> l = adminManager.GetUserHistory(currUserId, buyerId);
                logger.Info($"GetBuyerPurchaseHistory was called with parameters [currUserId = {currUserId}, buyerId = {buyerId}]");
                return new Response<IReadOnlyCollection<Purchase>>(l);
            }
            catch (MarketException mex)
            {
                logger.Error(mex, $"method: GetBuyerPurchaseHistory, parameters: [currUserId = {currUserId}, buyerId = {buyerId}]");
                return new Response<IReadOnlyCollection<Purchase>>(mex.Message);
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"method: GetBuyerPurchaseHistory, parameters: [currUserId = {currUserId}, buyerId = {buyerId}]");
                return new Response<IReadOnlyCollection<Purchase>>("Sorry, an unexpected error occured. Please try again");
            }
        }

        public Response<IList<Purchase>> GetStorePurchaseHistory(int currUserId, int storeId)
        {
            try
            {
                IList<Purchase> l = adminManager.GetStoreHistory(currUserId, storeId);
                logger.Info($"GetStorePurchaseHistory was called with parameters [currUserId = {currUserId}, storeId = {storeId}]");
                return new Response<IList<Purchase>>(l);
            }
            catch (MarketException mex)
            {
                logger.Error(mex, $"method: GetStorePurchaseHistory, parameters: [currUserId = {currUserId}, storeId = {storeId}]");
                return new Response<IList<Purchase>>(mex.Message);
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"method: GetStorePurchaseHistory, parameters: [currUserId = {currUserId}, storeId = {storeId}]");
                return new Response<IList<Purchase>>("Sorry, an unexpected error occured. Please try again");
            }
        }

        //public Response<IList<Purchase>> GetBuyerPurchaseHistoryInStore(int currUserId, int buyerId, int storeId)
        //{
            //try
            //{
            //    IList<Purchase> l = adminManager.(currUserId, buyerId);
            //    logger.Info($"GetBuyerPurchaseHistoryInStore was called with parameters [currUserId = {currUserId}, buyerId = {buyerId}, storeId = {storeId}]");
            //    return new Response<IList<Purchase>>(l);
            //}
            //catch (MarketException mex)
            //{
            //    logger.Error(mex, $"method: GetBuyerPurchaseHistoryInStore, parameters: [currUserId = {currUserId}, buyerId = {buyerId}, storeId = {storeId}]");
            //    return new Response<IList<Purchase>>(mex.Message);
            //}
            //catch (Exception ex)
            //{
            //    logger.Error(ex, $"method: GetBuyerPurchaseHistoryInStore, parameters: [currUserId = {currUserId}, buyerId = {buyerId}, storeId = {storeId}]");
            //    return new Response<IList<Purchase>>("Sorry, an unexpected error occured. Please try again");
            //}
            //return new Response<IList<Purchase>>();
        //}

    }
}
