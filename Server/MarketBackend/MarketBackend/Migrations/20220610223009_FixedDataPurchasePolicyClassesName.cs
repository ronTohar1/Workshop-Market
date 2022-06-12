using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketBackend.Migrations
{
    public partial class FixedDataPurchasePolicyClassesName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DataIPurchasePolicy_DataStorePurchasePolicyManager_DataStorePurchasePolicyManagerId",
                table: "DataIPurchasePolicy");

            migrationBuilder.DropForeignKey(
                name: "FK_DataIPurchasePolicy_InterfacesPurchasePolicies_DataOrExpression_FirstPredId",
                table: "DataIPurchasePolicy");

            migrationBuilder.DropForeignKey(
                name: "FK_DataIPurchasePolicy_InterfacesPurchasePolicies_DataOrExpression_SecondPredId",
                table: "DataIPurchasePolicy");

            migrationBuilder.DropForeignKey(
                name: "FK_DataIPurchasePolicy_InterfacesPurchasePolicies_FirstPredId",
                table: "DataIPurchasePolicy");

            migrationBuilder.DropForeignKey(
                name: "FK_DataIPurchasePolicy_InterfacesPurchasePolicies_PolicyId",
                table: "DataIPurchasePolicy");

            migrationBuilder.DropForeignKey(
                name: "FK_DataIPurchasePolicy_InterfacesPurchasePolicies_SecondPredId",
                table: "DataIPurchasePolicy");

            migrationBuilder.DropForeignKey(
                name: "FK_DataIPurchasePolicy_PruchasePredicateExpressions_AllowingId",
                table: "DataIPurchasePolicy");

            migrationBuilder.DropForeignKey(
                name: "FK_DataIPurchasePolicy_PruchasePredicateExpressions_ConditionId",
                table: "DataIPurchasePolicy");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DataIPurchasePolicy",
                table: "DataIPurchasePolicy");

            migrationBuilder.RenameTable(
                name: "DataIPurchasePolicy",
                newName: "DataPurchasePolicy");

            migrationBuilder.RenameIndex(
                name: "IX_DataIPurchasePolicy_SecondPredId",
                table: "DataPurchasePolicy",
                newName: "IX_DataPurchasePolicy_SecondPredId");

            migrationBuilder.RenameIndex(
                name: "IX_DataIPurchasePolicy_PolicyId",
                table: "DataPurchasePolicy",
                newName: "IX_DataPurchasePolicy_PolicyId");

            migrationBuilder.RenameIndex(
                name: "IX_DataIPurchasePolicy_FirstPredId",
                table: "DataPurchasePolicy",
                newName: "IX_DataPurchasePolicy_FirstPredId");

            migrationBuilder.RenameIndex(
                name: "IX_DataIPurchasePolicy_DataStorePurchasePolicyManagerId",
                table: "DataPurchasePolicy",
                newName: "IX_DataPurchasePolicy_DataStorePurchasePolicyManagerId");

            migrationBuilder.RenameIndex(
                name: "IX_DataIPurchasePolicy_DataOrExpression_SecondPredId",
                table: "DataPurchasePolicy",
                newName: "IX_DataPurchasePolicy_DataOrExpression_SecondPredId");

            migrationBuilder.RenameIndex(
                name: "IX_DataIPurchasePolicy_DataOrExpression_FirstPredId",
                table: "DataPurchasePolicy",
                newName: "IX_DataPurchasePolicy_DataOrExpression_FirstPredId");

            migrationBuilder.RenameIndex(
                name: "IX_DataIPurchasePolicy_ConditionId",
                table: "DataPurchasePolicy",
                newName: "IX_DataPurchasePolicy_ConditionId");

            migrationBuilder.RenameIndex(
                name: "IX_DataIPurchasePolicy_AllowingId",
                table: "DataPurchasePolicy",
                newName: "IX_DataPurchasePolicy_AllowingId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DataPurchasePolicy",
                table: "DataPurchasePolicy",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DataPurchasePolicy_DataStorePurchasePolicyManager_DataStorePurchasePolicyManagerId",
                table: "DataPurchasePolicy",
                column: "DataStorePurchasePolicyManagerId",
                principalTable: "DataStorePurchasePolicyManager",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DataPurchasePolicy_InterfacesPurchasePolicies_DataOrExpression_FirstPredId",
                table: "DataPurchasePolicy",
                column: "DataOrExpression_FirstPredId",
                principalTable: "InterfacesPurchasePolicies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DataPurchasePolicy_InterfacesPurchasePolicies_DataOrExpression_SecondPredId",
                table: "DataPurchasePolicy",
                column: "DataOrExpression_SecondPredId",
                principalTable: "InterfacesPurchasePolicies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DataPurchasePolicy_InterfacesPurchasePolicies_FirstPredId",
                table: "DataPurchasePolicy",
                column: "FirstPredId",
                principalTable: "InterfacesPurchasePolicies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DataPurchasePolicy_InterfacesPurchasePolicies_PolicyId",
                table: "DataPurchasePolicy",
                column: "PolicyId",
                principalTable: "InterfacesPurchasePolicies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DataPurchasePolicy_InterfacesPurchasePolicies_SecondPredId",
                table: "DataPurchasePolicy",
                column: "SecondPredId",
                principalTable: "InterfacesPurchasePolicies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DataPurchasePolicy_PruchasePredicateExpressions_AllowingId",
                table: "DataPurchasePolicy",
                column: "AllowingId",
                principalTable: "PruchasePredicateExpressions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DataPurchasePolicy_PruchasePredicateExpressions_ConditionId",
                table: "DataPurchasePolicy",
                column: "ConditionId",
                principalTable: "PruchasePredicateExpressions",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DataPurchasePolicy_DataStorePurchasePolicyManager_DataStorePurchasePolicyManagerId",
                table: "DataPurchasePolicy");

            migrationBuilder.DropForeignKey(
                name: "FK_DataPurchasePolicy_InterfacesPurchasePolicies_DataOrExpression_FirstPredId",
                table: "DataPurchasePolicy");

            migrationBuilder.DropForeignKey(
                name: "FK_DataPurchasePolicy_InterfacesPurchasePolicies_DataOrExpression_SecondPredId",
                table: "DataPurchasePolicy");

            migrationBuilder.DropForeignKey(
                name: "FK_DataPurchasePolicy_InterfacesPurchasePolicies_FirstPredId",
                table: "DataPurchasePolicy");

            migrationBuilder.DropForeignKey(
                name: "FK_DataPurchasePolicy_InterfacesPurchasePolicies_PolicyId",
                table: "DataPurchasePolicy");

            migrationBuilder.DropForeignKey(
                name: "FK_DataPurchasePolicy_InterfacesPurchasePolicies_SecondPredId",
                table: "DataPurchasePolicy");

            migrationBuilder.DropForeignKey(
                name: "FK_DataPurchasePolicy_PruchasePredicateExpressions_AllowingId",
                table: "DataPurchasePolicy");

            migrationBuilder.DropForeignKey(
                name: "FK_DataPurchasePolicy_PruchasePredicateExpressions_ConditionId",
                table: "DataPurchasePolicy");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DataPurchasePolicy",
                table: "DataPurchasePolicy");

            migrationBuilder.RenameTable(
                name: "DataPurchasePolicy",
                newName: "DataIPurchasePolicy");

            migrationBuilder.RenameIndex(
                name: "IX_DataPurchasePolicy_SecondPredId",
                table: "DataIPurchasePolicy",
                newName: "IX_DataIPurchasePolicy_SecondPredId");

            migrationBuilder.RenameIndex(
                name: "IX_DataPurchasePolicy_PolicyId",
                table: "DataIPurchasePolicy",
                newName: "IX_DataIPurchasePolicy_PolicyId");

            migrationBuilder.RenameIndex(
                name: "IX_DataPurchasePolicy_FirstPredId",
                table: "DataIPurchasePolicy",
                newName: "IX_DataIPurchasePolicy_FirstPredId");

            migrationBuilder.RenameIndex(
                name: "IX_DataPurchasePolicy_DataStorePurchasePolicyManagerId",
                table: "DataIPurchasePolicy",
                newName: "IX_DataIPurchasePolicy_DataStorePurchasePolicyManagerId");

            migrationBuilder.RenameIndex(
                name: "IX_DataPurchasePolicy_DataOrExpression_SecondPredId",
                table: "DataIPurchasePolicy",
                newName: "IX_DataIPurchasePolicy_DataOrExpression_SecondPredId");

            migrationBuilder.RenameIndex(
                name: "IX_DataPurchasePolicy_DataOrExpression_FirstPredId",
                table: "DataIPurchasePolicy",
                newName: "IX_DataIPurchasePolicy_DataOrExpression_FirstPredId");

            migrationBuilder.RenameIndex(
                name: "IX_DataPurchasePolicy_ConditionId",
                table: "DataIPurchasePolicy",
                newName: "IX_DataIPurchasePolicy_ConditionId");

            migrationBuilder.RenameIndex(
                name: "IX_DataPurchasePolicy_AllowingId",
                table: "DataIPurchasePolicy",
                newName: "IX_DataIPurchasePolicy_AllowingId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DataIPurchasePolicy",
                table: "DataIPurchasePolicy",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DataIPurchasePolicy_DataStorePurchasePolicyManager_DataStorePurchasePolicyManagerId",
                table: "DataIPurchasePolicy",
                column: "DataStorePurchasePolicyManagerId",
                principalTable: "DataStorePurchasePolicyManager",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DataIPurchasePolicy_InterfacesPurchasePolicies_DataOrExpression_FirstPredId",
                table: "DataIPurchasePolicy",
                column: "DataOrExpression_FirstPredId",
                principalTable: "InterfacesPurchasePolicies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DataIPurchasePolicy_InterfacesPurchasePolicies_DataOrExpression_SecondPredId",
                table: "DataIPurchasePolicy",
                column: "DataOrExpression_SecondPredId",
                principalTable: "InterfacesPurchasePolicies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DataIPurchasePolicy_InterfacesPurchasePolicies_FirstPredId",
                table: "DataIPurchasePolicy",
                column: "FirstPredId",
                principalTable: "InterfacesPurchasePolicies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DataIPurchasePolicy_InterfacesPurchasePolicies_PolicyId",
                table: "DataIPurchasePolicy",
                column: "PolicyId",
                principalTable: "InterfacesPurchasePolicies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DataIPurchasePolicy_InterfacesPurchasePolicies_SecondPredId",
                table: "DataIPurchasePolicy",
                column: "SecondPredId",
                principalTable: "InterfacesPurchasePolicies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DataIPurchasePolicy_PruchasePredicateExpressions_AllowingId",
                table: "DataIPurchasePolicy",
                column: "AllowingId",
                principalTable: "PruchasePredicateExpressions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DataIPurchasePolicy_PruchasePredicateExpressions_ConditionId",
                table: "DataIPurchasePolicy",
                column: "ConditionId",
                principalTable: "PruchasePredicateExpressions",
                principalColumn: "Id");
        }
    }
}
