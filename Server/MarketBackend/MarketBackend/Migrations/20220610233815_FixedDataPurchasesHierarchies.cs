using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketBackend.Migrations
{
    public partial class FixedDataPurchasesHierarchies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "FK_DataPurchasePolicy_InterfacesPurchasePolicies_SecondPredId",
                table: "DataPurchasePolicy");

            migrationBuilder.DropForeignKey(
                name: "FK_DataPurchasePolicy_PruchasePredicateExpressions_AllowingId",
                table: "DataPurchasePolicy");

            migrationBuilder.DropForeignKey(
                name: "FK_DataPurchasePolicy_PruchasePredicateExpressions_ConditionId",
                table: "DataPurchasePolicy");

            migrationBuilder.DropIndex(
                name: "IX_DataPurchasePolicy_AllowingId",
                table: "DataPurchasePolicy");

            migrationBuilder.DropIndex(
                name: "IX_DataPurchasePolicy_ConditionId",
                table: "DataPurchasePolicy");

            migrationBuilder.DropIndex(
                name: "IX_DataPurchasePolicy_DataOrExpression_FirstPredId",
                table: "DataPurchasePolicy");

            migrationBuilder.DropIndex(
                name: "IX_DataPurchasePolicy_DataOrExpression_SecondPredId",
                table: "DataPurchasePolicy");

            migrationBuilder.DropIndex(
                name: "IX_DataPurchasePolicy_FirstPredId",
                table: "DataPurchasePolicy");

            migrationBuilder.DropIndex(
                name: "IX_DataPurchasePolicy_SecondPredId",
                table: "DataPurchasePolicy");

            migrationBuilder.DropColumn(
                name: "AllowingId",
                table: "DataPurchasePolicy");

            migrationBuilder.DropColumn(
                name: "ConditionId",
                table: "DataPurchasePolicy");

            migrationBuilder.DropColumn(
                name: "DataOrExpression_FirstPredId",
                table: "DataPurchasePolicy");

            migrationBuilder.DropColumn(
                name: "DataOrExpression_SecondPredId",
                table: "DataPurchasePolicy");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "DataPurchasePolicy");

            migrationBuilder.DropColumn(
                name: "FirstPredId",
                table: "DataPurchasePolicy");

            migrationBuilder.DropColumn(
                name: "SecondPredId",
                table: "DataPurchasePolicy");

            migrationBuilder.AddColumn<int>(
                name: "AllowingId",
                table: "InterfacesPurchasePolicies",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ConditionId",
                table: "InterfacesPurchasePolicies",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DataAndExpression_FirstPredId",
                table: "InterfacesPurchasePolicies",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DataAndExpression_SecondPredId",
                table: "InterfacesPurchasePolicies",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FirstPredId",
                table: "InterfacesPurchasePolicies",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SecondPredId",
                table: "InterfacesPurchasePolicies",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_InterfacesPurchasePolicies_AllowingId",
                table: "InterfacesPurchasePolicies",
                column: "AllowingId");

            migrationBuilder.CreateIndex(
                name: "IX_InterfacesPurchasePolicies_ConditionId",
                table: "InterfacesPurchasePolicies",
                column: "ConditionId");

            migrationBuilder.CreateIndex(
                name: "IX_InterfacesPurchasePolicies_DataAndExpression_FirstPredId",
                table: "InterfacesPurchasePolicies",
                column: "DataAndExpression_FirstPredId");

            migrationBuilder.CreateIndex(
                name: "IX_InterfacesPurchasePolicies_DataAndExpression_SecondPredId",
                table: "InterfacesPurchasePolicies",
                column: "DataAndExpression_SecondPredId");

            migrationBuilder.CreateIndex(
                name: "IX_InterfacesPurchasePolicies_FirstPredId",
                table: "InterfacesPurchasePolicies",
                column: "FirstPredId");

            migrationBuilder.CreateIndex(
                name: "IX_InterfacesPurchasePolicies_SecondPredId",
                table: "InterfacesPurchasePolicies",
                column: "SecondPredId");

            migrationBuilder.AddForeignKey(
                name: "FK_InterfacesPurchasePolicies_InterfacesPurchasePolicies_DataAndExpression_FirstPredId",
                table: "InterfacesPurchasePolicies",
                column: "DataAndExpression_FirstPredId",
                principalTable: "InterfacesPurchasePolicies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InterfacesPurchasePolicies_InterfacesPurchasePolicies_DataAndExpression_SecondPredId",
                table: "InterfacesPurchasePolicies",
                column: "DataAndExpression_SecondPredId",
                principalTable: "InterfacesPurchasePolicies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InterfacesPurchasePolicies_InterfacesPurchasePolicies_FirstPredId",
                table: "InterfacesPurchasePolicies",
                column: "FirstPredId",
                principalTable: "InterfacesPurchasePolicies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InterfacesPurchasePolicies_InterfacesPurchasePolicies_SecondPredId",
                table: "InterfacesPurchasePolicies",
                column: "SecondPredId",
                principalTable: "InterfacesPurchasePolicies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InterfacesPurchasePolicies_PruchasePredicateExpressions_AllowingId",
                table: "InterfacesPurchasePolicies",
                column: "AllowingId",
                principalTable: "PruchasePredicateExpressions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InterfacesPurchasePolicies_PruchasePredicateExpressions_ConditionId",
                table: "InterfacesPurchasePolicies",
                column: "ConditionId",
                principalTable: "PruchasePredicateExpressions",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InterfacesPurchasePolicies_InterfacesPurchasePolicies_DataAndExpression_FirstPredId",
                table: "InterfacesPurchasePolicies");

            migrationBuilder.DropForeignKey(
                name: "FK_InterfacesPurchasePolicies_InterfacesPurchasePolicies_DataAndExpression_SecondPredId",
                table: "InterfacesPurchasePolicies");

            migrationBuilder.DropForeignKey(
                name: "FK_InterfacesPurchasePolicies_InterfacesPurchasePolicies_FirstPredId",
                table: "InterfacesPurchasePolicies");

            migrationBuilder.DropForeignKey(
                name: "FK_InterfacesPurchasePolicies_InterfacesPurchasePolicies_SecondPredId",
                table: "InterfacesPurchasePolicies");

            migrationBuilder.DropForeignKey(
                name: "FK_InterfacesPurchasePolicies_PruchasePredicateExpressions_AllowingId",
                table: "InterfacesPurchasePolicies");

            migrationBuilder.DropForeignKey(
                name: "FK_InterfacesPurchasePolicies_PruchasePredicateExpressions_ConditionId",
                table: "InterfacesPurchasePolicies");

            migrationBuilder.DropIndex(
                name: "IX_InterfacesPurchasePolicies_AllowingId",
                table: "InterfacesPurchasePolicies");

            migrationBuilder.DropIndex(
                name: "IX_InterfacesPurchasePolicies_ConditionId",
                table: "InterfacesPurchasePolicies");

            migrationBuilder.DropIndex(
                name: "IX_InterfacesPurchasePolicies_DataAndExpression_FirstPredId",
                table: "InterfacesPurchasePolicies");

            migrationBuilder.DropIndex(
                name: "IX_InterfacesPurchasePolicies_DataAndExpression_SecondPredId",
                table: "InterfacesPurchasePolicies");

            migrationBuilder.DropIndex(
                name: "IX_InterfacesPurchasePolicies_FirstPredId",
                table: "InterfacesPurchasePolicies");

            migrationBuilder.DropIndex(
                name: "IX_InterfacesPurchasePolicies_SecondPredId",
                table: "InterfacesPurchasePolicies");

            migrationBuilder.DropColumn(
                name: "AllowingId",
                table: "InterfacesPurchasePolicies");

            migrationBuilder.DropColumn(
                name: "ConditionId",
                table: "InterfacesPurchasePolicies");

            migrationBuilder.DropColumn(
                name: "DataAndExpression_FirstPredId",
                table: "InterfacesPurchasePolicies");

            migrationBuilder.DropColumn(
                name: "DataAndExpression_SecondPredId",
                table: "InterfacesPurchasePolicies");

            migrationBuilder.DropColumn(
                name: "FirstPredId",
                table: "InterfacesPurchasePolicies");

            migrationBuilder.DropColumn(
                name: "SecondPredId",
                table: "InterfacesPurchasePolicies");

            migrationBuilder.AddColumn<int>(
                name: "AllowingId",
                table: "DataPurchasePolicy",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ConditionId",
                table: "DataPurchasePolicy",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DataOrExpression_FirstPredId",
                table: "DataPurchasePolicy",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DataOrExpression_SecondPredId",
                table: "DataPurchasePolicy",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "DataPurchasePolicy",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "FirstPredId",
                table: "DataPurchasePolicy",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SecondPredId",
                table: "DataPurchasePolicy",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DataPurchasePolicy_AllowingId",
                table: "DataPurchasePolicy",
                column: "AllowingId");

            migrationBuilder.CreateIndex(
                name: "IX_DataPurchasePolicy_ConditionId",
                table: "DataPurchasePolicy",
                column: "ConditionId");

            migrationBuilder.CreateIndex(
                name: "IX_DataPurchasePolicy_DataOrExpression_FirstPredId",
                table: "DataPurchasePolicy",
                column: "DataOrExpression_FirstPredId");

            migrationBuilder.CreateIndex(
                name: "IX_DataPurchasePolicy_DataOrExpression_SecondPredId",
                table: "DataPurchasePolicy",
                column: "DataOrExpression_SecondPredId");

            migrationBuilder.CreateIndex(
                name: "IX_DataPurchasePolicy_FirstPredId",
                table: "DataPurchasePolicy",
                column: "FirstPredId");

            migrationBuilder.CreateIndex(
                name: "IX_DataPurchasePolicy_SecondPredId",
                table: "DataPurchasePolicy",
                column: "SecondPredId");

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
    }
}
