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
        private SimplifiedMockDbSet<DataMember, int> mockMembers;
        public SimplifiedDbSet<DataMember, int> SimplifiedMembers { get => mockMembers; set => mockMembers = GetIfSimplifiedMockDbSet(value); }
        private SimplifiedMockDbSet<DataStore, int> mockStores;
        public SimplifiedDbSet<DataStore, int> SimplifiedStores { get => mockStores; set => mockStores = GetIfSimplifiedMockDbSet(value); }
        private SimplifiedMockDbSet<DataProduct, int> mockProducts;
        public SimplifiedDbSet<DataProduct, int> SimplifiedProducts { get => mockProducts; set => mockProducts = GetIfSimplifiedMockDbSet(value); }
        private SimplifiedMockDbSet<DataBid, int> mockBids;
        public SimplifiedDbSet<DataBid, int> SimplifiedBids { get => mockBids; set => mockBids = GetIfSimplifiedMockDbSet(value); }
        private SimplifiedMockDbSet<DataCart, int> mockCarts;
        public SimplifiedDbSet<DataCart, int> SimplifiedCarts { get => mockCarts; set => mockCarts = GetIfSimplifiedMockDbSet(value); }
        private SimplifiedMockDbSet<DataManagerPermission, int> mockManagerPermissions;
        public SimplifiedDbSet<DataManagerPermission, int> SimplifiedManagerPermissions { get => mockManagerPermissions; set => mockManagerPermissions = GetIfSimplifiedMockDbSet(value); }
        private SimplifiedMockDbSet<DataPurchaseOption, int> mockPurchaseOptions;
        public SimplifiedDbSet<DataPurchaseOption, int> SimplifiedPurchaseOptions { get => mockPurchaseOptions; set => mockPurchaseOptions = GetIfSimplifiedMockDbSet(value); }
        private SimplifiedMockDbSet<DataStoreMemberRoles, int> mockStoreMemberRoles;
        public SimplifiedDbSet<DataStoreMemberRoles, int> SimplifiedStoreMemberRoles { get => mockStoreMemberRoles; set => mockStoreMemberRoles = GetIfSimplifiedMockDbSet(value); }
        private SimplifiedMockDbSet<DataProductReview, int> mockProductReview;
        public SimplifiedDbSet<DataProductReview, int> SimplifiedProductReview { get => mockProductReview; set => mockProductReview = GetIfSimplifiedMockDbSet(value); }
        private SimplifiedMockDbSet<DataAppointmentsNode, int> mockAppointmentsNodes;
        public SimplifiedDbSet<DataAppointmentsNode, int> SimplifiedAppointmentsNodes { get => mockAppointmentsNodes; set => mockAppointmentsNodes = GetIfSimplifiedMockDbSet(value); }
        private SimplifiedMockDbSet<DataShoppingBag, int> mockShoppingBags;
        public SimplifiedDbSet<DataShoppingBag, int> SimplifiedShoppingBags { get => mockShoppingBags; set => mockShoppingBags = GetIfSimplifiedMockDbSet(value); }
        private SimplifiedMockDbSet<DataProductInBag, int> mockProductInBags;
        public SimplifiedDbSet<DataProductInBag, int> SimplifiedProductInBags { get => mockProductInBags; set => mockProductInBags = GetIfSimplifiedMockDbSet(value); }
        private SimplifiedMockDbSet<DataNotification, int> mockNotifications;
        public SimplifiedDbSet<DataNotification, int> SimplifiedNotifications { get => mockNotifications; set => mockNotifications = GetIfSimplifiedMockDbSet(value); }
        private SimplifiedMockDbSet<DataPurchase, int> mockPurchases;
        public SimplifiedDbSet<DataPurchase, int> SimplifiedPurchases { get => mockPurchases; set => mockPurchases = GetIfSimplifiedMockDbSet(value); }
        private SimplifiedMockDbSet<DataDateDiscount, int> mockDateDiscounts;
        public SimplifiedDbSet<DataDateDiscount, int> SimplifiedDateDiscounts { get => mockDateDiscounts; set => mockDateDiscounts = GetIfSimplifiedMockDbSet(value); }
        private SimplifiedMockDbSet<DataOneProductDiscount, int> mockOneProductDiscounts;
        public SimplifiedDbSet<DataOneProductDiscount, int> SimplifiedOneProductDiscounts { get => mockOneProductDiscounts; set => mockOneProductDiscounts = GetIfSimplifiedMockDbSet(value); }
        private SimplifiedMockDbSet<DataStoreDiscount, int> mockStoreDiscounts;
        public SimplifiedDbSet<DataStoreDiscount, int> SimplifiedStoreDiscounts { get => mockStoreDiscounts; set => mockStoreDiscounts = GetIfSimplifiedMockDbSet(value); }
        private SimplifiedMockDbSet<DataDTOs.Market.StoreManagement.DiscountPolicy.DiscountExpressions.LogicalExpressions.DataAndExpression, int> mockDiscountAndExpressions;
        public SimplifiedDbSet<DataDTOs.Market.StoreManagement.DiscountPolicy.DiscountExpressions.LogicalExpressions.DataAndExpression, int> SimplifiedDiscountAndExpressions { get => mockDiscountAndExpressions; set => mockDiscountAndExpressions = GetIfSimplifiedMockDbSet(value); }
        private SimplifiedMockDbSet<DataDTOs.Market.StoreManagement.DiscountPolicy.DiscountExpressions.LogicalExpressions.DataOrExpression, int> mockDiscountOrExpressions;
        public SimplifiedDbSet<DataDTOs.Market.StoreManagement.DiscountPolicy.DiscountExpressions.LogicalExpressions.DataOrExpression, int> SimplifiedDiscountOrExpressions { get => mockDiscountOrExpressions; set => mockDiscountOrExpressions = GetIfSimplifiedMockDbSet(value); }
        private SimplifiedMockDbSet<DataXorExpression, int> mockXorExpressions;
        public SimplifiedDbSet<DataXorExpression, int> SimplifiedXorExpressions { get => mockXorExpressions; set => mockXorExpressions = GetIfSimplifiedMockDbSet(value); }
        private SimplifiedMockDbSet<DataMaxExpression, int> mockMaxExpressions;
        public SimplifiedDbSet<DataMaxExpression, int> SimplifiedMaxExpressions { get => mockMaxExpressions; set => mockMaxExpressions = GetIfSimplifiedMockDbSet(value); }
        private SimplifiedMockDbSet<DataBagValuePredicate, int> mockBagValuePredicates;
        public SimplifiedDbSet<DataBagValuePredicate, int> SimplifiedBagValuePredicates { get => mockBagValuePredicates; set => mockBagValuePredicates = GetIfSimplifiedMockDbSet(value); }
        private SimplifiedMockDbSet<DataProductAmountPredicate, int> mockProductAmountPredicates;
        public SimplifiedDbSet<DataProductAmountPredicate, int> SimplifiedProductAmountPredicates { get => mockProductAmountPredicates; set => mockProductAmountPredicates = GetIfSimplifiedMockDbSet(value); }
        private SimplifiedMockDbSet<DataConditionDiscount, int> mockConditionDiscounts;
        public SimplifiedDbSet<DataConditionDiscount, int> SimplifiedConditionDiscounts { get => mockConditionDiscounts; set => mockConditionDiscounts = GetIfSimplifiedMockDbSet(value); }
        private SimplifiedMockDbSet<DataIfDiscount, int> mockIfDiscounts;
        public SimplifiedDbSet<DataIfDiscount, int> SimplifiedIfDiscounts { get => mockIfDiscounts; set => mockIfDiscounts = GetIfSimplifiedMockDbSet(value); }
        private SimplifiedMockDbSet<DataLogicalExpression, int> mockLogicalExpressions;
        public SimplifiedDbSet<DataLogicalExpression, int> SimplifiedLogicalExpressions { get => mockLogicalExpressions; set => mockLogicalExpressions = GetIfSimplifiedMockDbSet(value); }
        private SimplifiedMockDbSet<DataConditionDiscount, int> mockConditionExpressions;
        public SimplifiedDbSet<DataConditionDiscount, int> SimplifiedConditionExpressions { get => mockConditionExpressions; set => mockConditionExpressions = GetIfSimplifiedMockDbSet(value); }
        private SimplifiedMockDbSet<DataDiscountExpression, int> mockDiscountExpressions;
        public SimplifiedDbSet<DataDiscountExpression, int> SimplifiedDiscountExpressions { get => mockDiscountExpressions; set => mockDiscountExpressions = GetIfSimplifiedMockDbSet(value); }
        private SimplifiedMockDbSet<DataExpression, int> mockExpressions;
        public SimplifiedDbSet<DataExpression, int> SimplifiedExpressions { get => mockExpressions; set => mockExpressions = GetIfSimplifiedMockDbSet(value); }
        private SimplifiedMockDbSet<DataDTOs.Market.StoreManagement.DiscountPolicy.DiscountInterfaces.DataPredicateExpression, int> mockDiscountPredicateExpressions;
        public SimplifiedDbSet<DataDTOs.Market.StoreManagement.DiscountPolicy.DiscountInterfaces.DataPredicateExpression, int> SimplifiedDiscountPredicateExpressions { get => mockDiscountPredicateExpressions; set => mockDiscountPredicateExpressions = GetIfSimplifiedMockDbSet(value); }
        private SimplifiedMockDbSet<DataDiscount, int> mockDiscounts;
        public SimplifiedDbSet<DataDiscount, int> SimplifiedDiscounts { get => mockDiscounts; set => mockDiscounts = GetIfSimplifiedMockDbSet(value); }
        private SimplifiedMockDbSet<DataDTOs.Market.StoreManagement.PurchasesPolicy.LogicalOperators.DataAndExpression, int> mockPurchaseAndExpressions;
        public SimplifiedDbSet<DataDTOs.Market.StoreManagement.PurchasesPolicy.LogicalOperators.DataAndExpression, int> SimplifiedPurchaseAndExpressions { get => mockPurchaseAndExpressions; set => mockPurchaseAndExpressions = GetIfSimplifiedMockDbSet(value); }
        private SimplifiedMockDbSet<DataImpliesExpression, int> mockImpliesExpressions;
        public SimplifiedDbSet<DataImpliesExpression, int> SimplifiedImpliesExpressions { get => mockImpliesExpressions; set => mockImpliesExpressions = GetIfSimplifiedMockDbSet(value); }
        private SimplifiedMockDbSet<DataDTOs.Market.StoreManagement.PurchasesPolicy.LogicalOperators.DataOrExpression, int> mockPurchaseOrExpressions;
        public SimplifiedDbSet<DataDTOs.Market.StoreManagement.PurchasesPolicy.LogicalOperators.DataOrExpression, int> SimplifiedPurchaseOrExpressions { get => mockPurchaseOrExpressions; set => mockPurchaseOrExpressions = GetIfSimplifiedMockDbSet(value); }
        private SimplifiedMockDbSet<DataCheckProductLessPredicate, int> mockCheckProductLessPredicates;
        public SimplifiedDbSet<DataCheckProductLessPredicate, int> SimplifiedCheckProductLessPredicates { get => mockCheckProductLessPredicates; set => mockCheckProductLessPredicates = GetIfSimplifiedMockDbSet(value); }
        private SimplifiedMockDbSet<DataCheckProductMoreEqualsPredicate, int> mockCheckProductMoreEqualsPredicates;
        public SimplifiedDbSet<DataCheckProductMoreEqualsPredicate, int> SimplifiedCheckProductMoreEqualsPredicates { get => mockCheckProductMoreEqualsPredicates; set => mockCheckProductMoreEqualsPredicates = GetIfSimplifiedMockDbSet(value); }
        private SimplifiedMockDbSet<DataDTOs.Market.StoreManagement.PurchasesPolicy.PurchasesInterfaces.DataPredicateExpression, int> mockPruchasePredicateExpressions;
        public SimplifiedDbSet<DataDTOs.Market.StoreManagement.PurchasesPolicy.PurchasesInterfaces.DataPredicateExpression, int> SimplifiedPruchasePredicateExpressions { get => mockPruchasePredicateExpressions; set => mockPruchasePredicateExpressions = GetIfSimplifiedMockDbSet(value); }
        private SimplifiedMockDbSet<DataIPurchasePolicy, int> mockInterfacesPurchasePolicies;
        public SimplifiedDbSet<DataIPurchasePolicy, int> SimplifiedInterfacesPurchasePolicies { get => mockInterfacesPurchasePolicies; set => mockInterfacesPurchasePolicies = GetIfSimplifiedMockDbSet(value); }
        private SimplifiedMockDbSet<DataRestrictionExpression, int> mockRestrictionExpressions;
        public SimplifiedDbSet<DataRestrictionExpression, int> SimplifiedRestrictionExpressions { get => mockRestrictionExpressions; set => mockRestrictionExpressions = GetIfSimplifiedMockDbSet(value); }
        private SimplifiedMockDbSet<DataAfterHourProductRestriction, int> mockDataAfterHourProductRestrictions;
        public SimplifiedDbSet<DataAfterHourProductRestriction, int> SimplifiedDataAfterHourProductRestrictions { get => mockDataAfterHourProductRestrictions; set => mockDataAfterHourProductRestrictions = GetIfSimplifiedMockDbSet(value); }
        private SimplifiedMockDbSet<DataAfterHourRestriction, int> mockAfterHourRestrictions;
        public SimplifiedDbSet<DataAfterHourRestriction, int> SimplifiedAfterHourRestrictions { get => mockAfterHourRestrictions; set => mockAfterHourRestrictions = GetIfSimplifiedMockDbSet(value); }
        private SimplifiedMockDbSet<DataAtLeastAmountRestriction, int> mockAtLeastAmountRestrictions;
        public SimplifiedDbSet<DataAtLeastAmountRestriction, int> SimplifiedAtLeastAmountRestrictions { get => mockAtLeastAmountRestrictions; set => mockAtLeastAmountRestrictions = GetIfSimplifiedMockDbSet(value); }
        private SimplifiedMockDbSet<DataAtMostAmountRestriction, int> mockAtMostAmountRestrictions;
        public SimplifiedDbSet<DataAtMostAmountRestriction, int> SimplifiedAtMostAmountRestrictions { get => mockAtMostAmountRestrictions; set => mockAtMostAmountRestrictions = GetIfSimplifiedMockDbSet(value); }
        private SimplifiedMockDbSet<DataBeforeHourProductRestriction, int> mockBeforeHourProductRestrictions;
        public SimplifiedDbSet<DataBeforeHourProductRestriction, int> SimplifiedBeforeHourProductRestrictions { get => mockBeforeHourProductRestrictions; set => mockBeforeHourProductRestrictions = GetIfSimplifiedMockDbSet(value); }
        private SimplifiedMockDbSet<DataBeforeHourRestriction, int> mockBeforeHourRestrictions;
        public SimplifiedDbSet<DataBeforeHourRestriction, int> SimplifiedBeforeHourRestrictions { get => mockBeforeHourRestrictions; set => mockBeforeHourRestrictions = GetIfSimplifiedMockDbSet(value); }
        private SimplifiedMockDbSet<DataDateRestriction, int> mockDateRestrictions;
        public SimplifiedDbSet<DataDateRestriction, int> SimplifiedDateRestrictions { get => mockDateRestrictions; set => mockDateRestrictions = GetIfSimplifiedMockDbSet(value); }
        private SimplifiedMockDbSet<DataPurchasePolicy, int> mockPurchasePolicies;
        public SimplifiedDbSet<DataPurchasePolicy, int> SimplifiedPurchasePolicies { get => mockPurchasePolicies; set => mockPurchasePolicies = GetIfSimplifiedMockDbSet(value); }

        public DatabaseMock()
        {
            mockMembers = new SimplifiedMockDbSet<DataMember, int>(dataObject => dataObject.Id);
            mockStores = new SimplifiedMockDbSet<DataStore, int>(dataObject => dataObject.Id);
            mockProducts = new SimplifiedMockDbSet<DataProduct, int>(dataObject => dataObject.Id);
            mockBids = new SimplifiedMockDbSet<DataBid, int>(dataObject => dataObject.Id);
            mockCarts = new SimplifiedMockDbSet<DataCart, int>(dataObject => dataObject.Id); 
            mockManagerPermissions = new SimplifiedMockDbSet<DataManagerPermission, int>(dataObject => dataObject.Id);
            mockPurchaseOptions = new SimplifiedMockDbSet<DataPurchaseOption, int>(dataObject => dataObject.Id);
            mockStoreMemberRoles = new SimplifiedMockDbSet<DataStoreMemberRoles, int>(dataObject => dataObject.Id);
            mockProductReview = new SimplifiedMockDbSet<DataProductReview, int>(dataObject => dataObject.Id);
            mockAppointmentsNodes = new SimplifiedMockDbSet<DataAppointmentsNode, int>(dataObject => dataObject.Id);
            mockShoppingBags = new SimplifiedMockDbSet<DataShoppingBag, int>(dataObject => dataObject.Id);
            mockProductInBags = new SimplifiedMockDbSet<DataProductInBag, int>(dataObject => dataObject.Id);
            mockNotifications = new SimplifiedMockDbSet<DataNotification, int>(dataObject => dataObject.Id);
            mockPurchases = new SimplifiedMockDbSet<DataPurchase, int>(dataObject => dataObject.Id);
            mockDateDiscounts = new SimplifiedMockDbSet<DataDateDiscount, int>(dataObject => dataObject.Id);
            mockOneProductDiscounts = new SimplifiedMockDbSet<DataOneProductDiscount, int>(dataObject => dataObject.Id);
            mockStoreDiscounts = new SimplifiedMockDbSet<DataStoreDiscount, int>(dataObject => dataObject.Id);
            mockDiscountAndExpressions = new SimplifiedMockDbSet<DataDTOs.Market.StoreManagement.DiscountPolicy.DiscountExpressions.LogicalExpressions.DataAndExpression, int>(dataObject => dataObject.Id);
            mockDiscountOrExpressions = new SimplifiedMockDbSet<DataDTOs.Market.StoreManagement.DiscountPolicy.DiscountExpressions.LogicalExpressions.DataOrExpression, int>(dataObject => dataObject.Id);
            mockXorExpressions = new SimplifiedMockDbSet<DataXorExpression, int>(dataObject => dataObject.Id);
            mockMaxExpressions = new SimplifiedMockDbSet<DataMaxExpression, int>(dataObject => dataObject.Id);
            mockBagValuePredicates = new SimplifiedMockDbSet<DataBagValuePredicate, int>(dataObject => dataObject.Id);
            mockProductAmountPredicates = new SimplifiedMockDbSet<DataProductAmountPredicate, int>(dataObject => dataObject.Id);
            mockConditionDiscounts = new SimplifiedMockDbSet<DataConditionDiscount, int>(dataObject => dataObject.Id);
            mockIfDiscounts = new SimplifiedMockDbSet<DataIfDiscount, int>(dataObject => dataObject.Id);
            mockLogicalExpressions = new SimplifiedMockDbSet<DataLogicalExpression, int>(dataObject => dataObject.Id);
            mockConditionExpressions = new SimplifiedMockDbSet<DataConditionDiscount, int>(dataObject => dataObject.Id);
            mockDiscountExpressions = new SimplifiedMockDbSet<DataDiscountExpression, int>(dataObject => dataObject.Id);
            mockExpressions = new SimplifiedMockDbSet<DataExpression, int>(dataObject => dataObject.Id);
            mockDiscountPredicateExpressions = new SimplifiedMockDbSet<DataDTOs.Market.StoreManagement.DiscountPolicy.DiscountInterfaces.DataPredicateExpression, int>(dataObject => dataObject.Id); 
            mockDiscounts = new SimplifiedMockDbSet<DataDiscount, int>(dataObject => dataObject.Id); 
            mockPurchaseAndExpressions = new SimplifiedMockDbSet<DataDTOs.Market.StoreManagement.PurchasesPolicy.LogicalOperators.DataAndExpression, int>(dataObject => dataObject.Id); 
            mockImpliesExpressions = new SimplifiedMockDbSet<DataImpliesExpression, int>(dataObject => dataObject.Id); 
            mockPurchaseOrExpressions = new SimplifiedMockDbSet<DataDTOs.Market.StoreManagement.PurchasesPolicy.LogicalOperators.DataOrExpression, int>(dataObject => dataObject.Id);
            mockCheckProductLessPredicates = new SimplifiedMockDbSet<DataCheckProductLessPredicate, int>(dataObject => dataObject.Id);
            mockCheckProductMoreEqualsPredicates = new SimplifiedMockDbSet<DataCheckProductMoreEqualsPredicate, int>(dataObject => dataObject.Id);
            mockPruchasePredicateExpressions = new SimplifiedMockDbSet<DataDTOs.Market.StoreManagement.PurchasesPolicy.PurchasesInterfaces.DataPredicateExpression, int>(dataObject => dataObject.Id);
            mockInterfacesPurchasePolicies = new SimplifiedMockDbSet<DataIPurchasePolicy, int>(dataObject => dataObject.Id); 
            mockRestrictionExpressions = new SimplifiedMockDbSet<DataRestrictionExpression, int>(dataObject => dataObject.Id);
            mockDataAfterHourProductRestrictions = new SimplifiedMockDbSet<DataAfterHourProductRestriction, int>(dataObject => dataObject.Id);
            mockAfterHourRestrictions = new SimplifiedMockDbSet<DataAfterHourRestriction, int>(dataObject => dataObject.Id);
            mockAtLeastAmountRestrictions = new SimplifiedMockDbSet<DataAtLeastAmountRestriction, int>(dataObject => dataObject.Id);
            mockAtMostAmountRestrictions = new SimplifiedMockDbSet<DataAtMostAmountRestriction, int>(dataObject => dataObject.Id);
            mockBeforeHourProductRestrictions = new SimplifiedMockDbSet<DataBeforeHourProductRestriction, int>(dataObject => dataObject.Id);
            mockBeforeHourRestrictions = new SimplifiedMockDbSet<DataBeforeHourRestriction, int>(dataObject => dataObject.Id);
            mockDateRestrictions = new SimplifiedMockDbSet<DataDateRestriction, int>(dataObject => dataObject.Id);
            mockPurchasePolicies = new SimplifiedMockDbSet<DataPurchasePolicy, int>(dataObject => dataObject.Id);
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

        private static SimplifiedMockDbSet<T, U> GetIfSimplifiedMockDbSet<T, U>(SimplifiedDbSet<T, U> elements) where T : class
        {
            if (!(elements is SimplifiedMockDbSet<T, U>))
                throw new Exception("type should be SimplifiedMockDbSet"); 
            return (SimplifiedMockDbSet<T, U>)elements;
        }
    }
}
