using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FarmFresh.Infrastructure.Data.Migrations
{
    public partial class AddInverseProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                schema: "dbo",
                table: "Product",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                schema: "dbo",
                table: "Product",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserCreatedById",
                schema: "dbo",
                table: "Product",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserUpdatedById",
                schema: "dbo",
                table: "Product",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Product_UserCreatedById",
                schema: "dbo",
                table: "Product",
                column: "UserCreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Product_UserUpdatedById",
                schema: "dbo",
                table: "Product",
                column: "UserUpdatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_User_UserCreatedById",
                schema: "dbo",
                table: "Product",
                column: "UserCreatedById",
                principalSchema: "dbo",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_User_UserUpdatedById",
                schema: "dbo",
                table: "Product",
                column: "UserUpdatedById",
                principalSchema: "dbo",
                principalTable: "User",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_User_UserCreatedById",
                schema: "dbo",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_User_UserUpdatedById",
                schema: "dbo",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_UserCreatedById",
                schema: "dbo",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_UserUpdatedById",
                schema: "dbo",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "dbo",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "dbo",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "UserCreatedById",
                schema: "dbo",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "UserUpdatedById",
                schema: "dbo",
                table: "Product");
        }
    }
}
