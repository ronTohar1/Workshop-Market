using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketBackend.Migrations
{
    public partial class CompletedProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_DataManagerPermission",
                table: "DataManagerPermission");

            migrationBuilder.AddColumn<double>(
                name: "Productdicount",
                table: "DataProduct",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "DataManagerPermission",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DataManagerPermission",
                table: "DataManagerPermission",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "DataPurchaseOption",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PurchaseOption = table.Column<int>(type: "int", nullable: false),
                    DataProductId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataPurchaseOption", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DataPurchaseOption_DataProduct_DataProductId",
                        column: x => x.DataProductId,
                        principalTable: "DataProduct",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DataPurchaseOption_DataProductId",
                table: "DataPurchaseOption",
                column: "DataProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DataPurchaseOption");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DataManagerPermission",
                table: "DataManagerPermission");

            migrationBuilder.DropColumn(
                name: "Productdicount",
                table: "DataProduct");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "DataManagerPermission");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DataManagerPermission",
                table: "DataManagerPermission",
                column: "Permission");
        }
    }
}
