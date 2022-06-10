using MarketBackend.DataLayer.DataDTOs;
using MarketBackend.DataLayer.DataDTOs.Buyers;
using MarketBackend.DataLayer.DataDTOs.Buyers.Carts;
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
    public class Database : DbContext
    {

        private static Database instance = null; 

        public static Database GetInstance()
        {
            if (instance == null)
                instance = new Database();  
            return instance;
        }

        private Database() : base()
        {

        }

        private const string databaseName = "MarketDatabase";
        private const string instanceName = "SQLEXPRESS";
        private const string ip = "192.168.56.101";
        private const string port = "50488";
        private const string databaseUsername = "amitZivan";
        private const string databasePassword = "passMarket";

        public DbSet<DataMember> Members { get; set; }
        public DbSet<DataStore> Stores { get; set; }
        public DbSet<DataProduct> Products { get; set; }
        public DbSet<DataBid> Bids { get; set; }
        public DbSet<DataCart> Carts { get; set; }
        public DbSet<DataManagerPermission> ManagerPermissions { get; set; }
        public DbSet<DataPurchaseOption> PurchaseOptions { get; set; }
        public DbSet<DataStoreMemberRoles> StoreMemberRoles { get; set; }
        public DbSet<DataProductReview> ProductReview { get; set; }
        
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
        public DbSet<DataConditionExpression> ConditionDiscounts { get; set; }
        public DbSet<DataIfDiscount> IfDiscounts { get; set; }
        public DbSet<DataLogicalExpression> LogicalExpressions { get; set; }
        public DbSet<DataConditionExpression> ConditionExpressions { get; set; }
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
        public DbSet<DataDTOs.Market.StoreManagement.PurchasesPolicy.PurchasesInterfaces.DataPurchasePolicy> InterfacesPurchasePolicies { get; set; }
        public DbSet<DataRestrictionExpression> RestrictionExpressions { get; set; }
        public DbSet<DataAfterHourProductRestriction> DataAfterHourProductRestrictions { get; set; }
        public DbSet<DataAfterHourRestriction> AfterHourRestrictions { get; set; }
        public DbSet<DataAtLeastAmountRestriction> AtLeastAmountRestrictions { get; set; }
        public DbSet<DataAtMostAmountRestriction> AtMostAmountRestrictions { get; set; }
        public DbSet<DataBeforeHourProductRestriction> BeforeHourProductRestrictions { get; set; }
        public DbSet<DataBeforeHourRestriction> BeforeHourRestrictions { get; set; }
        public DbSet<DataDateRestriction> DateRestrictions { get; set; }


        // connection setup functions

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string localVMConnectionString = "Data Source = tcp:" + ip + "\\" + instanceName + "." + databaseName + "," + port + "; " +
                "Database=" + databaseName + "; " +
                "Integrated Security = False; " +
                "User Id = " + databaseUsername + "; " +
                "Password = " + databasePassword + "; " +
                "Encrypt = True; " +
                "TrustServerCertificate = True; " +
                "MultipleActiveResultSets = True";  // todo: check if need more security 

            optionsBuilder.UseSqlServer(localVMConnectionString); 
        }

        // setting (not defualt) primary keys

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DataStoreMemberRoles>()
                .HasKey(storeMemberRoles => new { storeMemberRoles.MemberId, storeMemberRoles.StoreId }); 
        }

        //private void DiscountsWithoutCascades(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<DataDiscount>().OnD
        //}
    }
}
