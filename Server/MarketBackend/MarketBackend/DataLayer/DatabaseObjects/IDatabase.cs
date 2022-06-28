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
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.DataLayer.DatabaseObjects
{
    public interface IDatabase
    {
        private static IDictionary<int, IDatabase> threadsToInstances = new ConcurrentDictionary<int, IDatabase>(); // threads ids 

        public static IDatabase GetInstance()
        {
            int threadId = Thread.CurrentThread.ManagedThreadId; 
            if (!threadsToInstances.ContainsKey(threadId))
            {
                threadsToInstances.Add(threadId, GetNewDatabaseInstance());
                AddActionWhenThisThreadFinishes(() => RemoveIDatabaseInstance(threadId)); 
            }
            return threadsToInstances[threadId];
        }

        private static IDatabase GetNewDatabaseInstance()
        {
            var dbConfigs = MarketBackend.SystemSettings.AppConfigs.GetInstance();
            bool using_database = dbConfigs.ShouldUpdateDatabase;
            if (using_database)
                return Database.GetNewInstance(); 
            else
                return DatabaseMock.GetNewInstance();
        }

        private static void AddActionWhenThisThreadFinishes(Action action)
        {
            Thread currentThread = Thread.CurrentThread;
            new Thread(() =>
            {
                currentThread.Join();
                action();
            }).Start(); 
        }

        private static void RemoveIDatabaseInstance(int threadId)
        {
            if (threadsToInstances.ContainsKey(threadId))
            {
                threadsToInstances[threadId].Dispose();
                threadsToInstances.Remove(threadId);
            }
        }

        public static void RemoveInstances()
        {
            foreach (int threadId in threadsToInstances.Keys)
            {
                RemoveIDatabaseInstance(threadId); 
            }
        }

        protected void Dispose();

        public abstract SimplifiedDbSet<DataMember, int> SimplifiedMembers { get; set; }
        public abstract SimplifiedDbSet<DataStore, int> SimplifiedStores { get; set; }
        public abstract SimplifiedDbSet<DataProduct, int> SimplifiedProducts { get; set; }
        public abstract SimplifiedDbSet<DataBid, int> SimplifiedBids { get; set; }
        public abstract SimplifiedDbSet<DataBidMemberId, int> SimplifiedBidMemberIds { get; set; }
        public abstract SimplifiedDbSet<DataCart, int> SimplifiedCarts { get; set; }
        public abstract SimplifiedDbSet<DataManagerPermission, int> SimplifiedManagerPermissions { get; set; }
        public abstract SimplifiedDbSet<DataPurchaseOption, int> SimplifiedPurchaseOptions { get; set; }
        public abstract SimplifiedDbSet<DataStoreMemberRoles, int> SimplifiedStoreMemberRoles { get; set; }
        public abstract SimplifiedDbSet<DataProductReview, int> SimplifiedProductReview { get; set; }
        public abstract SimplifiedDbSet<DataAppointmentsNode, int> SimplifiedAppointmentsNodes { get; set; }
        public abstract SimplifiedDbSet<DataShoppingBag, int> SimplifiedShoppingBags { get; set; }
        public abstract SimplifiedDbSet<DataProductInBag, int> SimplifiedProductInBags { get; set; }

        public abstract SimplifiedDbSet<DataNotification, int> SimplifiedNotifications { get; set; }
        public abstract SimplifiedDbSet<DataPurchase, int> SimplifiedPurchases { get; set; }
        public abstract SimplifiedDbSet<DataStoreCoOwnerAppointmentApproving, int> SimplifiedStoreCoOwnerAppointmentApproving { get; set; }


        // discounts hierarchies

        public abstract SimplifiedDbSet<DataDateDiscount, int> SimplifiedDateDiscounts { get; set; }
        public abstract SimplifiedDbSet<DataOneProductDiscount, int> SimplifiedOneProductDiscounts { get; set; }
        public abstract SimplifiedDbSet<DataStoreDiscount, int> SimplifiedStoreDiscounts { get; set; }
        public abstract SimplifiedDbSet<DataDTOs.Market.StoreManagement.DiscountPolicy.DiscountExpressions.LogicalExpressions.DataAndExpression, int> SimplifiedDiscountAndExpressions { get; set; }
        public abstract SimplifiedDbSet<DataDTOs.Market.StoreManagement.DiscountPolicy.DiscountExpressions.LogicalExpressions.DataOrExpression, int> SimplifiedDiscountOrExpressions { get; set; }
        public abstract SimplifiedDbSet<DataXorExpression, int> SimplifiedXorExpressions { get; set; }

        public abstract SimplifiedDbSet<DataMaxExpression, int> SimplifiedMaxExpressions { get; set; }
        public abstract SimplifiedDbSet<DataBagValuePredicate, int> SimplifiedBagValuePredicates { get; set; }

        public abstract SimplifiedDbSet<DataProductAmountPredicate, int> SimplifiedProductAmountPredicates { get; set; }
        public abstract SimplifiedDbSet<DataConditionDiscount, int> SimplifiedConditionDiscounts { get; set; }
        public abstract SimplifiedDbSet<DataIfDiscount, int> SimplifiedIfDiscounts { get; set; }
        public abstract SimplifiedDbSet<DataLogicalExpression, int> SimplifiedLogicalExpressions { get; set; }
        public abstract SimplifiedDbSet<DataConditionDiscount, int> SimplifiedConditionExpressions { get; set; }
        public abstract SimplifiedDbSet<DataDiscountExpression, int> SimplifiedDiscountExpressions { get; set; }
        public abstract SimplifiedDbSet<DataExpression, int> SimplifiedExpressions { get; set; }
        public abstract SimplifiedDbSet<DataDTOs.Market.StoreManagement.DiscountPolicy.DiscountInterfaces.DataPredicateExpression, int> SimplifiedDiscountPredicateExpressions { get; set; }
        public abstract SimplifiedDbSet<DataDiscount, int> SimplifiedDiscounts { get; set; }

        // purchase policies hierarchy

        public abstract SimplifiedDbSet<DataDTOs.Market.StoreManagement.PurchasesPolicy.LogicalOperators.DataAndExpression, int> SimplifiedPurchaseAndExpressions { get; set; }
        public abstract SimplifiedDbSet<DataImpliesExpression, int> SimplifiedImpliesExpressions { get; set; }
        public abstract SimplifiedDbSet<DataDTOs.Market.StoreManagement.PurchasesPolicy.LogicalOperators.DataOrExpression, int> SimplifiedPurchaseOrExpressions { get; set; }
        public abstract SimplifiedDbSet<DataCheckProductLessPredicate, int> SimplifiedCheckProductLessPredicates { get; set; }
        public abstract SimplifiedDbSet<DataCheckProductMoreEqualsPredicate, int> SimplifiedCheckProductMoreEqualsPredicates { get; set; }
        public abstract SimplifiedDbSet<DataDTOs.Market.StoreManagement.PurchasesPolicy.PurchasesInterfaces.DataPredicateExpression, int> SimplifiedPruchasePredicateExpressions { get; set; }
        public abstract SimplifiedDbSet<DataIPurchasePolicy, int> SimplifiedInterfacesPurchasePolicies { get; set; }
        public abstract SimplifiedDbSet<DataRestrictionExpression, int> SimplifiedRestrictionExpressions { get; set; }
        public abstract SimplifiedDbSet<DataAfterHourProductRestriction, int> SimplifiedDataAfterHourProductRestrictions { get; set; }
        public abstract SimplifiedDbSet<DataAfterHourRestriction, int> SimplifiedAfterHourRestrictions { get; set; }
        public abstract SimplifiedDbSet<DataAtLeastAmountRestriction, int> SimplifiedAtLeastAmountRestrictions { get; set; }
        public abstract SimplifiedDbSet<DataAtMostAmountRestriction, int> SimplifiedAtMostAmountRestrictions { get; set; }
        public abstract SimplifiedDbSet<DataBeforeHourProductRestriction, int> SimplifiedBeforeHourProductRestrictions { get; set; }
        public abstract SimplifiedDbSet<DataBeforeHourRestriction, int> SimplifiedBeforeHourRestrictions { get; set; }
        public abstract SimplifiedDbSet<DataDateRestriction, int> SimplifiedDateRestrictions { get; set; }
        public abstract SimplifiedDbSet<DataPurchasePolicy, int> SimplifiedPurchasePolicies { get; set; }

        public void Remove(Object toRemove);
        public static void RemoveAllTables(IDatabase db)
        {
            db.SimplifiedMembers.RemoveRange(db.SimplifiedMembers.ToList());
            db.SimplifiedStores.RemoveRange(db.SimplifiedStores.ToList());
            db.SimplifiedProducts.RemoveRange(db.SimplifiedProducts.ToList());
            db.SimplifiedBids.RemoveRange(db.SimplifiedBids.ToList());
            db.SimplifiedBidMemberIds.RemoveRange(db.SimplifiedBidMemberIds.ToList());
            db.SimplifiedCarts.RemoveRange(db.SimplifiedCarts.ToList());
            db.SimplifiedManagerPermissions.RemoveRange(db.SimplifiedManagerPermissions.ToList());
            db.SimplifiedPurchaseOptions.RemoveRange(db.SimplifiedPurchaseOptions.ToList());
            db.SimplifiedStoreMemberRoles.RemoveRange(db.SimplifiedStoreMemberRoles.ToList());
            db.SimplifiedProductReview.RemoveRange(db.SimplifiedProductReview.ToList());
            db.SimplifiedAppointmentsNodes.RemoveRange(db.SimplifiedAppointmentsNodes.ToList());
            db.SimplifiedShoppingBags.RemoveRange(db.SimplifiedShoppingBags.ToList());
            db.SimplifiedProductInBags.RemoveRange(db.SimplifiedProductInBags.ToList());
            db.SimplifiedNotifications.RemoveRange(db.SimplifiedNotifications.ToList());
            db.SimplifiedPurchases.RemoveRange(db.SimplifiedPurchases.ToList());
            db.SimplifiedStoreCoOwnerAppointmentApproving.RemoveRange(db.SimplifiedStoreCoOwnerAppointmentApproving.ToList());
            db.SimplifiedDateDiscounts.RemoveRange(db.SimplifiedDateDiscounts.ToList());
            db.SimplifiedOneProductDiscounts.RemoveRange(db.SimplifiedOneProductDiscounts.ToList());
            db.SimplifiedStoreDiscounts.RemoveRange(db.SimplifiedStoreDiscounts.ToList());
            db.SimplifiedDiscountOrExpressions.RemoveRange(db.SimplifiedDiscountOrExpressions.ToList());
            db.SimplifiedDiscountAndExpressions.RemoveRange(db.SimplifiedDiscountAndExpressions.ToList());
            db.SimplifiedXorExpressions.RemoveRange(db.SimplifiedXorExpressions.ToList());
            db.SimplifiedMaxExpressions.RemoveRange(db.SimplifiedMaxExpressions.ToList());
            db.SimplifiedBagValuePredicates.RemoveRange(db.SimplifiedBagValuePredicates.ToList());
            db.SimplifiedProductAmountPredicates.RemoveRange(db.SimplifiedProductAmountPredicates.ToList());
            db.SimplifiedConditionDiscounts.RemoveRange(db.SimplifiedConditionDiscounts.ToList());
            db.SimplifiedIfDiscounts.RemoveRange(db.SimplifiedIfDiscounts.ToList());
            db.SimplifiedLogicalExpressions.RemoveRange(db.SimplifiedLogicalExpressions.ToList());
            db.SimplifiedConditionExpressions.RemoveRange(db.SimplifiedConditionExpressions.ToList());
            db.SimplifiedDiscountExpressions.RemoveRange(db.SimplifiedDiscountExpressions.ToList());
            db.SimplifiedExpressions.RemoveRange(db.SimplifiedExpressions.ToList());
            db.SimplifiedDiscountPredicateExpressions.RemoveRange(db.SimplifiedDiscountPredicateExpressions.ToList());
            db.SimplifiedDiscounts.RemoveRange(db.SimplifiedDiscounts.ToList());
            db.SimplifiedPurchaseAndExpressions.RemoveRange(db.SimplifiedPurchaseAndExpressions.ToList());
            db.SimplifiedImpliesExpressions.RemoveRange(db.SimplifiedImpliesExpressions.ToList());
            db.SimplifiedPurchaseOrExpressions.RemoveRange(db.SimplifiedPurchaseOrExpressions.ToList());
            db.SimplifiedCheckProductLessPredicates.RemoveRange(db.SimplifiedCheckProductLessPredicates.ToList());
            db.SimplifiedCheckProductMoreEqualsPredicates.RemoveRange(db.SimplifiedCheckProductMoreEqualsPredicates.ToList());
            db.SimplifiedPruchasePredicateExpressions.RemoveRange(db.SimplifiedPruchasePredicateExpressions.ToList());
            db.SimplifiedInterfacesPurchasePolicies.RemoveRange(db.SimplifiedInterfacesPurchasePolicies.ToList());
            db.SimplifiedRestrictionExpressions.RemoveRange(db.SimplifiedRestrictionExpressions.ToList());
            db.SimplifiedDataAfterHourProductRestrictions.RemoveRange(db.SimplifiedDataAfterHourProductRestrictions.ToList());
            db.SimplifiedAfterHourRestrictions.RemoveRange(db.SimplifiedAfterHourRestrictions.ToList());
            db.SimplifiedAtLeastAmountRestrictions.RemoveRange(db.SimplifiedAtLeastAmountRestrictions.ToList());
            db.SimplifiedAtMostAmountRestrictions.RemoveRange(db.SimplifiedAtMostAmountRestrictions.ToList());
            db.SimplifiedBeforeHourProductRestrictions.RemoveRange(db.SimplifiedBeforeHourProductRestrictions.ToList());
            db.SimplifiedBeforeHourRestrictions.RemoveRange(db.SimplifiedBeforeHourRestrictions.ToList());
            db.SimplifiedDateRestrictions.RemoveRange(db.SimplifiedDateRestrictions.ToList());
            db.SimplifiedPurchasePolicies.RemoveRange(db.SimplifiedPurchasePolicies.ToList());

            db.SaveChanges();
        }
        public int SaveChanges();
    }
}
