using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookManager.Migrations
{
    /// <inheritdoc />
    public partial class Migration4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Author",
                table: "Books",
                newName: "author");

            migrationBuilder.RenameColumn(
                name: "title",
                table: "Books",
                newName: "name");

            migrationBuilder.AlterColumn<string>(
                name: "desciption",
                table: "Publishers",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "desciption",
                table: "Categorys",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "createAt",
                table: "Books",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "exemplary",
                table: "Books",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CustomLogs",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    operation = table.Column<string>(type: "text", nullable: false),
                    trace = table.Column<string>(type: "text", nullable: false),
                    createAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomLogs", x => x.id);
                });

            migrationBuilder.InsertData(
                table: "Categorys",
                columns: new[] { "id", "desciption" },
                values: new object[,]
                {
                    { 1, "Livro Tecnico" },
                    { 2, "Livro Informatica" }
                });

            migrationBuilder.InsertData(
                table: "Publishers",
                columns: new[] { "id", "desciption" },
                values: new object[,]
                {
                    { 1, "Editora Books" },
                    { 2, "EditoraBookman" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "id", "email", "name", "password", "role" },
                values: new object[,]
                {
                    { 5, "Marcos@gmail.com", "Marcos Simões", "dat@s35", "ADM" },
                    { 6, "optedev@gmail.com", "Dantas Rocha", "dat@s35", "ADM" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomLogs");

            migrationBuilder.DeleteData(
                table: "Categorys",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categorys",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: 6);

            migrationBuilder.DropColumn(
                name: "createAt",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "exemplary",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "author",
                table: "Books",
                newName: "Author");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Books",
                newName: "title");

            migrationBuilder.AlterColumn<string>(
                name: "desciption",
                table: "Publishers",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "desciption",
                table: "Categorys",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
