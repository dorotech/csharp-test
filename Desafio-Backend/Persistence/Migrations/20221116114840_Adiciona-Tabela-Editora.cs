using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Desafio_Backend.Migrations
{
    public partial class AdicionaTabelaEditora : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "idEditora",
                table: "Livro",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Editora",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nome = table.Column<string>(type: "text", nullable: false),
                    descricao = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Editora", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Livro_idEditora",
                table: "Livro",
                column: "idEditora");

            migrationBuilder.AddForeignKey(
                name: "FK_Livro_Editora_idEditora",
                table: "Livro",
                column: "idEditora",
                principalTable: "Editora",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Livro_Editora_idEditora",
                table: "Livro");

            migrationBuilder.DropTable(
                name: "Editora");

            migrationBuilder.DropIndex(
                name: "IX_Livro_idEditora",
                table: "Livro");

            migrationBuilder.DropColumn(
                name: "idEditora",
                table: "Livro");
        }
    }
}
