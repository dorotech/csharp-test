using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LibraryApi.Migrations
{
    /// <inheritdoc />
    public partial class seed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "ImageUrl", "Name" },
                values: new object[,]
                {
                    { 2, "Yoshihiro Togashi", "https://upload.wikimedia.org/wikipedia/pt/6/63/Hunter_x_Hunter_Volume_1.JPG", "Hunter x Hunter 02" },
                    { 4, "Yoshihiro Togashi", "https://upload.wikimedia.org/wikipedia/pt/6/63/Hunter_x_Hunter_Volume_1.JPG", "Hunter x Hunter 04" },
                    { 5, "Yoshihiro Togashi", "https://upload.wikimedia.org/wikipedia/pt/6/63/Hunter_x_Hunter_Volume_1.JPG", "Hunter x Hunter 05" },
                    { 6, "Yoshihiro Togashi", "https://upload.wikimedia.org/wikipedia/pt/6/63/Hunter_x_Hunter_Volume_1.JPG", "Hunter x Hunter 06" },
                    { 7, "Yoshihiro Togashi", "https://upload.wikimedia.org/wikipedia/pt/6/63/Hunter_x_Hunter_Volume_1.JPG", "Hunter x Hunter 07" },
                    { 8, "Yoshihiro Togashi", "https://upload.wikimedia.org/wikipedia/pt/6/63/Hunter_x_Hunter_Volume_1.JPG", "Hunter x Hunter 08" },
                    { 9, "Yoshihiro Togashi", "https://upload.wikimedia.org/wikipedia/pt/6/63/Hunter_x_Hunter_Volume_1.JPG", "Hunter x Hunter 09" },
                    { 10, "Yoshihiro Togashi", "https://upload.wikimedia.org/wikipedia/pt/6/63/Hunter_x_Hunter_Volume_1.JPG", "Hunter x Hunter 10" },
                    { 11, "Yoshihiro Togashi", "https://upload.wikimedia.org/wikipedia/pt/6/63/Hunter_x_Hunter_Volume_1.JPG", "Hunter x Hunter 11" },
                    { 12, "Yoshihiro Togashi", "https://upload.wikimedia.org/wikipedia/pt/6/63/Hunter_x_Hunter_Volume_1.JPG", "Hunter x Hunter 12" },
                    { 13, "Yoshihiro Togashi", "https://upload.wikimedia.org/wikipedia/pt/6/63/Hunter_x_Hunter_Volume_1.JPG", "Hunter x Hunter 13" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 13);
        }
    }
}
