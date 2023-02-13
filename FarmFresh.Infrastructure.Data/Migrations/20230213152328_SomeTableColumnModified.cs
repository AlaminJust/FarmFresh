using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FarmFresh.Infrastructure.Data.Migrations
{
    public partial class SomeTableColumnModified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentStatus",
                schema: "dbo",
                table: "PaymentDetail");

            migrationBuilder.AddColumn<decimal>(
                name: "VoucherDiscount",
                schema: "dbo",
                table: "PaymentDetail",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VoucherId",
                schema: "dbo",
                table: "PaymentDetail",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<byte>(
                name: "OrderStatus",
                schema: "dbo",
                table: "Order",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.CreateIndex(
                name: "IX_PaymentDetail_VoucherId",
                schema: "dbo",
                table: "PaymentDetail",
                column: "VoucherId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentDetail_Voucher_VoucherId",
                schema: "dbo",
                table: "PaymentDetail",
                column: "VoucherId",
                principalSchema: "dbo",
                principalTable: "Voucher",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentDetail_Voucher_VoucherId",
                schema: "dbo",
                table: "PaymentDetail");

            migrationBuilder.DropIndex(
                name: "IX_PaymentDetail_VoucherId",
                schema: "dbo",
                table: "PaymentDetail");

            migrationBuilder.DropColumn(
                name: "VoucherDiscount",
                schema: "dbo",
                table: "PaymentDetail");

            migrationBuilder.DropColumn(
                name: "VoucherId",
                schema: "dbo",
                table: "PaymentDetail");

            migrationBuilder.DropColumn(
                name: "OrderStatus",
                schema: "dbo",
                table: "Order");

            migrationBuilder.AddColumn<byte>(
                name: "PaymentStatus",
                schema: "dbo",
                table: "PaymentDetail",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
        }
    }
}
