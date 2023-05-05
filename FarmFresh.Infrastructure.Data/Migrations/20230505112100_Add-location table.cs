using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FarmFresh.Infrastructure.Data.Migrations
{
    public partial class Addlocationtable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Location",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Latitude = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Longitude = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Speed = table.Column<float>(type: "real", nullable: true),
                    Altitude = table.Column<double>(type: "float", nullable: true),
                    Accuracy = table.Column<float>(type: "real", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    State = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Country = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ZipCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    LocationType = table.Column<byte>(type: "tinyint", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "date", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "date", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Location_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Location_UserId",
                schema: "dbo",
                table: "Location",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Location",
                schema: "dbo");
        }
    }
}
