using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketBackend.Migrations
{
    public partial class ChangingSomeIdsToNotAutoIncrement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "FK_Purchases_Members_DataMemberId2",
                table: "Purchases");

            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_Stores_StoreId2",
                table: "Purchases");

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
                name: "IX_ShoppingBags_StoreId2",
                table: "ShoppingBags");

            migrationBuilder.DropIndex(
                name: "IX_Purchases_DataMemberId2",
                table: "Purchases");

            migrationBuilder.DropIndex(
                name: "IX_Purchases_StoreId2",
                table: "Purchases");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PurchasePolicies",
                table: "PurchasePolicies");

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

            migrationBuilder.DropIndex(
                name: "IX_Notifications_DataMemberId2",
                table: "Notifications");

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
                name: "DataMemberId2",
                table: "Notifications");

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

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Stores",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
                name: "StoreId",
                table: "ShoppingBags",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DataMemberId",
                table: "Purchases",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DataStore",
                table: "Purchases",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "PurchasePolicies",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
                defaultValue: 0);

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
                name: "DataMemberId",
                table: "Notifications",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Members",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Bids",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DataAppointmentsNodeId",
                table: "AppointmentsNodes",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Stores",
                table: "Stores",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PurchasePolicies",
                table: "PurchasePolicies",
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
                name: "IX_ShoppingBags_StoreId",
                table: "ShoppingBags",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_DataMemberId",
                table: "Purchases",
                column: "DataMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_DataStore",
                table: "Purchases",
                column: "DataStore");

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
                name: "IX_Notifications_DataMemberId",
                table: "Notifications",
                column: "DataMemberId");

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
                name: "FK_Members_Bids_DataBidId",
                table: "Members",
                column: "DataBidId",
                principalTable: "Bids",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Members_DataMemberId",
                table: "Notifications",
                column: "DataMemberId",
                principalTable: "Members",
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
                name: "FK_Purchases_Members_DataMemberId",
                table: "Purchases",
                column: "DataMemberId",
                principalTable: "Members",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_Stores_DataStore",
                table: "Purchases",
                column: "DataStore",
                principalTable: "Stores",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingBags_Stores_StoreId",
                table: "ShoppingBags",
                column: "StoreId",
                principalTable: "Stores",
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "FK_Members_Bids_DataBidId",
                table: "Members");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Members_DataMemberId",
                table: "Notifications");

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
                name: "FK_Purchases_Members_DataMemberId",
                table: "Purchases");

            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_Stores_DataStore",
                table: "Purchases");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingBags_Stores_StoreId",
                table: "ShoppingBags");

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
                name: "IX_ShoppingBags_StoreId",
                table: "ShoppingBags");

            migrationBuilder.DropIndex(
                name: "IX_Purchases_DataMemberId",
                table: "Purchases");

            migrationBuilder.DropIndex(
                name: "IX_Purchases_DataStore",
                table: "Purchases");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PurchasePolicies",
                table: "PurchasePolicies");

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

            migrationBuilder.DropIndex(
                name: "IX_Notifications_DataMemberId",
                table: "Notifications");

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
                name: "StoreId",
                table: "ShoppingBags");

            migrationBuilder.DropColumn(
                name: "DataMemberId",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "DataStore",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "PurchasePolicies");

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
                name: "DataMemberId",
                table: "Notifications");

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
                name: "StoreId2",
                table: "ShoppingBags",
                type: "nvarchar(450)",
                nullable: true);

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
                name: "Id2",
                table: "PurchasePolicies",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

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
                name: "DataMemberId2",
                table: "Notifications",
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

            migrationBuilder.AddPrimaryKey(
                name: "PK_Stores",
                table: "Stores",
                column: "Id2");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PurchasePolicies",
                table: "PurchasePolicies",
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
                name: "IX_Notifications_DataMemberId2",
                table: "Notifications",
                column: "DataMemberId2");

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
    }
}
