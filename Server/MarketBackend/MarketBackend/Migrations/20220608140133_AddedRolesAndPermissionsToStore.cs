using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketBackend.Migrations
{
    public partial class AddedRolesAndPermissionsToStore : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DataStoreMemberRoles",
                columns: table => new
                {
                    StoreId = table.Column<int>(type: "int", nullable: false),
                    MemberId = table.Column<int>(type: "int", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    DataStoreId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataStoreMemberRoles", x => new { x.MemberId, x.StoreId });
                    table.ForeignKey(
                        name: "FK_DataStoreMemberRoles_Stores_DataStoreId",
                        column: x => x.DataStoreId,
                        principalTable: "Stores",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DataManagerPermission",
                columns: table => new
                {
                    Permission = table.Column<int>(type: "int", nullable: false),
                    DataStoreMemberRolesMemberId = table.Column<int>(type: "int", nullable: true),
                    DataStoreMemberRolesStoreId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataManagerPermission", x => x.Permission);
                    table.ForeignKey(
                        name: "FK_DataManagerPermission_DataStoreMemberRoles_DataStoreMemberRolesMemberId_DataStoreMemberRolesStoreId",
                        columns: x => new { x.DataStoreMemberRolesMemberId, x.DataStoreMemberRolesStoreId },
                        principalTable: "DataStoreMemberRoles",
                        principalColumns: new[] { "MemberId", "StoreId" });
                });

            migrationBuilder.CreateIndex(
                name: "IX_DataManagerPermission_DataStoreMemberRolesMemberId_DataStoreMemberRolesStoreId",
                table: "DataManagerPermission",
                columns: new[] { "DataStoreMemberRolesMemberId", "DataStoreMemberRolesStoreId" });

            migrationBuilder.CreateIndex(
                name: "IX_DataStoreMemberRoles_DataStoreId",
                table: "DataStoreMemberRoles",
                column: "DataStoreId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DataManagerPermission");

            migrationBuilder.DropTable(
                name: "DataStoreMemberRoles");
        }
    }
}
