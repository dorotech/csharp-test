using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoroTechChallenge.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TAB_AUTORES",
                columns: table => new
                {
                    ID_AUTOR = table.Column<int>(type: "INT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NOME_AUTOR = table.Column<string>(type: "VARCHAR(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TAB_AUTORES", x => x.ID_AUTOR);
                });

            migrationBuilder.CreateTable(
                name: "TAB_EDITORA",
                columns: table => new
                {
                    ID_EDITORA = table.Column<int>(type: "INT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NOME_EDITORA = table.Column<string>(type: "VARCHAR(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TAB_EDITORA", x => x.ID_EDITORA);
                });

            migrationBuilder.CreateTable(
                name: "TAB_GENEROS",
                columns: table => new
                {
                    ID_GENERO = table.Column<int>(type: "INT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NOME_GENERO = table.Column<string>(type: "VARCHAR(30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TAB_GENEROS", x => x.ID_GENERO);
                });

            migrationBuilder.CreateTable(
                name: "TAB_LIVROS",
                columns: table => new
                {
                    ID_LIVRO = table.Column<int>(type: "INT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DESCRICACO = table.Column<string>(type: "VARCHAR(MAX)", nullable: false),
                    DATA_PUBLICACAO = table.Column<string>(type: "VARCHAR(30)", nullable: false),
                    ID_AUTOR = table.Column<int>(type: "INT", nullable: false),
                    ID_GENERO = table.Column<int>(type: "INT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TAB_LIVROS", x => x.ID_LIVRO);
                    table.ForeignKey(
                        name: "FK_TAB_LIVROS_TAB_AUTORES_ID_AUTOR",
                        column: x => x.ID_AUTOR,
                        principalTable: "TAB_AUTORES",
                        principalColumn: "ID_AUTOR",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TAB_LIVROS_TAB_GENEROS_ID_GENERO",
                        column: x => x.ID_GENERO,
                        principalTable: "TAB_GENEROS",
                        principalColumn: "ID_GENERO",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TAB_BOOK_PUBLISHING_COMPANY",
                columns: table => new
                {
                    ID_BOOK_PUBLISHING_COMPANY = table.Column<int>(type: "INT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BOOK_ID = table.Column<int>(type: "INT", nullable: false),
                    PUBLISHING_COMPANY_ID = table.Column<int>(type: "INT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TAB_BOOK_PUBLISHING_COMPANY", x => x.ID_BOOK_PUBLISHING_COMPANY);
                    table.ForeignKey(
                        name: "FK_TAB_BOOK_PUBLISHING_COMPANY_TAB_EDITORA_PUBLISHING_COMPANY_ID",
                        column: x => x.PUBLISHING_COMPANY_ID,
                        principalTable: "TAB_EDITORA",
                        principalColumn: "ID_EDITORA",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TAB_BOOK_PUBLISHING_COMPANY_TAB_LIVROS_BOOK_ID",
                        column: x => x.BOOK_ID,
                        principalTable: "TAB_LIVROS",
                        principalColumn: "ID_LIVRO",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TAB_BOOK_PUBLISHING_COMPANY_BOOK_ID",
                table: "TAB_BOOK_PUBLISHING_COMPANY",
                column: "BOOK_ID");

            migrationBuilder.CreateIndex(
                name: "IX_TAB_BOOK_PUBLISHING_COMPANY_PUBLISHING_COMPANY_ID",
                table: "TAB_BOOK_PUBLISHING_COMPANY",
                column: "PUBLISHING_COMPANY_ID");

            migrationBuilder.CreateIndex(
                name: "IX_TAB_LIVROS_ID_AUTOR",
                table: "TAB_LIVROS",
                column: "ID_AUTOR");

            migrationBuilder.CreateIndex(
                name: "IX_TAB_LIVROS_ID_GENERO",
                table: "TAB_LIVROS",
                column: "ID_GENERO");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TAB_BOOK_PUBLISHING_COMPANY");

            migrationBuilder.DropTable(
                name: "TAB_EDITORA");

            migrationBuilder.DropTable(
                name: "TAB_LIVROS");

            migrationBuilder.DropTable(
                name: "TAB_AUTORES");

            migrationBuilder.DropTable(
                name: "TAB_GENEROS");
        }
    }
}
