using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Book.Infra.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(100)", nullable: false),
                    Name = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false),
                    Image = table.Column<string>(type: "varchar(100)", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Genre = table.Column<string>(type: "varchar(100)", nullable: false),
                    Author = table.Column<string>(type: "varchar(100)", nullable: false),
                    Publisher = table.Column<string>(type: "varchar(100)", nullable: false),
                    Edition = table.Column<string>(type: "varchar(100)", nullable: false),
                    Isbn = table.Column<string>(type: "varchar(100)", nullable: false),
                    Language = table.Column<string>(type: "varchar(100)", nullable: false),
                    Pages = table.Column<int>(type: "int", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");
        }
    }
}
