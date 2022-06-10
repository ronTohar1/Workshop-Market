using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketBackend.Migrations
{
    public partial class AddedMostOfProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AmountInInventory",
                table: "DataProduct",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "DataProduct",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "PricePerUnit",
                table: "DataProduct",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "DataProductReview",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MemberId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataProductReview", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DataProductReview_DataProduct_ProductId",
                        column: x => x.ProductId,
                        principalTable: "DataProduct",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DataProductReview_Members_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DataProductReview_MemberId",
                table: "DataProductReview",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_DataProductReview_ProductId",
                table: "DataProductReview",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DataProductReview");

            migrationBuilder.DropColumn(
                name: "AmountInInventory",
                table: "DataProduct");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "DataProduct");

            migrationBuilder.DropColumn(
                name: "PricePerUnit",
                table: "DataProduct");
        }
    }
}
