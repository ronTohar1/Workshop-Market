using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketBackend.Migrations
{
    public partial class StoreFounderAndProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DataPurchase_DataStore_DataStore",
                table: "DataPurchase");

            migrationBuilder.DropForeignKey(
                name: "FK_DataShoppingBag_DataStore_StoreId",
                table: "DataShoppingBag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DataStore",
                table: "DataStore");

            migrationBuilder.RenameTable(
                name: "DataStore",
                newName: "Stores");

            migrationBuilder.AddColumn<int>(
                name: "FounderMemberId",
                table: "Stores",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsOpen",
                table: "Stores",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Stores",
                table: "Stores",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "DataProduct",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataStoreId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataProduct", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DataProduct_Stores_DataStoreId",
                        column: x => x.DataStoreId,
                        principalTable: "Stores",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DataProduct_DataStoreId",
                table: "DataProduct",
                column: "DataStoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_DataPurchase_Stores_DataStore",
                table: "DataPurchase",
                column: "DataStore",
                principalTable: "Stores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DataShoppingBag_Stores_StoreId",
                table: "DataShoppingBag",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DataPurchase_Stores_DataStore",
                table: "DataPurchase");

            migrationBuilder.DropForeignKey(
                name: "FK_DataShoppingBag_Stores_StoreId",
                table: "DataShoppingBag");

            migrationBuilder.DropTable(
                name: "DataProduct");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Stores",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "FounderMemberId",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "IsOpen",
                table: "Stores");

            migrationBuilder.RenameTable(
                name: "Stores",
                newName: "DataStore");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DataStore",
                table: "DataStore",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DataPurchase_DataStore_DataStore",
                table: "DataPurchase",
                column: "DataStore",
                principalTable: "DataStore",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DataShoppingBag_DataStore_StoreId",
                table: "DataShoppingBag",
                column: "StoreId",
                principalTable: "DataStore",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
