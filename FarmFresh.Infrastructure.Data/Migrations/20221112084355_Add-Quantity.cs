using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FarmFresh.Infrastructure.Data.Migrations
{
    public partial class AddQuantity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                schema: "dbo",
                table: "Product",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Role",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 12, 14, 43, 55, 348, DateTimeKind.Local).AddTicks(4510));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Role",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 12, 14, 43, 55, 348, DateTimeKind.Local).AddTicks(4595));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                schema: "dbo",
                table: "Product");

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
    }
}
