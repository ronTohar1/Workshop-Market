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
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.DataLayer.DatabaseObjects
{
    public class Database : DbContext, IDatabase
    {

        private static IDatabase instance = null; 

        public static IDatabase GetInstance()
        {
            if (instance == null)
                instance = new Database();  
            return instance;
        }

        private static IDatabase newDatabaseInstance()
        {
            bool using_database = false;
            if (using_database)
                return new Database(); 
            else
                return new DatabaseMock()
        }

        // needs to be private (or protected for testing), sometimes is public for adding migrations to the database 
        protected Database() : base()
        {
            SimplifiedMembers = DbSetToSimplifiedDbSet<DataMember, int>(this.Members);
            SimplifiedStores = DbSetToSimplifiedDbSet<DataStore, int>(this.Stores);
            SimplifiedProducts = DbSetToSimplifiedDbSet<DataProduct, int>(this.Products);
            SimplifiedBids = DbSetToSimplifiedDbSet<DataBid, int>(this.Bids);
            SimplifiedCarts = DbSetToSimplifiedDbSet<DataCart, int>(this.Carts);
            SimplifiedManagerPermissions = DbSetToSimplifiedDbSet<DataManagerPermission, int>(this.ManagerPermissions);
            SimplifiedPurchaseOptions = DbSetToSimplifiedDbSet<DataPurchaseOption, int>(this.PurchaseOptions);
            SimplifiedStoreMemberRoles = DbSetToSimplifiedDbSet<DataStoreMemberRoles, int>(this.StoreMemberRoles);
            SimplifiedProductReview = DbSetToSimplifiedDbSet<DataProductReview, int>(this.ProductReview);
            SimplifiedAppointmentsNodes = DbSetToSimplifiedDbSet<DataAppointmentsNode, int>(this.AppointmentsNodes);
            SimplifiedShoppingBags = DbSetToSimplifiedDbSet<DataShoppingBag, int>(this.ShoppingBags);
            SimplifiedProductInBags = DbSetToSimplifiedDbSet<DataProductInBag, int>(this.ProductInBags);
            SimplifiedNotifications = DbSetToSimplifiedDbSet<DataNotification, int>(this.Notifications);
            SimplifiedPurchases = DbSetToSimplifiedDbSet<DataPurchase, int>(this.Purchases);
            SimplifiedDateDiscounts = DbSetToSimplifiedDbSet<DataDateDiscount, int>(this.DateDiscounts);
            SimplifiedOneProductDiscounts = DbSetToSimplifiedDbSet<DataOneProductDiscount, int>(this.OneProductDiscounts);
            SimplifiedStoreDiscounts = DbSetToSimplifiedDbSet<DataStoreDiscount, int>(this.StoreDiscounts);
            SimplifiedDiscountAndExpressions = DbSetToSimplifiedDbSet<DataDTOs.Market.StoreManagement.DiscountPolicy.DiscountExpressions.LogicalExpressions.DataAndExpression, int>(this.DiscountAndExpressions);
            SimplifiedDiscountOrExpressions = DbSetToSimplifiedDbSet<DataDTOs.Market.StoreManagement.DiscountPolicy.DiscountExpressions.LogicalExpressions.DataOrExpression, int>(this.DiscountOrExpressions);
            SimplifiedXorExpressions = DbSetToSimplifiedDbSet<DataXorExpression, int>(this.XorExpressions);
            SimplifiedMaxExpressions = DbSetToSimplifiedDbSet<DataMaxExpression, int>(this.MaxExpressions);
            SimplifiedBagValuePredicates = DbSetToSimplifiedDbSet<DataBagValuePredicate, int>(this.BagValuePredicates);
            SimplifiedProductAmountPredicates = DbSetToSimplifiedDbSet<DataProductAmountPredicate, int>(this.ProductAmountPredicates);
            SimplifiedConditionDiscounts = DbSetToSimplifiedDbSet<DataConditionDiscount, int>(this.ConditionDiscounts);
            SimplifiedIfDiscounts = DbSetToSimplifiedDbSet<DataIfDiscount, int>(this.IfDiscounts);
            SimplifiedLogicalExpressions = DbSetToSimplifiedDbSet<DataLogicalExpression, int>(this.LogicalExpressions);
            SimplifiedConditionExpressions = DbSetToSimplifiedDbSet<DataConditionDiscount, int>(this.ConditionExpressions);
            SimplifiedDiscountExpressions = DbSetToSimplifiedDbSet<DataDiscountExpression, int>(this.DiscountExpressions);
            SimplifiedExpressions = DbSetToSimplifiedDbSet<DataExpression, int>(this.Expressions);
            SimplifiedDiscountPredicateExpressions = DbSetToSimplifiedDbSet<DataDTOs.Market.StoreManagement.DiscountPolicy.DiscountInterfaces.DataPredicateExpression, int>(this.DiscountPredicateExpressions);
            SimplifiedDiscounts = DbSetToSimplifiedDbSet<DataDiscount, int>(this.Discounts);
            SimplifiedPurchaseAndExpressions = DbSetToSimplifiedDbSet<DataDTOs.Market.StoreManagement.PurchasesPolicy.LogicalOperators.DataAndExpression, int>(this.PurchaseAndExpressions);
            SimplifiedImpliesExpressions = DbSetToSimplifiedDbSet<DataImpliesExpression, int>(this.ImpliesExpressions);
            SimplifiedPurchaseOrExpressions = DbSetToSimplifiedDbSet<DataDTOs.Market.StoreManagement.PurchasesPolicy.LogicalOperators.DataOrExpression, int>(this.PurchaseOrExpressions);
            SimplifiedCheckProductLessPredicates = DbSetToSimplifiedDbSet<DataCheckProductLessPredicate, int>(this.CheckProductLessPredicates);
            SimplifiedCheckProductMoreEqualsPredicates = DbSetToSimplifiedDbSet<DataCheckProductMoreEqualsPredicate, int>(this.CheckProductMoreEqualsPredicates);
            SimplifiedPruchasePredicateExpressions = DbSetToSimplifiedDbSet<DataDTOs.Market.StoreManagement.PurchasesPolicy.PurchasesInterfaces.DataPredicateExpression, int>(this.PruchasePredicateExpressions);
            SimplifiedInterfacesPurchasePolicies = DbSetToSimplifiedDbSet<DataIPurchasePolicy, int>(this.InterfacesPurchasePolicies);
            SimplifiedRestrictionExpressions = DbSetToSimplifiedDbSet<DataRestrictionExpression, int>(this.RestrictionExpressions);
            SimplifiedDataAfterHourProductRestrictions = DbSetToSimplifiedDbSet<DataAfterHourProductRestriction, int>(this.DataAfterHourProductRestrictions);
            SimplifiedAfterHourRestrictions = DbSetToSimplifiedDbSet<DataAfterHourRestriction, int>(this.AfterHourRestrictions);
            SimplifiedAtLeastAmountRestrictions = DbSetToSimplifiedDbSet<DataAtLeastAmountRestriction, int>(this.AtLeastAmountRestrictions);
            SimplifiedAtMostAmountRestrictions = DbSetToSimplifiedDbSet<DataAtMostAmountRestriction, int>(this.AtMostAmountRestrictions);
            SimplifiedBeforeHourProductRestrictions = DbSetToSimplifiedDbSet<DataBeforeHourProductRestriction, int>(this.BeforeHourProductRestrictions);
            SimplifiedBeforeHourRestrictions = DbSetToSimplifiedDbSet<DataBeforeHourRestriction, int>(this.BeforeHourRestrictions);
            SimplifiedDateRestrictions = DbSetToSimplifiedDbSet<DataDateRestriction, int>(this.DateRestrictions);
            SimplifiedPurchasePolicies = DbSetToSimplifiedDbSet<DataPurchasePolicy, int>(this.PurchasePolicies);
        }

        private static SimplifiedDatabaseDbSet<T, U> DbSetToSimplifiedDbSet<T, U>(DbSet<T> dbSet) where T : class
        {
            return new SimplifiedDatabaseDbSet<T, U>(dbSet);
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

        // Implementing IDatabase

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

        public void Remove(object toRemove)
        {
            base.Remove(toRemove);
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
