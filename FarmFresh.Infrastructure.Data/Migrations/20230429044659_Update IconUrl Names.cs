using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FarmFresh.Infrastructure.Data.Migrations
{
    public partial class UpdateIconUrlNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Icon",
                schema: "dbo",
                table: "ProductCategory",
                newName: "IconUrl");

            migrationBuilder.AlterColumn<string>(
                name: "IconUrl",
                schema: "dbo",
                table: "ProductCategory",
                type: "nvarchar(264)",
                maxLength: 264,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IconUrl",
                schema: "dbo",
                table: "ProductCategory",
                newName: "Icon");

            migrationBuilder.AlterColumn<string>(
                name: "Icon",
                schema: "dbo",
                table: "ProductCategory",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(264)",
                oldMaxLength: 264,
                oldNullable: true);
        }
    }
}
