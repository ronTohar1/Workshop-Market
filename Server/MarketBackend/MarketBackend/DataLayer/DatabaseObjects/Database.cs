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
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.DataLayer.DatabaseObjects
{
    public class Database : DbContext
    {

        private static Database instance = null; 

        public static Database GetInstance()
        {
            if (instance == null)
                instance = new Database();  
            return instance;
        }

        // needs to be private (or protected for testing), sometimes is public for adding migrations to the database 
        private Database() : base()
        {
            
        }


        public DbSet<DataMember> Members { get; set; }
        public DbSet<DataStore> Stores { get; set; }
        public DbSet<DataProduct> Products { get; set; }
        public DbSet<DataBid> Bids { get; set; }
        public DbSet<DataCart> Carts { get; set; }
        public DbSet<DataManagerPermission> ManagerPermissions { get; set; }
        public DbSet<DataPurchaseOption> PurchaseOptions { get; set; }
        public DbSet<DataStoreMemberRoles> StoreMemberRoles { get; set; }
        public DbSet<DataProductReview> ProductReview { get; set; }
        public DbSet<DataAppointmentsNode> AppointmentsNodes { get; set; }
        public DbSet<DataShoppingBag> ShoppingBags { get; set; }
        public DbSet<DataProductInBag> ProductInBags { get; set; }

        public DbSet<DataNotification> Notifications { get; set; }
        public DbSet<DataPurchase> Purchases { get; set; }

        // discounts hierarchies

        public DbSet<DataDateDiscount> DateDiscounts { get; set; }
        public DbSet<DataOneProductDiscount> OneProductDiscounts { get; set; }
        public DbSet<DataStoreDiscount> StoreDiscounts { get; set; }
        public DbSet<DataDTOs.Market.StoreManagement.DiscountPolicy.DiscountExpressions.LogicalExpressions.DataAndExpression> DiscountAndExpressions { get; set; }
        public DbSet<DataDTOs.Market.StoreManagement.DiscountPolicy.DiscountExpressions.LogicalExpressions.DataOrExpression> DiscountOrExpressions { get; set; }
        public DbSet<DataXorExpression> XorExpressions { get; set; }
        public DbSet<DataMaxExpression> MaxExpressions { get; set; }
        public DbSet<DataBagValuePredicate> BagValuePredicates { get; set; }
        public DbSet<DataProductAmountPredicate> ProductAmountPredicates { get; set; }
        public DbSet<DataConditionDiscount> ConditionDiscounts { get; set; }
        public DbSet<DataIfDiscount> IfDiscounts { get; set; }
        public DbSet<DataLogicalExpression> LogicalExpressions { get; set; }
        public DbSet<DataConditionDiscount> ConditionExpressions { get; set; }
        public DbSet<DataDiscountExpression> DiscountExpressions { get; set; }
        public DbSet<DataExpression> Expressions { get; set; }
        public DbSet<DataDTOs.Market.StoreManagement.DiscountPolicy.DiscountInterfaces.DataPredicateExpression> DiscountPredicateExpressions { get; set; }
        public DbSet<DataDiscount> Discounts { get; set; }

        // purchase policies hierarchy

        public DbSet<DataDTOs.Market.StoreManagement.PurchasesPolicy.LogicalOperators.DataAndExpression> PurchaseAndExpressions { get; set; }
        public DbSet<DataImpliesExpression> ImpliesExpressions { get; set; }
        public DbSet<DataDTOs.Market.StoreManagement.PurchasesPolicy.LogicalOperators.DataOrExpression> PurchaseOrExpressions { get; set; }
        public DbSet<DataCheckProductLessPredicate> CheckProductLessPredicates { get; set; }
        public DbSet<DataCheckProductMoreEqualsPredicate> CheckProductMoreEqualsPredicates { get; set; }
        public DbSet<DataDTOs.Market.StoreManagement.PurchasesPolicy.PurchasesInterfaces.DataPredicateExpression> PruchasePredicateExpressions { get; set; }
        public DbSet<DataIPurchasePolicy> InterfacesPurchasePolicies { get; set; }
        public DbSet<DataRestrictionExpression> RestrictionExpressions { get; set; }
        public DbSet<DataAfterHourProductRestriction> DataAfterHourProductRestrictions { get; set; }
        public DbSet<DataAfterHourRestriction> AfterHourRestrictions { get; set; }
        public DbSet<DataAtLeastAmountRestriction> AtLeastAmountRestrictions { get; set; }
        public DbSet<DataAtMostAmountRestriction> AtMostAmountRestrictions { get; set; }
        public DbSet<DataBeforeHourProductRestriction> BeforeHourProductRestrictions { get; set; }
        public DbSet<DataBeforeHourRestriction> BeforeHourRestrictions { get; set; }
        public DbSet<DataDateRestriction> DateRestrictions { get; set; }
        public DbSet<DataPurchasePolicy> PurchasePolicies { get; set; }

        // connection setup functions

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var dbConfigs = new AppConfig();

            string localVMConnectionString = "Data Source = tcp:" + dbConfigs.ip + "\\" + dbConfigs.instanceName + "." + dbConfigs.databaseName + "," + dbConfigs.port + "; " +
                "Database=" + dbConfigs.databaseName + "; " +
                "Integrated Security = False; " +
                "User Id = " + dbConfigs.databaseUsername + "; " +
                "Password = " + dbConfigs.databasePassword + "; " +
                "Encrypt = True; " +
                "TrustServerCertificate = True; " +
                "MultipleActiveResultSets = True";  // todo: check if need more security 
            
            optionsBuilder.UseSqlServer(localVMConnectionString); 
        }

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
        }

        // setting (not defualt) primary keys

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<DataStoreMemberRoles>()
        //        .HasKey(storeMemberRoles => new { storeMemberRoles.MemberId, storeMemberRoles.StoreId }); 
        //}

        //private void DiscountsWithoutCascades(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<DataDiscount>().OnD
        //}
    }
}
