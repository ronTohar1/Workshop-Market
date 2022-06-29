using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketBackend.Migrations
{
    public partial class TryingOptionalForeginKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stores_DataStoreDiscountPolicyManager_DiscountManagerId",
                table: "Stores");

            migrationBuilder.DropForeignKey(
                name: "FK_Stores_DataStorePurchasePolicyManager_PurchaseManagerId",
                table: "Stores");

            migrationBuilder.AlterColumn<int>(
                name: "PurchaseManagerId",
                table: "Stores",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "DiscountManagerId",
                table: "Stores",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Stores_DataStoreDiscountPolicyManager_DiscountManagerId",
                table: "Stores",
                column: "DiscountManagerId",
                principalTable: "DataStoreDiscountPolicyManager",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Stores_DataStorePurchasePolicyManager_PurchaseManagerId",
                table: "Stores",
                column: "PurchaseManagerId",
                principalTable: "DataStorePurchasePolicyManager",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stores_DataStoreDiscountPolicyManager_DiscountManagerId",
                table: "Stores");

            migrationBuilder.DropForeignKey(
                name: "FK_Stores_DataStorePurchasePolicyManager_PurchaseManagerId",
                table: "Stores");

            migrationBuilder.AlterColumn<int>(
                name: "PurchaseManagerId",
                table: "Stores",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DiscountManagerId",
                table: "Stores",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Stores_DataStoreDiscountPolicyManager_DiscountManagerId",
                table: "Stores",
                column: "DiscountManagerId",
                principalTable: "DataStoreDiscountPolicyManager",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stores_DataStorePurchasePolicyManager_PurchaseManagerId",
                table: "Stores",
                column: "PurchaseManagerId",
                principalTable: "DataStorePurchasePolicyManager",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
