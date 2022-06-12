using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketBackend.Migrations
{
    public partial class AddedBidsToStore : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DataBidId",
                table: "Members",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DataBid",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    MemberId = table.Column<int>(type: "int", nullable: false),
                    Bid = table.Column<double>(type: "float", nullable: false),
                    CounterOffer = table.Column<bool>(type: "bit", nullable: false),
                    Offer = table.Column<double>(type: "float", nullable: false),
                    DataStoreId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataBid", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DataBid_DataProduct_ProductId",
                        column: x => x.ProductId,
                        principalTable: "DataProduct",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DataBid_Members_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DataBid_Stores_DataStoreId",
                        column: x => x.DataStoreId,
                        principalTable: "Stores",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Members_DataBidId",
                table: "Members",
                column: "DataBidId");

            migrationBuilder.CreateIndex(
                name: "IX_DataBid_DataStoreId",
                table: "DataBid",
                column: "DataStoreId");

            migrationBuilder.CreateIndex(
                name: "IX_DataBid_MemberId",
                table: "DataBid",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_DataBid_ProductId",
                table: "DataBid",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Members_DataBid_DataBidId",
                table: "Members",
                column: "DataBidId",
                principalTable: "DataBid",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Members_DataBid_DataBidId",
                table: "Members");

            migrationBuilder.DropTable(
                name: "DataBid");

            migrationBuilder.DropIndex(
                name: "IX_Members_DataBidId",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "DataBidId",
                table: "Members");
        }
    }
}
