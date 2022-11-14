using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FarmFresh.Infrastructure.Data.Migrations
{
    public partial class VendorAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductProductBrand",
                schema: "dbo");

            migrationBuilder.AddColumn<int>(
                name: "VendorId",
                schema: "dbo",
                table: "Product",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Vendor",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "date", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "date", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendor", x => x.Id);
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Vendor",
                columns: new[] { "Id", "CreatedOn", "IsDeleted", "Name", "PhoneNumber", "UpdatedOn" },
                values: new object[] { 1, new DateTime(2022, 11, 14, 8, 38, 30, 656, DateTimeKind.Local).AddTicks(3059), false, "Vendor 1", "1234567890", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Vendor",
                columns: new[] { "Id", "CreatedOn", "IsDeleted", "Name", "PhoneNumber", "UpdatedOn" },
                values: new object[] { 2, new DateTime(2022, 11, 14, 8, 38, 30, 656, DateTimeKind.Local).AddTicks(3059), false, "Vendor 2", "1234567890", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Vendor",
                columns: new[] { "Id", "CreatedOn", "IsDeleted", "Name", "PhoneNumber", "UpdatedOn" },
                values: new object[] { 3, new DateTime(2022, 11, 14, 8, 38, 30, 656, DateTimeKind.Local).AddTicks(3059), false, "Vendor 3", "1234567890", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.CreateIndex(
                name: "IX_Product_BrandId",
                schema: "dbo",
                table: "Product",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_VendorId",
                schema: "dbo",
                table: "Product",
                column: "VendorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_ProductBrand_BrandId",
                schema: "dbo",
                table: "Product",
                column: "BrandId",
                principalSchema: "dbo",
                principalTable: "ProductBrand",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Vendor_VendorId",
                schema: "dbo",
                table: "Product",
                column: "VendorId",
                principalSchema: "dbo",
                principalTable: "Vendor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_ProductBrand_BrandId",
                schema: "dbo",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Vendor_VendorId",
                schema: "dbo",
                table: "Product");

            migrationBuilder.DropTable(
                name: "Vendor",
                schema: "dbo");

            migrationBuilder.DropIndex(
                name: "IX_Product_BrandId",
                schema: "dbo",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_VendorId",
                schema: "dbo",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "VendorId",
                schema: "dbo",
                table: "Product");

            migrationBuilder.CreateTable(
                name: "ProductProductBrand",
                schema: "dbo",
                columns: table => new
                {
                    ProductBrandsId = table.Column<int>(type: "int", nullable: false),
                    ProductsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductProductBrand", x => new { x.ProductBrandsId, x.ProductsId });
                    table.ForeignKey(
                        name: "FK_ProductProductBrand_Product_ProductsId",
                        column: x => x.ProductsId,
                        principalSchema: "dbo",
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductProductBrand_ProductBrand_ProductBrandsId",
                        column: x => x.ProductBrandsId,
                        principalSchema: "dbo",
                        principalTable: "ProductBrand",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductProductBrand_ProductsId",
                schema: "dbo",
                table: "ProductProductBrand",
                column: "ProductsId");
        }
    }
}
