using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketBackend.Migrations
{
    public partial class ChangingSomeIdsToNotAutoIncrement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.RenameIndex(
                name: "IX_DataShoppingBag_StoreId",
                table: "ShoppingBags",
                newName: "IX_ShoppingBags_StoreId");

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
                name: "IX_DataPurchase_DataStore",
                table: "Purchases",
                newName: "IX_Purchases_DataStore");

            migrationBuilder.RenameIndex(
                name: "IX_DataPurchase_DataMemberId",
                table: "Purchases",
                newName: "IX_Purchases_DataMemberId");

            migrationBuilder.RenameIndex(
                name: "IX_DataProductInBag_DataShoppingBagId",
                table: "ProductInBags",
                newName: "IX_ProductInBags_DataShoppingBagId");

            migrationBuilder.RenameIndex(
                name: "IX_DataNotification_DataMemberId",
                table: "Notifications",
                newName: "IX_Notifications_DataMemberId");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Stores",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Products",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Members",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Discounts",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Bids",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "AppointmentsNodes",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "PurchasePolicies",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShoppingBags",
                table: "ShoppingBags",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PurchasePolicies",
                table: "PurchasePolicies",
                column: "Id");

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
                name: "FK_Notifications_Members_DataMemberId",
                table: "Notifications",
                column: "DataMemberId",
                principalTable: "Members",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductInBags_ShoppingBags_DataShoppingBagId",
                table: "ProductInBags",
                column: "DataShoppingBagId",
                principalTable: "ShoppingBags",
                principalColumn: "Id");

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
                name: "FK_ShoppingBags_Carts_DataCartId",
                table: "ShoppingBags",
                column: "DataCartId",
                principalTable: "Carts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingBags_Stores_StoreId",
                table: "ShoppingBags",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Members_DataMemberId",
                table: "Notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductInBags_ShoppingBags_DataShoppingBagId",
                table: "ProductInBags");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchasePolicies_DataStorePurchasePolicyManager_DataStorePurchasePolicyManagerId",
                table: "PurchasePolicies");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchasePolicies_InterfacesPurchasePolicies_PolicyId",
                table: "PurchasePolicies");

            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_Members_DataMemberId",
                table: "Purchases");

            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_Stores_DataStore",
                table: "Purchases");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingBags_Carts_DataCartId",
                table: "ShoppingBags");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingBags_Stores_StoreId",
                table: "ShoppingBags");

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
                name: "IX_ShoppingBags_StoreId",
                table: "DataShoppingBag",
                newName: "IX_DataShoppingBag_StoreId");

            migrationBuilder.RenameIndex(
                name: "IX_ShoppingBags_DataCartId",
                table: "DataShoppingBag",
                newName: "IX_DataShoppingBag_DataCartId");

            migrationBuilder.RenameIndex(
                name: "IX_Purchases_DataStore",
                table: "DataPurchase",
                newName: "IX_DataPurchase_DataStore");

            migrationBuilder.RenameIndex(
                name: "IX_Purchases_DataMemberId",
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

            migrationBuilder.RenameIndex(
                name: "IX_Notifications_DataMemberId",
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
        }
    }
}
