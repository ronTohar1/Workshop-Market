using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketBackend.Migrations
{
    public partial class AddedBidMememberIdsDbSet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DataBidMemberId_Bids_DataBidId",
                table: "DataBidMemberId");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DataBidMemberId",
                table: "DataBidMemberId");

            migrationBuilder.RenameTable(
                name: "DataBidMemberId",
                newName: "BidMemberIds");

            migrationBuilder.RenameIndex(
                name: "IX_DataBidMemberId_DataBidId",
                table: "BidMemberIds",
                newName: "IX_BidMemberIds_DataBidId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BidMemberIds",
                table: "BidMemberIds",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BidMemberIds_Bids_DataBidId",
                table: "BidMemberIds",
                column: "DataBidId",
                principalTable: "Bids",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BidMemberIds_Bids_DataBidId",
                table: "BidMemberIds");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BidMemberIds",
                table: "BidMemberIds");

            migrationBuilder.RenameTable(
                name: "BidMemberIds",
                newName: "DataBidMemberId");

            migrationBuilder.RenameIndex(
                name: "IX_BidMemberIds_DataBidId",
                table: "DataBidMemberId",
                newName: "IX_DataBidMemberId_DataBidId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DataBidMemberId",
                table: "DataBidMemberId",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DataBidMemberId_Bids_DataBidId",
                table: "DataBidMemberId",
                column: "DataBidId",
                principalTable: "Bids",
                principalColumn: "Id");
        }
    }
}
