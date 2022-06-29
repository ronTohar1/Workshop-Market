using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketBackend.Migrations
{
    public partial class AddedDailyMarketStatistics : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DailyMarketStatistics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NumberOfAdminsLogin = table.Column<int>(type: "int", nullable: false),
                    NumberOfCoOwnersLogin = table.Column<int>(type: "int", nullable: false),
                    NumberOfManagersLogin = table.Column<int>(type: "int", nullable: false),
                    NumberOfMembersLogin = table.Column<int>(type: "int", nullable: false),
                    NumberOfGuestsLogin = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyMarketStatistics", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DailyMarketStatistics");
        }
    }
}
