using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FarmFresh.Infrastructure.Data.Migrations
{
    public partial class SomeDesignChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItem_User_UserId",
                schema: "dbo",
                table: "CartItem");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Voucher_VoucherId",
                schema: "dbo",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_VoucherId",
                schema: "dbo",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "VoucherId",
                schema: "dbo",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "DiscountParcent",
                schema: "dbo",
                table: "Discount");

            migrationBuilder.RenameColumn(
                name: "Discount",
                schema: "dbo",
                table: "Voucher",
                newName: "VoucherValue");

            migrationBuilder.AddColumn<byte>(
                name: "DiscountType",
                schema: "dbo",
                table: "Discount",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<decimal>(
                name: "DiscountValue",
                schema: "dbo",
                table: "Discount",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                schema: "dbo",
                table: "CartItem",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "VoucherId",
                schema: "dbo",
                table: "Cart",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cart_VoucherId",
                schema: "dbo",
                table: "Cart",
                column: "VoucherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cart_Voucher_VoucherId",
                schema: "dbo",
                table: "Cart",
                column: "VoucherId",
                principalSchema: "dbo",
                principalTable: "Voucher",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItem_User_UserId",
                schema: "dbo",
                table: "CartItem",
                column: "UserId",
                principalSchema: "dbo",
                principalTable: "User",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cart_Voucher_VoucherId",
                schema: "dbo",
                table: "Cart");

            migrationBuilder.DropForeignKey(
                name: "FK_CartItem_User_UserId",
                schema: "dbo",
                table: "CartItem");

            migrationBuilder.DropIndex(
                name: "IX_Cart_VoucherId",
                schema: "dbo",
                table: "Cart");

            migrationBuilder.DropColumn(
                name: "DiscountType",
                schema: "dbo",
                table: "Discount");

            migrationBuilder.DropColumn(
                name: "DiscountValue",
                schema: "dbo",
                table: "Discount");

            migrationBuilder.DropColumn(
                name: "VoucherId",
                schema: "dbo",
                table: "Cart");

            migrationBuilder.RenameColumn(
                name: "VoucherValue",
                schema: "dbo",
                table: "Voucher",
                newName: "Discount");

            migrationBuilder.AddColumn<int>(
                name: "VoucherId",
                schema: "dbo",
                table: "Order",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "DiscountParcent",
                schema: "dbo",
                table: "Discount",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                schema: "dbo",
                table: "CartItem",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Order_VoucherId",
                schema: "dbo",
                table: "Order",
                column: "VoucherId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItem_User_UserId",
                schema: "dbo",
                table: "CartItem",
                column: "UserId",
                principalSchema: "dbo",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Voucher_VoucherId",
                schema: "dbo",
                table: "Order",
                column: "VoucherId",
                principalSchema: "dbo",
                principalTable: "Voucher",
                principalColumn: "Id");
        }
    }
}
