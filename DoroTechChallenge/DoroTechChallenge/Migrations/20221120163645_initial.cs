using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoroTechChallenge.Migrations
{
    public partial class initial : Migration
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
                    TITULO = table.Column<string>(type: "VARCHAR(50)", nullable: false),
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
                name: "TAB_LIVRO_EDITORA",
                columns: table => new
                {
                    ID_LIVRO_EDITORA = table.Column<int>(type: "INT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_LIVRO = table.Column<int>(type: "INT", nullable: false),
                    ID_EDITORA = table.Column<int>(type: "INT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TAB_LIVRO_EDITORA", x => x.ID_LIVRO_EDITORA);
                    table.ForeignKey(
                        name: "FK_TAB_LIVRO_EDITORA_TAB_EDITORA_ID_EDITORA",
                        column: x => x.ID_EDITORA,
                        principalTable: "TAB_EDITORA",
                        principalColumn: "ID_EDITORA",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TAB_LIVRO_EDITORA_TAB_LIVROS_ID_LIVRO",
                        column: x => x.ID_LIVRO,
                        principalTable: "TAB_LIVROS",
                        principalColumn: "ID_LIVRO",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "TAB_AUTORES",
                columns: new[] { "ID_AUTOR", "NOME_AUTOR" },
                values: new object[,]
                {
                    { 1, "sun tzu" },
                    { 2, "J. R. R. Tolkien" },
                    { 3, "J. K. Rowling" }
                });

            migrationBuilder.InsertData(
                table: "TAB_EDITORA",
                columns: new[] { "ID_EDITORA", "NOME_EDITORA" },
                values: new object[,]
                {
                    { 1, "globo" },
                    { 2, "record" }
                });

            migrationBuilder.InsertData(
                table: "TAB_GENEROS",
                columns: new[] { "ID_GENERO", "NOME_GENERO" },
                values: new object[,]
                {
                    { 1, "guerra" },
                    { 2, "aventura" }
                });

            migrationBuilder.InsertData(
                table: "TAB_LIVROS",
                columns: new[] { "ID_LIVRO", "ID_AUTOR", "DESCRICACO", "ID_GENERO", "DATA_PUBLICACAO", "TITULO" },
                values: new object[] { 1, 2, "Lorem Ipsum is simply dummy text of the printing and typesetting industry", 2, "2022-11-20 13:36:45.4889244", "o senhor dos aneis" });

            migrationBuilder.InsertData(
                table: "TAB_LIVROS",
                columns: new[] { "ID_LIVRO", "ID_AUTOR", "DESCRICACO", "ID_GENERO", "DATA_PUBLICACAO", "TITULO" },
                values: new object[] { 2, 3, "Lorem Ipsum is simply dummy text of the printing and typesetting industry", 2, "0001-01-01 00:00:00", "harry potter" });

            migrationBuilder.InsertData(
                table: "TAB_LIVROS",
                columns: new[] { "ID_LIVRO", "ID_AUTOR", "DESCRICACO", "ID_GENERO", "DATA_PUBLICACAO", "TITULO" },
                values: new object[] { 3, 1, "Lorem Ipsum is simply dummy text of the printing and typesetting industry", 1, "9999-12-31 23:59:59.9999999", "a arte da guerra" });

            migrationBuilder.InsertData(
                table: "TAB_LIVRO_EDITORA",
                columns: new[] { "ID_LIVRO_EDITORA", "ID_LIVRO", "ID_EDITORA" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 1, 2 },
                    { 3, 2, 1 },
                    { 4, 2, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TAB_LIVRO_EDITORA_ID_EDITORA",
                table: "TAB_LIVRO_EDITORA",
                column: "ID_EDITORA");

            migrationBuilder.CreateIndex(
                name: "IX_TAB_LIVRO_EDITORA_ID_LIVRO",
                table: "TAB_LIVRO_EDITORA",
                column: "ID_LIVRO");

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
                name: "TAB_LIVRO_EDITORA");

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
