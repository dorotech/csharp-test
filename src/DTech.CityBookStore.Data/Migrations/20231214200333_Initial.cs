using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DTech.CityBookStore.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false),
                    Author = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false),
                    Language = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Edition = table.Column<int>(type: "int", nullable: false),
                    Pages = table.Column<int>(type: "int", nullable: false),
                    Publishing = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    ISBN10 = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    ISBN13 = table.Column<string>(type: "varchar(14)", maxLength: 14, nullable: false),
                    DimensionLength = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    DimensionHeight = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    DimensionWidth = table.Column<decimal>(type: "decimal(5,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "varchar(350)", maxLength: 350, nullable: false),
                    Login = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false),
                    Email = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false),
                    Password = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset(7)", nullable: false),
                    LastLoginDate = table.Column<DateTimeOffset>(type: "datetimeoffset(7)", nullable: true),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Book_Unique_ISBN10",
                table: "Books",
                column: "ISBN10",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Book_Unique_ISBN13",
                table: "Books",
                column: "ISBN13",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Unique_Login",
                table: "Users",
                column: "Login",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
