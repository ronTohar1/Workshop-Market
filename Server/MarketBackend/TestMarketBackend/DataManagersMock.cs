using MarketBackend.DataLayer.DataDTOs;
using MarketBackend.DataLayer.DataDTOs.Buyers;
using MarketBackend.DataLayer.DataDTOs.Buyers.Carts;
using MarketBackend.DataLayer.DataDTOs.Market;
using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement;
using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement.DiscountPolicy;
using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement.PurchasesPolicy;
using MarketBackend.DataLayer.DataManagementObjects;
using MarketBackend.DataLayer.DataManagers;
using MarketBackend.DataLayer.DataManagers.DataManagersInherentsForTesting;
using Moq;
using System;

namespace TestMarketBackend
{
    public class DataManagersMock
    {
        public static void InitMockDataManagers()
        {
            // Cart + misc
            Mock<ForTestingCartDataManager> cart = new Mock<ForTestingCartDataManager>();
            Mock<ForTestingShoppingBagDataManager> shoppingBag = new Mock<ForTestingShoppingBagDataManager>();
            Mock<ForTestingProductInBagDataManager> productInBag = new Mock<ForTestingProductInBagDataManager>();

            // Member
            Mock<ForTestingMemberDataManager> member = new Mock<ForTestingMemberDataManager>();
            
            // Store + misc
            Mock<ForTestingStoreDataManager> store = new Mock<ForTestingStoreDataManager>();
            Mock<ForTestingBidDataManager> bid = new Mock<ForTestingBidDataManager>();
            Mock<ForTestingDiscountDataManager> discount = new Mock<ForTestingDiscountDataManager>();
            Mock<ForTestingHierarchyDataManager> hierarchy = new Mock<ForTestingHierarchyDataManager>();
            Mock<ForTestingManagerPermissionDataManager> managerPermission = new Mock<ForTestingManagerPermissionDataManager>();
            Mock<ForTestingStoreMemberRolesDataManager> storeMemberRoles = new Mock<ForTestingStoreMemberRolesDataManager>();
            
            // Products + misc
            Mock<ForTestingProductDataManager> product = new Mock<ForTestingProductDataManager>();
            Mock<ForTestingProductReviewDataManager> productReview = new Mock<ForTestingProductReviewDataManager>();

            //Purchase + misc
            Mock<ForTestingPurchaseDataManager> purchase = new Mock<ForTestingPurchaseDataManager>();
            Mock<ForTestingPurchaseOptionsDataManager> purchaseOption = new Mock<ForTestingPurchaseOptionsDataManager>();
            Mock<ForTestingPurchasePolicyDataManager> purchasePolicy = new Mock<ForTestingPurchasePolicyDataManager>();


            // Cart
            cart.Setup(x => x.Add(It.IsAny<DataCart>()));
            cart.Setup(x => x.Remove(It.IsAny<int>()));
            cart.Setup(x => x.Update(It.IsAny<int>(), It.IsAny<Action<DataCart>>()));
            cart.Setup(x => x.Find(It.IsAny<int>())).Returns((DataCart)null);
            cart.Setup(x => x.Save());
            CartDataManager.ForTestingSetInstance(cart.Object);

            // Shopping Bag
            shoppingBag.Setup(x => x.Add(It.IsAny<DataShoppingBag>()));
            shoppingBag.Setup(x => x.Remove(It.IsAny<int>()));
            shoppingBag.Setup(x => x.Update(It.IsAny<int>(), It.IsAny<Action<DataShoppingBag>>()));
            shoppingBag.Setup(x => x.Find(It.IsAny<int>())).Returns((DataShoppingBag)null);
            shoppingBag.Setup(x => x.Save());
            ShoppingBagDataManager.ForTestingSetInstance(shoppingBag.Object);

            // Product In Bag
            productInBag.Setup(x => x.Add(It.IsAny<DataProductInBag>()));
            productInBag.Setup(x => x.Remove(It.IsAny<int>()));
            productInBag.Setup(x => x.Update(It.IsAny<int>(), It.IsAny<Action<DataProductInBag>>()));
            productInBag.Setup(x => x.Find(It.IsAny<int>())).Returns((DataProductInBag)null);
            productInBag.Setup(x => x.Save());
            ProductInBagDataManager.ForTestingSetInstance(productInBag.Object);

            // Member
            member.Setup(x => x.Add(It.IsAny<DataMember>()));
            member.Setup(x => x.Remove(It.IsAny<int>()));
            member.Setup(x => x.Update(It.IsAny<int>(), It.IsAny<Action<DataMember>>()));
            member.Setup(x => x.Find(It.IsAny<int>())).Returns((DataMember)null);
            member.Setup(x => x.GetNextId()).Returns(0);
            member.Setup(x => x.Save());
            MemberDataManager.ForTestingSetInstance(member.Object);

            // Store
            store.Setup(x => x.Add(It.IsAny<DataStore>()));
            store.Setup(x => x.Remove(It.IsAny<int>()));
            store.Setup(x => x.Update(It.IsAny<int>(), It.IsAny<Action<DataStore>>()));
            store.Setup(x => x.Find(It.IsAny<int>())).Returns(new DataStore());
            store.Setup(x => x.Save());
            StoreDataManager.ForTestingSetInstance(store.Object);

            // Bid
            bid.Setup(x => x.Add(It.IsAny<DataBid>()));
            bid.Setup(x => x.Remove(It.IsAny<int>()));
            bid.Setup(x => x.Update(It.IsAny<int>(), It.IsAny<Action<DataBid>>()));
            bid.Setup(x => x.Find(It.IsAny<int>())).Returns(new DataBid());
            bid.Setup(x => x.Save());
            BidDataManager.ForTestingSetInstance(bid.Object);

            // Discount
            discount.Setup(x => x.Add(It.IsAny<DataDiscount>()));
            discount.Setup(x => x.Remove(It.IsAny<int>()));
            discount.Setup(x => x.Update(It.IsAny<int>(), It.IsAny<Action<DataDiscount>>()));
            discount.Setup(x => x.Find(It.IsAny<int>())).Returns(new DataDiscount());
            discount.Setup(x => x.Save());
            DiscountDataManager.ForTestingSetInstance(discount.Object);

            // Hierarchy
            hierarchy.Setup(x => x.Add(It.IsAny<DataAppointmentsNode>()));
            hierarchy.Setup(x => x.Remove(It.IsAny<int>()));
            hierarchy.Setup(x => x.Update(It.IsAny<int>(), It.IsAny<Action<DataAppointmentsNode>>()));
            hierarchy.Setup(x => x.Find(It.IsAny<int>())).Returns(new DataAppointmentsNode());
            hierarchy.Setup(x => x.Save());
            HierarchyDataManager.ForTestingSetInstance(hierarchy.Object);

            // Store member roles
            storeMemberRoles.Setup(x => x.Add(It.IsAny<DataStoreMemberRoles>()));
            storeMemberRoles.Setup(x => x.Remove(It.IsAny<int>()));
            storeMemberRoles.Setup(x => x.Update(It.IsAny<int>(), It.IsAny<Action<DataStoreMemberRoles>>()));
            storeMemberRoles.Setup(x => x.Find(It.IsAny<int>())).Returns(new DataStoreMemberRoles());
            storeMemberRoles.Setup(x => x.Save());
            StoreMemberRolesDataManager.ForTestingSetInstance(storeMemberRoles.Object);

            // Manager permission
            managerPermission.Setup(x => x.Add(It.IsAny<DataManagerPermission>()));
            managerPermission.Setup(x => x.Remove(It.IsAny<int>()));
            managerPermission.Setup(x => x.Update(It.IsAny<int>(), It.IsAny<Action<DataManagerPermission>>()));
            managerPermission.Setup(x => x.Find(It.IsAny<int>())).Returns(new DataManagerPermission());
            managerPermission.Setup(x => x.Save());
            ManagerPermissionDataManager.ForTestingSetInstance(managerPermission.Object);

            // Product
            product.Setup(x => x.Add(It.IsAny<DataProduct>()));
            product.Setup(x => x.Remove(It.IsAny<int>()));
            product.Setup(x => x.Update(It.IsAny<int>(), It.IsAny<Action<DataProduct>>()));
            product.Setup(x => x.Find(It.IsAny<int>())).Returns(new DataProduct());
            product.Setup(x => x.Save());
            ProductDataManager.ForTestingSetInstance(product.Object);

            // Product review
            productReview.Setup(x => x.Add(It.IsAny<DataProductReview>()));
            productReview.Setup(x => x.Remove(It.IsAny<int>()));
            productReview.Setup(x => x.Update(It.IsAny<int>(), It.IsAny<Action<DataProductReview>>()));
            productReview.Setup(x => x.Find(It.IsAny<int>())).Returns(new DataProductReview());
            productReview.Setup(x => x.Save());
            ProductReviewDataManager.ForTestingSetInstance(productReview.Object);

            // Purchase
            purchase.Setup(x => x.Add(It.IsAny<DataPurchase>()));
            purchase.Setup(x => x.Remove(It.IsAny<int>()));
            purchase.Setup(x => x.Update(It.IsAny<int>(), It.IsAny<Action<DataPurchase>>()));
            purchase.Setup(x => x.Find(It.IsAny<int>())).Returns(new DataPurchase());
            purchase.Setup(x => x.Save());
            PurchaseDataManager.ForTestingSetInstance(purchase.Object);

            // Purchase option
            purchaseOption.Setup(x => x.Add(It.IsAny<DataPurchaseOption>()));
            purchaseOption.Setup(x => x.Remove(It.IsAny<int>()));
            purchaseOption.Setup(x => x.Update(It.IsAny<int>(), It.IsAny<Action<DataPurchaseOption>>()));
            purchaseOption.Setup(x => x.Find(It.IsAny<int>())).Returns(new DataPurchaseOption());
            purchaseOption.Setup(x => x.Save());
            PurchaseOptionsDataManager.ForTestingSetInstance(purchaseOption.Object);

            // Purchase policy
            purchasePolicy.Setup(x => x.Add(It.IsAny<DataPurchasePolicy>()));
            purchasePolicy.Setup(x => x.Remove(It.IsAny<int>()));
            purchasePolicy.Setup(x => x.Update(It.IsAny<int>(), It.IsAny<Action<DataPurchasePolicy>>()));
            purchasePolicy.Setup(x => x.Find(It.IsAny<int>())).Returns(new DataPurchasePolicy());
            purchasePolicy.Setup(x => x.Save());
            PurchasePolicyDataManager.ForTestingSetInstance(purchasePolicy.Object);
        }
    }
}
