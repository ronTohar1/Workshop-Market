using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketBackend.Migrations
{
    public partial class DataStoreMemberRolesFiledToStoreAndPKToId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ManagerPermissions_StoreMemberRoles_DataStoreMemberRolesMemberId_DataStoreMemberRolesStoreId",
                table: "ManagerPermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_StoreMemberRoles_Stores_DataStoreId",
                table: "StoreMemberRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StoreMemberRoles",
                table: "StoreMemberRoles");

            migrationBuilder.DropIndex(
                name: "IX_StoreMemberRoles_DataStoreId",
                table: "StoreMemberRoles");

            migrationBuilder.DropIndex(
                name: "IX_ManagerPermissions_DataStoreMemberRolesMemberId_DataStoreMemberRolesStoreId",
                table: "ManagerPermissions");

            migrationBuilder.DropColumn(
                name: "DataStoreId",
                table: "StoreMemberRoles");

            migrationBuilder.DropColumn(
                name: "DataStoreMemberRolesMemberId",
                table: "ManagerPermissions");

            migrationBuilder.RenameColumn(
                name: "DataStoreMemberRolesStoreId",
                table: "ManagerPermissions",
                newName: "DataStoreMemberRolesId");

            migrationBuilder.AlterColumn<int>(
                name: "StoreId",
                table: "StoreMemberRoles",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "StoreMemberRoles",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StoreMemberRoles",
                table: "StoreMemberRoles",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_StoreMemberRoles_StoreId",
                table: "StoreMemberRoles",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_ManagerPermissions_DataStoreMemberRolesId",
                table: "ManagerPermissions",
                column: "DataStoreMemberRolesId");

            migrationBuilder.AddForeignKey(
                name: "FK_ManagerPermissions_StoreMemberRoles_DataStoreMemberRolesId",
                table: "ManagerPermissions",
                column: "DataStoreMemberRolesId",
                principalTable: "StoreMemberRoles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StoreMemberRoles_Stores_StoreId",
                table: "StoreMemberRoles",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ManagerPermissions_StoreMemberRoles_DataStoreMemberRolesId",
                table: "ManagerPermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_StoreMemberRoles_Stores_StoreId",
                table: "StoreMemberRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StoreMemberRoles",
                table: "StoreMemberRoles");

            migrationBuilder.DropIndex(
                name: "IX_StoreMemberRoles_StoreId",
                table: "StoreMemberRoles");

            migrationBuilder.DropIndex(
                name: "IX_ManagerPermissions_DataStoreMemberRolesId",
                table: "ManagerPermissions");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "StoreMemberRoles");

            migrationBuilder.RenameColumn(
                name: "DataStoreMemberRolesId",
                table: "ManagerPermissions",
                newName: "DataStoreMemberRolesStoreId");

            migrationBuilder.AlterColumn<int>(
                name: "StoreId",
                table: "StoreMemberRoles",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DataStoreId",
                table: "StoreMemberRoles",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DataStoreMemberRolesMemberId",
                table: "ManagerPermissions",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_StoreMemberRoles",
                table: "StoreMemberRoles",
                columns: new[] { "MemberId", "StoreId" });

            migrationBuilder.CreateIndex(
                name: "IX_StoreMemberRoles_DataStoreId",
                table: "StoreMemberRoles",
                column: "DataStoreId");

            migrationBuilder.CreateIndex(
                name: "IX_ManagerPermissions_DataStoreMemberRolesMemberId_DataStoreMemberRolesStoreId",
                table: "ManagerPermissions",
                columns: new[] { "DataStoreMemberRolesMemberId", "DataStoreMemberRolesStoreId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ManagerPermissions_StoreMemberRoles_DataStoreMemberRolesMemberId_DataStoreMemberRolesStoreId",
                table: "ManagerPermissions",
                columns: new[] { "DataStoreMemberRolesMemberId", "DataStoreMemberRolesStoreId" },
                principalTable: "StoreMemberRoles",
                principalColumns: new[] { "MemberId", "StoreId" });

            migrationBuilder.AddForeignKey(
                name: "FK_StoreMemberRoles_Stores_DataStoreId",
                table: "StoreMemberRoles",
                column: "DataStoreId",
                principalTable: "Stores",
                principalColumn: "Id");
        }
    }
}
