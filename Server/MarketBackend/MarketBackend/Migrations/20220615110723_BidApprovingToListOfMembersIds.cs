using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketBackend.Migrations
{
    public partial class BidApprovingToListOfMembersIds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Members_Bids_DataBidId",
                table: "Members");

            migrationBuilder.DropIndex(
                name: "IX_Members_DataBidId",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "DataBidId",
                table: "Members");

            migrationBuilder.CreateTable(
                name: "DataBidMemberId",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MemberId = table.Column<int>(type: "int", nullable: false),
                    DataBidId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataBidMemberId", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DataBidMemberId_Bids_DataBidId",
                        column: x => x.DataBidId,
                        principalTable: "Bids",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DataBidMemberId_DataBidId",
                table: "DataBidMemberId",
                column: "DataBidId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DataBidMemberId");

            migrationBuilder.AddColumn<int>(
                name: "DataBidId",
                table: "Members",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Members_DataBidId",
                table: "Members",
                column: "DataBidId");

            migrationBuilder.AddForeignKey(
                name: "FK_Members_Bids_DataBidId",
                table: "Members",
                column: "DataBidId",
                principalTable: "Bids",
                principalColumn: "Id");
        }
    }
}
