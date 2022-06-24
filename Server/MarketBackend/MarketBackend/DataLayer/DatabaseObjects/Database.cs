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
using MarketBackend.SystemSettings;
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

        public static Database GetNewInstance()
        {
            return new Database(); 
        }

        private static IDatabase newDatabaseInstance()
        {
            bool using_database = false;
            if (using_database)
                return new Database();
            else
                return new DatabaseMock(); 
        }

        // needs to be private (or protected for testing), sometimes is public for adding migrations to the database 
        public Database() : base()
        {
            SimplifiedMembers = DbSetToSimplifiedDbSet<DataMember, int>(this.Members, dataObject => dataObject.Id);
            SimplifiedStores = DbSetToSimplifiedDbSet<DataStore, int>(this.Stores, dataObject => dataObject.Id);
            SimplifiedProducts = DbSetToSimplifiedDbSet<DataProduct, int>(this.Products, dataObject => dataObject.Id);
            SimplifiedBids = DbSetToSimplifiedDbSet<DataBid, int>(this.Bids, dataObject => dataObject.Id);
            SimplifiedCarts = DbSetToSimplifiedDbSet<DataCart, int>(this.Carts, dataObject => dataObject.Id);
            SimplifiedManagerPermissions = DbSetToSimplifiedDbSet<DataManagerPermission, int>(this.ManagerPermissions, dataObject => dataObject.Id);
            SimplifiedPurchaseOptions = DbSetToSimplifiedDbSet<DataPurchaseOption, int>(this.PurchaseOptions, dataObject => dataObject.Id);
            SimplifiedStoreMemberRoles = DbSetToSimplifiedDbSet<DataStoreMemberRoles, int>(this.StoreMemberRoles, dataObject => dataObject.Id);
            SimplifiedProductReview = DbSetToSimplifiedDbSet<DataProductReview, int>(this.ProductReview, dataObject => dataObject.Id);
            SimplifiedAppointmentsNodes = DbSetToSimplifiedDbSet<DataAppointmentsNode, int>(this.AppointmentsNodes, dataObject => dataObject.Id);
            SimplifiedShoppingBags = DbSetToSimplifiedDbSet<DataShoppingBag, int>(this.ShoppingBags, dataObject => dataObject.Id);
            SimplifiedProductInBags = DbSetToSimplifiedDbSet<DataProductInBag, int>(this.ProductInBags, dataObject => dataObject.Id);
            SimplifiedNotifications = DbSetToSimplifiedDbSet<DataNotification, int>(this.Notifications, dataObject => dataObject.Id);
            SimplifiedPurchases = DbSetToSimplifiedDbSet<DataPurchase, int>(this.Purchases, dataObject => dataObject.Id);
            SimplifiedDateDiscounts = DbSetToSimplifiedDbSet<DataDateDiscount, int>(this.DateDiscounts, dataObject => dataObject.Id);
            SimplifiedOneProductDiscounts = DbSetToSimplifiedDbSet<DataOneProductDiscount, int>(this.OneProductDiscounts, dataObject => dataObject.Id);
            SimplifiedStoreDiscounts = DbSetToSimplifiedDbSet<DataStoreDiscount, int>(this.StoreDiscounts, dataObject => dataObject.Id);
            SimplifiedDiscountAndExpressions = DbSetToSimplifiedDbSet<DataDTOs.Market.StoreManagement.DiscountPolicy.DiscountExpressions.LogicalExpressions.DataAndExpression, int>(this.DiscountAndExpressions, dataObject => dataObject.Id);
            SimplifiedDiscountOrExpressions = DbSetToSimplifiedDbSet<DataDTOs.Market.StoreManagement.DiscountPolicy.DiscountExpressions.LogicalExpressions.DataOrExpression, int>(this.DiscountOrExpressions, dataObject => dataObject.Id);
            SimplifiedXorExpressions = DbSetToSimplifiedDbSet<DataXorExpression, int>(this.XorExpressions, dataObject => dataObject.Id);
            SimplifiedMaxExpressions = DbSetToSimplifiedDbSet<DataMaxExpression, int>(this.MaxExpressions, dataObject => dataObject.Id);
            SimplifiedBagValuePredicates = DbSetToSimplifiedDbSet<DataBagValuePredicate, int>(this.BagValuePredicates, dataObject => dataObject.Id);
            SimplifiedProductAmountPredicates = DbSetToSimplifiedDbSet<DataProductAmountPredicate, int>(this.ProductAmountPredicates, dataObject => dataObject.Id);
            SimplifiedConditionDiscounts = DbSetToSimplifiedDbSet<DataConditionDiscount, int>(this.ConditionDiscounts, dataObject => dataObject.Id);
            SimplifiedIfDiscounts = DbSetToSimplifiedDbSet<DataIfDiscount, int>(this.IfDiscounts, dataObject => dataObject.Id);
            SimplifiedLogicalExpressions = DbSetToSimplifiedDbSet<DataLogicalExpression, int>(this.LogicalExpressions, dataObject => dataObject.Id);
            SimplifiedConditionExpressions = DbSetToSimplifiedDbSet<DataConditionDiscount, int>(this.ConditionExpressions, dataObject => dataObject.Id);
            SimplifiedDiscountExpressions = DbSetToSimplifiedDbSet<DataDiscountExpression, int>(this.DiscountExpressions, dataObject => dataObject.Id);
            SimplifiedExpressions = DbSetToSimplifiedDbSet<DataExpression, int>(this.Expressions, dataObject => dataObject.Id);
            SimplifiedDiscountPredicateExpressions = DbSetToSimplifiedDbSet<DataDTOs.Market.StoreManagement.DiscountPolicy.DiscountInterfaces.DataPredicateExpression, int>(this.DiscountPredicateExpressions, dataObject => dataObject.Id);
            SimplifiedDiscounts = DbSetToSimplifiedDbSet<DataDiscount, int>(this.Discounts, dataObject => dataObject.Id);
            SimplifiedPurchaseAndExpressions = DbSetToSimplifiedDbSet<DataDTOs.Market.StoreManagement.PurchasesPolicy.LogicalOperators.DataAndExpression, int>(this.PurchaseAndExpressions, dataObject => dataObject.Id);
            SimplifiedImpliesExpressions = DbSetToSimplifiedDbSet<DataImpliesExpression, int>(this.ImpliesExpressions, dataObject => dataObject.Id);
            SimplifiedPurchaseOrExpressions = DbSetToSimplifiedDbSet<DataDTOs.Market.StoreManagement.PurchasesPolicy.LogicalOperators.DataOrExpression, int>(this.PurchaseOrExpressions, dataObject => dataObject.Id);
            SimplifiedCheckProductLessPredicates = DbSetToSimplifiedDbSet<DataCheckProductLessPredicate, int>(this.CheckProductLessPredicates, dataObject => dataObject.Id);
            SimplifiedCheckProductMoreEqualsPredicates = DbSetToSimplifiedDbSet<DataCheckProductMoreEqualsPredicate, int>(this.CheckProductMoreEqualsPredicates, dataObject => dataObject.Id);
            SimplifiedPruchasePredicateExpressions = DbSetToSimplifiedDbSet<DataDTOs.Market.StoreManagement.PurchasesPolicy.PurchasesInterfaces.DataPredicateExpression, int>(this.PruchasePredicateExpressions, dataObject => dataObject.Id);
            SimplifiedInterfacesPurchasePolicies = DbSetToSimplifiedDbSet<DataIPurchasePolicy, int>(this.InterfacesPurchasePolicies, dataObject => dataObject.Id);
            SimplifiedRestrictionExpressions = DbSetToSimplifiedDbSet<DataRestrictionExpression, int>(this.RestrictionExpressions, dataObject => dataObject.Id);
            SimplifiedDataAfterHourProductRestrictions = DbSetToSimplifiedDbSet<DataAfterHourProductRestriction, int>(this.DataAfterHourProductRestrictions, dataObject => dataObject.Id);
            SimplifiedAfterHourRestrictions = DbSetToSimplifiedDbSet<DataAfterHourRestriction, int>(this.AfterHourRestrictions, dataObject => dataObject.Id);
            SimplifiedAtLeastAmountRestrictions = DbSetToSimplifiedDbSet<DataAtLeastAmountRestriction, int>(this.AtLeastAmountRestrictions, dataObject => dataObject.Id);
            SimplifiedAtMostAmountRestrictions = DbSetToSimplifiedDbSet<DataAtMostAmountRestriction, int>(this.AtMostAmountRestrictions, dataObject => dataObject.Id);
            SimplifiedBeforeHourProductRestrictions = DbSetToSimplifiedDbSet<DataBeforeHourProductRestriction, int>(this.BeforeHourProductRestrictions, dataObject => dataObject.Id);
            SimplifiedBeforeHourRestrictions = DbSetToSimplifiedDbSet<DataBeforeHourRestriction, int>(this.BeforeHourRestrictions, dataObject => dataObject.Id);
            SimplifiedDateRestrictions = DbSetToSimplifiedDbSet<DataDateRestriction, int>(this.DateRestrictions, dataObject => dataObject.Id);
            SimplifiedPurchasePolicies = DbSetToSimplifiedDbSet<DataPurchasePolicy, int>(this.PurchasePolicies, dataObject => dataObject.Id);
        }

        private SimplifiedDatabaseDbSet<T, U> DbSetToSimplifiedDbSet<T, U>(DbSet<T> dbSet, Func<T, U> getId) where T : class
        {
            return new SimplifiedDatabaseDbSet<T, U>(dbSet, this, getId);
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


        public override int SaveChanges()
        {
            try
            {
                int result = base.SaveChanges();
                return result;
            }
            catch (DbUpdateConcurrencyException exception)
            {
                Random random = new Random();
                Thread.Sleep((int)random.NextInt64(1, 60));
                foreach (EntityEntry entry in exception.Entries)
                {
                    entry.Reload();
                }
                return SaveChanges();
            }
            catch (Exception exception)
            {
                throw exception; 
            }

        }

        // connection setup functions

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var dbConfigs = AppConfigs.GetInstance();

            string localVMConnectionString = "Data Source = tcp:" + dbConfigs.DatabaseIp + "\\" + dbConfigs.DatabaseInstanceName + "." + dbConfigs.DatabaseName + "," + dbConfigs.DatabasePort + "; " +
                "Database=" + dbConfigs.DatabaseName + "; " +
                "Integrated Security = False; " +
                "User Id = " + dbConfigs.DatabaseUsername + "; " +
                "Password = " + dbConfigs.DatabasePassword + "; " +
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
