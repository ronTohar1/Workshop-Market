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

        public Response<bool> IsAdmin(int adminId)
        {
            try
            {
                bool res = adminManager.ContainAdmin(adminId);
                logger.Info($"IsAdmin was called with id = {adminId}");
                return new Response<bool>(res);
            }
            catch (MarketException mex)
            {
                logger.Error(mex, $"method: IsAdmin ,parameters [adminId = {adminId}]");
                return new Response<bool>(mex.Message);
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"method: IsAdmin ,parameters [adminId = {adminId}]");
                return new Response<bool>("Sorry, an unexpected error occured. Please try again");
            }
        }

        public Response<IList<ServiceMember>> GetLoggedInMembers(int requestingId)
        {
            try
            {
                IDictionary<int, Member> members =  adminManager.GetLoggedInMembers(requestingId);
                IList<ServiceMember> res = members.Keys.Select(key => new ServiceMember(members[key])).ToList();
                logger.Info($"GetLoggedInMembers was called with parameters [requestingId = {requestingId}]");
                return new Response<IList<ServiceMember>>(res);
            }
            catch (MarketException mex)
            {
                logger.Error(mex, $"method: GetLoggedInMembers, parameters: [requestingId = {requestingId}]");
                return new Response<IList<ServiceMember>>(mex.Message);
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"method: GetLoggedInMembers, parameters: [requestingId = {requestingId}]");
                return new Response<IList<ServiceMember>>("Sorry, an unexpected error occured. Please try again");
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
                return new Response<ServiceMember>(new ServiceMember(member));
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


        public Response<string> GetEventLogs(int userId)
        {
            try
            {
                string logs = adminManager.GetEventLogs(userId);
                logger.Info($"GetEventLogs was called with parameters: [userId = {userId}]");
                Response<string> output = new Response<string>(logs);
                if (output.ErrorOccured)
                {
                    output.ErrorOccured = false;
                    output.Value = output.ErrorMessage;
                    output.ErrorMessage = "";
                }
                return output;

            }
            catch (MarketException mex)
            {
                logger.Error(mex, $"method: GetEventLogs, parameters: [userId = {userId}]");
                return new Response<string>(mex.Message);
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"method: GetEventLogs, parameters: [userId = {userId}]");
                return new Response<string>("Sorry, an unexpected error occured. Please try again");
            }
        }

        public Response<string> GetErrorLogs(int userId)
        {
            try
            {
                string logs = adminManager.GetErrorLogs(userId);
                logger.Info($"GetErrorLogs was called with parameters: [userId = {userId}]");
                Response<string> output =  new Response<string>(logs);
                if (output.ErrorOccured) {
                    output.ErrorOccured = false;
                    output.Value = output.ErrorMessage;
                    output.ErrorMessage = "";
                }
                return output;
                
            }
            catch (MarketException mex)
            {
                logger.Error(mex, $"method: GetErrorLogs, parameters: [userId = {userId}]");
                return new Response<string>(mex.Message);
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"method: GetErrorLogs, parameters: [userId = {userId}]");
                return new Response<string>("Sorry, an unexpected error occured. Please try again");
            }
        }
        public Response<int[]> GetDailyVisitores(int memberId, DateTime fromDate, DateTime toDate)
        {
            // this function should return the daily cut of visitores in a given interval of dates
            // the output is an array of size 5 of all the given entries of visitores as follows:
            // [number_of_admin_visits, number_of_storeOwners_visitors, number_of_managers_without_any_stores_visits,
            // number_of_simple_members(not manager or store owner), number_of_guests]

            // ** Imporatant ** check that memberId is admin, fromDate<=toDate, and that fromDate<=currentDate
            try
            {
                logger.Info($"GetDailyVisitores was called with parameters: [memberId = {memberId}, fromDate = {fromDate}, toDate = {toDate}]");
                return new Response<int[]>(new int[5] { 0, 0, 3, 0, 0 });
            }
            catch (MarketException mex)
            {
                logger.Error(mex, $"method: GetDailyVisitores was called with parameters: [memberId = {memberId}, fromDate = {fromDate}, toDate = {toDate}]");
                return new Response<int[]>(mex.Message);
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"GetDailyVisitores was called with parameters: [memberId = {memberId}, fromDate = {fromDate}, toDate = {toDate}]");
                return new Response<int[]>("Sorry, an unexpected error occured. Please try again");
            }
        }
    }
}
