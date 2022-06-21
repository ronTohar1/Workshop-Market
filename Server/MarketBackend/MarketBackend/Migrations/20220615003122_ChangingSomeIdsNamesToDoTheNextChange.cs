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

            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Members",
                table: "Members");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Discounts",
                table: "Discounts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bids",
                table: "Bids");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppointmentsNodes",
                table: "AppointmentsNodes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DataShoppingBag",
                table: "DataShoppingBag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DataPurchasePolicy",
                table: "DataPurchasePolicy");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DataPurchase",
                table: "DataPurchase");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DataProductInBag",
                table: "DataProductInBag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DataNotification",
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

            migrationBuilder.RenameColumn(
                name: "FounderId",
                table: "Stores",
                newName: "FounderId2");

            migrationBuilder.RenameColumn(
                name: "AppointmentsId",
                table: "Stores",
                newName: "AppointmentsId2");

            migrationBuilder.RenameIndex(
                name: "IX_Stores_FounderId",
                table: "Stores",
                newName: "IX_Stores_FounderId2");

            migrationBuilder.RenameIndex(
                name: "IX_Stores_AppointmentsId",
                table: "Stores",
                newName: "IX_Stores_AppointmentsId2");

            migrationBuilder.RenameColumn(
                name: "StoreId",
                table: "StoreMemberRoles",
                newName: "StoreId2");

            migrationBuilder.RenameIndex(
                name: "IX_StoreMemberRoles_StoreId",
                table: "StoreMemberRoles",
                newName: "IX_StoreMemberRoles_StoreId2");

            migrationBuilder.RenameColumn(
                name: "DataProductId",
                table: "PurchaseOptions",
                newName: "DataProductId2");

            migrationBuilder.RenameIndex(
                name: "IX_PurchaseOptions_DataProductId",
                table: "PurchaseOptions",
                newName: "IX_PurchaseOptions_DataProductId2");

            migrationBuilder.RenameColumn(
                name: "DataStoreId",
                table: "Products",
                newName: "DataStoreId2");

            migrationBuilder.RenameIndex(
                name: "IX_Products_DataStoreId",
                table: "Products",
                newName: "IX_Products_DataStoreId2");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "ProductReview",
                newName: "ProductId2");

            migrationBuilder.RenameColumn(
                name: "MemberId",
                table: "ProductReview",
                newName: "MemberId2");

            migrationBuilder.RenameIndex(
                name: "IX_ProductReview_ProductId",
                table: "ProductReview",
                newName: "IX_ProductReview_ProductId2");

            migrationBuilder.RenameIndex(
                name: "IX_ProductReview_MemberId",
                table: "ProductReview",
                newName: "IX_ProductReview_MemberId2");

            migrationBuilder.RenameColumn(
                name: "DataBidId",
                table: "Members",
                newName: "DataBidId2");

            migrationBuilder.RenameIndex(
                name: "IX_Members_DataBidId",
                table: "Members",
                newName: "IX_Members_DataBidId2");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "Bids",
                newName: "ProductId2");

            migrationBuilder.RenameColumn(
                name: "MemberId",
                table: "Bids",
                newName: "MemberId2");

            migrationBuilder.RenameColumn(
                name: "DataStoreId",
                table: "Bids",
                newName: "DataStoreId2");

            migrationBuilder.RenameIndex(
                name: "IX_Bids_ProductId",
                table: "Bids",
                newName: "IX_Bids_ProductId2");

            migrationBuilder.RenameIndex(
                name: "IX_Bids_MemberId",
                table: "Bids",
                newName: "IX_Bids_MemberId2");

            migrationBuilder.RenameIndex(
                name: "IX_Bids_DataStoreId",
                table: "Bids",
                newName: "IX_Bids_DataStoreId2");

            migrationBuilder.RenameColumn(
                name: "DataAppointmentsNodeId",
                table: "AppointmentsNodes",
                newName: "DataAppointmentsNodeId2");

            migrationBuilder.RenameIndex(
                name: "IX_AppointmentsNodes_DataAppointmentsNodeId",
                table: "AppointmentsNodes",
                newName: "IX_AppointmentsNodes_DataAppointmentsNodeId2");

            migrationBuilder.RenameColumn(
                name: "StoreId",
                table: "ShoppingBags",
                newName: "StoreId2");

            migrationBuilder.RenameIndex(
                name: "IX_DataShoppingBag_StoreId",
                table: "ShoppingBags",
                newName: "IX_ShoppingBags_StoreId2");

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

            migrationBuilder.RenameColumn(
                name: "DataMemberId",
                table: "Purchases",
                newName: "DataMemberId2");

            migrationBuilder.RenameIndex(
                name: "IX_DataPurchase_DataStore",
                table: "Purchases",
                newName: "IX_Purchases_DataStore");

            migrationBuilder.RenameIndex(
                name: "IX_DataPurchase_DataMemberId",
                table: "Purchases",
                newName: "IX_Purchases_DataMemberId2");

            migrationBuilder.RenameIndex(
                name: "IX_DataProductInBag_DataShoppingBagId",
                table: "ProductInBags",
                newName: "IX_ProductInBags_DataShoppingBagId");

            migrationBuilder.RenameColumn(
                name: "DataMemberId",
                table: "Notifications",
                newName: "DataMemberId2");

            migrationBuilder.RenameIndex(
                name: "IX_DataNotification_DataMemberId",
                table: "Notifications",
                newName: "IX_Notifications_DataMemberId2");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Stores",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Id2",
                table: "Stores",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Products",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Id2",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Members",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Id2",
                table: "Members",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Discounts",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Id2",
                table: "Discounts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Bids",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Id2",
                table: "Bids",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "AppointmentsNodes",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Id2",
                table: "AppointmentsNodes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "PurchasePolicies",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Id2",
                table: "PurchasePolicies",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
                name: "FK_Purchases_Stores_DataStore",
                table: "Purchases",
                column: "DataStore",
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
                name: "FK_Purchases_Stores_DataStore",
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

            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Members",
                table: "Members");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Discounts",
                table: "Discounts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bids",
                table: "Bids");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppointmentsNodes",
                table: "AppointmentsNodes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShoppingBags",
                table: "ShoppingBags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Purchases",
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

            migrationBuilder.DropColumn(
                name: "Id2",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "Id2",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Id2",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "Id2",
                table: "Discounts");

            migrationBuilder.DropColumn(
                name: "Id2",
                table: "Bids");

            migrationBuilder.DropColumn(
                name: "Id2",
                table: "AppointmentsNodes");

            migrationBuilder.DropColumn(
                name: "Id2",
                table: "PurchasePolicies");

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

            migrationBuilder.RenameColumn(
                name: "FounderId2",
                table: "Stores",
                newName: "FounderId");

            migrationBuilder.RenameColumn(
                name: "AppointmentsId2",
                table: "Stores",
                newName: "AppointmentsId");

            migrationBuilder.RenameIndex(
                name: "IX_Stores_FounderId2",
                table: "Stores",
                newName: "IX_Stores_FounderId");

            migrationBuilder.RenameIndex(
                name: "IX_Stores_AppointmentsId2",
                table: "Stores",
                newName: "IX_Stores_AppointmentsId");

            migrationBuilder.RenameColumn(
                name: "StoreId2",
                table: "StoreMemberRoles",
                newName: "StoreId");

            migrationBuilder.RenameIndex(
                name: "IX_StoreMemberRoles_StoreId2",
                table: "StoreMemberRoles",
                newName: "IX_StoreMemberRoles_StoreId");

            migrationBuilder.RenameColumn(
                name: "DataProductId2",
                table: "PurchaseOptions",
                newName: "DataProductId");

            migrationBuilder.RenameIndex(
                name: "IX_PurchaseOptions_DataProductId2",
                table: "PurchaseOptions",
                newName: "IX_PurchaseOptions_DataProductId");

            migrationBuilder.RenameColumn(
                name: "DataStoreId2",
                table: "Products",
                newName: "DataStoreId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_DataStoreId2",
                table: "Products",
                newName: "IX_Products_DataStoreId");

            migrationBuilder.RenameColumn(
                name: "ProductId2",
                table: "ProductReview",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "MemberId2",
                table: "ProductReview",
                newName: "MemberId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductReview_ProductId2",
                table: "ProductReview",
                newName: "IX_ProductReview_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductReview_MemberId2",
                table: "ProductReview",
                newName: "IX_ProductReview_MemberId");

            migrationBuilder.RenameColumn(
                name: "DataBidId2",
                table: "Members",
                newName: "DataBidId");

            migrationBuilder.RenameIndex(
                name: "IX_Members_DataBidId2",
                table: "Members",
                newName: "IX_Members_DataBidId");

            migrationBuilder.RenameColumn(
                name: "ProductId2",
                table: "Bids",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "MemberId2",
                table: "Bids",
                newName: "MemberId");

            migrationBuilder.RenameColumn(
                name: "DataStoreId2",
                table: "Bids",
                newName: "DataStoreId");

            migrationBuilder.RenameIndex(
                name: "IX_Bids_ProductId2",
                table: "Bids",
                newName: "IX_Bids_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Bids_MemberId2",
                table: "Bids",
                newName: "IX_Bids_MemberId");

            migrationBuilder.RenameIndex(
                name: "IX_Bids_DataStoreId2",
                table: "Bids",
                newName: "IX_Bids_DataStoreId");

            migrationBuilder.RenameColumn(
                name: "DataAppointmentsNodeId2",
                table: "AppointmentsNodes",
                newName: "DataAppointmentsNodeId");

            migrationBuilder.RenameIndex(
                name: "IX_AppointmentsNodes_DataAppointmentsNodeId2",
                table: "AppointmentsNodes",
                newName: "IX_AppointmentsNodes_DataAppointmentsNodeId");

            migrationBuilder.RenameColumn(
                name: "StoreId2",
                table: "DataShoppingBag",
                newName: "StoreId");

            migrationBuilder.RenameIndex(
                name: "IX_ShoppingBags_StoreId2",
                table: "DataShoppingBag",
                newName: "IX_DataShoppingBag_StoreId");

            migrationBuilder.RenameIndex(
                name: "IX_ShoppingBags_DataCartId",
                table: "DataShoppingBag",
                newName: "IX_DataShoppingBag_DataCartId");

            migrationBuilder.RenameColumn(
                name: "DataMemberId2",
                table: "DataPurchase",
                newName: "DataMemberId");

            migrationBuilder.RenameIndex(
                name: "IX_Purchases_DataStore",
                table: "DataPurchase",
                newName: "IX_DataPurchase_DataStore");

            migrationBuilder.RenameIndex(
                name: "IX_Purchases_DataMemberId2",
                table: "DataPurchase",
                newName: "IX_DataPurchase_DataMemberId");

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

            migrationBuilder.RenameColumn(
                name: "DataMemberId2",
                table: "DataNotification",
                newName: "DataMemberId");

            migrationBuilder.RenameIndex(
                name: "IX_Notifications_DataMemberId2",
                table: "DataNotification",
                newName: "IX_DataNotification_DataMemberId");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Stores",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Products",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Members",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Discounts",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Bids",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "AppointmentsNodes",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "DataPurchasePolicy",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

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
