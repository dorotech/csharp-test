using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class dois : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "StockMoviments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StockMoviments_IsDeleted",
                table: "StockMoviments",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Publishers_IsDeleted",
                table: "Publishers",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_IsDeleted",
                table: "Categories",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Books_IsDeleted",
                table: "Books",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Authors_IsDeleted",
                table: "Authors",
                column: "IsDeleted");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_StockMoviments_IsDeleted",
                table: "StockMoviments");

            migrationBuilder.DropIndex(
                name: "IX_Publishers_IsDeleted",
                table: "Publishers");

            migrationBuilder.DropIndex(
                name: "IX_Categories_IsDeleted",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Books_IsDeleted",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Authors_IsDeleted",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "StockMoviments");
        }
    }
}
