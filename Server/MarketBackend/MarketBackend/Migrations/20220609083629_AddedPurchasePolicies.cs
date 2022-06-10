using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketBackend.Migrations
{
    public partial class AddedPurchasePolicies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expressions_PredicateExpressions_PredicateId",
                table: "Expressions");

            migrationBuilder.DropForeignKey(
                name: "FK_Expressions_PredicateExpressions_TestId",
                table: "Expressions");

            migrationBuilder.DropForeignKey(
                name: "FK_PredicateExpressions_PredicateExpressions_FirstId",
                table: "PredicateExpressions");

            migrationBuilder.DropForeignKey(
                name: "FK_PredicateExpressions_PredicateExpressions_SecondId",
                table: "PredicateExpressions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PredicateExpressions",
                table: "PredicateExpressions");

            migrationBuilder.RenameTable(
                name: "PredicateExpressions",
                newName: "DiscountPredicateExpressions");

            migrationBuilder.RenameIndex(
                name: "IX_PredicateExpressions_SecondId",
                table: "DiscountPredicateExpressions",
                newName: "IX_DiscountPredicateExpressions_SecondId");

            migrationBuilder.RenameIndex(
                name: "IX_PredicateExpressions_FirstId",
                table: "DiscountPredicateExpressions",
                newName: "IX_DiscountPredicateExpressions_FirstId");

            migrationBuilder.AddColumn<int>(
                name: "PurchaseManagerId",
                table: "Stores",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_DiscountPredicateExpressions",
                table: "DiscountPredicateExpressions",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "DataStorePurchasePolicyManager",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataStorePurchasePolicyManager", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InterfacesPurchasePolicies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Hour = table.Column<int>(type: "int", nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: true),
                    Amount = table.Column<int>(type: "int", nullable: true),
                    DataAtLeastAmountRestriction_ProductId = table.Column<int>(type: "int", nullable: true),
                    DataAtLeastAmountRestriction_Amount = table.Column<int>(type: "int", nullable: true),
                    DataBeforeHourProductRestriction_ProductId = table.Column<int>(type: "int", nullable: true),
                    DataBeforeHourProductRestriction_Amount = table.Column<int>(type: "int", nullable: true),
                    Year = table.Column<int>(type: "int", nullable: true),
                    Month = table.Column<int>(type: "int", nullable: true),
                    Day = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InterfacesPurchasePolicies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PruchasePredicateExpressions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: true),
                    Amount = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PruchasePredicateExpressions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DataPurchasePolicy",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PolicyId = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataStorePurchasePolicyManagerId = table.Column<int>(type: "int", nullable: true),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstPredId = table.Column<int>(type: "int", nullable: true),
                    SecondPredId = table.Column<int>(type: "int", nullable: true),
                    ConditionId = table.Column<int>(type: "int", nullable: true),
                    AllowingId = table.Column<int>(type: "int", nullable: true),
                    DataOrExpression_FirstPredId = table.Column<int>(type: "int", nullable: true),
                    DataOrExpression_SecondPredId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataPurchasePolicy", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DataPurchasePolicy_DataPurchasePolicy_PolicyId",
                        column: x => x.PolicyId,
                        principalTable: "DataPurchasePolicy",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DataPurchasePolicy_DataStorePurchasePolicyManager_DataStorePurchasePolicyManagerId",
                        column: x => x.DataStorePurchasePolicyManagerId,
                        principalTable: "DataStorePurchasePolicyManager",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DataPurchasePolicy_InterfacesPurchasePolicies_DataOrExpression_FirstPredId",
                        column: x => x.DataOrExpression_FirstPredId,
                        principalTable: "InterfacesPurchasePolicies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DataPurchasePolicy_InterfacesPurchasePolicies_DataOrExpression_SecondPredId",
                        column: x => x.DataOrExpression_SecondPredId,
                        principalTable: "InterfacesPurchasePolicies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DataPurchasePolicy_InterfacesPurchasePolicies_FirstPredId",
                        column: x => x.FirstPredId,
                        principalTable: "InterfacesPurchasePolicies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DataPurchasePolicy_InterfacesPurchasePolicies_SecondPredId",
                        column: x => x.SecondPredId,
                        principalTable: "InterfacesPurchasePolicies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DataPurchasePolicy_PruchasePredicateExpressions_AllowingId",
                        column: x => x.AllowingId,
                        principalTable: "PruchasePredicateExpressions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DataPurchasePolicy_PruchasePredicateExpressions_ConditionId",
                        column: x => x.ConditionId,
                        principalTable: "PruchasePredicateExpressions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Stores_PurchaseManagerId",
                table: "Stores",
                column: "PurchaseManagerId");

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
                name: "IX_DataPurchasePolicy_DataStorePurchasePolicyManagerId",
                table: "DataPurchasePolicy",
                column: "DataStorePurchasePolicyManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_DataPurchasePolicy_FirstPredId",
                table: "DataPurchasePolicy",
                column: "FirstPredId");

            migrationBuilder.CreateIndex(
                name: "IX_DataPurchasePolicy_PolicyId",
                table: "DataPurchasePolicy",
                column: "PolicyId");

            migrationBuilder.CreateIndex(
                name: "IX_DataPurchasePolicy_SecondPredId",
                table: "DataPurchasePolicy",
                column: "SecondPredId");

            migrationBuilder.AddForeignKey(
                name: "FK_DiscountPredicateExpressions_DiscountPredicateExpressions_FirstId",
                table: "DiscountPredicateExpressions",
                column: "FirstId",
                principalTable: "DiscountPredicateExpressions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DiscountPredicateExpressions_DiscountPredicateExpressions_SecondId",
                table: "DiscountPredicateExpressions",
                column: "SecondId",
                principalTable: "DiscountPredicateExpressions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Expressions_DiscountPredicateExpressions_PredicateId",
                table: "Expressions",
                column: "PredicateId",
                principalTable: "DiscountPredicateExpressions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Expressions_DiscountPredicateExpressions_TestId",
                table: "Expressions",
                column: "TestId",
                principalTable: "DiscountPredicateExpressions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Stores_DataStorePurchasePolicyManager_PurchaseManagerId",
                table: "Stores",
                column: "PurchaseManagerId",
                principalTable: "DataStorePurchasePolicyManager",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DiscountPredicateExpressions_DiscountPredicateExpressions_FirstId",
                table: "DiscountPredicateExpressions");

            migrationBuilder.DropForeignKey(
                name: "FK_DiscountPredicateExpressions_DiscountPredicateExpressions_SecondId",
                table: "DiscountPredicateExpressions");

            migrationBuilder.DropForeignKey(
                name: "FK_Expressions_DiscountPredicateExpressions_PredicateId",
                table: "Expressions");

            migrationBuilder.DropForeignKey(
                name: "FK_Expressions_DiscountPredicateExpressions_TestId",
                table: "Expressions");

            migrationBuilder.DropForeignKey(
                name: "FK_Stores_DataStorePurchasePolicyManager_PurchaseManagerId",
                table: "Stores");

            migrationBuilder.DropTable(
                name: "DataPurchasePolicy");

            migrationBuilder.DropTable(
                name: "DataStorePurchasePolicyManager");

            migrationBuilder.DropTable(
                name: "InterfacesPurchasePolicies");

            migrationBuilder.DropTable(
                name: "PruchasePredicateExpressions");

            migrationBuilder.DropIndex(
                name: "IX_Stores_PurchaseManagerId",
                table: "Stores");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DiscountPredicateExpressions",
                table: "DiscountPredicateExpressions");

            migrationBuilder.DropColumn(
                name: "PurchaseManagerId",
                table: "Stores");

            migrationBuilder.RenameTable(
                name: "DiscountPredicateExpressions",
                newName: "PredicateExpressions");

            migrationBuilder.RenameIndex(
                name: "IX_DiscountPredicateExpressions_SecondId",
                table: "PredicateExpressions",
                newName: "IX_PredicateExpressions_SecondId");

            migrationBuilder.RenameIndex(
                name: "IX_DiscountPredicateExpressions_FirstId",
                table: "PredicateExpressions",
                newName: "IX_PredicateExpressions_FirstId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PredicateExpressions",
                table: "PredicateExpressions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Expressions_PredicateExpressions_PredicateId",
                table: "Expressions",
                column: "PredicateId",
                principalTable: "PredicateExpressions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Expressions_PredicateExpressions_TestId",
                table: "Expressions",
                column: "TestId",
                principalTable: "PredicateExpressions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PredicateExpressions_PredicateExpressions_FirstId",
                table: "PredicateExpressions",
                column: "FirstId",
                principalTable: "PredicateExpressions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PredicateExpressions_PredicateExpressions_SecondId",
                table: "PredicateExpressions",
                column: "SecondId",
                principalTable: "PredicateExpressions",
                principalColumn: "Id");
        }
    }
}
