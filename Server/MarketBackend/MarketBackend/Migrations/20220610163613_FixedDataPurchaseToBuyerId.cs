using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketBackend.Migrations
{
    public partial class FixedDataPurchaseToBuyerId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DataAppointmentsNode_DataAppointmentsNode_DataAppointmentsNodeId",
                table: "DataAppointmentsNode");

            migrationBuilder.DropForeignKey(
                name: "FK_DataBid_DataProduct_ProductId",
                table: "DataBid");

            migrationBuilder.DropForeignKey(
                name: "FK_DataBid_Members_MemberId",
                table: "DataBid");

            migrationBuilder.DropForeignKey(
                name: "FK_DataBid_Stores_DataStoreId",
                table: "DataBid");

            migrationBuilder.DropForeignKey(
                name: "FK_DataManagerPermission_DataStoreMemberRoles_DataStoreMemberRolesMemberId_DataStoreMemberRolesStoreId",
                table: "DataManagerPermission");

            migrationBuilder.DropForeignKey(
                name: "FK_DataProduct_Stores_DataStoreId",
                table: "DataProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_DataProductReview_DataProduct_ProductId",
                table: "DataProductReview");

            migrationBuilder.DropForeignKey(
                name: "FK_DataProductReview_Members_MemberId",
                table: "DataProductReview");

            migrationBuilder.DropForeignKey(
                name: "FK_DataPurchase_Members_DataMember",
                table: "DataPurchase");

            migrationBuilder.DropForeignKey(
                name: "FK_DataPurchaseOption_DataProduct_DataProductId",
                table: "DataPurchaseOption");

            migrationBuilder.DropForeignKey(
                name: "FK_DataShoppingBag_DataCart_DataCartId",
                table: "DataShoppingBag");

            migrationBuilder.DropForeignKey(
                name: "FK_DataStoreMemberRoles_Stores_DataStoreId",
                table: "DataStoreMemberRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_Members_DataBid_DataBidId",
                table: "Members");

            migrationBuilder.DropForeignKey(
                name: "FK_Members_DataCart_CartId",
                table: "Members");

            migrationBuilder.DropForeignKey(
                name: "FK_Stores_DataAppointmentsNode_AppointmentsId",
                table: "Stores");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DataStoreMemberRoles",
                table: "DataStoreMemberRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DataPurchaseOption",
                table: "DataPurchaseOption");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DataProductReview",
                table: "DataProductReview");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DataProduct",
                table: "DataProduct");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DataManagerPermission",
                table: "DataManagerPermission");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DataCart",
                table: "DataCart");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DataBid",
                table: "DataBid");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DataAppointmentsNode",
                table: "DataAppointmentsNode");

            migrationBuilder.RenameTable(
                name: "DataStoreMemberRoles",
                newName: "StoreMemberRoles");

            migrationBuilder.RenameTable(
                name: "DataPurchaseOption",
                newName: "PurchaseOptions");

            migrationBuilder.RenameTable(
                name: "DataProductReview",
                newName: "ProductReview");

            migrationBuilder.RenameTable(
                name: "DataProduct",
                newName: "Products");

            migrationBuilder.RenameTable(
                name: "DataManagerPermission",
                newName: "ManagerPermissions");

            migrationBuilder.RenameTable(
                name: "DataCart",
                newName: "Carts");

            migrationBuilder.RenameTable(
                name: "DataBid",
                newName: "Bids");

            migrationBuilder.RenameTable(
                name: "DataAppointmentsNode",
                newName: "AppointmentsNodes");

            migrationBuilder.RenameColumn(
                name: "DataMember",
                table: "DataPurchase",
                newName: "DataMemberId");

            migrationBuilder.RenameIndex(
                name: "IX_DataPurchase_DataMember",
                table: "DataPurchase",
                newName: "IX_DataPurchase_DataMemberId");

            migrationBuilder.RenameIndex(
                name: "IX_DataStoreMemberRoles_DataStoreId",
                table: "StoreMemberRoles",
                newName: "IX_StoreMemberRoles_DataStoreId");

            migrationBuilder.RenameIndex(
                name: "IX_DataPurchaseOption_DataProductId",
                table: "PurchaseOptions",
                newName: "IX_PurchaseOptions_DataProductId");

            migrationBuilder.RenameIndex(
                name: "IX_DataProductReview_ProductId",
                table: "ProductReview",
                newName: "IX_ProductReview_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_DataProductReview_MemberId",
                table: "ProductReview",
                newName: "IX_ProductReview_MemberId");

            migrationBuilder.RenameIndex(
                name: "IX_DataProduct_DataStoreId",
                table: "Products",
                newName: "IX_Products_DataStoreId");

            migrationBuilder.RenameIndex(
                name: "IX_DataManagerPermission_DataStoreMemberRolesMemberId_DataStoreMemberRolesStoreId",
                table: "ManagerPermissions",
                newName: "IX_ManagerPermissions_DataStoreMemberRolesMemberId_DataStoreMemberRolesStoreId");

            migrationBuilder.RenameIndex(
                name: "IX_DataBid_ProductId",
                table: "Bids",
                newName: "IX_Bids_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_DataBid_MemberId",
                table: "Bids",
                newName: "IX_Bids_MemberId");

            migrationBuilder.RenameIndex(
                name: "IX_DataBid_DataStoreId",
                table: "Bids",
                newName: "IX_Bids_DataStoreId");

            migrationBuilder.RenameIndex(
                name: "IX_DataAppointmentsNode_DataAppointmentsNodeId",
                table: "AppointmentsNodes",
                newName: "IX_AppointmentsNodes_DataAppointmentsNodeId");

            migrationBuilder.AddColumn<int>(
                name: "BuyerId",
                table: "DataPurchase",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_StoreMemberRoles",
                table: "StoreMemberRoles",
                columns: new[] { "MemberId", "StoreId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_PurchaseOptions",
                table: "PurchaseOptions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductReview",
                table: "ProductReview",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                table: "Products",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ManagerPermissions",
                table: "ManagerPermissions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Carts",
                table: "Carts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bids",
                table: "Bids",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppointmentsNodes",
                table: "AppointmentsNodes",
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
                name: "FK_DataPurchase_Members_DataMemberId",
                table: "DataPurchase",
                column: "DataMemberId",
                principalTable: "Members",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DataShoppingBag_Carts_DataCartId",
                table: "DataShoppingBag",
                column: "DataCartId",
                principalTable: "Carts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ManagerPermissions_StoreMemberRoles_DataStoreMemberRolesMemberId_DataStoreMemberRolesStoreId",
                table: "ManagerPermissions",
                columns: new[] { "DataStoreMemberRolesMemberId", "DataStoreMemberRolesStoreId" },
                principalTable: "StoreMemberRoles",
                principalColumns: new[] { "MemberId", "StoreId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Members_Bids_DataBidId",
                table: "Members",
                column: "DataBidId",
                principalTable: "Bids",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Members_Carts_CartId",
                table: "Members",
                column: "CartId",
                principalTable: "Carts",
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
                name: "FK_StoreMemberRoles_Stores_DataStoreId",
                table: "StoreMemberRoles",
                column: "DataStoreId",
                principalTable: "Stores",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Stores_AppointmentsNodes_AppointmentsId",
                table: "Stores",
                column: "AppointmentsId",
                principalTable: "AppointmentsNodes",
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
                name: "FK_DataPurchase_Members_DataMemberId",
                table: "DataPurchase");

            migrationBuilder.DropForeignKey(
                name: "FK_DataShoppingBag_Carts_DataCartId",
                table: "DataShoppingBag");

            migrationBuilder.DropForeignKey(
                name: "FK_ManagerPermissions_StoreMemberRoles_DataStoreMemberRolesMemberId_DataStoreMemberRolesStoreId",
                table: "ManagerPermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_Members_Bids_DataBidId",
                table: "Members");

            migrationBuilder.DropForeignKey(
                name: "FK_Members_Carts_CartId",
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
                name: "FK_StoreMemberRoles_Stores_DataStoreId",
                table: "StoreMemberRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_Stores_AppointmentsNodes_AppointmentsId",
                table: "Stores");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StoreMemberRoles",
                table: "StoreMemberRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PurchaseOptions",
                table: "PurchaseOptions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductReview",
                table: "ProductReview");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ManagerPermissions",
                table: "ManagerPermissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Carts",
                table: "Carts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bids",
                table: "Bids");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppointmentsNodes",
                table: "AppointmentsNodes");

            migrationBuilder.DropColumn(
                name: "BuyerId",
                table: "DataPurchase");

            migrationBuilder.RenameTable(
                name: "StoreMemberRoles",
                newName: "DataStoreMemberRoles");

            migrationBuilder.RenameTable(
                name: "PurchaseOptions",
                newName: "DataPurchaseOption");

            migrationBuilder.RenameTable(
                name: "Products",
                newName: "DataProduct");

            migrationBuilder.RenameTable(
                name: "ProductReview",
                newName: "DataProductReview");

            migrationBuilder.RenameTable(
                name: "ManagerPermissions",
                newName: "DataManagerPermission");

            migrationBuilder.RenameTable(
                name: "Carts",
                newName: "DataCart");

            migrationBuilder.RenameTable(
                name: "Bids",
                newName: "DataBid");

            migrationBuilder.RenameTable(
                name: "AppointmentsNodes",
                newName: "DataAppointmentsNode");

            migrationBuilder.RenameColumn(
                name: "DataMemberId",
                table: "DataPurchase",
                newName: "DataMember");

            migrationBuilder.RenameIndex(
                name: "IX_DataPurchase_DataMemberId",
                table: "DataPurchase",
                newName: "IX_DataPurchase_DataMember");

            migrationBuilder.RenameIndex(
                name: "IX_StoreMemberRoles_DataStoreId",
                table: "DataStoreMemberRoles",
                newName: "IX_DataStoreMemberRoles_DataStoreId");

            migrationBuilder.RenameIndex(
                name: "IX_PurchaseOptions_DataProductId",
                table: "DataPurchaseOption",
                newName: "IX_DataPurchaseOption_DataProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_DataStoreId",
                table: "DataProduct",
                newName: "IX_DataProduct_DataStoreId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductReview_ProductId",
                table: "DataProductReview",
                newName: "IX_DataProductReview_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductReview_MemberId",
                table: "DataProductReview",
                newName: "IX_DataProductReview_MemberId");

            migrationBuilder.RenameIndex(
                name: "IX_ManagerPermissions_DataStoreMemberRolesMemberId_DataStoreMemberRolesStoreId",
                table: "DataManagerPermission",
                newName: "IX_DataManagerPermission_DataStoreMemberRolesMemberId_DataStoreMemberRolesStoreId");

            migrationBuilder.RenameIndex(
                name: "IX_Bids_ProductId",
                table: "DataBid",
                newName: "IX_DataBid_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Bids_MemberId",
                table: "DataBid",
                newName: "IX_DataBid_MemberId");

            migrationBuilder.RenameIndex(
                name: "IX_Bids_DataStoreId",
                table: "DataBid",
                newName: "IX_DataBid_DataStoreId");

            migrationBuilder.RenameIndex(
                name: "IX_AppointmentsNodes_DataAppointmentsNodeId",
                table: "DataAppointmentsNode",
                newName: "IX_DataAppointmentsNode_DataAppointmentsNodeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DataStoreMemberRoles",
                table: "DataStoreMemberRoles",
                columns: new[] { "MemberId", "StoreId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_DataPurchaseOption",
                table: "DataPurchaseOption",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DataProduct",
                table: "DataProduct",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DataProductReview",
                table: "DataProductReview",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DataManagerPermission",
                table: "DataManagerPermission",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DataCart",
                table: "DataCart",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DataBid",
                table: "DataBid",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DataAppointmentsNode",
                table: "DataAppointmentsNode",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DataAppointmentsNode_DataAppointmentsNode_DataAppointmentsNodeId",
                table: "DataAppointmentsNode",
                column: "DataAppointmentsNodeId",
                principalTable: "DataAppointmentsNode",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DataBid_DataProduct_ProductId",
                table: "DataBid",
                column: "ProductId",
                principalTable: "DataProduct",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DataBid_Members_MemberId",
                table: "DataBid",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DataBid_Stores_DataStoreId",
                table: "DataBid",
                column: "DataStoreId",
                principalTable: "Stores",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DataManagerPermission_DataStoreMemberRoles_DataStoreMemberRolesMemberId_DataStoreMemberRolesStoreId",
                table: "DataManagerPermission",
                columns: new[] { "DataStoreMemberRolesMemberId", "DataStoreMemberRolesStoreId" },
                principalTable: "DataStoreMemberRoles",
                principalColumns: new[] { "MemberId", "StoreId" });

            migrationBuilder.AddForeignKey(
                name: "FK_DataProduct_Stores_DataStoreId",
                table: "DataProduct",
                column: "DataStoreId",
                principalTable: "Stores",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DataProductReview_DataProduct_ProductId",
                table: "DataProductReview",
                column: "ProductId",
                principalTable: "DataProduct",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DataProductReview_Members_MemberId",
                table: "DataProductReview",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DataPurchase_Members_DataMember",
                table: "DataPurchase",
                column: "DataMember",
                principalTable: "Members",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DataPurchaseOption_DataProduct_DataProductId",
                table: "DataPurchaseOption",
                column: "DataProductId",
                principalTable: "DataProduct",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DataShoppingBag_DataCart_DataCartId",
                table: "DataShoppingBag",
                column: "DataCartId",
                principalTable: "DataCart",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DataStoreMemberRoles_Stores_DataStoreId",
                table: "DataStoreMemberRoles",
                column: "DataStoreId",
                principalTable: "Stores",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Members_DataBid_DataBidId",
                table: "Members",
                column: "DataBidId",
                principalTable: "DataBid",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Members_DataCart_CartId",
                table: "Members",
                column: "CartId",
                principalTable: "DataCart",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Stores_DataAppointmentsNode_AppointmentsId",
                table: "Stores",
                column: "AppointmentsId",
                principalTable: "DataAppointmentsNode",
                principalColumn: "Id");
        }
    }
}
