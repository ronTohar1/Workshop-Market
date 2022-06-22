using MarketBackend.DataLayer.DatabaseObjects.DbSetMocks;
using MarketBackend.DataLayer.DataDTOs;
using MarketBackend.DataLayer.DataDTOs.Buyers;
using MarketBackend.DataLayer.DataDTOs.Buyers.Carts;
using MarketBackend.DataLayer.DataDTOs.Market;
using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement;
using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement.DiscountPolicy;
using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement.DiscountPolicy.DiscountExpressions;
using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement.DiscountPolicy.DiscountExpressions.BasicDiscounts;
using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement.DiscountPolicy.DiscountExpressions.LogicalExpressions;
using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement.DiscountPolicy.DiscountExpressions.NumericExpressions;
using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement.DiscountPolicy.DiscountExpressions.PredicatesExpressions;
using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement.DiscountPolicy.DiscountInterfaces;
using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement.PurchasesPolicy;
using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement.PurchasesPolicy.LogicalOperators;
using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement.PurchasesPolicy.PredicatePolicies;
using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement.PurchasesPolicy.PurchasesInterfaces;
using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement.PurchasesPolicy.RestrictionPolicies;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.DataLayer.DatabaseObjects
{
    public class DatabaseMock : IDatabase
    {
        public SimplifiedDbSet<DataMember, int> SimplifiedMembers { get; set; }
        public SimplifiedDbSet<DataStore, int> SimplifiedStores { get; set; }
        public SimplifiedDbSet<DataProduct, int> SimplifiedProducts { get; set; }
        public SimplifiedDbSet<DataBid, int> SimplifiedBids { get; set; }
        public SimplifiedDbSet<DataCart, int> SimplifiedCarts { get; set; }
        public SimplifiedDbSet<DataManagerPermission, int> SimplifiedManagerPermissions { get; set; }
        public SimplifiedDbSet<DataPurchaseOption, int> SimplifiedPurchaseOptions { get; set; }
        public SimplifiedDbSet<DataStoreMemberRoles, int> SimplifiedStoreMemberRoles { get; set; }
        public SimplifiedDbSet<DataProductReview, int> SimplifiedProductReview { get; set; }
        public SimplifiedDbSet<DataAppointmentsNode, int> SimplifiedAppointmentsNodes { get; set; }
        public SimplifiedDbSet<DataShoppingBag, int> SimplifiedShoppingBags { get; set; }
        public SimplifiedDbSet<DataProductInBag, int> SimplifiedProductInBags { get; set; }

        public SimplifiedDbSet<DataNotification, int> SimplifiedNotifications { get; set; }
        public SimplifiedDbSet<DataPurchase, int> SimplifiedPurchases { get; set; }

        // discounts hierarchies

        public SimplifiedDbSet<DataDateDiscount, int> SimplifiedDateDiscounts { get; set; }
        public SimplifiedDbSet<DataOneProductDiscount, int> SimplifiedOneProductDiscounts { get; set; }
        public SimplifiedDbSet<DataStoreDiscount, int> SimplifiedStoreDiscounts { get; set; }
        public SimplifiedDbSet<DataDTOs.Market.StoreManagement.DiscountPolicy.DiscountExpressions.LogicalExpressions.DataAndExpression, int> SimplifiedDiscountAndExpressions { get; set; }
        public SimplifiedDbSet<DataDTOs.Market.StoreManagement.DiscountPolicy.DiscountExpressions.LogicalExpressions.DataOrExpression, int> SimplifiedDiscountOrExpressions { get; set; }
        public SimplifiedDbSet<DataXorExpression, int> SimplifiedXorExpressions { get; set; }

        public SimplifiedDbSet<DataMaxExpression, int> SimplifiedMaxExpressions { get; set; }
        public SimplifiedDbSet<DataBagValuePredicate, int> SimplifiedBagValuePredicates { get; set; }

        public SimplifiedDbSet<DataProductAmountPredicate, int> SimplifiedProductAmountPredicates { get; set; }
        public SimplifiedDbSet<DataConditionDiscount, int> SimplifiedConditionDiscounts { get; set; }
        public SimplifiedDbSet<DataIfDiscount, int> SimplifiedIfDiscounts { get; set; }
        public SimplifiedDbSet<DataLogicalExpression, int> SimplifiedLogicalExpressions { get; set; }
        public SimplifiedDbSet<DataConditionDiscount, int> SimplifiedConditionExpressions { get; set; }
        public SimplifiedDbSet<DataDiscountExpression, int> SimplifiedDiscountExpressions { get; set; }
        public SimplifiedDbSet<DataExpression, int> SimplifiedExpressions { get; set; }
        public SimplifiedDbSet<DataDTOs.Market.StoreManagement.DiscountPolicy.DiscountInterfaces.DataPredicateExpression, int> SimplifiedDiscountPredicateExpressions { get; set; }
        public SimplifiedDbSet<DataDiscount, int> SimplifiedDiscounts { get; set; }

        // purchase policies hierarchy

        public SimplifiedDbSet<DataDTOs.Market.StoreManagement.PurchasesPolicy.LogicalOperators.DataAndExpression, int> SimplifiedPurchaseAndExpressions { get; set; }
        public SimplifiedDbSet<DataImpliesExpression, int> SimplifiedImpliesExpressions { get; set; }
        public SimplifiedDbSet<DataDTOs.Market.StoreManagement.PurchasesPolicy.LogicalOperators.DataOrExpression, int> SimplifiedPurchaseOrExpressions { get; set; }
        public SimplifiedDbSet<DataCheckProductLessPredicate, int> SimplifiedCheckProductLessPredicates { get; set; }
        public SimplifiedDbSet<DataCheckProductMoreEqualsPredicate, int> SimplifiedCheckProductMoreEqualsPredicates { get; set; }
        public SimplifiedDbSet<DataDTOs.Market.StoreManagement.PurchasesPolicy.PurchasesInterfaces.DataPredicateExpression, int> SimplifiedPruchasePredicateExpressions { get; set; }
        public SimplifiedDbSet<DataIPurchasePolicy, int> SimplifiedInterfacesPurchasePolicies { get; set; }
        public SimplifiedDbSet<DataRestrictionExpression, int> SimplifiedRestrictionExpressions { get; set; }
        public SimplifiedDbSet<DataAfterHourProductRestriction, int> SimplifiedDataAfterHourProductRestrictions { get; set; }
        public SimplifiedDbSet<DataAfterHourRestriction, int> SimplifiedAfterHourRestrictions { get; set; }
        public SimplifiedDbSet<DataAtLeastAmountRestriction, int> SimplifiedAtLeastAmountRestrictions { get; set; }
        public SimplifiedDbSet<DataAtMostAmountRestriction, int> SimplifiedAtMostAmountRestrictions { get; set; }
        public SimplifiedDbSet<DataBeforeHourProductRestriction, int> SimplifiedBeforeHourProductRestrictions { get; set; }
        public SimplifiedDbSet<DataBeforeHourRestriction, int> SimplifiedBeforeHourRestrictions { get; set; }
        public SimplifiedDbSet<DataDateRestriction, int> SimplifiedDateRestrictions { get; set; }
        public SimplifiedDbSet<DataPurchasePolicy, int> SimplifiedPurchasePolicies { get; set; }

        public DatabaseMock()
        {
            SimplifiedMembers = CreateSimplifiedDbSet<DataMember, int>(dataObject => dataObject.Id);
            SimplifiedStores = CreateSimplifiedDbSet<DataStore, int>(dataObject => dataObject.Id);
            SimplifiedProducts = CreateSimplifiedDbSet<DataProduct, int>(dataObject => dataObject.Id);
            SimplifiedBids = CreateSimplifiedDbSet<DataBid, int>(dataObject => dataObject.Id);
            SimplifiedCarts = CreateSimplifiedDbSet<DataCart, int>(dataObject => dataObject.Id); 
            SimplifiedManagerPermissions = CreateSimplifiedDbSet<DataManagerPermission, int>(dataObject => dataObject.Id);
            SimplifiedPurchaseOptions = CreateSimplifiedDbSet<DataPurchaseOption, int>(dataObject => dataObject.Id);
            SimplifiedStoreMemberRoles = CreateSimplifiedDbSet<DataStoreMemberRoles, int>(dataObject => dataObject.Id);
            SimplifiedProductReview = CreateSimplifiedDbSet<DataProductReview, int>(dataObject => dataObject.Id);
            SimplifiedAppointmentsNodes = CreateSimplifiedDbSet<DataAppointmentsNode, int>(dataObject => dataObject.Id);
            SimplifiedShoppingBags = CreateSimplifiedDbSet<DataShoppingBag, int>(dataObject => dataObject.Id);
            SimplifiedProductInBags = CreateSimplifiedDbSet<DataProductInBag, int>(dataObject => dataObject.Id);
            SimplifiedNotifications = CreateSimplifiedDbSet<DataNotification, int>(dataObject => dataObject.Id);
            SimplifiedPurchases = CreateSimplifiedDbSet<DataPurchase, int>(dataObject => dataObject.Id);
            SimplifiedDateDiscounts = CreateSimplifiedDbSet<DataDateDiscount, int>(dataObject => dataObject.Id);
            SimplifiedOneProductDiscounts = CreateSimplifiedDbSet<DataOneProductDiscount, int>(dataObject => dataObject.Id);
            SimplifiedStoreDiscounts = CreateSimplifiedDbSet<DataStoreDiscount, int>(dataObject => dataObject.Id);
            SimplifiedDiscountAndExpressions = CreateSimplifiedDbSet<DataDTOs.Market.StoreManagement.DiscountPolicy.DiscountExpressions.LogicalExpressions.DataAndExpression, int>(dataObject => dataObject.Id);
            SimplifiedDiscountOrExpressions = CreateSimplifiedDbSet<DataDTOs.Market.StoreManagement.DiscountPolicy.DiscountExpressions.LogicalExpressions.DataOrExpression, int>(dataObject => dataObject.Id);
            SimplifiedXorExpressions = CreateSimplifiedDbSet<DataXorExpression, int>(dataObject => dataObject.Id);
            SimplifiedMaxExpressions = CreateSimplifiedDbSet<DataMaxExpression, int>(dataObject => dataObject.Id);
            SimplifiedBagValuePredicates = CreateSimplifiedDbSet<DataBagValuePredicate, int>(dataObject => dataObject.Id);
            SimplifiedProductAmountPredicates = CreateSimplifiedDbSet<DataProductAmountPredicate, int>(dataObject => dataObject.Id);
            SimplifiedConditionDiscounts = CreateSimplifiedDbSet<DataConditionDiscount, int>(dataObject => dataObject.Id);
            SimplifiedIfDiscounts = CreateSimplifiedDbSet<DataIfDiscount, int>(dataObject => dataObject.Id);
            SimplifiedLogicalExpressions = CreateSimplifiedDbSet<DataLogicalExpression, int>(dataObject => dataObject.Id);
            SimplifiedConditionExpressions = CreateSimplifiedDbSet<DataConditionDiscount, int>(dataObject => dataObject.Id);
            SimplifiedDiscountExpressions = CreateSimplifiedDbSet<DataDiscountExpression, int>(dataObject => dataObject.Id);
            SimplifiedExpressions = CreateSimplifiedDbSet<DataExpression, int>(dataObject => dataObject.Id);
            SimplifiedDiscountPredicateExpressions = CreateSimplifiedDbSet<DataDTOs.Market.StoreManagement.DiscountPolicy.DiscountInterfaces.DataPredicateExpression, int>(dataObject => dataObject.Id); 
            SimplifiedDiscounts = CreateSimplifiedDbSet<DataDiscount, int>(dataObject => dataObject.Id); 
            SimplifiedPurchaseAndExpressions = CreateSimplifiedDbSet<DataDTOs.Market.StoreManagement.PurchasesPolicy.LogicalOperators.DataAndExpression, int>(dataObject => dataObject.Id); 
            SimplifiedImpliesExpressions = CreateSimplifiedDbSet<DataImpliesExpression, int>(dataObject => dataObject.Id); 
            SimplifiedPurchaseOrExpressions = CreateSimplifiedDbSet<DataDTOs.Market.StoreManagement.PurchasesPolicy.LogicalOperators.DataOrExpression, int>(dataObject => dataObject.Id);
            SimplifiedCheckProductLessPredicates = CreateSimplifiedDbSet<DataCheckProductLessPredicate, int>(dataObject => dataObject.Id);
            SimplifiedCheckProductMoreEqualsPredicates = CreateSimplifiedDbSet<DataCheckProductMoreEqualsPredicate, int>(dataObject => dataObject.Id);
            SimplifiedPruchasePredicateExpressions = CreateSimplifiedDbSet<DataDTOs.Market.StoreManagement.PurchasesPolicy.PurchasesInterfaces.DataPredicateExpression, int>(dataObject => dataObject.Id);
            SimplifiedInterfacesPurchasePolicies = CreateSimplifiedDbSet<DataIPurchasePolicy, int>(dataObject => dataObject.Id); 
            SimplifiedRestrictionExpressions = CreateSimplifiedDbSet<DataRestrictionExpression, int>(dataObject => dataObject.Id);
            SimplifiedDataAfterHourProductRestrictions = CreateSimplifiedDbSet<DataAfterHourProductRestriction, int>(dataObject => dataObject.Id);
            SimplifiedAfterHourRestrictions = CreateSimplifiedDbSet<DataAfterHourRestriction, int>(dataObject => dataObject.Id);
            SimplifiedAtLeastAmountRestrictions = CreateSimplifiedDbSet<DataAtLeastAmountRestriction, int>(dataObject => dataObject.Id);
            SimplifiedAtMostAmountRestrictions = CreateSimplifiedDbSet<DataAtMostAmountRestriction, int>(dataObject => dataObject.Id);
            SimplifiedBeforeHourProductRestrictions = CreateSimplifiedDbSet<DataBeforeHourProductRestriction, int>(dataObject => dataObject.Id);
            SimplifiedBeforeHourRestrictions = CreateSimplifiedDbSet<DataBeforeHourRestriction, int>(dataObject => dataObject.Id);
            SimplifiedDateRestrictions = CreateSimplifiedDbSet<DataDateRestriction, int>(dataObject => dataObject.Id);
            SimplifiedPurchasePolicies = CreateSimplifiedDbSet<DataPurchasePolicy, int>(dataObject => dataObject.Id);
        }

        private static SimplifiedDbSet<T, U> CreateSimplifiedDbSet<T, U>(Func<T, U> getId) where T : class
        {
            return new SimplifiedMockDbSet<T, U>(getId); 
        }

        public void Remove(Object toRemove)
        {
            if (toRemove.GetType() == typeof(DataMember))
                SimplifiedMembers.Remove((DataMember)toRemove);
            else if (toRemove.GetType() == typeof(DataStore))
                SimplifiedStores.Remove((DataStore)toRemove);
            else if (toRemove.GetType() == typeof(DataProduct))
                SimplifiedProducts.Remove((DataProduct)toRemove);
            else if (toRemove.GetType() == typeof(DataBid))
                SimplifiedBids.Remove((DataBid)toRemove);
            else if (toRemove.GetType() == typeof(DataCart))
                SimplifiedCarts.Remove((DataCart)toRemove);
            else if (toRemove.GetType() == typeof(DataManagerPermission))
                SimplifiedManagerPermissions.Remove((DataManagerPermission)toRemove);
            else if (toRemove.GetType() == typeof(DataPurchaseOption))
                SimplifiedPurchaseOptions.Remove((DataPurchaseOption)toRemove);
            else if (toRemove.GetType() == typeof(DataStoreMemberRoles))
                SimplifiedStoreMemberRoles.Remove((DataStoreMemberRoles)toRemove);
            else if (toRemove.GetType() == typeof(DataProductReview))
                SimplifiedProductReview.Remove((DataProductReview)toRemove);
            else if (toRemove.GetType() == typeof(DataAppointmentsNode))
                SimplifiedAppointmentsNodes.Remove((DataAppointmentsNode)toRemove);
            else if (toRemove.GetType() == typeof(DataShoppingBag))
                SimplifiedShoppingBags.Remove((DataShoppingBag)toRemove);
            else if (toRemove.GetType() == typeof(DataProductInBag))
                SimplifiedProductInBags.Remove((DataProductInBag)toRemove);
            else if (toRemove.GetType() == typeof(DataNotification))
                SimplifiedNotifications.Remove((DataNotification)toRemove);
            else if (toRemove.GetType() == typeof(DataPurchase))
                SimplifiedPurchases.Remove((DataPurchase)toRemove);
            else if (toRemove.GetType() == typeof(DataDateDiscount))
                SimplifiedDateDiscounts.Remove((DataDateDiscount)toRemove);
            else if (toRemove.GetType() == typeof(DataOneProductDiscount))
                SimplifiedOneProductDiscounts.Remove((DataOneProductDiscount)toRemove);
            else if (toRemove.GetType() == typeof(DataStoreDiscount))
                SimplifiedStoreDiscounts.Remove((DataStoreDiscount)toRemove);
            else if (toRemove.GetType() == typeof(DataDTOs.Market.StoreManagement.DiscountPolicy.DiscountExpressions.LogicalExpressions.DataAndExpression))
                SimplifiedDiscountAndExpressions.Remove((DataDTOs.Market.StoreManagement.DiscountPolicy.DiscountExpressions.LogicalExpressions.DataAndExpression)toRemove);
            else if (toRemove.GetType() == typeof(DataDTOs.Market.StoreManagement.DiscountPolicy.DiscountExpressions.LogicalExpressions.DataOrExpression))
                SimplifiedDiscountOrExpressions.Remove((DataDTOs.Market.StoreManagement.DiscountPolicy.DiscountExpressions.LogicalExpressions.DataOrExpression)toRemove);
            else if (toRemove.GetType() == typeof(DataXorExpression))
                SimplifiedXorExpressions.Remove((DataXorExpression)toRemove);
            else if (toRemove.GetType() == typeof(DataMaxExpression))
                SimplifiedMaxExpressions.Remove((DataMaxExpression)toRemove);
            else if (toRemove.GetType() == typeof(DataBagValuePredicate))
                SimplifiedBagValuePredicates.Remove((DataBagValuePredicate)toRemove);
            else if (toRemove.GetType() == typeof(DataProductAmountPredicate))
                SimplifiedProductAmountPredicates.Remove((DataProductAmountPredicate)toRemove);
            else if (toRemove.GetType() == typeof(DataConditionDiscount))
                SimplifiedConditionDiscounts.Remove((DataConditionDiscount)toRemove);
            else if (toRemove.GetType() == typeof(DataIfDiscount))
                SimplifiedIfDiscounts.Remove((DataIfDiscount)toRemove);
            else if (toRemove.GetType() == typeof(DataLogicalExpression))
                SimplifiedLogicalExpressions.Remove((DataLogicalExpression)toRemove);
            else if (toRemove.GetType() == typeof(DataConditionDiscount))
                SimplifiedConditionDiscounts.Remove((DataConditionDiscount)toRemove);
            else if (toRemove.GetType() == typeof(DataDiscountExpression))
                SimplifiedDiscountExpressions.Remove((DataDiscountExpression)toRemove);
            else if (toRemove.GetType() == typeof(DataExpression))
                SimplifiedExpressions.Remove((DataExpression)toRemove);
            else if (toRemove.GetType() == typeof(DataDTOs.Market.StoreManagement.DiscountPolicy.DiscountInterfaces.DataPredicateExpression))
                SimplifiedDiscountPredicateExpressions.Remove((DataDTOs.Market.StoreManagement.DiscountPolicy.DiscountInterfaces.DataPredicateExpression)toRemove);
            else if (toRemove.GetType() == typeof(DataDiscount))
                SimplifiedDiscounts.Remove((DataDiscount)toRemove);
            else if (toRemove.GetType() == typeof(DataDTOs.Market.StoreManagement.PurchasesPolicy.LogicalOperators.DataAndExpression))
                SimplifiedPurchaseAndExpressions.Remove((DataDTOs.Market.StoreManagement.PurchasesPolicy.LogicalOperators.DataAndExpression)toRemove);
            else if (toRemove.GetType() == typeof(DataImpliesExpression))
                SimplifiedImpliesExpressions.Remove((DataImpliesExpression)toRemove);
            else if (toRemove.GetType() == typeof(DataDTOs.Market.StoreManagement.PurchasesPolicy.LogicalOperators.DataOrExpression))
                SimplifiedPurchaseOrExpressions.Remove((DataDTOs.Market.StoreManagement.PurchasesPolicy.LogicalOperators.DataOrExpression)toRemove);
            else if (toRemove.GetType() == typeof(DataCheckProductLessPredicate))
                SimplifiedCheckProductLessPredicates.Remove((DataCheckProductLessPredicate)toRemove);
            else if (toRemove.GetType() == typeof(DataCheckProductMoreEqualsPredicate))
                SimplifiedCheckProductMoreEqualsPredicates.Remove((DataCheckProductMoreEqualsPredicate)toRemove);
            else if (toRemove.GetType() == typeof(DataDTOs.Market.StoreManagement.PurchasesPolicy.PurchasesInterfaces.DataPredicateExpression))
                SimplifiedPruchasePredicateExpressions.Remove((DataDTOs.Market.StoreManagement.PurchasesPolicy.PurchasesInterfaces.DataPredicateExpression)toRemove);
            else if (toRemove.GetType() == typeof(DataIPurchasePolicy))
                SimplifiedInterfacesPurchasePolicies.Remove((DataIPurchasePolicy)toRemove);
            else if (toRemove.GetType() == typeof(DataRestrictionExpression))
                SimplifiedRestrictionExpressions.Remove((DataRestrictionExpression)toRemove);
            else if (toRemove.GetType() == typeof(DataAfterHourProductRestriction))
                SimplifiedDataAfterHourProductRestrictions.Remove((DataAfterHourProductRestriction)toRemove);
            else if (toRemove.GetType() == typeof(DataAfterHourRestriction))
                SimplifiedAfterHourRestrictions.Remove((DataAfterHourRestriction)toRemove);
            else if (toRemove.GetType() == typeof(DataAtLeastAmountRestriction))
                SimplifiedAtLeastAmountRestrictions.Remove((DataAtLeastAmountRestriction)toRemove);
            else if (toRemove.GetType() == typeof(DataAtMostAmountRestriction))
                SimplifiedAtMostAmountRestrictions.Remove((DataAtMostAmountRestriction)toRemove);
            else if (toRemove.GetType() == typeof(DataBeforeHourProductRestriction))
                SimplifiedBeforeHourProductRestrictions.Remove((DataBeforeHourProductRestriction)toRemove);
            else if (toRemove.GetType() == typeof(DataBeforeHourRestriction))
                SimplifiedBeforeHourRestrictions.Remove((DataBeforeHourRestriction)toRemove);
            else if (toRemove.GetType() == typeof(DataDateRestriction))
                SimplifiedDateRestrictions.Remove((DataDateRestriction)toRemove);
            else if (toRemove.GetType() == typeof(DataPurchasePolicy))
                SimplifiedPurchasePolicies.Remove((DataPurchasePolicy)toRemove);
            else
                throw new Exception("The given object's type is not in the database"); 
        }

        public int SaveChanges()
        {
            // not using database 
            return 0; 
        }
    }
}
