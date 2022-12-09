using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FarmFresh.Infrastructure.Data.Migrations
{
    public partial class AddRelationShipUserWithVoucher : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "Vouchers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Vouchers_CreatedBy",
                table: "Vouchers",
                column: "CreatedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_Vouchers_User_CreatedBy",
                table: "Vouchers",
                column: "CreatedBy",
                principalSchema: "dbo",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vouchers_User_CreatedBy",
                table: "Vouchers");

            migrationBuilder.DropIndex(
                name: "IX_Vouchers_CreatedBy",
                table: "Vouchers");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Vouchers");
        }
    }
}
