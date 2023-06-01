using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FarmFresh.Infrastructure.Data.Migrations
{
    public partial class AddProductHistoryTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductHistory",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Point = table.Column<float>(type: "real", nullable: false),
                    UpdateBy = table.Column<int>(type: "int", nullable: false),
                    HistoryType = table.Column<byte>(type: "tinyint", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "date", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "date", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductHistory_Product_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "dbo",
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductHistory_User_UpdateBy",
                        column: x => x.UpdateBy,
                        principalSchema: "dbo",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductHistory_ProductId",
                schema: "dbo",
                table: "ProductHistory",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductHistory_UpdateBy",
                schema: "dbo",
                table: "ProductHistory",
                column: "UpdateBy");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductHistory",
                schema: "dbo");
        }
    }
}
