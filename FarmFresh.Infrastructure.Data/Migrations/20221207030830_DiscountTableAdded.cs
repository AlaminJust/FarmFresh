using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FarmFresh.Infrastructure.Data.Migrations
{
    public partial class DiscountTableAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DiscountId",
                schema: "dbo",
                table: "Product",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Discount",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    DiscountParcent = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "date", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "date", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discount", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Product_DiscountId",
                schema: "dbo",
                table: "Product",
                column: "DiscountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Discount_DiscountId",
                schema: "dbo",
                table: "Product",
                column: "DiscountId",
                principalSchema: "dbo",
                principalTable: "Discount",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Discount_DiscountId",
                schema: "dbo",
                table: "Product");

            migrationBuilder.DropTable(
                name: "Discount",
                schema: "dbo");

            migrationBuilder.DropIndex(
                name: "IX_Product_DiscountId",
                schema: "dbo",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "DiscountId",
                schema: "dbo",
                table: "Product");
        }
    }
}
