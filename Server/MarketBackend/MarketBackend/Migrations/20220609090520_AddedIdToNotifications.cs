using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketBackend.Migrations
{
    public partial class AddedIdToNotifications : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_DataNotification",
                table: "DataNotification");

            migrationBuilder.AlterColumn<string>(
                name: "Notification",
                table: "DataNotification",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "DataNotification",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DataNotification",
                table: "DataNotification",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_DataNotification",
                table: "DataNotification");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "DataNotification");

            migrationBuilder.AlterColumn<string>(
                name: "Notification",
                table: "DataNotification",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DataNotification",
                table: "DataNotification",
                column: "Notification");
        }
    }
}
