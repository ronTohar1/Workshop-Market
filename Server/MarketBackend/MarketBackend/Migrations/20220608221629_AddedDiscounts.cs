using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketBackend.Migrations
{
    public partial class AddedDiscounts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DiscountManagerId",
                table: "Stores",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "DataStoreDiscountPolicyManager",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataStoreDiscountPolicyManager", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PredicateExpressions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstId = table.Column<int>(type: "int", nullable: true),
                    SecondId = table.Column<int>(type: "int", nullable: true),
                    Worth = table.Column<int>(type: "int", nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PredicateExpressions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PredicateExpressions_PredicateExpressions_FirstId",
                        column: x => x.FirstId,
                        principalTable: "PredicateExpressions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PredicateExpressions_PredicateExpressions_SecondId",
                        column: x => x.SecondId,
                        principalTable: "PredicateExpressions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Expressions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PredicateId = table.Column<int>(type: "int", nullable: true),
                    DiscountExpressionId = table.Column<int>(type: "int", nullable: true),
                    TestId = table.Column<int>(type: "int", nullable: true),
                    ThenId = table.Column<int>(type: "int", nullable: true),
                    ElseId = table.Column<int>(type: "int", nullable: true),
                    DataMaxExpressionId = table.Column<int>(type: "int", nullable: true),
                    Discount = table.Column<int>(type: "int", nullable: true),
                    Year = table.Column<int>(type: "int", nullable: true),
                    Month = table.Column<int>(type: "int", nullable: true),
                    Day = table.Column<int>(type: "int", nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expressions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Expressions_Expressions_DataMaxExpressionId",
                        column: x => x.DataMaxExpressionId,
                        principalTable: "Expressions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Expressions_Expressions_DiscountExpressionId",
                        column: x => x.DiscountExpressionId,
                        principalTable: "Expressions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Expressions_Expressions_ElseId",
                        column: x => x.ElseId,
                        principalTable: "Expressions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Expressions_Expressions_ThenId",
                        column: x => x.ThenId,
                        principalTable: "Expressions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Expressions_PredicateExpressions_PredicateId",
                        column: x => x.PredicateId,
                        principalTable: "PredicateExpressions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Expressions_PredicateExpressions_TestId",
                        column: x => x.TestId,
                        principalTable: "PredicateExpressions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Discounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiscountExpressionId = table.Column<int>(type: "int", nullable: true),
                    DataStoreDiscountPolicyManagerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Discounts_DataStoreDiscountPolicyManager_DataStoreDiscountPolicyManagerId",
                        column: x => x.DataStoreDiscountPolicyManagerId,
                        principalTable: "DataStoreDiscountPolicyManager",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Discounts_Expressions_DiscountExpressionId",
                        column: x => x.DiscountExpressionId,
                        principalTable: "Expressions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Stores_DiscountManagerId",
                table: "Stores",
                column: "DiscountManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_Discounts_DataStoreDiscountPolicyManagerId",
                table: "Discounts",
                column: "DataStoreDiscountPolicyManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_Discounts_DiscountExpressionId",
                table: "Discounts",
                column: "DiscountExpressionId");

            migrationBuilder.CreateIndex(
                name: "IX_Expressions_DataMaxExpressionId",
                table: "Expressions",
                column: "DataMaxExpressionId");

            migrationBuilder.CreateIndex(
                name: "IX_Expressions_DiscountExpressionId",
                table: "Expressions",
                column: "DiscountExpressionId");

            migrationBuilder.CreateIndex(
                name: "IX_Expressions_ElseId",
                table: "Expressions",
                column: "ElseId");

            migrationBuilder.CreateIndex(
                name: "IX_Expressions_PredicateId",
                table: "Expressions",
                column: "PredicateId");

            migrationBuilder.CreateIndex(
                name: "IX_Expressions_TestId",
                table: "Expressions",
                column: "TestId");

            migrationBuilder.CreateIndex(
                name: "IX_Expressions_ThenId",
                table: "Expressions",
                column: "ThenId");

            migrationBuilder.CreateIndex(
                name: "IX_PredicateExpressions_FirstId",
                table: "PredicateExpressions",
                column: "FirstId");

            migrationBuilder.CreateIndex(
                name: "IX_PredicateExpressions_SecondId",
                table: "PredicateExpressions",
                column: "SecondId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stores_DataStoreDiscountPolicyManager_DiscountManagerId",
                table: "Stores",
                column: "DiscountManagerId",
                principalTable: "DataStoreDiscountPolicyManager",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stores_DataStoreDiscountPolicyManager_DiscountManagerId",
                table: "Stores");

            migrationBuilder.DropTable(
                name: "Discounts");

            migrationBuilder.DropTable(
                name: "DataStoreDiscountPolicyManager");

            migrationBuilder.DropTable(
                name: "Expressions");

            migrationBuilder.DropTable(
                name: "PredicateExpressions");

            migrationBuilder.DropIndex(
                name: "IX_Stores_DiscountManagerId",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "DiscountManagerId",
                table: "Stores");
        }
    }
}
