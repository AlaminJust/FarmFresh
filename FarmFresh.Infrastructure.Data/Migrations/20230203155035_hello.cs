using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FarmFresh.Infrastructure.Data.Migrations
{
    public partial class hello : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Discount",
                keyColumn: "Id",
                keyValue: 1,
                column: "DiscountValue",
                value: 2m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Discount",
                keyColumn: "Id",
                keyValue: 1,
                column: "DiscountValue",
                value: 100m);
        }
    }
}
