using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FarmFresh.Infrastructure.Data.Migrations
{
    public partial class AddSeedValueForProductCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "dbo",
                table: "ProductCategory",
                columns: new[] { "Id", "CategoryDescription", "CategoryName", "CreatedOn", "IsDeleted", "ParentCategoryId", "UpdatedOn" },
                values: new object[] { 1, "Mobile Category", "Mobile", new DateTime(2022, 11, 14, 8, 38, 30, 656, DateTimeKind.Local).AddTicks(3059), false, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "ProductCategory",
                columns: new[] { "Id", "CategoryDescription", "CategoryName", "CreatedOn", "IsDeleted", "ParentCategoryId", "UpdatedOn" },
                values: new object[] { 2, "Laptop Category", "Laptop", new DateTime(2022, 11, 14, 8, 38, 30, 656, DateTimeKind.Local).AddTicks(3059), false, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "ProductCategory",
                columns: new[] { "Id", "CategoryDescription", "CategoryName", "CreatedOn", "IsDeleted", "ParentCategoryId", "UpdatedOn" },
                values: new object[] { 3, "Tablet Category", "Tablet", new DateTime(2022, 11, 14, 8, 38, 30, 656, DateTimeKind.Local).AddTicks(3059), false, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "ProductCategory",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "ProductCategory",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "ProductCategory",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
