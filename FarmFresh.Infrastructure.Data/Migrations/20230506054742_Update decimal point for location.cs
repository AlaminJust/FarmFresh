using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FarmFresh.Infrastructure.Data.Migrations
{
    public partial class Updatedecimalpointforlocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Longitude",
                schema: "dbo",
                table: "Location",
                type: "decimal(18,8)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Latitude",
                schema: "dbo",
                table: "Location",
                type: "decimal(18,8)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Longitude",
                schema: "dbo",
                table: "Location",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,8)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Latitude",
                schema: "dbo",
                table: "Location",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,8)",
                oldNullable: true);
        }
    }
}
