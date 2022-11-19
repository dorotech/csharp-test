using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Desafio_Backend.Migrations
{
    public partial class CorrecaoTabelaLivro_Autor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "id",
                table: "Livro_Autor",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<string>(
                name: "urlCapa",
                table: "Livro",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "nome",
                table: "Livro",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "descricao",
                table: "Livro",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "nome",
                table: "Genero",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "descricao",
                table: "Genero",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "nome",
                table: "Autor",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "descricao",
                table: "Autor",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Livro_Autor",
                table: "Livro_Autor",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_Livro_Autor_idAutor",
                table: "Livro_Autor",
                column: "idAutor");

            migrationBuilder.CreateIndex(
                name: "IX_Livro_Autor_idLivro",
                table: "Livro_Autor",
                column: "idLivro");

            migrationBuilder.CreateIndex(
                name: "IX_Livro_idGenero",
                table: "Livro",
                column: "idGenero");

            migrationBuilder.AddForeignKey(
                name: "FK_Livro_Genero_idGenero",
                table: "Livro",
                column: "idGenero",
                principalTable: "Genero",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Livro_Autor_Autor_idAutor",
                table: "Livro_Autor",
                column: "idAutor",
                principalTable: "Autor",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Livro_Autor_Livro_idLivro",
                table: "Livro_Autor",
                column: "idLivro",
                principalTable: "Livro",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Livro_Genero_idGenero",
                table: "Livro");

            migrationBuilder.DropForeignKey(
                name: "FK_Livro_Autor_Autor_idAutor",
                table: "Livro_Autor");

            migrationBuilder.DropForeignKey(
                name: "FK_Livro_Autor_Livro_idLivro",
                table: "Livro_Autor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Livro_Autor",
                table: "Livro_Autor");

            migrationBuilder.DropIndex(
                name: "IX_Livro_Autor_idAutor",
                table: "Livro_Autor");

            migrationBuilder.DropIndex(
                name: "IX_Livro_Autor_idLivro",
                table: "Livro_Autor");

            migrationBuilder.DropIndex(
                name: "IX_Livro_idGenero",
                table: "Livro");

            migrationBuilder.DropColumn(
                name: "id",
                table: "Livro_Autor");

            migrationBuilder.AlterColumn<string>(
                name: "urlCapa",
                table: "Livro",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "nome",
                table: "Livro",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "descricao",
                table: "Livro",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "nome",
                table: "Genero",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "descricao",
                table: "Genero",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "nome",
                table: "Autor",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "descricao",
                table: "Autor",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
