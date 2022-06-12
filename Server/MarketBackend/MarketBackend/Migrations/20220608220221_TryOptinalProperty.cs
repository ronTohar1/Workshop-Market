using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketBackend.Migrations
{
    public partial class TryOptinalProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FounderMemberId",
                table: "Stores");

            migrationBuilder.RenameColumn(
                name: "Productdicount",
                table: "DataProduct",
                newName: "ProductDiscount");

            migrationBuilder.AddColumn<int>(
                name: "FounderId",
                table: "Stores",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Stores_FounderId",
                table: "Stores",
                column: "FounderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stores_Members_FounderId",
                table: "Stores",
                column: "FounderId",
                principalTable: "Members",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stores_Members_FounderId",
                table: "Stores");

            migrationBuilder.DropIndex(
                name: "IX_Stores_FounderId",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "FounderId",
                table: "Stores");

            migrationBuilder.RenameColumn(
                name: "ProductDiscount",
                table: "DataProduct",
                newName: "Productdicount");

            migrationBuilder.AddColumn<int>(
                name: "FounderMemberId",
                table: "Stores",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
