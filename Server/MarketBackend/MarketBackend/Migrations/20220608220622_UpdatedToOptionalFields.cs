using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketBackend.Migrations
{
    public partial class UpdatedToOptionalFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DataBid_DataProduct_ProductId",
                table: "DataBid");

            migrationBuilder.DropForeignKey(
                name: "FK_DataBid_Members_MemberId",
                table: "DataBid");

            migrationBuilder.DropForeignKey(
                name: "FK_DataProductReview_DataProduct_ProductId",
                table: "DataProductReview");

            migrationBuilder.DropForeignKey(
                name: "FK_DataProductReview_Members_MemberId",
                table: "DataProductReview");

            migrationBuilder.DropForeignKey(
                name: "FK_DataPurchase_Members_DataMember",
                table: "DataPurchase");

            migrationBuilder.DropForeignKey(
                name: "FK_DataPurchase_Stores_DataStore",
                table: "DataPurchase");

            migrationBuilder.DropForeignKey(
                name: "FK_DataShoppingBag_Stores_StoreId",
                table: "DataShoppingBag");

            migrationBuilder.DropForeignKey(
                name: "FK_Members_DataCart_CartId",
                table: "Members");

            migrationBuilder.DropForeignKey(
                name: "FK_Stores_DataAppointmentsNode_AppointmentsId",
                table: "Stores");

            migrationBuilder.AlterColumn<int>(
                name: "AppointmentsId",
                table: "Stores",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CartId",
                table: "Members",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "StoreId",
                table: "DataShoppingBag",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "DataStore",
                table: "DataPurchase",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "DataMember",
                table: "DataPurchase",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "DataProductReview",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "MemberId",
                table: "DataProductReview",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Permission",
                table: "DataManagerPermission",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "DataBid",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "MemberId",
                table: "DataBid",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_DataBid_DataProduct_ProductId",
                table: "DataBid",
                column: "ProductId",
                principalTable: "DataProduct",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DataBid_Members_MemberId",
                table: "DataBid",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DataProductReview_DataProduct_ProductId",
                table: "DataProductReview",
                column: "ProductId",
                principalTable: "DataProduct",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DataProductReview_Members_MemberId",
                table: "DataProductReview",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DataPurchase_Members_DataMember",
                table: "DataPurchase",
                column: "DataMember",
                principalTable: "Members",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DataPurchase_Stores_DataStore",
                table: "DataPurchase",
                column: "DataStore",
                principalTable: "Stores",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DataShoppingBag_Stores_StoreId",
                table: "DataShoppingBag",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Members_DataCart_CartId",
                table: "Members",
                column: "CartId",
                principalTable: "DataCart",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Stores_DataAppointmentsNode_AppointmentsId",
                table: "Stores",
                column: "AppointmentsId",
                principalTable: "DataAppointmentsNode",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DataBid_DataProduct_ProductId",
                table: "DataBid");

            migrationBuilder.DropForeignKey(
                name: "FK_DataBid_Members_MemberId",
                table: "DataBid");

            migrationBuilder.DropForeignKey(
                name: "FK_DataProductReview_DataProduct_ProductId",
                table: "DataProductReview");

            migrationBuilder.DropForeignKey(
                name: "FK_DataProductReview_Members_MemberId",
                table: "DataProductReview");

            migrationBuilder.DropForeignKey(
                name: "FK_DataPurchase_Members_DataMember",
                table: "DataPurchase");

            migrationBuilder.DropForeignKey(
                name: "FK_DataPurchase_Stores_DataStore",
                table: "DataPurchase");

            migrationBuilder.DropForeignKey(
                name: "FK_DataShoppingBag_Stores_StoreId",
                table: "DataShoppingBag");

            migrationBuilder.DropForeignKey(
                name: "FK_Members_DataCart_CartId",
                table: "Members");

            migrationBuilder.DropForeignKey(
                name: "FK_Stores_DataAppointmentsNode_AppointmentsId",
                table: "Stores");

            migrationBuilder.AlterColumn<int>(
                name: "AppointmentsId",
                table: "Stores",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CartId",
                table: "Members",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "StoreId",
                table: "DataShoppingBag",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DataStore",
                table: "DataPurchase",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DataMember",
                table: "DataPurchase",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "DataProductReview",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MemberId",
                table: "DataProductReview",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Permission",
                table: "DataManagerPermission",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "DataBid",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MemberId",
                table: "DataBid",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DataBid_DataProduct_ProductId",
                table: "DataBid",
                column: "ProductId",
                principalTable: "DataProduct",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DataBid_Members_MemberId",
                table: "DataBid",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DataProductReview_DataProduct_ProductId",
                table: "DataProductReview",
                column: "ProductId",
                principalTable: "DataProduct",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DataProductReview_Members_MemberId",
                table: "DataProductReview",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DataPurchase_Members_DataMember",
                table: "DataPurchase",
                column: "DataMember",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DataPurchase_Stores_DataStore",
                table: "DataPurchase",
                column: "DataStore",
                principalTable: "Stores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DataShoppingBag_Stores_StoreId",
                table: "DataShoppingBag",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Members_DataCart_CartId",
                table: "Members",
                column: "CartId",
                principalTable: "DataCart",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stores_DataAppointmentsNode_AppointmentsId",
                table: "Stores",
                column: "AppointmentsId",
                principalTable: "DataAppointmentsNode",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
