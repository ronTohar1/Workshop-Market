using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketBackend.Migrations
{
    public partial class ShoppingBags : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DataStore",
                table: "DataPurchase",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "DataStore",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataStore", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DataShoppingBag",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoreId = table.Column<int>(type: "int", nullable: false),
                    DataCartId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataShoppingBag", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DataShoppingBag_DataCart_DataCartId",
                        column: x => x.DataCartId,
                        principalTable: "DataCart",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DataShoppingBag_DataStore_StoreId",
                        column: x => x.StoreId,
                        principalTable: "DataStore",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DataProductInBag",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    StoreId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    DataShoppingBagId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataProductInBag", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DataProductInBag_DataShoppingBag_DataShoppingBagId",
                        column: x => x.DataShoppingBagId,
                        principalTable: "DataShoppingBag",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DataPurchase_DataStore",
                table: "DataPurchase",
                column: "DataStore");

            migrationBuilder.CreateIndex(
                name: "IX_DataProductInBag_DataShoppingBagId",
                table: "DataProductInBag",
                column: "DataShoppingBagId");

            migrationBuilder.CreateIndex(
                name: "IX_DataShoppingBag_DataCartId",
                table: "DataShoppingBag",
                column: "DataCartId");

            migrationBuilder.CreateIndex(
                name: "IX_DataShoppingBag_StoreId",
                table: "DataShoppingBag",
                column: "StoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_DataPurchase_DataStore_DataStore",
                table: "DataPurchase",
                column: "DataStore",
                principalTable: "DataStore",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DataPurchase_DataStore_DataStore",
                table: "DataPurchase");

            migrationBuilder.DropTable(
                name: "DataProductInBag");

            migrationBuilder.DropTable(
                name: "DataShoppingBag");

            migrationBuilder.DropTable(
                name: "DataStore");

            migrationBuilder.DropIndex(
                name: "IX_DataPurchase_DataStore",
                table: "DataPurchase");

            migrationBuilder.DropColumn(
                name: "DataStore",
                table: "DataPurchase");
        }
    }
}
