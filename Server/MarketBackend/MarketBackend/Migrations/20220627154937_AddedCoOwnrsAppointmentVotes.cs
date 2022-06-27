using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketBackend.Migrations
{
    public partial class AddedCoOwnrsAppointmentVotes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CoOwnerAppointmentApproving",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    newCoOwnerId = table.Column<int>(type: "int", nullable: false),
                    ApprovingMemberId = table.Column<int>(type: "int", nullable: false),
                    AppointedFirst = table.Column<bool>(type: "bit", nullable: false),
                    DataStoreId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoOwnerAppointmentApproving", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CoOwnerAppointmentApproving_Stores_DataStoreId",
                        column: x => x.DataStoreId,
                        principalTable: "Stores",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CoOwnerAppointmentApproving_DataStoreId",
                table: "CoOwnerAppointmentApproving",
                column: "DataStoreId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CoOwnerAppointmentApproving");
        }
    }
}
