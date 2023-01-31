using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FarmFresh.Infrastructure.Data.Migrations
{
    public partial class addseedvaluefordiscounttable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Discount",
                columns: new[] { "Id", "CreatedOn", "Description", "DiscountType", "DiscountValue", "IsDeleted", "Name", "UpdatedOn" },
                values: new object[] { 1, new DateTime(2023, 1, 31, 8, 38, 30, 656, DateTimeKind.Local).AddTicks(3059), "Only allow for a fixed price", (byte)2, 100m, false, "Fixed price", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Discount",
                columns: new[] { "Id", "CreatedOn", "Description", "DiscountType", "DiscountValue", "IsDeleted", "Name", "UpdatedOn" },
                values: new object[] { 2, new DateTime(2023, 1, 31, 8, 38, 30, 656, DateTimeKind.Local).AddTicks(3059), "Discount by percentage for a special product", (byte)1, 10m, false, "Fixed price", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Discount",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Discount",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
