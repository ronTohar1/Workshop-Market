using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketBackend.Migrations
{
    public partial class BidMemberToBidMemberId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bids_Members_MemberId",
                table: "Bids");

            migrationBuilder.DropIndex(
                name: "IX_Bids_MemberId",
                table: "Bids");

            migrationBuilder.AlterColumn<int>(
                name: "MemberId",
                table: "Bids",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "MemberId",
                table: "Bids",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Bids_MemberId",
                table: "Bids",
                column: "MemberId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bids_Members_MemberId",
                table: "Bids",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "Id");
        }
    }
}
