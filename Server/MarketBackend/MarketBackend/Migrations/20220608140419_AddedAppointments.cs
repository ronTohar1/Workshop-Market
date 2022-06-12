using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketBackend.Migrations
{
    public partial class AddedAppointments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AppointmentsId",
                table: "Stores",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DataAppointmentsNodeId",
                table: "DataStoreMemberRoles",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DataAppointmentsNode",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MemberId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataAppointmentsNode", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Stores_AppointmentsId",
                table: "Stores",
                column: "AppointmentsId");

            migrationBuilder.CreateIndex(
                name: "IX_DataStoreMemberRoles_DataAppointmentsNodeId",
                table: "DataStoreMemberRoles",
                column: "DataAppointmentsNodeId");

            migrationBuilder.AddForeignKey(
                name: "FK_DataStoreMemberRoles_DataAppointmentsNode_DataAppointmentsNodeId",
                table: "DataStoreMemberRoles",
                column: "DataAppointmentsNodeId",
                principalTable: "DataAppointmentsNode",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Stores_DataAppointmentsNode_AppointmentsId",
                table: "Stores",
                column: "AppointmentsId",
                principalTable: "DataAppointmentsNode",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DataStoreMemberRoles_DataAppointmentsNode_DataAppointmentsNodeId",
                table: "DataStoreMemberRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_Stores_DataAppointmentsNode_AppointmentsId",
                table: "Stores");

            migrationBuilder.DropTable(
                name: "DataAppointmentsNode");

            migrationBuilder.DropIndex(
                name: "IX_Stores_AppointmentsId",
                table: "Stores");

            migrationBuilder.DropIndex(
                name: "IX_DataStoreMemberRoles_DataAppointmentsNodeId",
                table: "DataStoreMemberRoles");

            migrationBuilder.DropColumn(
                name: "AppointmentsId",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "DataAppointmentsNodeId",
                table: "DataStoreMemberRoles");
        }
    }
}
