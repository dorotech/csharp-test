using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Desafio_Backend.Migrations
{
    public partial class CorrecaoTabelaLivro : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "dataPublicacao",
                table: "Livro");

            migrationBuilder.AddColumn<int>(
                name: "anoPublicacao",
                table: "Livro",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "anoPublicacao",
                table: "Livro");

            migrationBuilder.AddColumn<DateTime>(
                name: "dataPublicacao",
                table: "Livro",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
