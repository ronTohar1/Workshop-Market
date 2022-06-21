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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.DataLayer.DatabaseObjects
{
    public interface IDatabase
    {
        public abstract DbSet<DataMember> Members { get; set; }
        public abstract DbSet<DataStore> Stores { get; set; }
        public abstract DbSet<DataProduct> Products { get; set; }
        public abstract DbSet<DataBid> Bids { get; set; }
        public abstract DbSet<DataCart> Carts { get; set; }
        public abstract DbSet<DataManagerPermission> ManagerPermissions { get; set; }
        public abstract DbSet<DataPurchaseOption> PurchaseOptions { get; set; }
        public abstract DbSet<DataStoreMemberRoles> StoreMemberRoles { get; set; }
        public abstract DbSet<DataProductReview> ProductReview { get; set; }
        public abstract DbSet<DataAppointmentsNode> AppointmentsNodes { get; set; }
        public abstract DbSet<DataShoppingBag> ShoppingBags { get; set; }
        public abstract DbSet<DataProductInBag> ProductInBags { get; set; }

        public abstract DbSet<DataNotification> Notifications { get; set; }
        public abstract DbSet<DataPurchase> Purchases { get; set; }

        // discounts hierarchies

        public abstract DbSet<DataDateDiscount> DateDiscounts { get; set; }
        public abstract DbSet<DataOneProductDiscount> OneProductDiscounts { get; set; }
        public abstract DbSet<DataStoreDiscount> StoreDiscounts { get; set; }
        public abstract DbSet<DataDTOs.Market.StoreManagement.DiscountPolicy.DiscountExpressions.LogicalExpressions.DataAndExpression> DiscountAndExpressions { get; set; }
        public abstract DbSet<DataDTOs.Market.StoreManagement.DiscountPolicy.DiscountExpressions.LogicalExpressions.DataOrExpression> DiscountOrExpressions { get; set; }
        public abstract DbSet<DataXorExpression> XorExpressions { get; set; }

        public abstract DbSet<DataMaxExpression> MaxExpressions { get; set; }
        public abstract DbSet<DataBagValuePredicate> BagValuePredicates { get; set; }

        public abstract DbSet<DataProductAmountPredicate> ProductAmountPredicates { get; set; }
        public abstract DbSet<DataConditionDiscount> ConditionDiscounts { get; set; }
        public abstract DbSet<DataIfDiscount> IfDiscounts { get; set; }
        public abstract DbSet<DataLogicalExpression> LogicalExpressions { get; set; }
        public abstract DbSet<DataConditionDiscount> ConditionExpressions { get; set; }
        public abstract DbSet<DataDiscountExpression> DiscountExpressions { get; set; }
        public abstract DbSet<DataExpression> Expressions { get; set; }
        public abstract DbSet<DataDTOs.Market.StoreManagement.DiscountPolicy.DiscountInterfaces.DataPredicateExpression> DiscountPredicateExpressions { get; set; }
        public abstract DbSet<DataDiscount> Discounts { get; set; }

        // purchase policies hierarchy

        public abstract DbSet<DataDTOs.Market.StoreManagement.PurchasesPolicy.LogicalOperators.DataAndExpression> PurchaseAndExpressions { get; set; }
        public abstract DbSet<DataImpliesExpression> ImpliesExpressions { get; set; }
        public abstract DbSet<DataDTOs.Market.StoreManagement.PurchasesPolicy.LogicalOperators.DataOrExpression> PurchaseOrExpressions { get; set; }
        public abstract DbSet<DataCheckProductLessPredicate> CheckProductLessPredicates { get; set; }
        public abstract DbSet<DataCheckProductMoreEqualsPredicate> CheckProductMoreEqualsPredicates { get; set; }
        public abstract DbSet<DataDTOs.Market.StoreManagement.PurchasesPolicy.PurchasesInterfaces.DataPredicateExpression> PruchasePredicateExpressions { get; set; }
        public abstract DbSet<DataIPurchasePolicy> InterfacesPurchasePolicies { get; set; }
        public abstract DbSet<DataRestrictionExpression> RestrictionExpressions { get; set; }
        public abstract DbSet<DataAfterHourProductRestriction> DataAfterHourProductRestrictions { get; set; }
        public abstract DbSet<DataAfterHourRestriction> AfterHourRestrictions { get; set; }
        public abstract DbSet<DataAtLeastAmountRestriction> AtLeastAmountRestrictions { get; set; }
        public abstract DbSet<DataAtMostAmountRestriction> AtMostAmountRestrictions { get; set; }
        public abstract DbSet<DataBeforeHourProductRestriction> BeforeHourProductRestrictions { get; set; }
        public abstract DbSet<DataBeforeHourRestriction> BeforeHourRestrictions { get; set; }
        public abstract DbSet<DataDateRestriction> DateRestrictions { get; set; }
        public abstract DbSet<DataPurchasePolicy> PurchasePolicies { get; set; }

        public void Remove(Object toRemove); 
        public void RemoveAllTables()
        {
            this.Members.RemoveRange(this.Members);
            this.Stores.RemoveRange(this.Stores);
            this.Products.RemoveRange(this.Products);
            this.Bids.RemoveRange(this.Bids);
            this.Carts.RemoveRange(this.Carts);
            this.ManagerPermissions.RemoveRange(this.ManagerPermissions);
            this.PurchaseOptions.RemoveRange(this.PurchaseOptions);
            this.StoreMemberRoles.RemoveRange(this.StoreMemberRoles);
            this.ProductReview.RemoveRange(this.ProductReview);
            this.AppointmentsNodes.RemoveRange(this.AppointmentsNodes);
            this.ShoppingBags.RemoveRange(this.ShoppingBags);
            this.ProductInBags.RemoveRange(this.ProductInBags);
            this.Notifications.RemoveRange(this.Notifications);
            this.Purchases.RemoveRange(this.Purchases);
            this.DateDiscounts.RemoveRange(this.DateDiscounts);
            this.OneProductDiscounts.RemoveRange(this.OneProductDiscounts);
            this.Notifications.RemoveRange(this.Notifications);
            this.StoreDiscounts.RemoveRange(this.StoreDiscounts);
            this.DiscountOrExpressions.RemoveRange(this.DiscountOrExpressions);
            this.DiscountAndExpressions.RemoveRange(this.DiscountAndExpressions);
            this.XorExpressions.RemoveRange(this.XorExpressions);
            this.MaxExpressions.RemoveRange(this.MaxExpressions);
            this.BagValuePredicates.RemoveRange(this.BagValuePredicates);
            this.ProductAmountPredicates.RemoveRange(this.ProductAmountPredicates);
            this.ConditionDiscounts.RemoveRange(this.ConditionDiscounts);
            this.IfDiscounts.RemoveRange(this.IfDiscounts);
            this.LogicalExpressions.RemoveRange(this.LogicalExpressions);
            this.ConditionExpressions.RemoveRange(this.ConditionExpressions);
            this.DiscountExpressions.RemoveRange(this.DiscountExpressions);
            this.Expressions.RemoveRange(this.Expressions);
            this.DiscountPredicateExpressions.RemoveRange(this.DiscountPredicateExpressions);
            this.Discounts.RemoveRange(this.Discounts);
            this.PurchaseAndExpressions.RemoveRange(this.PurchaseAndExpressions);
            this.ImpliesExpressions.RemoveRange(this.ImpliesExpressions);
            this.PurchaseOrExpressions.RemoveRange(this.PurchaseOrExpressions);
            this.CheckProductLessPredicates.RemoveRange(this.CheckProductLessPredicates);
            this.CheckProductMoreEqualsPredicates.RemoveRange(this.CheckProductMoreEqualsPredicates);
            this.PruchasePredicateExpressions.RemoveRange(this.PruchasePredicateExpressions);
            this.InterfacesPurchasePolicies.RemoveRange(this.InterfacesPurchasePolicies);
            this.RestrictionExpressions.RemoveRange(this.RestrictionExpressions);
            this.DataAfterHourProductRestrictions.RemoveRange(this.DataAfterHourProductRestrictions);
            this.AfterHourRestrictions.RemoveRange(this.AfterHourRestrictions);
            this.AtLeastAmountRestrictions.RemoveRange(this.AtLeastAmountRestrictions);
            this.AtMostAmountRestrictions.RemoveRange(this.AtMostAmountRestrictions);
            this.BeforeHourProductRestrictions.RemoveRange(this.BeforeHourProductRestrictions);
            this.BeforeHourRestrictions.RemoveRange(this.BeforeHourRestrictions);
            this.DateRestrictions.RemoveRange(this.DateRestrictions);
            this.PurchasePolicies.RemoveRange(this.PurchasePolicies);

            this.SaveChanges();
        }
        public int SaveChanges();
    }
}
