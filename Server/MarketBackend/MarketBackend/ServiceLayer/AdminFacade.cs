﻿using MarketBackend.BusinessLayer;
using MarketBackend.BusinessLayer.Admins;
using MarketBackend.BusinessLayer.Market.StoreManagment;
using MarketBackend.ServiceLayer.ServiceDTO;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketBackend.BusinessLayer.Buyers.Members;
namespace MarketBackend.ServiceLayer
{
    public class AdminFacade : IAdminFacade
    {
        private AdminManager adminManager;
        private Logger logger;

        public AdminFacade(AdminManager adminManager, Logger logger)
        {
            this.adminManager = adminManager;
            this.logger = logger;
        }

        public Response<IReadOnlyCollection<ServicePurchase>> GetBuyerPurchaseHistory(int currUserId, int buyerId)
        {
            try
            {
                IReadOnlyCollection<Purchase> l = adminManager.GetUserHistory(currUserId, buyerId);
                IReadOnlyCollection<ServicePurchase> servicePurchases = purchasesToServicePurchases(l);
                logger.Info($"GetBuyerPurchaseHistory was called with parameters [currUserId = {currUserId}, buyerId = {buyerId}]");
                return new Response<IReadOnlyCollection<ServicePurchase>>(servicePurchases);
            }
            catch (MarketException mex)
            {
                logger.Error(mex, $"method: GetBuyerPurchaseHistory, parameters: [currUserId = {currUserId}, buyerId = {buyerId}]");
                return new Response<IReadOnlyCollection<ServicePurchase>>(mex.Message);
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"method: GetBuyerPurchaseHistory, parameters: [currUserId = {currUserId}, buyerId = {buyerId}]");
                return new Response<IReadOnlyCollection<ServicePurchase>>("Sorry, an unexpected error occured. Please try again");
            }
        }

        public Response<IReadOnlyCollection<ServicePurchase>> GetStorePurchaseHistory(int currUserId, int storeId)
        {
            try
            {
                IList<Purchase> l = adminManager.GetStoreHistory(currUserId, storeId);
                IReadOnlyCollection<ServicePurchase> servicePurchases = purchasesToServicePurchases(l);
                logger.Info($"GetStorePurchaseHistory was called with parameters [currUserId = {currUserId}, storeId = {storeId}]");
                return new Response<IReadOnlyCollection<ServicePurchase>>(servicePurchases);
            }
            catch (MarketException mex)
            {
                logger.Error(mex, $"method: GetStorePurchaseHistory, parameters: [currUserId = {currUserId}, storeId = {storeId}]");
                return new Response<IReadOnlyCollection<ServicePurchase>>(mex.Message);
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"method: GetStorePurchaseHistory, parameters: [currUserId = {currUserId}, storeId = {storeId}]");
                return new Response<IReadOnlyCollection<ServicePurchase>>("Sorry, an unexpected error occured. Please try again");
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

        private IReadOnlyCollection<ServicePurchase> purchasesToServicePurchases(IEnumerable<Purchase> purchases)
        {
            List<ServicePurchase> result = new List<ServicePurchase>();
            foreach (Purchase purchase in purchases)
            {
                result.Add(new ServicePurchase(purchase.purchaseDate, purchase.purchasePrice, purchase.purchaseDescription));
            }

            return result;
        }

        // r 6.2
        public Response<bool> RemoveMember(int requestingId, int memberToRemoveId)
        {
            // todo: implement
            return new Response<bool>("Not implemented yet");
        }

        // r 6 d
        public Response<bool> RemoveMemberIfHasNoRoles(int requestingId, int memberToRemoveId)
        {
            try
            {
                bool res = adminManager.RemoveMember(requestingId, memberToRemoveId);
                logger.Info($"RemoveMember was called with parameters [requestingId = {requestingId}, memberToRemoveId = {memberToRemoveId}]");
                return new Response<bool>(res);
            }
            catch (MarketException mex)
            {
                logger.Error(mex, $"method: RemoveMember, parameters: [requestingId = {requestingId}, memberToRemoveId = {memberToRemoveId}]");
                return new Response<bool>(mex.Message);
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"method: RemoveMember, parameters: [requestingId = {requestingId}, memberToRemoveId = {memberToRemoveId}]");
                return new Response<bool>("Sorry, an unexpected error occured. Please try again");
            }
        }

        public Response<bool> MemberExists(int memberId)
        {
            try
            {
                bool res = adminManager.MemberExists(memberId);
                logger.Info($"RemoveMember was called with parameters [memberId = {memberId}]");
                return new Response<bool>(res);
            }
            catch (MarketException mex)
            {
                logger.Error(mex, $"method: RemoveMember, parameters: [memberId = {memberId}]");
                return new Response<bool>(mex.Message);
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"method: RemoveMember, parameters: [memberId = {memberId}]");
                return new Response<bool>("Sorry, an unexpected error occured. Please try again");
            }
        }

        public Response<IList<int>> GetLoggedInMembers(int requestingId)
        {
            try
            {
                IList<int> res = adminManager.GetLoggedInMembers(requestingId);
                logger.Info($"GetLoggedInMembers was called with parameters [requestingId = {requestingId}]");
                return new Response<IList<int>>(res);
            }
            catch (MarketException mex)
            {
                logger.Error(mex, $"method: GetLoggedInMembers, parameters: [requestingId = {requestingId}]");
                return new Response<IList<int>>(mex.Message);
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"method: GetLoggedInMembers, parameters: [requestingId = {requestingId}]");
                return new Response<IList<int>>("Sorry, an unexpected error occured. Please try again");
            }
        }

        public Response<ServiceMember> GetMemberInfo(int requestingId, int memberId)
        {
            try
            {
                Member? member = adminManager.GetMemberInfo(requestingId, memberId);
                if (member == null)
                    return new Response<ServiceMember>($"There isn't such a member with id {memberId}");
                logger.Info($"GetLoggedInMembers was called with parameters [requestingId = {requestingId}, memberId = {memberId}]");
                return new Response<ServiceMember>(new ServiceMember(memberId,member));
            }
            catch (MarketException mex)
            {
                logger.Error(mex, $"method: GetLoggedInMembers, parameters: [requestingId = {requestingId}, memberId = {memberId}]");
                return new Response<ServiceMember>(mex.Message);
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"method: GetLoggedInMembers, parameters: [requestingId = {requestingId}, memberId = {memberId}]");
                return new Response<ServiceMember>("Sorry, an unexpected error occured. Please try again");
            }
        }

        public Response<double> GetSystemDailyProfit(int memberId)
        {
            try
            {
                double total = adminManager.GetSystemDailyProfit(memberId);
                logger.Info($"GetSystemDailyProfit was called with parameters: [memberId = {memberId}]");
                return new Response<double>(total);
            }
            catch (MarketException mex)
            {
                logger.Error(mex, $"method: GetSystemDailyProfit, parameters: [memberId = {memberId}]");
                return new Response<double>(mex.Message);
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"method: GetSystemDailyProfit, parameters: [memberId = {memberId}]");
                return new Response<double>("Sorry, an unexpected error occured. Please try again");
            }
        }
    }
}
