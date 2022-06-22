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
                instance = new DatabaseMock();  
            return instance;
        }

        // needs to be private (or protected for testing), sometimes is public for adding migrations to the database 
        public Database() : base()
        {
            simplifiedDatabaseMembers = new SimplifiedDatabaseDbSet<DataMember, int>(this.Members);
            simplifiedDatabaseStores = new SimplifiedDatabaseDbSet<DataStore, int>(this.Stores);
            simplifiedDatabaseProducts = new SimplifiedDatabaseDbSet<DataProduct, int>(this.Products);
            simplifiedDatabaseBids = new SimplifiedDatabaseDbSet<DataBid, int>(this.Bids);
            simplifiedDatabaseCarts = new SimplifiedDatabaseDbSet<DataCart, int>(this.Carts);
            simplifiedDatabaseManagerPermissions = new SimplifiedDatabaseDbSet<DataManagerPermission, int>(this.ManagerPermissions);
            simplifiedDatabasePurchaseOptions = new SimplifiedDatabaseDbSet<DataPurchaseOption, int>(this.PurchaseOptions);
            simplifiedDatabaseStoreMemberRoles = new SimplifiedDatabaseDbSet<DataStoreMemberRoles, int>(this.StoreMemberRoles);
            simplifiedDatabaseProductReview = new SimplifiedDatabaseDbSet<DataProductReview, int>(this.ProductReview);
            simplifiedDatabaseAppointmentsNodes = new SimplifiedDatabaseDbSet<DataAppointmentsNode, int>(this.AppointmentsNodes);
            simplifiedDatabaseShoppingBags = new SimplifiedDatabaseDbSet<DataShoppingBag, int>(this.ShoppingBags);
            simplifiedDatabaseProductInBags = new SimplifiedDatabaseDbSet<DataProductInBag, int>(this.ProductInBags);
            simplifiedDatabaseNotifications = new SimplifiedDatabaseDbSet<DataNotification, int>(this.Notifications);
            simplifiedDatabasePurchases = new SimplifiedDatabaseDbSet<DataPurchase, int>(this.Purchases);
            simplifiedDatabaseDateDiscounts = new SimplifiedDatabaseDbSet<DataDateDiscount, int>(this.DateDiscounts);
            simplifiedDatabaseOneProductDiscounts = new SimplifiedDatabaseDbSet<DataOneProductDiscount, int>(this.OneProductDiscounts);
            simplifiedDatabaseStoreDiscounts = new SimplifiedDatabaseDbSet<DataStoreDiscount, int>(this.StoreDiscounts);
            simplifiedDatabaseDiscountAndExpressions = new SimplifiedDatabaseDbSet<DataDTOs.Market.StoreManagement.DiscountPolicy.DiscountExpressions.LogicalExpressions.DataAndExpression, int>(this.DiscountAndExpressions);
            simplifiedDatabaseDiscountOrExpressions = new SimplifiedDatabaseDbSet<DataDTOs.Market.StoreManagement.DiscountPolicy.DiscountExpressions.LogicalExpressions.DataOrExpression, int>(this.DiscountOrExpressions);
            simplifiedDatabaseXorExpressions = new SimplifiedDatabaseDbSet<DataXorExpression, int>(this.XorExpressions);
            simplifiedDatabaseMaxExpressions = new SimplifiedDatabaseDbSet<DataMaxExpression, int>(this.MaxExpressions);
            simplifiedDatabaseBagValuePredicates = new SimplifiedDatabaseDbSet<DataBagValuePredicate, int>(this.BagValuePredicates);
            simplifiedDatabaseProductAmountPredicates = new SimplifiedDatabaseDbSet<DataProductAmountPredicate, int>(this.ProductAmountPredicates);
            simplifiedDatabaseConditionDiscounts = new SimplifiedDatabaseDbSet<DataConditionDiscount, int>(this.ConditionDiscounts);
            simplifiedDatabaseIfDiscounts = new SimplifiedDatabaseDbSet<DataIfDiscount, int>(this.IfDiscounts);
            simplifiedDatabaseLogicalExpressions = new SimplifiedDatabaseDbSet<DataLogicalExpression, int>(this.LogicalExpressions);
            simplifiedDatabaseConditionExpressions = new SimplifiedDatabaseDbSet<DataConditionDiscount, int>(this.ConditionExpressions);
            simplifiedDatabaseDiscountExpressions = new SimplifiedDatabaseDbSet<DataDiscountExpression, int>(this.DiscountExpressions);
            simplifiedDatabaseExpressions = new SimplifiedDatabaseDbSet<DataExpression, int>(this.Expressions);
            simplifiedDatabaseDiscountPredicateExpressions = new SimplifiedDatabaseDbSet<DataDTOs.Market.StoreManagement.DiscountPolicy.DiscountInterfaces.DataPredicateExpression, int>(this.DiscountPredicateExpressions);
            simplifiedDatabaseDiscounts = new SimplifiedDatabaseDbSet<DataDiscount, int>(this.Discounts);
            simplifiedDatabasePurchaseAndExpressions = new SimplifiedDatabaseDbSet<DataDTOs.Market.StoreManagement.PurchasesPolicy.LogicalOperators.DataAndExpression, int>(this.PurchaseAndExpressions);
            simplifiedDatabaseImpliesExpressions = new SimplifiedDatabaseDbSet<DataImpliesExpression, int>(this.ImpliesExpressions);
            simplifiedDatabasePurchaseOrExpressions = new SimplifiedDatabaseDbSet<DataDTOs.Market.StoreManagement.PurchasesPolicy.LogicalOperators.DataOrExpression, int>(this.PurchaseOrExpressions);
            simplifiedDatabaseCheckProductLessPredicates = new SimplifiedDatabaseDbSet<DataCheckProductLessPredicate, int>(this.CheckProductLessPredicates);
            simplifiedDatabaseCheckProductMoreEqualsPredicates = new SimplifiedDatabaseDbSet<DataCheckProductMoreEqualsPredicate, int>(this.CheckProductMoreEqualsPredicates);
            simplifiedDatabasePruchasePredicateExpressions = new SimplifiedDatabaseDbSet<DataDTOs.Market.StoreManagement.PurchasesPolicy.PurchasesInterfaces.DataPredicateExpression, int>(this.PruchasePredicateExpressions);
            simplifiedDatabaseInterfacesPurchasePolicies = new SimplifiedDatabaseDbSet<DataIPurchasePolicy, int>(this.InterfacesPurchasePolicies);
            simplifiedDatabaseRestrictionExpressions = new SimplifiedDatabaseDbSet<DataRestrictionExpression, int>(this.RestrictionExpressions);
            simplifiedDatabaseDataAfterHourProductRestrictions = new SimplifiedDatabaseDbSet<DataAfterHourProductRestriction, int>(this.DataAfterHourProductRestrictions);
            simplifiedDatabaseAfterHourRestrictions = new SimplifiedDatabaseDbSet<DataAfterHourRestriction, int>(this.AfterHourRestrictions);
            simplifiedDatabaseAtLeastAmountRestrictions = new SimplifiedDatabaseDbSet<DataAtLeastAmountRestriction, int>(this.AtLeastAmountRestrictions);
            simplifiedDatabaseAtMostAmountRestrictions = new SimplifiedDatabaseDbSet<DataAtMostAmountRestriction, int>(this.AtMostAmountRestrictions);
            simplifiedDatabaseBeforeHourProductRestrictions = new SimplifiedDatabaseDbSet<DataBeforeHourProductRestriction, int>(this.BeforeHourProductRestrictions);
            simplifiedDatabaseBeforeHourRestrictions = new SimplifiedDatabaseDbSet<DataBeforeHourRestriction, int>(this.BeforeHourRestrictions);
            simplifiedDatabaseDateRestrictions = new SimplifiedDatabaseDbSet<DataDateRestriction, int>(this.DateRestrictions);
            simplifiedDatabasePurchasePolicies = new SimplifiedDatabaseDbSet<DataPurchasePolicy, int>(this.PurchasePolicies);
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


        // implementing IDatabase

        private SimplifiedDatabaseDbSet<DataMember, int> simplifiedDatabaseMembers;
        public SimplifiedDbSet<DataMember, int> SimplifiedMembers { get => simplifiedDatabaseMembers; set => simplifiedDatabaseMembers = GetIfSimplifiedDatabaseDbSet(value); }
        private SimplifiedDatabaseDbSet<DataStore, int> simplifiedDatabaseStores;
        public SimplifiedDbSet<DataStore, int> SimplifiedStores { get => simplifiedDatabaseStores; set => simplifiedDatabaseStores = GetIfSimplifiedDatabaseDbSet(value); }
        private SimplifiedDatabaseDbSet<DataProduct, int> simplifiedDatabaseProducts;
        public SimplifiedDbSet<DataProduct, int> SimplifiedProducts { get => simplifiedDatabaseProducts; set => simplifiedDatabaseProducts = GetIfSimplifiedDatabaseDbSet(value); }
        private SimplifiedDatabaseDbSet<DataBid, int> simplifiedDatabaseBids;
        public SimplifiedDbSet<DataBid, int> SimplifiedBids { get => simplifiedDatabaseBids; set => simplifiedDatabaseBids = GetIfSimplifiedDatabaseDbSet(value); }
        private SimplifiedDatabaseDbSet<DataCart, int> simplifiedDatabaseCarts;
        public SimplifiedDbSet<DataCart, int> SimplifiedCarts { get => simplifiedDatabaseCarts; set => simplifiedDatabaseCarts = GetIfSimplifiedDatabaseDbSet(value); }
        private SimplifiedDatabaseDbSet<DataManagerPermission, int> simplifiedDatabaseManagerPermissions;
        public SimplifiedDbSet<DataManagerPermission, int> SimplifiedManagerPermissions { get => simplifiedDatabaseManagerPermissions; set => simplifiedDatabaseManagerPermissions = GetIfSimplifiedDatabaseDbSet(value); }
        private SimplifiedDatabaseDbSet<DataPurchaseOption, int> simplifiedDatabasePurchaseOptions;
        public SimplifiedDbSet<DataPurchaseOption, int> SimplifiedPurchaseOptions { get => simplifiedDatabasePurchaseOptions; set => simplifiedDatabasePurchaseOptions = GetIfSimplifiedDatabaseDbSet(value); }
        private SimplifiedDatabaseDbSet<DataStoreMemberRoles, int> simplifiedDatabaseStoreMemberRoles;
        public SimplifiedDbSet<DataStoreMemberRoles, int> SimplifiedStoreMemberRoles { get => simplifiedDatabaseStoreMemberRoles; set => simplifiedDatabaseStoreMemberRoles = GetIfSimplifiedDatabaseDbSet(value); }
        private SimplifiedDatabaseDbSet<DataProductReview, int> simplifiedDatabaseProductReview;
        public SimplifiedDbSet<DataProductReview, int> SimplifiedProductReview { get => simplifiedDatabaseProductReview; set => simplifiedDatabaseProductReview = GetIfSimplifiedDatabaseDbSet(value); }
        private SimplifiedDatabaseDbSet<DataAppointmentsNode, int> simplifiedDatabaseAppointmentsNodes;
        public SimplifiedDbSet<DataAppointmentsNode, int> SimplifiedAppointmentsNodes { get => simplifiedDatabaseAppointmentsNodes; set => simplifiedDatabaseAppointmentsNodes = GetIfSimplifiedDatabaseDbSet(value); }
        private SimplifiedDatabaseDbSet<DataShoppingBag, int> simplifiedDatabaseShoppingBags;
        public SimplifiedDbSet<DataShoppingBag, int> SimplifiedShoppingBags { get => simplifiedDatabaseShoppingBags; set => simplifiedDatabaseShoppingBags = GetIfSimplifiedDatabaseDbSet(value); }
        private SimplifiedDatabaseDbSet<DataProductInBag, int> simplifiedDatabaseProductInBags;
        public SimplifiedDbSet<DataProductInBag, int> SimplifiedProductInBags { get => simplifiedDatabaseProductInBags; set => simplifiedDatabaseProductInBags = GetIfSimplifiedDatabaseDbSet(value); }
        private SimplifiedDatabaseDbSet<DataNotification, int> simplifiedDatabaseNotifications;
        public SimplifiedDbSet<DataNotification, int> SimplifiedNotifications { get => simplifiedDatabaseNotifications; set => simplifiedDatabaseNotifications = GetIfSimplifiedDatabaseDbSet(value); }
        private SimplifiedDatabaseDbSet<DataPurchase, int> simplifiedDatabasePurchases;
        public SimplifiedDbSet<DataPurchase, int> SimplifiedPurchases { get => simplifiedDatabasePurchases; set => simplifiedDatabasePurchases = GetIfSimplifiedDatabaseDbSet(value); }
        private SimplifiedDatabaseDbSet<DataDateDiscount, int> simplifiedDatabaseDateDiscounts;
        public SimplifiedDbSet<DataDateDiscount, int> SimplifiedDateDiscounts { get => simplifiedDatabaseDateDiscounts; set => simplifiedDatabaseDateDiscounts = GetIfSimplifiedDatabaseDbSet(value); }
        private SimplifiedDatabaseDbSet<DataOneProductDiscount, int> simplifiedDatabaseOneProductDiscounts;
        public SimplifiedDbSet<DataOneProductDiscount, int> SimplifiedOneProductDiscounts { get => simplifiedDatabaseOneProductDiscounts; set => simplifiedDatabaseOneProductDiscounts = GetIfSimplifiedDatabaseDbSet(value); }
        private SimplifiedDatabaseDbSet<DataStoreDiscount, int> simplifiedDatabaseStoreDiscounts;
        public SimplifiedDbSet<DataStoreDiscount, int> SimplifiedStoreDiscounts { get => simplifiedDatabaseStoreDiscounts; set => simplifiedDatabaseStoreDiscounts = GetIfSimplifiedDatabaseDbSet(value); }
        private SimplifiedDatabaseDbSet<DataDTOs.Market.StoreManagement.DiscountPolicy.DiscountExpressions.LogicalExpressions.DataAndExpression, int> simplifiedDatabaseDiscountAndExpressions;
        public SimplifiedDbSet<DataDTOs.Market.StoreManagement.DiscountPolicy.DiscountExpressions.LogicalExpressions.DataAndExpression, int> SimplifiedDiscountAndExpressions { get => simplifiedDatabaseDiscountAndExpressions; set => simplifiedDatabaseDiscountAndExpressions = GetIfSimplifiedDatabaseDbSet(value); }
        private SimplifiedDatabaseDbSet<DataDTOs.Market.StoreManagement.DiscountPolicy.DiscountExpressions.LogicalExpressions.DataOrExpression, int> simplifiedDatabaseDiscountOrExpressions;
        public SimplifiedDbSet<DataDTOs.Market.StoreManagement.DiscountPolicy.DiscountExpressions.LogicalExpressions.DataOrExpression, int> SimplifiedDiscountOrExpressions { get => simplifiedDatabaseDiscountOrExpressions; set => simplifiedDatabaseDiscountOrExpressions = GetIfSimplifiedDatabaseDbSet(value); }
        private SimplifiedDatabaseDbSet<DataXorExpression, int> simplifiedDatabaseXorExpressions;
        public SimplifiedDbSet<DataXorExpression, int> SimplifiedXorExpressions { get => simplifiedDatabaseXorExpressions; set => simplifiedDatabaseXorExpressions = GetIfSimplifiedDatabaseDbSet(value); }
        private SimplifiedDatabaseDbSet<DataMaxExpression, int> simplifiedDatabaseMaxExpressions;
        public SimplifiedDbSet<DataMaxExpression, int> SimplifiedMaxExpressions { get => simplifiedDatabaseMaxExpressions; set => simplifiedDatabaseMaxExpressions = GetIfSimplifiedDatabaseDbSet(value); }
        private SimplifiedDatabaseDbSet<DataBagValuePredicate, int> simplifiedDatabaseBagValuePredicates;
        public SimplifiedDbSet<DataBagValuePredicate, int> SimplifiedBagValuePredicates { get => simplifiedDatabaseBagValuePredicates; set => simplifiedDatabaseBagValuePredicates = GetIfSimplifiedDatabaseDbSet(value); }
        private SimplifiedDatabaseDbSet<DataProductAmountPredicate, int> simplifiedDatabaseProductAmountPredicates;
        public SimplifiedDbSet<DataProductAmountPredicate, int> SimplifiedProductAmountPredicates { get => simplifiedDatabaseProductAmountPredicates; set => simplifiedDatabaseProductAmountPredicates = GetIfSimplifiedDatabaseDbSet(value); }
        private SimplifiedDatabaseDbSet<DataConditionDiscount, int> simplifiedDatabaseConditionDiscounts;
        public SimplifiedDbSet<DataConditionDiscount, int> SimplifiedConditionDiscounts { get => simplifiedDatabaseConditionDiscounts; set => simplifiedDatabaseConditionDiscounts = GetIfSimplifiedDatabaseDbSet(value); }
        private SimplifiedDatabaseDbSet<DataIfDiscount, int> simplifiedDatabaseIfDiscounts;
        public SimplifiedDbSet<DataIfDiscount, int> SimplifiedIfDiscounts { get => simplifiedDatabaseIfDiscounts; set => simplifiedDatabaseIfDiscounts = GetIfSimplifiedDatabaseDbSet(value); }
        private SimplifiedDatabaseDbSet<DataLogicalExpression, int> simplifiedDatabaseLogicalExpressions;
        public SimplifiedDbSet<DataLogicalExpression, int> SimplifiedLogicalExpressions { get => simplifiedDatabaseLogicalExpressions; set => simplifiedDatabaseLogicalExpressions = GetIfSimplifiedDatabaseDbSet(value); }
        private SimplifiedDatabaseDbSet<DataConditionDiscount, int> simplifiedDatabaseConditionExpressions;
        public SimplifiedDbSet<DataConditionDiscount, int> SimplifiedConditionExpressions { get => simplifiedDatabaseConditionExpressions; set => simplifiedDatabaseConditionExpressions = GetIfSimplifiedDatabaseDbSet(value); }
        private SimplifiedDatabaseDbSet<DataDiscountExpression, int> simplifiedDatabaseDiscountExpressions;
        public SimplifiedDbSet<DataDiscountExpression, int> SimplifiedDiscountExpressions { get => simplifiedDatabaseDiscountExpressions; set => simplifiedDatabaseDiscountExpressions = GetIfSimplifiedDatabaseDbSet(value); }
        private SimplifiedDatabaseDbSet<DataExpression, int> simplifiedDatabaseExpressions;
        public SimplifiedDbSet<DataExpression, int> SimplifiedExpressions { get => simplifiedDatabaseExpressions; set => simplifiedDatabaseExpressions = GetIfSimplifiedDatabaseDbSet(value); }
        private SimplifiedDatabaseDbSet<DataDTOs.Market.StoreManagement.DiscountPolicy.DiscountInterfaces.DataPredicateExpression, int> simplifiedDatabaseDiscountPredicateExpressions;
        public SimplifiedDbSet<DataDTOs.Market.StoreManagement.DiscountPolicy.DiscountInterfaces.DataPredicateExpression, int> SimplifiedDiscountPredicateExpressions { get => simplifiedDatabaseDiscountPredicateExpressions; set => simplifiedDatabaseDiscountPredicateExpressions = GetIfSimplifiedDatabaseDbSet(value); }
        private SimplifiedDatabaseDbSet<DataDiscount, int> simplifiedDatabaseDiscounts;
        public SimplifiedDbSet<DataDiscount, int> SimplifiedDiscounts { get => simplifiedDatabaseDiscounts; set => simplifiedDatabaseDiscounts = GetIfSimplifiedDatabaseDbSet(value); }
        private SimplifiedDatabaseDbSet<DataDTOs.Market.StoreManagement.PurchasesPolicy.LogicalOperators.DataAndExpression, int> simplifiedDatabasePurchaseAndExpressions;
        public SimplifiedDbSet<DataDTOs.Market.StoreManagement.PurchasesPolicy.LogicalOperators.DataAndExpression, int> SimplifiedPurchaseAndExpressions { get => simplifiedDatabasePurchaseAndExpressions; set => simplifiedDatabasePurchaseAndExpressions = GetIfSimplifiedDatabaseDbSet(value); }
        private SimplifiedDatabaseDbSet<DataImpliesExpression, int> simplifiedDatabaseImpliesExpressions;
        public SimplifiedDbSet<DataImpliesExpression, int> SimplifiedImpliesExpressions { get => simplifiedDatabaseImpliesExpressions; set => simplifiedDatabaseImpliesExpressions = GetIfSimplifiedDatabaseDbSet(value); }
        private SimplifiedDatabaseDbSet<DataDTOs.Market.StoreManagement.PurchasesPolicy.LogicalOperators.DataOrExpression, int> simplifiedDatabasePurchaseOrExpressions;
        public SimplifiedDbSet<DataDTOs.Market.StoreManagement.PurchasesPolicy.LogicalOperators.DataOrExpression, int> SimplifiedPurchaseOrExpressions { get => simplifiedDatabasePurchaseOrExpressions; set => simplifiedDatabasePurchaseOrExpressions = GetIfSimplifiedDatabaseDbSet(value); }
        private SimplifiedDatabaseDbSet<DataCheckProductLessPredicate, int> simplifiedDatabaseCheckProductLessPredicates;
        public SimplifiedDbSet<DataCheckProductLessPredicate, int> SimplifiedCheckProductLessPredicates { get => simplifiedDatabaseCheckProductLessPredicates; set => simplifiedDatabaseCheckProductLessPredicates = GetIfSimplifiedDatabaseDbSet(value); }
        private SimplifiedDatabaseDbSet<DataCheckProductMoreEqualsPredicate, int> simplifiedDatabaseCheckProductMoreEqualsPredicates;
        public SimplifiedDbSet<DataCheckProductMoreEqualsPredicate, int> SimplifiedCheckProductMoreEqualsPredicates { get => simplifiedDatabaseCheckProductMoreEqualsPredicates; set => simplifiedDatabaseCheckProductMoreEqualsPredicates = GetIfSimplifiedDatabaseDbSet(value); }
        private SimplifiedDatabaseDbSet<DataDTOs.Market.StoreManagement.PurchasesPolicy.PurchasesInterfaces.DataPredicateExpression, int> simplifiedDatabasePruchasePredicateExpressions;
        public SimplifiedDbSet<DataDTOs.Market.StoreManagement.PurchasesPolicy.PurchasesInterfaces.DataPredicateExpression, int> SimplifiedPruchasePredicateExpressions { get => simplifiedDatabasePruchasePredicateExpressions; set => simplifiedDatabasePruchasePredicateExpressions = GetIfSimplifiedDatabaseDbSet(value); }
        private SimplifiedDatabaseDbSet<DataIPurchasePolicy, int> simplifiedDatabaseInterfacesPurchasePolicies;
        public SimplifiedDbSet<DataIPurchasePolicy, int> SimplifiedInterfacesPurchasePolicies { get => simplifiedDatabaseInterfacesPurchasePolicies; set => simplifiedDatabaseInterfacesPurchasePolicies = GetIfSimplifiedDatabaseDbSet(value); }
        private SimplifiedDatabaseDbSet<DataRestrictionExpression, int> simplifiedDatabaseRestrictionExpressions;
        public SimplifiedDbSet<DataRestrictionExpression, int> SimplifiedRestrictionExpressions { get => simplifiedDatabaseRestrictionExpressions; set => simplifiedDatabaseRestrictionExpressions = GetIfSimplifiedDatabaseDbSet(value); }
        private SimplifiedDatabaseDbSet<DataAfterHourProductRestriction, int> simplifiedDatabaseDataAfterHourProductRestrictions;
        public SimplifiedDbSet<DataAfterHourProductRestriction, int> SimplifiedDataAfterHourProductRestrictions { get => simplifiedDatabaseDataAfterHourProductRestrictions; set => simplifiedDatabaseDataAfterHourProductRestrictions = GetIfSimplifiedDatabaseDbSet(value); }
        private SimplifiedDatabaseDbSet<DataAfterHourRestriction, int> simplifiedDatabaseAfterHourRestrictions;
        public SimplifiedDbSet<DataAfterHourRestriction, int> SimplifiedAfterHourRestrictions { get => simplifiedDatabaseAfterHourRestrictions; set => simplifiedDatabaseAfterHourRestrictions = GetIfSimplifiedDatabaseDbSet(value); }
        private SimplifiedDatabaseDbSet<DataAtLeastAmountRestriction, int> simplifiedDatabaseAtLeastAmountRestrictions;
        public SimplifiedDbSet<DataAtLeastAmountRestriction, int> SimplifiedAtLeastAmountRestrictions { get => simplifiedDatabaseAtLeastAmountRestrictions; set => simplifiedDatabaseAtLeastAmountRestrictions = GetIfSimplifiedDatabaseDbSet(value); }
        private SimplifiedDatabaseDbSet<DataAtMostAmountRestriction, int> simplifiedDatabaseAtMostAmountRestrictions;
        public SimplifiedDbSet<DataAtMostAmountRestriction, int> SimplifiedAtMostAmountRestrictions { get => simplifiedDatabaseAtMostAmountRestrictions; set => simplifiedDatabaseAtMostAmountRestrictions = GetIfSimplifiedDatabaseDbSet(value); }
        private SimplifiedDatabaseDbSet<DataBeforeHourProductRestriction, int> simplifiedDatabaseBeforeHourProductRestrictions;
        public SimplifiedDbSet<DataBeforeHourProductRestriction, int> SimplifiedBeforeHourProductRestrictions { get => simplifiedDatabaseBeforeHourProductRestrictions; set => simplifiedDatabaseBeforeHourProductRestrictions = GetIfSimplifiedDatabaseDbSet(value); }
        private SimplifiedDatabaseDbSet<DataBeforeHourRestriction, int> simplifiedDatabaseBeforeHourRestrictions;
        public SimplifiedDbSet<DataBeforeHourRestriction, int> SimplifiedBeforeHourRestrictions { get => simplifiedDatabaseBeforeHourRestrictions; set => simplifiedDatabaseBeforeHourRestrictions = GetIfSimplifiedDatabaseDbSet(value); }
        private SimplifiedDatabaseDbSet<DataDateRestriction, int> simplifiedDatabaseDateRestrictions;
        public SimplifiedDbSet<DataDateRestriction, int> SimplifiedDateRestrictions { get => simplifiedDatabaseDateRestrictions; set => simplifiedDatabaseDateRestrictions = GetIfSimplifiedDatabaseDbSet(value); }
        private SimplifiedDatabaseDbSet<DataPurchasePolicy, int> simplifiedDatabasePurchasePolicies;
        public SimplifiedDbSet<DataPurchasePolicy, int> SimplifiedPurchasePolicies { get => simplifiedDatabasePurchasePolicies; set => simplifiedDatabasePurchasePolicies = GetIfSimplifiedDatabaseDbSet(value); }

        private static SimplifiedDatabaseDbSet<T, U> GetIfSimplifiedDatabaseDbSet<T, U>(SimplifiedDbSet<T, U> elements) where T : class
        {
            if (!(elements is SimplifiedDatabaseDbSet<T, U>))
                throw new Exception("type should be SimplifiedDatabaseDbSet");
            return (SimplifiedDatabaseDbSet<T, U>)elements;
        }

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
