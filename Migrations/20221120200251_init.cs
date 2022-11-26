using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dorotecbackendtest.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "admin",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false),
                    login = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_admin", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "book",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    price = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "nvarchar(123)", maxLength: 123, nullable: false),
                    author = table.Column<string>(type: "nvarchar(123)", maxLength: 123, nullable: false),
                    genre = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false),
                    edition = table.Column<int>(type: "int", nullable: false),
                    pages = table.Column<int>(type: "int", nullable: false),
                    publishdate = table.Column<DateTime>(name: "publish_date", type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_book", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_admin_login",
                table: "admin",
                column: "login",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_admin_login_password",
                table: "admin",
                columns: new[] { "login", "password" });

            migrationBuilder.CreateIndex(
                name: "IX_book_author_name_genre_edition",
                table: "book",
                columns: new[] { "author", "name", "genre", "edition" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "admin");

            migrationBuilder.DropTable(
                name: "book");
        }
    }
}
