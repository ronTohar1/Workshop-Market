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
        private DbSetMock<DataMember> mockMembers;
        public DbSet<DataMember> Members { get => mockMembers; set => mockMembers = GetIfDbSetMock(value); }
        private DbSetMock<DataStore> mockStores;
        public DbSet<DataStore> Stores { get => mockStores; set => mockStores = GetIfDbSetMock(value); }
        private DbSetMock<DataProduct> mockProducts;
        public DbSet<DataProduct> Products { get => mockProducts; set => mockProducts = GetIfDbSetMock(value); }
        private DbSetMock<DataBid> mockBids;
        public DbSet<DataBid> Bids { get => mockBids; set => mockBids = GetIfDbSetMock(value); }
        private DbSetMock<DataCart> mockCarts;
        public DbSet<DataCart> Carts { get => mockCarts; set => mockCarts = GetIfDbSetMock(value); }
        private DbSetMock<DataManagerPermission> mockManagerPermissions;
        public DbSet<DataManagerPermission> ManagerPermissions { get => mockManagerPermissions; set => mockManagerPermissions = GetIfDbSetMock(value); }
        private DbSetMock<DataPurchaseOption> mockPurchaseOptions;
        public DbSet<DataPurchaseOption> PurchaseOptions { get => mockPurchaseOptions; set => mockPurchaseOptions = GetIfDbSetMock(value); }
        private DbSetMock<DataStoreMemberRoles> mockStoreMemberRoles;
        public DbSet<DataStoreMemberRoles> StoreMemberRoles { get => mockStoreMemberRoles; set => mockStoreMemberRoles = GetIfDbSetMock(value); }
        private DbSetMock<DataProductReview> mockProductReview;
        public DbSet<DataProductReview> ProductReview { get => mockProductReview; set => mockProductReview = GetIfDbSetMock(value); }
        private DbSetMock<DataAppointmentsNode> mockAppointmentsNodes;
        public DbSet<DataAppointmentsNode> AppointmentsNodes { get => mockAppointmentsNodes; set => mockAppointmentsNodes = GetIfDbSetMock(value); }
        private DbSetMock<DataShoppingBag> mockShoppingBags;
        public DbSet<DataShoppingBag> ShoppingBags { get => mockShoppingBags; set => mockShoppingBags = GetIfDbSetMock(value); }
        private DbSetMock<DataProductInBag> mockProductInBags;
        public DbSet<DataProductInBag> ProductInBags { get => mockProductInBags; set => mockProductInBags = GetIfDbSetMock(value); }
        private DbSetMock<DataNotification> mockNotifications;
        public DbSet<DataNotification> Notifications { get => mockNotifications; set => mockNotifications = GetIfDbSetMock(value); }
        private DbSetMock<DataPurchase> mockPurchases;
        public DbSet<DataPurchase> Purchases { get => mockPurchases; set => mockPurchases = GetIfDbSetMock(value); }
        private DbSetMock<DataDateDiscount> mockDateDiscounts;
        public DbSet<DataDateDiscount> DateDiscounts { get => mockDateDiscounts; set => mockDateDiscounts = GetIfDbSetMock(value); }
        private DbSetMock<DataOneProductDiscount> mockOneProductDiscounts;
        public DbSet<DataOneProductDiscount> OneProductDiscounts { get => mockOneProductDiscounts; set => mockOneProductDiscounts = GetIfDbSetMock(value); }
        private DbSetMock<DataStoreDiscount> mockStoreDiscounts;
        public DbSet<DataStoreDiscount> StoreDiscounts { get => mockStoreDiscounts; set => mockStoreDiscounts = GetIfDbSetMock(value); }
        private DbSetMock<DataDTOs.Market.StoreManagement.DiscountPolicy.DiscountExpressions.LogicalExpressions.DataAndExpression> mockDiscountAndExpressions;
        public DbSet<DataDTOs.Market.StoreManagement.DiscountPolicy.DiscountExpressions.LogicalExpressions.DataAndExpression> DiscountAndExpressions { get => mockDiscountAndExpressions; set => mockDiscountAndExpressions = GetIfDbSetMock(value); }
        private DbSetMock<DataDTOs.Market.StoreManagement.DiscountPolicy.DiscountExpressions.LogicalExpressions.DataOrExpression> mockDiscountOrExpressions;
        public DbSet<DataDTOs.Market.StoreManagement.DiscountPolicy.DiscountExpressions.LogicalExpressions.DataOrExpression> DiscountOrExpressions { get => mockDiscountOrExpressions; set => mockDiscountOrExpressions = GetIfDbSetMock(value); }
        private DbSetMock<DataXorExpression> mockXorExpressions;
        public DbSet<DataXorExpression> XorExpressions { get => mockXorExpressions; set => mockXorExpressions = GetIfDbSetMock(value); }
        private DbSetMock<DataMaxExpression> mockMaxExpressions;
        public DbSet<DataMaxExpression> MaxExpressions { get => mockMaxExpressions; set => mockMaxExpressions = GetIfDbSetMock(value); }
        private DbSetMock<DataBagValuePredicate> mockBagValuePredicates;
        public DbSet<DataBagValuePredicate> BagValuePredicates { get => mockBagValuePredicates; set => mockBagValuePredicates = GetIfDbSetMock(value); }
        private DbSetMock<DataProductAmountPredicate> mockProductAmountPredicates;
        public DbSet<DataProductAmountPredicate> ProductAmountPredicates { get => mockProductAmountPredicates; set => mockProductAmountPredicates = GetIfDbSetMock(value); }
        private DbSetMock<DataConditionDiscount> mockConditionDiscounts;
        public DbSet<DataConditionDiscount> ConditionDiscounts { get => mockConditionDiscounts; set => mockConditionDiscounts = GetIfDbSetMock(value); }
        private DbSetMock<DataIfDiscount> mockIfDiscounts;
        public DbSet<DataIfDiscount> IfDiscounts { get => mockIfDiscounts; set => mockIfDiscounts = GetIfDbSetMock(value); }
        private DbSetMock<DataLogicalExpression> mockLogicalExpressions;
        public DbSet<DataLogicalExpression> LogicalExpressions { get => mockLogicalExpressions; set => mockLogicalExpressions = GetIfDbSetMock(value); }
        private DbSetMock<DataConditionDiscount> mockConditionExpressions;
        public DbSet<DataConditionDiscount> ConditionExpressions { get => mockConditionExpressions; set => mockConditionExpressions = GetIfDbSetMock(value); }
        private DbSetMock<DataDiscountExpression> mockDiscountExpressions;
        public DbSet<DataDiscountExpression> DiscountExpressions { get => mockDiscountExpressions; set => mockDiscountExpressions = GetIfDbSetMock(value); }
        private DbSetMock<DataExpression> mockExpressions;
        public DbSet<DataExpression> Expressions { get => mockExpressions; set => mockExpressions = GetIfDbSetMock(value); }
        private DbSetMock<DataDTOs.Market.StoreManagement.DiscountPolicy.DiscountInterfaces.DataPredicateExpression> mockDiscountPredicateExpressions;
        public DbSet<DataDTOs.Market.StoreManagement.DiscountPolicy.DiscountInterfaces.DataPredicateExpression> DiscountPredicateExpressions { get => mockDiscountPredicateExpressions; set => mockDiscountPredicateExpressions = GetIfDbSetMock(value); }
        private DbSetMock<DataDiscount> mockDiscounts;
        public DbSet<DataDiscount> Discounts { get => mockDiscounts; set => mockDiscounts = GetIfDbSetMock(value); }
        private DbSetMock<DataDTOs.Market.StoreManagement.PurchasesPolicy.LogicalOperators.DataAndExpression> mockPurchaseAndExpressions;
        public DbSet<DataDTOs.Market.StoreManagement.PurchasesPolicy.LogicalOperators.DataAndExpression> PurchaseAndExpressions { get => mockPurchaseAndExpressions; set => mockPurchaseAndExpressions = GetIfDbSetMock(value); }
        private DbSetMock<DataImpliesExpression> mockImpliesExpressions;
        public DbSet<DataImpliesExpression> ImpliesExpressions { get => mockImpliesExpressions; set => mockImpliesExpressions = GetIfDbSetMock(value); }
        private DbSetMock<DataDTOs.Market.StoreManagement.PurchasesPolicy.LogicalOperators.DataOrExpression> mockPurchaseOrExpressions;
        public DbSet<DataDTOs.Market.StoreManagement.PurchasesPolicy.LogicalOperators.DataOrExpression> PurchaseOrExpressions { get => mockPurchaseOrExpressions; set => mockPurchaseOrExpressions = GetIfDbSetMock(value); }
        private DbSetMock<DataCheckProductLessPredicate> mockCheckProductLessPredicates;
        public DbSet<DataCheckProductLessPredicate> CheckProductLessPredicates { get => mockCheckProductLessPredicates; set => mockCheckProductLessPredicates = GetIfDbSetMock(value); }
        private DbSetMock<DataCheckProductMoreEqualsPredicate> mockCheckProductMoreEqualsPredicates;
        public DbSet<DataCheckProductMoreEqualsPredicate> CheckProductMoreEqualsPredicates { get => mockCheckProductMoreEqualsPredicates; set => mockCheckProductMoreEqualsPredicates = GetIfDbSetMock(value); }
        private DbSetMock<DataDTOs.Market.StoreManagement.PurchasesPolicy.PurchasesInterfaces.DataPredicateExpression> mockPruchasePredicateExpressions;
        public DbSet<DataDTOs.Market.StoreManagement.PurchasesPolicy.PurchasesInterfaces.DataPredicateExpression> PruchasePredicateExpressions { get => mockPruchasePredicateExpressions; set => mockPruchasePredicateExpressions = GetIfDbSetMock(value); }
        private DbSetMock<DataIPurchasePolicy> mockInterfacesPurchasePolicies;
        public DbSet<DataIPurchasePolicy> InterfacesPurchasePolicies { get => mockInterfacesPurchasePolicies; set => mockInterfacesPurchasePolicies = GetIfDbSetMock(value); }
        private DbSetMock<DataRestrictionExpression> mockRestrictionExpressions;
        public DbSet<DataRestrictionExpression> RestrictionExpressions { get => mockRestrictionExpressions; set => mockRestrictionExpressions = GetIfDbSetMock(value); }
        private DbSetMock<DataAfterHourProductRestriction> mockDataAfterHourProductRestrictions;
        public DbSet<DataAfterHourProductRestriction> DataAfterHourProductRestrictions { get => mockDataAfterHourProductRestrictions; set => mockDataAfterHourProductRestrictions = GetIfDbSetMock(value); }
        private DbSetMock<DataAfterHourRestriction> mockAfterHourRestrictions;
        public DbSet<DataAfterHourRestriction> AfterHourRestrictions { get => mockAfterHourRestrictions; set => mockAfterHourRestrictions = GetIfDbSetMock(value); }
        private DbSetMock<DataAtLeastAmountRestriction> mockAtLeastAmountRestrictions;
        public DbSet<DataAtLeastAmountRestriction> AtLeastAmountRestrictions { get => mockAtLeastAmountRestrictions; set => mockAtLeastAmountRestrictions = GetIfDbSetMock(value); }
        private DbSetMock<DataAtMostAmountRestriction> mockAtMostAmountRestrictions;
        public DbSet<DataAtMostAmountRestriction> AtMostAmountRestrictions { get => mockAtMostAmountRestrictions; set => mockAtMostAmountRestrictions = GetIfDbSetMock(value); }
        private DbSetMock<DataBeforeHourProductRestriction> mockBeforeHourProductRestrictions;
        public DbSet<DataBeforeHourProductRestriction> BeforeHourProductRestrictions { get => mockBeforeHourProductRestrictions; set => mockBeforeHourProductRestrictions = GetIfDbSetMock(value); }
        private DbSetMock<DataBeforeHourRestriction> mockBeforeHourRestrictions;
        public DbSet<DataBeforeHourRestriction> BeforeHourRestrictions { get => mockBeforeHourRestrictions; set => mockBeforeHourRestrictions = GetIfDbSetMock(value); }
        private DbSetMock<DataDateRestriction> mockDateRestrictions;
        public DbSet<DataDateRestriction> DateRestrictions { get => mockDateRestrictions; set => mockDateRestrictions = GetIfDbSetMock(value); }
        private DbSetMock<DataPurchasePolicy> mockPurchasePolicies;
        public DbSet<DataPurchasePolicy> PurchasePolicies { get => mockPurchasePolicies; set => mockPurchasePolicies = GetIfDbSetMock(value); }

        public DatabaseMock()
        {
            mockMembers = new DbSetMock<DataMember>();
            mockStores = new DbSetMock<DataStore>();
            mockProducts = new DbSetMock<DataProduct>();
            mockBids = new DbSetMock<DataBid>();
            mockCarts = new DbSetMock<DataCart>(); 
            mockManagerPermissions = new DbSetMock<DataManagerPermission>();
            mockPurchaseOptions = new DbSetMock<DataPurchaseOption>();
            mockStoreMemberRoles = new DbSetMock<DataStoreMemberRoles>();
            mockProductReview = new DbSetMock<DataProductReview>();
            mockAppointmentsNodes = new DbSetMock<DataAppointmentsNode>();
            mockShoppingBags = new DbSetMock<DataShoppingBag>();
            mockProductInBags = new DbSetMock<DataProductInBag>();
            mockNotifications = new DbSetMock<DataNotification>();
            mockPurchases = new DbSetMock<DataPurchase>();
            mockDateDiscounts = new DbSetMock<DataDateDiscount>();
            mockOneProductDiscounts = new DbSetMock<DataOneProductDiscount>();
            mockStoreDiscounts = new DbSetMock<DataStoreDiscount>();
            mockDiscountAndExpressions = new DbSetMock<DataDTOs.Market.StoreManagement.DiscountPolicy.DiscountExpressions.LogicalExpressions.DataAndExpression>();
            mockDiscountOrExpressions = new DbSetMock<DataDTOs.Market.StoreManagement.DiscountPolicy.DiscountExpressions.LogicalExpressions.DataOrExpression>();
            mockXorExpressions = new DbSetMock<DataXorExpression>();
            mockMaxExpressions = new DbSetMock<DataMaxExpression>();
            mockBagValuePredicates = new DbSetMock<DataBagValuePredicate>();
            mockProductAmountPredicates = new DbSetMock<DataProductAmountPredicate>();
            mockConditionDiscounts = new DbSetMock<DataConditionDiscount>();
            mockIfDiscounts = new DbSetMock<DataIfDiscount>();
            mockLogicalExpressions = new DbSetMock<DataLogicalExpression>();
            mockConditionExpressions = new DbSetMock<DataConditionDiscount>();
            mockDiscountExpressions = new DbSetMock<DataDiscountExpression>();
            mockExpressions = new DbSetMock<DataExpression>();
            mockDiscountPredicateExpressions = new DbSetMock<DataDTOs.Market.StoreManagement.DiscountPolicy.DiscountInterfaces.DataPredicateExpression>(); 
            mockDiscounts = new DbSetMock<DataDiscount>(); 
            mockPurchaseAndExpressions = new DbSetMock<DataDTOs.Market.StoreManagement.PurchasesPolicy.LogicalOperators.DataAndExpression>(); 
            mockImpliesExpressions = new DbSetMock<DataImpliesExpression>(); 
            mockPurchaseOrExpressions = new DbSetMock<DataDTOs.Market.StoreManagement.PurchasesPolicy.LogicalOperators.DataOrExpression>();
            mockCheckProductLessPredicates = new DbSetMock<DataCheckProductLessPredicate>();
            mockCheckProductMoreEqualsPredicates = new DbSetMock<DataCheckProductMoreEqualsPredicate>();
            mockPruchasePredicateExpressions = new DbSetMock<DataDTOs.Market.StoreManagement.PurchasesPolicy.PurchasesInterfaces.DataPredicateExpression>();
            mockInterfacesPurchasePolicies = new DbSetMock<DataIPurchasePolicy>(); 
            mockRestrictionExpressions = new DbSetMock<DataRestrictionExpression>();
            mockDataAfterHourProductRestrictions = new DbSetMock<DataAfterHourProductRestriction>();
            mockAfterHourRestrictions = new DbSetMock<DataAfterHourRestriction>();
            mockAtLeastAmountRestrictions = new DbSetMock<DataAtLeastAmountRestriction>();
            mockAtMostAmountRestrictions = new DbSetMock<DataAtMostAmountRestriction>();
            mockBeforeHourProductRestrictions = new DbSetMock<DataBeforeHourProductRestriction>();
            mockBeforeHourRestrictions = new DbSetMock<DataBeforeHourRestriction>();
            mockDateRestrictions = new DbSetMock<DataDateRestriction>();
            mockPurchasePolicies = new DbSetMock<DataPurchasePolicy>();
        }

        public void Remove(Object toRemove)
        {
            if (toRemove.GetType() == typeof(DataMember))
                Members.Remove((DataMember)toRemove);
            else if (toRemove.GetType() == typeof(DataStore))
                Stores.Remove((DataStore)toRemove);
            else if (toRemove.GetType() == typeof(DataProduct))
                Products.Remove((DataProduct)toRemove);
            else if (toRemove.GetType() == typeof(DataBid))
                Bids.Remove((DataBid)toRemove);
            else if (toRemove.GetType() == typeof(DataCart))
                Carts.Remove((DataCart)toRemove);
            else if (toRemove.GetType() == typeof(DataManagerPermission))
                ManagerPermissions.Remove((DataManagerPermission)toRemove);
            else if (toRemove.GetType() == typeof(DataPurchaseOption))
                PurchaseOptions.Remove((DataPurchaseOption)toRemove);
            else if (toRemove.GetType() == typeof(DataStoreMemberRoles))
                StoreMemberRoles.Remove((DataStoreMemberRoles)toRemove);
            else if (toRemove.GetType() == typeof(DataProductReview))
                ProductReview.Remove((DataProductReview)toRemove);
            else if (toRemove.GetType() == typeof(DataAppointmentsNode))
                AppointmentsNodes.Remove((DataAppointmentsNode)toRemove);
            else if (toRemove.GetType() == typeof(DataShoppingBag))
                ShoppingBags.Remove((DataShoppingBag)toRemove);
            else if (toRemove.GetType() == typeof(DataProductInBag))
                ProductInBags.Remove((DataProductInBag)toRemove);
            else if (toRemove.GetType() == typeof(DataNotification))
                Notifications.Remove((DataNotification)toRemove);
            else if (toRemove.GetType() == typeof(DataPurchase))
                Purchases.Remove((DataPurchase)toRemove);
            else if (toRemove.GetType() == typeof(DataDateDiscount))
                DateDiscounts.Remove((DataDateDiscount)toRemove);
            else if (toRemove.GetType() == typeof(DataOneProductDiscount))
                OneProductDiscounts.Remove((DataOneProductDiscount)toRemove);
            else if (toRemove.GetType() == typeof(DataStoreDiscount))
                StoreDiscounts.Remove((DataStoreDiscount)toRemove);
            else if (toRemove.GetType() == typeof(DataDTOs.Market.StoreManagement.DiscountPolicy.DiscountExpressions.LogicalExpressions.DataAndExpression))
                DiscountAndExpressions.Remove((DataDTOs.Market.StoreManagement.DiscountPolicy.DiscountExpressions.LogicalExpressions.DataAndExpression)toRemove);
            else if (toRemove.GetType() == typeof(DataDTOs.Market.StoreManagement.DiscountPolicy.DiscountExpressions.LogicalExpressions.DataOrExpression))
                DiscountOrExpressions.Remove((DataDTOs.Market.StoreManagement.DiscountPolicy.DiscountExpressions.LogicalExpressions.DataOrExpression)toRemove);
            else if (toRemove.GetType() == typeof(DataXorExpression))
                XorExpressions.Remove((DataXorExpression)toRemove);
            else if (toRemove.GetType() == typeof(DataMaxExpression))
                MaxExpressions.Remove((DataMaxExpression)toRemove);
            else if (toRemove.GetType() == typeof(DataBagValuePredicate))
                BagValuePredicates.Remove((DataBagValuePredicate)toRemove);
            else if (toRemove.GetType() == typeof(DataProductAmountPredicate))
                ProductAmountPredicates.Remove((DataProductAmountPredicate)toRemove);
            else if (toRemove.GetType() == typeof(DataConditionDiscount))
                ConditionDiscounts.Remove((DataConditionDiscount)toRemove);
            else if (toRemove.GetType() == typeof(DataIfDiscount))
                IfDiscounts.Remove((DataIfDiscount)toRemove);
            else if (toRemove.GetType() == typeof(DataLogicalExpression))
                LogicalExpressions.Remove((DataLogicalExpression)toRemove);
            else if (toRemove.GetType() == typeof(DataConditionDiscount))
                ConditionDiscounts.Remove((DataConditionDiscount)toRemove);
            else if (toRemove.GetType() == typeof(DataDiscountExpression))
                DiscountExpressions.Remove((DataDiscountExpression)toRemove);
            else if (toRemove.GetType() == typeof(DataExpression))
                Expressions.Remove((DataExpression)toRemove);
            else if (toRemove.GetType() == typeof(DataDTOs.Market.StoreManagement.DiscountPolicy.DiscountInterfaces.DataPredicateExpression))
                DiscountPredicateExpressions.Remove((DataDTOs.Market.StoreManagement.DiscountPolicy.DiscountInterfaces.DataPredicateExpression)toRemove);
            else if (toRemove.GetType() == typeof(DataDiscount))
                Discounts.Remove((DataDiscount)toRemove);
            else if (toRemove.GetType() == typeof(DataDTOs.Market.StoreManagement.PurchasesPolicy.LogicalOperators.DataAndExpression))
                PurchaseAndExpressions.Remove((DataDTOs.Market.StoreManagement.PurchasesPolicy.LogicalOperators.DataAndExpression)toRemove);
            else if (toRemove.GetType() == typeof(DataImpliesExpression))
                ImpliesExpressions.Remove((DataImpliesExpression)toRemove);
            else if (toRemove.GetType() == typeof(DataDTOs.Market.StoreManagement.PurchasesPolicy.LogicalOperators.DataOrExpression))
                PurchaseOrExpressions.Remove((DataDTOs.Market.StoreManagement.PurchasesPolicy.LogicalOperators.DataOrExpression)toRemove);
            else if (toRemove.GetType() == typeof(DataCheckProductLessPredicate))
                CheckProductLessPredicates.Remove((DataCheckProductLessPredicate)toRemove);
            else if (toRemove.GetType() == typeof(DataCheckProductMoreEqualsPredicate))
                CheckProductMoreEqualsPredicates.Remove((DataCheckProductMoreEqualsPredicate)toRemove);
            else if (toRemove.GetType() == typeof(DataDTOs.Market.StoreManagement.PurchasesPolicy.PurchasesInterfaces.DataPredicateExpression))
                PruchasePredicateExpressions.Remove((DataDTOs.Market.StoreManagement.PurchasesPolicy.PurchasesInterfaces.DataPredicateExpression)toRemove);
            else if (toRemove.GetType() == typeof(DataIPurchasePolicy))
                InterfacesPurchasePolicies.Remove((DataIPurchasePolicy)toRemove);
            else if (toRemove.GetType() == typeof(DataRestrictionExpression))
                RestrictionExpressions.Remove((DataRestrictionExpression)toRemove);
            else if (toRemove.GetType() == typeof(DataAfterHourProductRestriction))
                DataAfterHourProductRestrictions.Remove((DataAfterHourProductRestriction)toRemove);
            else if (toRemove.GetType() == typeof(DataAfterHourRestriction))
                AfterHourRestrictions.Remove((DataAfterHourRestriction)toRemove);
            else if (toRemove.GetType() == typeof(DataAtLeastAmountRestriction))
                AtLeastAmountRestrictions.Remove((DataAtLeastAmountRestriction)toRemove);
            else if (toRemove.GetType() == typeof(DataAtMostAmountRestriction))
                AtMostAmountRestrictions.Remove((DataAtMostAmountRestriction)toRemove);
            else if (toRemove.GetType() == typeof(DataBeforeHourProductRestriction))
                BeforeHourProductRestrictions.Remove((DataBeforeHourProductRestriction)toRemove);
            else if (toRemove.GetType() == typeof(DataBeforeHourRestriction))
                BeforeHourRestrictions.Remove((DataBeforeHourRestriction)toRemove);
            else if (toRemove.GetType() == typeof(DataDateRestriction))
                DateRestrictions.Remove((DataDateRestriction)toRemove);
            else if (toRemove.GetType() == typeof(DataPurchasePolicy))
                PurchasePolicies.Remove((DataPurchasePolicy)toRemove);
            else
                throw new Exception("The given object's type is not in the database"); 
        }

        public int SaveChanges()
        {
            // not using database 
            return 0; 
        }

        private static DbSetMock<T> GetIfDbSetMock<T>(DbSet<T> elements) where T : class
        {
            if (!(elements is DbSetMock<T>))
                throw new Exception("type should be DbSetMock"); 
            return (DbSetMock<T>)elements;
        }
    }
}
