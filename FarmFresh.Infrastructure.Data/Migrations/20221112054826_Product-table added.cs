using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FarmFresh.Infrastructure.Data.Migrations
{
    public partial class Producttableadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "OldPrice",
                schema: "dbo",
                table: "Product",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Role",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 12, 11, 48, 26, 718, DateTimeKind.Local).AddTicks(297));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Role",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 12, 11, 48, 26, 718, DateTimeKind.Local).AddTicks(378));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OldPrice",
                schema: "dbo",
                table: "Product");

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Role",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 12, 9, 11, 10, 562, DateTimeKind.Local).AddTicks(8061));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Role",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 12, 9, 11, 10, 562, DateTimeKind.Local).AddTicks(8137));
        }
    }
}
