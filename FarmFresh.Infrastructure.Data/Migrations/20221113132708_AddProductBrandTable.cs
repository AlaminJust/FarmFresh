using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FarmFresh.Infrastructure.Data.Migrations
{
    public partial class AddProductBrandTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BrandId",
                schema: "dbo",
                table: "Product",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ProductBrand",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    ImageUrl = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "date", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductBrand", x => x.Id);
                });

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

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "ProductBrand",
                columns: new[] { "Id", "CreatedOn", "Description", "ImageUrl", "Name", "UpdatedOn" },
                values: new object[] { 1, new DateTime(2022, 11, 13, 8, 38, 30, 656, DateTimeKind.Local).AddTicks(3059), "Apple Brand", null, "Apple", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "ProductBrand",
                columns: new[] { "Id", "CreatedOn", "Description", "ImageUrl", "Name", "UpdatedOn" },
                values: new object[] { 2, new DateTime(2022, 11, 13, 8, 38, 30, 656, DateTimeKind.Local).AddTicks(3059), "Samsung Brand", null, "Samsung", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "ProductBrand",
                columns: new[] { "Id", "CreatedOn", "Description", "ImageUrl", "Name", "UpdatedOn" },
                values: new object[] { 3, new DateTime(2022, 11, 13, 8, 38, 30, 656, DateTimeKind.Local).AddTicks(3059), "Huawei Brand", null, "Huawei", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.CreateIndex(
                name: "IX_ProductProductBrand_ProductsId",
                schema: "dbo",
                table: "ProductProductBrand",
                column: "ProductsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductProductBrand",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ProductBrand",
                schema: "dbo");

            migrationBuilder.DropColumn(
                name: "BrandId",
                schema: "dbo",
                table: "Product");
        }
    }
}
