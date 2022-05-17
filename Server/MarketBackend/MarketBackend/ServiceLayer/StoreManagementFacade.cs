using MarketBackend.BusinessLayer.Market.StoreManagment;
using MarketBackend.ServiceLayer.ServiceDTO;
using NLog;
using SystemLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketBackend.BusinessLayer.Market;
using MarketBackend.BusinessLayer;

namespace MarketBackend.ServiceLayer
{
    internal class StoreManagementFacade
    {
        private StoreController storeController;
        private Logger logger;

        public StoreManagementFacade(StoreController storeController, Logger logger)
        {
            this.storeController = storeController;
            this.logger = logger;
        }

        //done
        public Response<int> AddNewProduct(int userId, int storeId, string productName, double price, string category)
        {
            try
            {
                Store s = storeController.GetStore(storeId);
                if (s == null)
                    return new Response<int>($"There isn't a store with an id {storeId}");
                int id = s.AddNewProduct(userId, productName, price, category);
                logger.Info($"AddNewProduct was called with parameters [userId = {userId}, storeId = {storeId}, productName = {productName}, price = {price}, category = {category}]");
                return new Response<int>(id);

            }
            catch (MarketException mex)
            {
                logger.Error(mex, $"method: AddNewProduct, parameters: [userId = {userId}, storeId = {storeId}, productName = {productName}, price = {price}, category = {category}]");
                return new Response<int>(mex.Message);
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"method: AddNewProduct, parameters: [userId = {userId}, storeId = {storeId}, productName = {productName}, price = {price}, category = {category}]");
                return new Response<int>("Sorry, an unexpected error occured. Please try again");
            }
        }

