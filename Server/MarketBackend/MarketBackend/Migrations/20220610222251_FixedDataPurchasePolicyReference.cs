using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketBackend.Migrations
{
    public partial class FixedDataPurchasePolicyReference : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DataPurchasePolicy_DataPurchasePolicy_PolicyId",
                table: "DataPurchasePolicy");

            migrationBuilder.AddForeignKey(
                name: "FK_DataPurchasePolicy_InterfacesPurchasePolicies_PolicyId",
                table: "DataPurchasePolicy",
                column: "PolicyId",
                principalTable: "InterfacesPurchasePolicies",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DataPurchasePolicy_InterfacesPurchasePolicies_PolicyId",
                table: "DataPurchasePolicy");

            migrationBuilder.AddForeignKey(
                name: "FK_DataPurchasePolicy_DataPurchasePolicy_PolicyId",
                table: "DataPurchasePolicy",
                column: "PolicyId",
                principalTable: "DataPurchasePolicy",
                principalColumn: "Id");
        }
    }
}
