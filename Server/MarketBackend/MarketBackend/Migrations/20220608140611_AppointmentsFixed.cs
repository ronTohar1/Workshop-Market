using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketBackend.Migrations
{
    public partial class AppointmentsFixed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DataStoreMemberRoles_DataAppointmentsNode_DataAppointmentsNodeId",
                table: "DataStoreMemberRoles");

            migrationBuilder.DropIndex(
                name: "IX_DataStoreMemberRoles_DataAppointmentsNodeId",
                table: "DataStoreMemberRoles");

            migrationBuilder.DropColumn(
                name: "DataAppointmentsNodeId",
                table: "DataStoreMemberRoles");

            migrationBuilder.AddColumn<int>(
                name: "DataAppointmentsNodeId",
                table: "DataAppointmentsNode",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DataAppointmentsNode_DataAppointmentsNodeId",
                table: "DataAppointmentsNode",
                column: "DataAppointmentsNodeId");

            migrationBuilder.AddForeignKey(
                name: "FK_DataAppointmentsNode_DataAppointmentsNode_DataAppointmentsNodeId",
                table: "DataAppointmentsNode",
                column: "DataAppointmentsNodeId",
                principalTable: "DataAppointmentsNode",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DataAppointmentsNode_DataAppointmentsNode_DataAppointmentsNodeId",
                table: "DataAppointmentsNode");

            migrationBuilder.DropIndex(
                name: "IX_DataAppointmentsNode_DataAppointmentsNodeId",
                table: "DataAppointmentsNode");

            migrationBuilder.DropColumn(
                name: "DataAppointmentsNodeId",
                table: "DataAppointmentsNode");

            migrationBuilder.AddColumn<int>(
                name: "DataAppointmentsNodeId",
                table: "DataStoreMemberRoles",
                type: "int",
                nullable: true);

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
        }
    }
}