        //done
        public Response<bool> AddProductToInventory(int userId, int storeId, int productId, int amount)
        {
            try
            {
                Store s = storeController.GetStore(storeId);
                if (s == null)
                    return new Response<bool>($"There isn't a store with an id {storeId}");
                s.AddProductToInventory(userId, productId, amount);
                logger.Info($"AddProductToInventory was called with parameters [userId = {userId}, storeId = {storeId}, productId = {productId}, amount = {amount}]");
                return new Response<bool>(true);
            }
            catch (MarketException mex)
            {
                logger.Error(mex, $"method: AddProductToInventory, parameters: [userId = {userId}, storeId = {storeId}, productId = {productId}, amount = {amount}]");
                return new Response<bool>(mex.Message);
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"method: AddProductToInventory, parameters: [userId = {userId}, storeId = {storeId}, productId = {productId}, amount = {amount}]");
                return new Response<bool>("Sorry, an unexpected error occured. Please try again");
            }
        }

        //done
        public Response<bool> DecreaseProduct(int userId, int storeId, int productId, int amount)
        {
            try
            {
                Store s = storeController.GetStore(storeId);
                if (s == null)
                    return new Response<bool>($"There isn't a store with an id {storeId}");
                s.DecreaseProductAmountFromInventory(userId, productId, amount);
                logger.Info($"DecreaseProduct was called with parameters: [userId = {userId}, storeId = {storeId}, productId = {productId}, amount = {amount}]");
                return new Response<bool>(true);
            }
            catch (MarketException mex)
            {
                logger.Error(mex, $"method: DecreaseProduct, parameters: [userId = {userId}, storeId = {storeId}, productId = {productId}, amount = {amount}]");
                return new Response<bool>(mex.Message);
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"method: DecreaseProduct, parameters: [userId = {userId}, storeId = {storeId}, productId = {productId}, amount = {amount}]");
                return new Response<bool>("Sorry, an unexpected error occured. Please try again");
            }
        }

        //done
        public Response<bool> AddPurchasePolicy(PurchaseOption type, int userId, int storeId)
        {
            try
            {
                Store s = storeController.GetStore(storeId);
                if (s == null)
                    return new Response<bool>($"There isn't a store with an id {storeId}");
                s.AddPurchaseOption(userId, type);
                logger.Info($"AddPurchasePolicy was called with parameters: [type = {type}, userId = {userId}, storeId = {storeId}]");
                return new Response<bool>(true);
            }
            catch (MarketException mex)
            {
                logger.Error(mex, $"method: AddPurchasePolicy, parameters: [type = {type}, userId = {userId}, storeId = {storeId}]");
                return new Response<bool>(mex.Message);
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"method: AddPurchasePolicy, parameters: [type = {type}, userId = {userId}, storeId = {storeId}]");
                return new Response<bool>("Sorry, an unexpected error occured. Please try again");
            }
        }

        //TODO
        public Response<bool> RemovePurchasePolicy(PurchaseOption type, int userId, int storeId)
        {
            //try
            //{
            //    Store s = storeController.getStore(storeId);
            //    if (s == null)
            //        return new Response<bool>($"There isn't a store with an id {storeId}");

            //    logger.Info($"RemovePurchasePolicy was called with parameters: [type = {type}, userId = {userId}, storeId = {storeId}]");
            //    return new Response<bool>("default");
            //}
            //catch (MarketException mex)
            //{
            //    logger.Error(mex, $"method: RemovePurchasePolicy, parameters: [type = {type}, userId = {userId}, storeId = {storeId}]");
            //    return new Response<bool>(mex.Message);
            //}
            //catch (Exception ex)
            //{
            //    logger.Error(ex, $"method: RemovePurchasePolicy, parameters: [type = {type}, userId = {userId}, storeId = {storeId}]");
            //    return new Response<bool>("Sorry, an unexpected error occured. Please try again");
            //}
            return new Response<bool>();
        }

        //TODO
        public Response<bool> AddDiscountPolicy(string discountCode, int userId, int storeId, int productId, double discount)
        {
            //try
            //{
            //    Store s = storeController.getStore(storeId);
            //    if (s == null)
            //        return new Response<bool>($"There isn't a store with an id {storeId}");

            //    logger.Info($"AddDiscountPolicy was called with parameters: [discountCode = {discountCode}, userId = {userId}, storeId = {storeId}, productId={productId}, discount={discount}]");
            //    return new Response<bool>("default");
            //}
            //catch (MarketException mex)
            //{
            //    logger.Error(mex, $"method: AddDiscountPolicy, parameters: [discountCode = {discountCode}, userId = {userId}, storeId = {storeId}, productId={productId}, discount={discount}]");
            //    return new Response<bool>(mex.Message);
            //}
            //catch (Exception ex)
            //{
            //    logger.Error(ex, $"method: AddDiscountPolicy, parameters: [discountCode = {discountCode}, userId = {userId}, storeId = {storeId}, productId={productId}, discount={discount}]");
            //    return new Response<bool>("Sorry, an unexpected error occured. Please try again");
            //}
            return new Response<bool>();
        }

        // r 4.5
        public Response<bool> RemoveCoOwnerAppointment(int requestingMemberId, int storeId, int coOwnerToRemoveId)
        {
            return new Response<bool>("Not implemented yet"); 
        }

        //TODO
        //public Response<bool> RemoveDiscountPolicy(string discountCode, int userId, int storeId, int productId, double discount)
        //{
        //    try
        //    {
        //        Store s = storeController.getStore(storeId);
        //        if (s == null)
        //            return new Response<bool>($"There isn't a store with an id {storeId}");

        //        logger.Info($"RemoveDiscountPolicy was called with parameters: [discountCode = {discountCode}, userId = {userId}, storeId = {storeId}, productId={productId}, discount={discount}]");
        //        return new Response<bool>("default");
        //    }
        //    catch (MarketException mex)
        //    {
        //        logger.Error(mex, $"method: RemoveDiscountPolicy, parameters: [discountCode = {discountCode}, userId = {userId}, storeId = {storeId}, productId={productId}, discount={discount}]");
        //        return new Response<bool>(mex.Message);
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.Error(ex, $"method: RemoveDiscountPolicy, parameters: [discountCode = {discountCode}, userId = {userId}, storeId = {storeId}, productId={productId}, discount={discount}]");
        //        return new Response<bool>("Sorry, an unexpected error occured. Please try again");
        //    }
        //}

        //done
        public Response<bool> MakeCoOwner(int userId, int targetUserId, int storeId)
        {
            try
            {
                Store s = storeController.GetStore(storeId);
                if (s == null)
                    return new Response<bool>($"There isn't a store with an id {storeId}");
                s.MakeCoOwner(userId, targetUserId);
                logger.Info($"MakeCoOwner was called with parameters: [userId = {userId}, targetUserId = {targetUserId}, storeId = {storeId}]");
                return new Response<bool>(true);
            }
            catch (MarketException mex)
            {
                logger.Error(mex, $"method: MakeCoOwner, parameters: [userId = {userId}, targetUserId = {targetUserId}, storeId = {storeId}]");
                return new Response<bool>(mex.Message);
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"method: MakeCoOwner, parameters: [userId = {userId}, targetUserId = {targetUserId}, storeId = {storeId}]");
                return new Response<bool>("Sorry, an unexpected error occured. Please try again");
            }
        }

        //done
        public Response<bool> MakeCoManager(int userId, int targetUserId, int storeId)
        {
            try
            {
                Store s = storeController.GetStore(storeId);
                if (s == null)
                    return new Response<bool>($"There isn't a store with an id {storeId}");
                s.MakeManager(userId, targetUserId);
                logger.Info($"MakeCoManager was called with parameters: [userId = {userId}, targetUserId = {targetUserId}, storeId = {storeId}]");
                return new Response<bool>(true);
            }
            catch (MarketException mex)
            {
                logger.Error(mex, $"method: MakeCoManager, parameters: [userId = {userId}, targetUserId = {targetUserId}, storeId = {storeId}]");
                return new Response<bool>(mex.Message);
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"method: MakeCoManager, parameters: [userId = {userId}, targetUserId = {targetUserId}, storeId = {storeId}]");
                return new Response<bool>("Sorry, an unexpected error occured. Please try again");
            }
        }

        //done 
        public Response<IList<int>> GetMembersInRole(int storeId, int memberId, Role role)
        {
            try
            {
                Store s = storeController.GetStore(storeId);
                if (s == null)
                    return new Response<IList<int>>($"There isn't a store with an id {storeId}");
                IList<int> l = s.GetMembersInRole(memberId, role);
                logger.Info($"GetMembersInRole was called with parameters: [storeId = {storeId}, memberId = {memberId}, role = {role}]");
                return new Response<IList<int>>(l);
            }
            catch (MarketException mex)
            {
                logger.Error(mex, $"method: GetMembersInRole, parameters: [storeId = {storeId}, memberId = {memberId}, role = {role}]");
                return new Response<IList<int>>(mex.Message);
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"method: GetMembersInRole, parameters: [storeId = {storeId}, memberId = {memberId}, role = {role}]");
                return new Response<IList<int>>("Sorry, an unexpected error occured. Please try again");
            }
        }

        // TODO
        public Response<ServiceMember> GetFounder(int storeId, int memberId)
        {
            try
            {
                Store s = storeController.GetStore(storeId);
                if (s == null)
                    return new Response<ServiceMember>($"There isn't a store with an id {storeId}");
                ServiceMember m = new ServiceMember(s.GetFounder(memberId));
                logger.Info($"GetFounder was called with parameters: [memberId = {memberId}]");
                return new Response<ServiceMember>(m);
            }
            catch (MarketException mex)
            {
                logger.Error(mex, $"method: GetFounder, parameters: [memberId = {memberId}]");
                return new Response<ServiceMember>(mex.Message);
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"method: GetFounder, parameters: [memberId = {memberId}]");
                return new Response<ServiceMember>("Sorry, an unexpected error occured. Please try again");
            }
        }

        //done
        public Response<IList<Permission>> GetManagerPermissions(int storeId, int requestingMemberId, int managerMemberId)
        {
            try
            {
                Store s = storeController.GetStore(storeId);
                if (s == null)
                    return new Response<IList<Permission>>($"There isn't a store with an id {storeId}");
                IList<Permission> l = s.GetManagerPermissions(requestingMemberId, managerMemberId);
                logger.Info($"GetManagerPermissions was called with parameters: [storeId = {storeId}, requestingMemberId = {requestingMemberId}, managerMemberId = {managerMemberId}]");
                return new Response<IList<Permission>>(l);
            }
            catch (MarketException mex)
            {
                logger.Error(mex, $"method: GetManagerPermissions, parameters: [storeId = {storeId}, requestingMemberId = {requestingMemberId}, managerMemberId = {managerMemberId}]");
                return new Response<IList<Permission>>(mex.Message);
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"method: GetManagerPermissions, parameters: [storeId = {storeId}, requestingMemberId = {requestingMemberId}, managerMemberId = {managerMemberId}]");
                return new Response<IList<Permission>>("Sorry, an unexpected error occured. Please try again");
            }
        }

        //done
        public Response<bool> ChangeManagerPermission(int userId, int targetUserId, int storeId, IList<Permission> permissions)
        {
            try
            {
                Store s = storeController.GetStore(storeId);
                if (s == null)
                    return new Response<bool>($"There isn't a store with an id {storeId}");
                s.ChangeManagerPermissions(userId, targetUserId, permissions);
                logger.Info($"ChangeManagerPermission was called with parameters: [userId = {userId}, targetUserId = {targetUserId}, storeId = {storeId}, permissions = {permissions.ToString}]");
                return new Response<bool>(true);
            }
            catch (MarketException mex)
            {
                logger.Error(mex, $"method: ChangeManagerPermission, parameters: [userId = {userId}, targetUserId = {targetUserId}, storeId = {storeId}, permissions = {permissions.ToString}]");
                return new Response<bool>(mex.Message);
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"method: ChangeManagerPermission, parameters: [userId = {userId}, targetUserId = {targetUserId}, storeId = {storeId}, permissions = {permissions.ToString}]");
                return new Response<bool>("Sorry, an unexpected error occured. Please try again");
            }
        }


        //done
        public Response<IList<Purchase>> GetPurchaseHistory(int userId, int storeId)
        {
            try
            {
                Store s = storeController.GetStore(storeId);
                if (s == null)
                    return new Response<IList<Purchase>>($"There isn't a store with an id {storeId}");
                IList<Purchase> l = s.GetPurchaseHistory(userId);
                logger.Info($"GetPurchaseHistory was called with parameters: [userId {userId}, storeId = {storeId}]");
                return new Response<IList<Purchase>>(l);
            }
            catch (MarketException mex)
            {
                logger.Error(mex, $"method: GetPurchaseHistory, parameters: [userId {userId}, storeId = {storeId}]");
                return new Response<IList<Purchase>>(mex.Message);
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"method: GetPurchaseHistory, parameters: [userId {userId}, storeId = {storeId}]");
                return new Response<IList<Purchase>>("Sorry, an unexpected error occured. Please try again");
            }
        }

        //done
        public Response<int> OpenStore(int userId, string storeName)
        {
            try
            {
                int id = storeController.OpenNewStore(userId, storeName);
                logger.Info($"OpenStore was called with parameters: [userId = {userId}, storeName = {storeName}]");
                return new Response<int>(id);
            }
            catch (MarketException mex)
            {
                logger.Error(mex, $"method: OpenStore, parameters: [userId = {userId}, storeName = {storeName}]");
                return new Response<int>(mex.Message);
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"method: OpenStore, parameters: [userId = {userId}, storeName = {storeName}]");
                return new Response<int>("Sorry, an unexpected error occured. Please try again");
            }
        }

    }
}
