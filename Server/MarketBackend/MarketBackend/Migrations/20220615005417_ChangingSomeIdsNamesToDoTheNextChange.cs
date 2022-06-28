using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketBackend.Migrations
{
    public partial class ChangingSomeIdsNamesToDoTheNextChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppointmentsNodes_AppointmentsNodes_DataAppointmentsNodeId",
                table: "AppointmentsNodes");

            migrationBuilder.DropForeignKey(
                name: "FK_Bids_Members_MemberId",
                table: "Bids");

            migrationBuilder.DropForeignKey(
                name: "FK_Bids_Products_ProductId",
                table: "Bids");

            migrationBuilder.DropForeignKey(
                name: "FK_Bids_Stores_DataStoreId",
                table: "Bids");

            migrationBuilder.DropForeignKey(
                name: "FK_DataNotification_Members_DataMemberId",
                table: "DataNotification");

            migrationBuilder.DropForeignKey(
                name: "FK_DataProductInBag_DataShoppingBag_DataShoppingBagId",
                table: "DataProductInBag");

            migrationBuilder.DropForeignKey(
                name: "FK_DataPurchase_Members_DataMemberId",
                table: "DataPurchase");

            migrationBuilder.DropForeignKey(
                name: "FK_DataPurchase_Stores_DataStore",
                table: "DataPurchase");

            migrationBuilder.DropForeignKey(
                name: "FK_DataPurchasePolicy_DataStorePurchasePolicyManager_DataStorePurchasePolicyManagerId",
                table: "DataPurchasePolicy");

            migrationBuilder.DropForeignKey(
                name: "FK_DataPurchasePolicy_InterfacesPurchasePolicies_PolicyId",
                table: "DataPurchasePolicy");

            migrationBuilder.DropForeignKey(
                name: "FK_DataShoppingBag_Carts_DataCartId",
                table: "DataShoppingBag");

            migrationBuilder.DropForeignKey(
                name: "FK_DataShoppingBag_Stores_StoreId",
                table: "DataShoppingBag");

            migrationBuilder.DropForeignKey(
                name: "FK_Members_Bids_DataBidId",
                table: "Members");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductReview_Members_MemberId",
                table: "ProductReview");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductReview_Products_ProductId",
                table: "ProductReview");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Stores_DataStoreId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOptions_Products_DataProductId",
                table: "PurchaseOptions");

            migrationBuilder.DropForeignKey(
                name: "FK_StoreMemberRoles_Stores_StoreId",
                table: "StoreMemberRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_Stores_AppointmentsNodes_AppointmentsId",
                table: "Stores");

            migrationBuilder.DropForeignKey(
                name: "FK_Stores_Members_FounderId",
                table: "Stores");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Stores",
                table: "Stores");

            migrationBuilder.DropIndex(
                name: "IX_Stores_AppointmentsId",
                table: "Stores");

            migrationBuilder.DropIndex(
                name: "IX_Stores_FounderId",
                table: "Stores");

            migrationBuilder.DropIndex(
                name: "IX_StoreMemberRoles_StoreId",
                table: "StoreMemberRoles");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseOptions_DataProductId",
                table: "PurchaseOptions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_DataStoreId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_ProductReview_MemberId",
                table: "ProductReview");

            migrationBuilder.DropIndex(
                name: "IX_ProductReview_ProductId",
                table: "ProductReview");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Members",
                table: "Members");

            migrationBuilder.DropIndex(
                name: "IX_Members_DataBidId",
                table: "Members");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Discounts",
                table: "Discounts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bids",
                table: "Bids");

            migrationBuilder.DropIndex(
                name: "IX_Bids_DataStoreId",
                table: "Bids");

            migrationBuilder.DropIndex(
                name: "IX_Bids_MemberId",
                table: "Bids");

            migrationBuilder.DropIndex(
                name: "IX_Bids_ProductId",
                table: "Bids");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppointmentsNodes",
                table: "AppointmentsNodes");

            migrationBuilder.DropIndex(
                name: "IX_AppointmentsNodes_DataAppointmentsNodeId",
                table: "AppointmentsNodes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DataShoppingBag",
                table: "DataShoppingBag");

            migrationBuilder.DropIndex(
                name: "IX_DataShoppingBag_StoreId",
                table: "DataShoppingBag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DataPurchasePolicy",
                table: "DataPurchasePolicy");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DataPurchase",
                table: "DataPurchase");

            migrationBuilder.DropIndex(
                name: "IX_DataPurchase_DataMemberId",
                table: "DataPurchase");

            migrationBuilder.DropIndex(
                name: "IX_DataPurchase_DataStore",
                table: "DataPurchase");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DataProductInBag",
                table: "DataProductInBag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DataNotification",
                table: "DataNotification");

            migrationBuilder.DropIndex(
                name: "IX_DataNotification_DataMemberId",
                table: "DataNotification");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "AppointmentsId",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "FounderId",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "StoreId",
                table: "StoreMemberRoles");

            migrationBuilder.DropColumn(
                name: "DataProductId",
                table: "PurchaseOptions");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "DataStoreId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "MemberId",
                table: "ProductReview");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "ProductReview");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "DataBidId",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Discounts");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Bids");

            migrationBuilder.DropColumn(
                name: "DataStoreId",
                table: "Bids");

            migrationBuilder.DropColumn(
                name: "MemberId",
                table: "Bids");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Bids");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "AppointmentsNodes");

            migrationBuilder.DropColumn(
                name: "DataAppointmentsNodeId",
                table: "AppointmentsNodes");

            migrationBuilder.DropColumn(
                name: "StoreId",
                table: "DataShoppingBag");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "DataPurchasePolicy");

            migrationBuilder.DropColumn(
                name: "DataMemberId",
                table: "DataPurchase");

            migrationBuilder.DropColumn(
                name: "DataStore",
                table: "DataPurchase");

            migrationBuilder.DropColumn(
                name: "DataMemberId",
                table: "DataNotification");

            migrationBuilder.RenameTable(
                name: "DataShoppingBag",
                newName: "ShoppingBags");

            migrationBuilder.RenameTable(
                name: "DataPurchasePolicy",
                newName: "PurchasePolicies");

            migrationBuilder.RenameTable(
                name: "DataPurchase",
                newName: "Purchases");

            migrationBuilder.RenameTable(
                name: "DataProductInBag",
                newName: "ProductInBags");

            migrationBuilder.RenameTable(
                name: "DataNotification",
                newName: "Notifications");

            migrationBuilder.RenameIndex(
                name: "IX_DataShoppingBag_DataCartId",
                table: "ShoppingBags",
                newName: "IX_ShoppingBags_DataCartId");

            migrationBuilder.RenameIndex(
                name: "IX_DataPurchasePolicy_PolicyId",
                table: "PurchasePolicies",
                newName: "IX_PurchasePolicies_PolicyId");

            migrationBuilder.RenameIndex(
                name: "IX_DataPurchasePolicy_DataStorePurchasePolicyManagerId",
                table: "PurchasePolicies",
                newName: "IX_PurchasePolicies_DataStorePurchasePolicyManagerId");

            migrationBuilder.RenameIndex(
                name: "IX_DataProductInBag_DataShoppingBagId",
                table: "ProductInBags",
                newName: "IX_ProductInBags_DataShoppingBagId");

            migrationBuilder.AddColumn<string>(
                name: "Id2",
                table: "Stores",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AppointmentsId2",
                table: "Stores",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FounderId2",
                table: "Stores",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StoreId2",
                table: "StoreMemberRoles",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DataProductId2",
                table: "PurchaseOptions",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Id2",
                table: "Products",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DataStoreId2",
                table: "Products",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MemberId2",
                table: "ProductReview",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductId2",
                table: "ProductReview",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Id2",
                table: "Members",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DataBidId2",
                table: "Members",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Id2",
                table: "Discounts",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Id2",
                table: "Bids",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DataStoreId2",
                table: "Bids",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MemberId2",
                table: "Bids",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductId2",
                table: "Bids",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Id2",
                table: "AppointmentsNodes",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DataAppointmentsNodeId2",
                table: "AppointmentsNodes",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StoreId2",
                table: "ShoppingBags",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Id2",
                table: "PurchasePolicies",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DataMemberId2",
                table: "Purchases",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StoreId2",
                table: "Purchases",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DataMemberId2",
                table: "Notifications",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Stores",
                table: "Stores",
                column: "Id2");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                table: "Products",
                column: "Id2");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Members",
                table: "Members",
                column: "Id2");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Discounts",
                table: "Discounts",
                column: "Id2");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bids",
                table: "Bids",
                column: "Id2");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppointmentsNodes",
                table: "AppointmentsNodes",
                column: "Id2");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShoppingBags",
                table: "ShoppingBags",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PurchasePolicies",
                table: "PurchasePolicies",
                column: "Id2");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Purchases",
                table: "Purchases",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductInBags",
                table: "ProductInBags",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Notifications",
                table: "Notifications",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Stores_AppointmentsId2",
                table: "Stores",
                column: "AppointmentsId2");

            migrationBuilder.CreateIndex(
                name: "IX_Stores_FounderId2",
                table: "Stores",
                column: "FounderId2");

            migrationBuilder.CreateIndex(
                name: "IX_StoreMemberRoles_StoreId2",
                table: "StoreMemberRoles",
                column: "StoreId2");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOptions_DataProductId2",
                table: "PurchaseOptions",
                column: "DataProductId2");

            migrationBuilder.CreateIndex(
                name: "IX_Products_DataStoreId2",
                table: "Products",
                column: "DataStoreId2");

            migrationBuilder.CreateIndex(
                name: "IX_ProductReview_MemberId2",
                table: "ProductReview",
                column: "MemberId2");

            migrationBuilder.CreateIndex(
                name: "IX_ProductReview_ProductId2",
                table: "ProductReview",
                column: "ProductId2");

            migrationBuilder.CreateIndex(
                name: "IX_Members_DataBidId2",
                table: "Members",
                column: "DataBidId2");

            migrationBuilder.CreateIndex(
                name: "IX_Bids_DataStoreId2",
                table: "Bids",
                column: "DataStoreId2");

            migrationBuilder.CreateIndex(
                name: "IX_Bids_MemberId2",
                table: "Bids",
                column: "MemberId2");

            migrationBuilder.CreateIndex(
                name: "IX_Bids_ProductId2",
                table: "Bids",
                column: "ProductId2");

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentsNodes_DataAppointmentsNodeId2",
                table: "AppointmentsNodes",
                column: "DataAppointmentsNodeId2");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingBags_StoreId2",
                table: "ShoppingBags",
                column: "StoreId2");

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_DataMemberId2",
                table: "Purchases",
                column: "DataMemberId2");

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_StoreId2",
                table: "Purchases",
                column: "StoreId2");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_DataMemberId2",
                table: "Notifications",
                column: "DataMemberId2");

            migrationBuilder.AddForeignKey(
                name: "FK_AppointmentsNodes_AppointmentsNodes_DataAppointmentsNodeId2",
                table: "AppointmentsNodes",
                column: "DataAppointmentsNodeId2",
                principalTable: "AppointmentsNodes",
                principalColumn: "Id2");

            migrationBuilder.AddForeignKey(
                name: "FK_Bids_Members_MemberId2",
                table: "Bids",
                column: "MemberId2",
                principalTable: "Members",
                principalColumn: "Id2");

            migrationBuilder.AddForeignKey(
                name: "FK_Bids_Products_ProductId2",
                table: "Bids",
                column: "ProductId2",
                principalTable: "Products",
                principalColumn: "Id2");

            migrationBuilder.AddForeignKey(
                name: "FK_Bids_Stores_DataStoreId2",
                table: "Bids",
                column: "DataStoreId2",
                principalTable: "Stores",
                principalColumn: "Id2");

            migrationBuilder.AddForeignKey(
                name: "FK_Members_Bids_DataBidId2",
                table: "Members",
                column: "DataBidId2",
                principalTable: "Bids",
                principalColumn: "Id2");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Members_DataMemberId2",
                table: "Notifications",
                column: "DataMemberId2",
                principalTable: "Members",
                principalColumn: "Id2");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductInBags_ShoppingBags_DataShoppingBagId",
                table: "ProductInBags",
                column: "DataShoppingBagId",
                principalTable: "ShoppingBags",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductReview_Members_MemberId2",
                table: "ProductReview",
                column: "MemberId2",
                principalTable: "Members",
                principalColumn: "Id2");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductReview_Products_ProductId2",
                table: "ProductReview",
                column: "ProductId2",
                principalTable: "Products",
                principalColumn: "Id2");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Stores_DataStoreId2",
                table: "Products",
                column: "DataStoreId2",
                principalTable: "Stores",
                principalColumn: "Id2");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOptions_Products_DataProductId2",
                table: "PurchaseOptions",
                column: "DataProductId2",
                principalTable: "Products",
                principalColumn: "Id2");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchasePolicies_DataStorePurchasePolicyManager_DataStorePurchasePolicyManagerId",
                table: "PurchasePolicies",
                column: "DataStorePurchasePolicyManagerId",
                principalTable: "DataStorePurchasePolicyManager",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchasePolicies_InterfacesPurchasePolicies_PolicyId",
                table: "PurchasePolicies",
                column: "PolicyId",
                principalTable: "InterfacesPurchasePolicies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_Members_DataMemberId2",
                table: "Purchases",
                column: "DataMemberId2",
                principalTable: "Members",
                principalColumn: "Id2");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_Stores_StoreId2",
                table: "Purchases",
                column: "StoreId2",
                principalTable: "Stores",
                principalColumn: "Id2");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingBags_Carts_DataCartId",
                table: "ShoppingBags",
                column: "DataCartId",
                principalTable: "Carts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingBags_Stores_StoreId2",
                table: "ShoppingBags",
                column: "StoreId2",
                principalTable: "Stores",
                principalColumn: "Id2");

            migrationBuilder.AddForeignKey(
                name: "FK_StoreMemberRoles_Stores_StoreId2",
                table: "StoreMemberRoles",
                column: "StoreId2",
                principalTable: "Stores",
                principalColumn: "Id2");

            migrationBuilder.AddForeignKey(
                name: "FK_Stores_AppointmentsNodes_AppointmentsId2",
                table: "Stores",
                column: "AppointmentsId2",
                principalTable: "AppointmentsNodes",
                principalColumn: "Id2");

            migrationBuilder.AddForeignKey(
                name: "FK_Stores_Members_FounderId2",
                table: "Stores",
                column: "FounderId2",
                principalTable: "Members",
                principalColumn: "Id2");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppointmentsNodes_AppointmentsNodes_DataAppointmentsNodeId2",
                table: "AppointmentsNodes");

            migrationBuilder.DropForeignKey(
                name: "FK_Bids_Members_MemberId2",
                table: "Bids");

            migrationBuilder.DropForeignKey(
                name: "FK_Bids_Products_ProductId2",
                table: "Bids");

            migrationBuilder.DropForeignKey(
                name: "FK_Bids_Stores_DataStoreId2",
                table: "Bids");

            migrationBuilder.DropForeignKey(
                name: "FK_Members_Bids_DataBidId2",
                table: "Members");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Members_DataMemberId2",
                table: "Notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductInBags_ShoppingBags_DataShoppingBagId",
                table: "ProductInBags");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductReview_Members_MemberId2",
                table: "ProductReview");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductReview_Products_ProductId2",
                table: "ProductReview");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Stores_DataStoreId2",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOptions_Products_DataProductId2",
                table: "PurchaseOptions");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchasePolicies_DataStorePurchasePolicyManager_DataStorePurchasePolicyManagerId",
                table: "PurchasePolicies");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchasePolicies_InterfacesPurchasePolicies_PolicyId",
                table: "PurchasePolicies");

            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_Members_DataMemberId2",
                table: "Purchases");

            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_Stores_StoreId2",
                table: "Purchases");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingBags_Carts_DataCartId",
                table: "ShoppingBags");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingBags_Stores_StoreId2",
                table: "ShoppingBags");

            migrationBuilder.DropForeignKey(
                name: "FK_StoreMemberRoles_Stores_StoreId2",
                table: "StoreMemberRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_Stores_AppointmentsNodes_AppointmentsId2",
                table: "Stores");

            migrationBuilder.DropForeignKey(
                name: "FK_Stores_Members_FounderId2",
                table: "Stores");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Stores",
                table: "Stores");

            migrationBuilder.DropIndex(
                name: "IX_Stores_AppointmentsId2",
                table: "Stores");

            migrationBuilder.DropIndex(
                name: "IX_Stores_FounderId2",
                table: "Stores");

            migrationBuilder.DropIndex(
                name: "IX_StoreMemberRoles_StoreId2",
                table: "StoreMemberRoles");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseOptions_DataProductId2",
                table: "PurchaseOptions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_DataStoreId2",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_ProductReview_MemberId2",
                table: "ProductReview");

            migrationBuilder.DropIndex(
                name: "IX_ProductReview_ProductId2",
                table: "ProductReview");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Members",
                table: "Members");

            migrationBuilder.DropIndex(
                name: "IX_Members_DataBidId2",
                table: "Members");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Discounts",
                table: "Discounts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bids",
                table: "Bids");

            migrationBuilder.DropIndex(
                name: "IX_Bids_DataStoreId2",
                table: "Bids");

            migrationBuilder.DropIndex(
                name: "IX_Bids_MemberId2",
                table: "Bids");

            migrationBuilder.DropIndex(
                name: "IX_Bids_ProductId2",
                table: "Bids");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppointmentsNodes",
                table: "AppointmentsNodes");

            migrationBuilder.DropIndex(
                name: "IX_AppointmentsNodes_DataAppointmentsNodeId2",
                table: "AppointmentsNodes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShoppingBags",
                table: "ShoppingBags");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingBags_StoreId2",
                table: "ShoppingBags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Purchases",
                table: "Purchases");

            migrationBuilder.DropIndex(
                name: "IX_Purchases_DataMemberId2",
                table: "Purchases");

            migrationBuilder.DropIndex(
                name: "IX_Purchases_StoreId2",
                table: "Purchases");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PurchasePolicies",
                table: "PurchasePolicies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductInBags",
                table: "ProductInBags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Notifications",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_DataMemberId2",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "Id2",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "AppointmentsId2",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "FounderId2",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "StoreId2",
                table: "StoreMemberRoles");

            migrationBuilder.DropColumn(
                name: "DataProductId2",
                table: "PurchaseOptions");

            migrationBuilder.DropColumn(
                name: "Id2",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "DataStoreId2",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "MemberId2",
                table: "ProductReview");

            migrationBuilder.DropColumn(
                name: "ProductId2",
                table: "ProductReview");

            migrationBuilder.DropColumn(
                name: "Id2",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "DataBidId2",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "Id2",
                table: "Discounts");

            migrationBuilder.DropColumn(
                name: "Id2",
                table: "Bids");

            migrationBuilder.DropColumn(
                name: "DataStoreId2",
                table: "Bids");

            migrationBuilder.DropColumn(
                name: "MemberId2",
                table: "Bids");

            migrationBuilder.DropColumn(
                name: "ProductId2",
                table: "Bids");

            migrationBuilder.DropColumn(
                name: "Id2",
                table: "AppointmentsNodes");

            migrationBuilder.DropColumn(
                name: "DataAppointmentsNodeId2",
                table: "AppointmentsNodes");

            migrationBuilder.DropColumn(
                name: "StoreId2",
                table: "ShoppingBags");

            migrationBuilder.DropColumn(
                name: "DataMemberId2",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "StoreId2",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "Id2",
                table: "PurchasePolicies");

            migrationBuilder.DropColumn(
                name: "DataMemberId2",
                table: "Notifications");

            migrationBuilder.RenameTable(
                name: "ShoppingBags",
                newName: "DataShoppingBag");

            migrationBuilder.RenameTable(
                name: "Purchases",
                newName: "DataPurchase");

            migrationBuilder.RenameTable(
                name: "PurchasePolicies",
                newName: "DataPurchasePolicy");

            migrationBuilder.RenameTable(
                name: "ProductInBags",
                newName: "DataProductInBag");

            migrationBuilder.RenameTable(
                name: "Notifications",
                newName: "DataNotification");

            migrationBuilder.RenameIndex(
                name: "IX_ShoppingBags_DataCartId",
                table: "DataShoppingBag",
                newName: "IX_DataShoppingBag_DataCartId");

            migrationBuilder.RenameIndex(
                name: "IX_PurchasePolicies_PolicyId",
                table: "DataPurchasePolicy",
                newName: "IX_DataPurchasePolicy_PolicyId");

            migrationBuilder.RenameIndex(
                name: "IX_PurchasePolicies_DataStorePurchasePolicyManagerId",
                table: "DataPurchasePolicy",
                newName: "IX_DataPurchasePolicy_DataStorePurchasePolicyManagerId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductInBags_DataShoppingBagId",
                table: "DataProductInBag",
                newName: "IX_DataProductInBag_DataShoppingBagId");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Stores",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "AppointmentsId",
                table: "Stores",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FounderId",
                table: "Stores",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StoreId",
                table: "StoreMemberRoles",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DataProductId",
                table: "PurchaseOptions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "DataStoreId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MemberId",
                table: "ProductReview",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "ProductReview",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Members",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "DataBidId",
                table: "Members",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Discounts",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Bids",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "DataStoreId",
                table: "Bids",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MemberId",
                table: "Bids",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Bids",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "AppointmentsNodes",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "DataAppointmentsNodeId",
                table: "AppointmentsNodes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StoreId",
                table: "DataShoppingBag",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DataMemberId",
                table: "DataPurchase",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DataStore",
                table: "DataPurchase",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "DataPurchasePolicy",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "DataMemberId",
                table: "DataNotification",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Stores",
                table: "Stores",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                table: "Products",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Members",
                table: "Members",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Discounts",
                table: "Discounts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bids",
                table: "Bids",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppointmentsNodes",
                table: "AppointmentsNodes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DataShoppingBag",
                table: "DataShoppingBag",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DataPurchase",
                table: "DataPurchase",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DataPurchasePolicy",
                table: "DataPurchasePolicy",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DataProductInBag",
                table: "DataProductInBag",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DataNotification",
                table: "DataNotification",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Stores_AppointmentsId",
                table: "Stores",
                column: "AppointmentsId");

            migrationBuilder.CreateIndex(
                name: "IX_Stores_FounderId",
                table: "Stores",
                column: "FounderId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreMemberRoles_StoreId",
                table: "StoreMemberRoles",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOptions_DataProductId",
                table: "PurchaseOptions",
                column: "DataProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_DataStoreId",
                table: "Products",
                column: "DataStoreId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductReview_MemberId",
                table: "ProductReview",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductReview_ProductId",
                table: "ProductReview",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Members_DataBidId",
                table: "Members",
                column: "DataBidId");

            migrationBuilder.CreateIndex(
                name: "IX_Bids_DataStoreId",
                table: "Bids",
                column: "DataStoreId");

            migrationBuilder.CreateIndex(
                name: "IX_Bids_MemberId",
                table: "Bids",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Bids_ProductId",
                table: "Bids",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentsNodes_DataAppointmentsNodeId",
                table: "AppointmentsNodes",
                column: "DataAppointmentsNodeId");

            migrationBuilder.CreateIndex(
                name: "IX_DataShoppingBag_StoreId",
                table: "DataShoppingBag",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_DataPurchase_DataMemberId",
                table: "DataPurchase",
                column: "DataMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_DataPurchase_DataStore",
                table: "DataPurchase",
                column: "DataStore");

            migrationBuilder.CreateIndex(
                name: "IX_DataNotification_DataMemberId",
                table: "DataNotification",
                column: "DataMemberId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppointmentsNodes_AppointmentsNodes_DataAppointmentsNodeId",
                table: "AppointmentsNodes",
                column: "DataAppointmentsNodeId",
                principalTable: "AppointmentsNodes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bids_Members_MemberId",
                table: "Bids",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bids_Products_ProductId",
                table: "Bids",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bids_Stores_DataStoreId",
                table: "Bids",
                column: "DataStoreId",
                principalTable: "Stores",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DataNotification_Members_DataMemberId",
                table: "DataNotification",
                column: "DataMemberId",
                principalTable: "Members",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DataProductInBag_DataShoppingBag_DataShoppingBagId",
                table: "DataProductInBag",
                column: "DataShoppingBagId",
                principalTable: "DataShoppingBag",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DataPurchase_Members_DataMemberId",
                table: "DataPurchase",
                column: "DataMemberId",
                principalTable: "Members",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DataPurchase_Stores_DataStore",
                table: "DataPurchase",
                column: "DataStore",
                principalTable: "Stores",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DataPurchasePolicy_DataStorePurchasePolicyManager_DataStorePurchasePolicyManagerId",
                table: "DataPurchasePolicy",
                column: "DataStorePurchasePolicyManagerId",
                principalTable: "DataStorePurchasePolicyManager",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DataPurchasePolicy_InterfacesPurchasePolicies_PolicyId",
                table: "DataPurchasePolicy",
                column: "PolicyId",
                principalTable: "InterfacesPurchasePolicies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DataShoppingBag_Carts_DataCartId",
                table: "DataShoppingBag",
                column: "DataCartId",
                principalTable: "Carts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DataShoppingBag_Stores_StoreId",
                table: "DataShoppingBag",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Members_Bids_DataBidId",
                table: "Members",
                column: "DataBidId",
                principalTable: "Bids",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductReview_Members_MemberId",
                table: "ProductReview",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductReview_Products_ProductId",
                table: "ProductReview",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Stores_DataStoreId",
                table: "Products",
                column: "DataStoreId",
                principalTable: "Stores",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOptions_Products_DataProductId",
                table: "PurchaseOptions",
                column: "DataProductId",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StoreMemberRoles_Stores_StoreId",
                table: "StoreMemberRoles",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Stores_AppointmentsNodes_AppointmentsId",
                table: "Stores",
                column: "AppointmentsId",
                principalTable: "AppointmentsNodes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Stores_Members_FounderId",
                table: "Stores",
                column: "FounderId",
                principalTable: "Members",
                principalColumn: "Id");
        }
    }
}
