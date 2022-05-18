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
using MarketBackend.ServiceLayer.ServiceDTO.DiscountDTO;
using MarketBackend.BusinessLayer.Market.StoreManagment.Discounts;
using MarketBackend.BusinessLayer.Market.StoreManagment.Discounts.DiscountInterfaces;
using MarketBackend.BusinessLayer.Market.StoreManagment.Discounts.DiscountExpressions;
using MarketBackend.ServiceLayer.ServiceDTO.PurchaseDTOs;
using MarketBackend.BusinessLayer.Market.StoreManagment.PurchasesPolicy.PurchaseInterfaces;
using MarketBackend.BusinessLayer.Market.StoreManagment.PurchasesPolicy;
using MarketBackend.BusinessLayer.Market.StoreManagment.PurchasesPolicy.RestrictionPolicies;
using MarketBackend.BusinessLayer.Market.StoreManagment.PurchasesPolicy.PredicatePolicies;

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

        private BusinessLayer.Market.StoreManagment.Discounts.DiscountInterfaces.IPredicateExpression ServicePredicateToPredicate(ServiceDTO.DiscountDTO.IServicePredicate spred, StoreDiscountPolicyManager manager)
        {
            if (spred is ServiceLogical)
            {
                if (spred is ServiceDTO.DiscountDTO.ServiceAnd)
                {
                    ServiceDTO.DiscountDTO.ServiceAnd pred = (ServiceDTO.DiscountDTO.ServiceAnd)spred;
                    return manager.NewAndExpression(ServicePredicateToPredicate(pred.firstExpression, manager), ServicePredicateToPredicate(pred.secondExpression, manager));
                }
                else if (spred is ServiceDTO.DiscountDTO.ServiceOr)
                {
                    ServiceDTO.DiscountDTO.ServiceOr pred = (ServiceDTO.DiscountDTO.ServiceOr)spred;
                    return manager.NewOrExpression(ServicePredicateToPredicate(pred.firstExpression, manager), ServicePredicateToPredicate(pred.secondExpression, manager));
                }
                else // Xor
                {
                    ServiceXor pred = (ServiceXor)spred;
                    return manager.NewXorExpression(ServicePredicateToPredicate(pred.firstExpression, manager), ServicePredicateToPredicate(pred.secondExpression, manager));
                }
            }
            else // Basic predicate
            {
                if (spred is ServiceBagValue)
                {
                    ServiceBagValue pred = (ServiceBagValue)spred;
                    return manager.NewBagValuePredicate(pred.worth);
                }
                else // ServiceProductAmount
                {
                    ServiceProductAmount pred = (ServiceProductAmount)spred;
                    return manager.NewProductAmountPredicate(pred.pid, pred.quantity);
                }

            }
        } 
        private IDiscountExpression ServiceDiscountToDiscount(IServiceDiscount discount, StoreDiscountPolicyManager manager)
        {
            if (discount is ServiceDateDiscount)
            {
                ServiceDateDiscount dis = (ServiceDateDiscount)discount;
                return manager.NewDateDiscount(dis.discount, dis.year, dis.month, dis.day);
            }
            else if (discount is ServiceProductDiscount)
            {
                ServiceProductDiscount dis = (ServiceProductDiscount)discount;
                return manager.NewProductDiscount(dis.productId, dis.discount);
            }
            else if (discount is ServiceMax)
            {
                ServiceMax dis = (ServiceMax)discount;
                IList<IDiscountExpression> disList = new List<IDiscountExpression>();
                foreach (IServiceDiscount d in dis.discounts)
                {
                    disList.Add(ServiceDiscountToDiscount(d, manager));
                }
                return manager.NewMaxExpression(disList);

            }
            else
            {
                ServiceStoreDiscount dis = (ServiceStoreDiscount)discount;
                return manager.NewStoreDiscount(dis.discount);
            }
        }
        private IExpression ServiceExpressionToExpression(IServiceExpression sexp, StoreDiscountPolicyManager manager)
        {
            if (sexp is IServiceDiscount)
            {
                return ServiceDiscountToDiscount((IServiceDiscount)sexp, manager);
            }
            else // IServiceConditional
            {
                if (sexp is ServiceConditionDiscount)
                {
                    ServiceConditionDiscount dis = (ServiceConditionDiscount)sexp;
                    return manager.NewConditionalDiscount(ServicePredicateToPredicate(dis.pred, manager), ServiceDiscountToDiscount(dis.then, manager));
                }
                else // ServiceIf
                {
                    ServiceIf dis = (ServiceIf)sexp;
                    return manager.NewIfDiscount(ServicePredicateToPredicate(dis.test, manager), ServiceDiscountToDiscount(dis.thenDis, manager), ServiceDiscountToDiscount(dis.elseDis, manager));
                }
            }
        }

        public Response<int> AddDiscountPolicy(IServiceExpression expression, string description, int storeId, int memberId)
        {
            try
            {
                Store? s = storeController.GetStore(storeId);
                if (s == null)
                    return new Response<int>($"There isn't a store with an id {storeId}");
                IExpression exp = ServiceExpressionToExpression(expression, s.discountManager);
                int id = s.AddDiscountPolicy(exp, description, memberId); 
                logger.Info($"AddDiscountPolicy was called with parameters: [description {description}, storeId = {storeId}, memberId = {memberId}]");
                return new Response<int>(id);
            }
            catch (MarketException mex)
            {
                logger.Error(mex, $"method: AddDiscountPolicy, parameters: [description {description}, storeId = {storeId}, memberId = {memberId}]");
                return new Response<int>(mex.Message);
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"method: AddDiscountPolicy, parameters: [description {description}, storeId = {storeId}, memberId = {memberId}]");
                return new Response<int>("Sorry, an unexpected error occured. Please try again");
            }
        }

        public Response<bool> RemoveDiscountPolicy(int disId, int storeId, int memberId)
        {
            try
            {
                Store? s = storeController.GetStore(storeId);
                if (s == null)
                    return new Response<bool>($"There isn't a store with an id {storeId}");
                s.RemoveDiscountPolicy(disId, memberId);
                logger.Info($"RemoveDiscountPolicy was called with parameters: [disId {disId}, storeId = {storeId}, memberId = {memberId}]");
                return new Response<bool>(true);
            }
            catch (MarketException mex)
            {
                logger.Error(mex, $"method: RemoveDiscountPolicy, parameters: [disId {disId}, storeId = {storeId}, memberId = {memberId}]");
                return new Response<bool>(mex.Message);
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"method: RemoveDiscountPolicy, parameters: [disId {disId}, storeId = {storeId}, memberId = {memberId}]");
                return new Response<bool>("Sorry, an unexpected error occured. Please try again");
            }
        }


        private BusinessLayer.Market.StoreManagment.PurchasesPolicy.PurchaseInterfaces.IPredicateExpression ServicePurchasePredicateToPurchasePredicate(ServiceDTO.PurchaseDTOs.IServicePredicate pred, StorePurchasePolicyManager manager)
        {
            if (pred is CheckProductMoreEqualsPredicate)
            {
                CheckProductMoreEqualsPredicate exp = (CheckProductMoreEqualsPredicate)pred;
                return manager.NewCheckProductMorePredicate(exp.productId, exp.amount);
            }
            else // product less
            {
                CheckProductLessPredicate exp = (CheckProductLessPredicate)pred;
                return manager.NewCheckProductLessPredicate(exp.productId, exp.amount);
            }
        }
        private IRestrictionExpression ServiceRestrictionToRestriction(IServiceRestriction expression, StorePurchasePolicyManager manager)
        {
            if (expression is ServiceAfterHourProduct)
            {
                ServiceAfterHourProduct exp = (ServiceAfterHourProduct)expression;
                return manager.NewAfterHourProductRestriction(exp.hour, exp.productId, exp.amount);
            }
            else if (expression is ServiceBeforeHourProduct)
            {
                ServiceBeforeHourProduct exp = (ServiceBeforeHourProduct)expression;
                return manager.NewBeforeHourProductRestriction(exp.hour, exp.productId, exp.amount);
            }
            else if (expression is ServiceBeforeHour)
            {
                ServiceBeforeHour exp = (ServiceBeforeHour)expression;
                return manager.NewBeforeHourRestriction(exp.hour);
            }
            else if (expression is ServiceAfterHour)
            {
                ServiceAfterHour exp = (ServiceAfterHour)expression;
                return manager.NewAfterHourRestriction(exp.hour);
            }
            else if (expression is ServiceAtMostAmount)
            {
                ServiceAtMostAmount exp = (ServiceAtMostAmount)expression;
                return manager.NewAtmostAmountRestriction(exp.productId, exp.amount);
            }
            else if (expression is ServiceAtlestAmount)
            {
                ServiceAtlestAmount exp = (ServiceAtlestAmount)expression;
                return manager.NewAtleastAmountRestriction(exp.productId, exp.amount);
            }
            else // date restriction
            {
                ServiceDateRestriction exp = (ServiceDateRestriction)expression;
                return manager.NewDateRestriction(exp.year, exp.month, exp.day);
            }


        }

        private IPurchasePolicy ServicePurchasePolicyToPurcahsePolicy(IServicePurchase expression, StorePurchasePolicyManager manager)
        {
            if (expression is IServiceRestriction)
            {
                return ServiceRestrictionToRestriction((IServiceRestriction)expression, manager);
            }
            else // logical 
            {
                if (expression is ServiceDTO.PurchaseDTOs.ServiceAnd)
                {
                    ServiceDTO.PurchaseDTOs.ServiceAnd exp = (ServiceDTO.PurchaseDTOs.ServiceAnd)expression;
                    return manager.NewAndExpression(ServiceRestrictionToRestriction(exp.firstPred, manager), ServiceRestrictionToRestriction(exp.secondPred, manager));
                }
                else if (expression is ServiceDTO.PurchaseDTOs.ServiceOr)
                {
                    ServiceDTO.PurchaseDTOs.ServiceOr exp = (ServiceDTO.PurchaseDTOs.ServiceOr)expression;
                    return manager.NewOrExpression(ServiceRestrictionToRestriction(exp.firstPred, manager), ServiceRestrictionToRestriction(exp.secondPred, manager));
                }
                else // implies
                {
                    ServiceImplies exp = (ServiceImplies)expression;
                    return manager.NewImpliesExpression(ServicePurchasePredicateToPurchasePredicate(exp.condition, manager), ServicePurchasePredicateToPurchasePredicate(exp.allowing, manager));
                }
            }

        }

        public Response<int> AddPurchasePolicy(IServicePurchase expression, string description, int storeId, int memberId)
        {
            try
            {
                Store? s = storeController.GetStore(storeId);
                if (s == null)
                    return new Response<int>($"There isn't a store with an id {storeId}");
                IPurchasePolicy exp = ServicePurchasePolicyToPurcahsePolicy(expression, s.purchaseManager);
                int id = s.AddPurchasePolicy(exp, description, memberId);
                logger.Info($"AddPurchasePolicy was called with parameters: [description {description}, storeId = {storeId}, memberId = {memberId}]");
                return new Response<int>(id);
            }
            catch (MarketException mex)
            {
                logger.Error(mex, $"method: AddPurchasePolicy, parameters: [description {description}, storeId = {storeId}, memberId = {memberId}]");
                return new Response<int>(mex.Message);
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"method: AddPurchasePolicy, parameters: [description {description}, storeId = {storeId}, memberId = {memberId}]");
                return new Response<int>("Sorry, an unexpected error occured. Please try again");
            }
        }

        public Response<bool> RemovePurchasePolicy(int policyId, int storeId, int memberId)
        {
            try
            {
                Store? s = storeController.GetStore(storeId);
                if (s == null)
                    return new Response<bool>($"There isn't a store with an id {storeId}");
                s.RemovePurchasePolicy(policyId, memberId);
                logger.Info($"RemovePurchasePolicy was called with parameters: [policyId {policyId}, storeId = {storeId}, memberId = {memberId}]");
                return new Response<bool>(true);
            }
            catch (MarketException mex)
            {
                logger.Error(mex, $"method: RemovePurchasePolicy, parameters: [policyId {policyId}, storeId = {storeId}, memberId = {memberId}]");
                return new Response<bool>(mex.Message);
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"method: RemovePurchasePolicy, parameters: [policyId {policyId}, storeId = {storeId}, memberId = {memberId}]");
                return new Response<bool>("Sorry, an unexpected error occured. Please try again");
            }
        }

    }
}
