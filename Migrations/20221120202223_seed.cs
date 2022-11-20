using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace dorotecbackendtest.Migrations
{
    /// <inheritdoc />
    public partial class seed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "admin",
                columns: new[] { "id", "login", "name", "password" },
                values: new object[] { 1, "admin", "Administrator", "$argon2id$v=19$m=65536,t=3,p=1$T+3MGwgihMsfgKVUpUy8yw$dy0CMs4pLXLvYuOWjFO3s4DJs77Rj7u5E23VmGjoQJY" });

            migrationBuilder.InsertData(
                table: "book",
                columns: new[] { "id", "author", "edition", "genre", "name", "pages", "price", "publish_date" },
                values: new object[,]
                {
                    { 1, "Adele W B Mann", 10, "fringilla", "Baker", 55, 76, new DateTime(2013, 1, 19, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "Moses E H Wilson", 4, "Donec", "Keaton", 199, 60, new DateTime(2022, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "Jonah S N Craft", 7, "Nullam", "Candice", 404, 64, new DateTime(2021, 11, 7, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, "Sandra S Dunn", 4, "tellus", "Abra", 89, 35, new DateTime(2013, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, "Madeson C J English", 5, "sem.", "Neville", 202, 59, new DateTime(1998, 4, 21, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, "Samantha S Jimenez", 9, "turpis", "Owen", 219, 62, new DateTime(2014, 3, 8, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, "Ulla Q Hill", 7, "conubia", "Carson", 328, 44, new DateTime(2011, 7, 27, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, "Hilel B Ellis", 2, "non", "Aurora", 434, 80, new DateTime(2011, 8, 24, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9, "Jeanette R P Mcneil", 3, "lacus.", "Lacey", 393, 54, new DateTime(1998, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 10, "Latifah Daniels", 1, "at", "Gil", 333, 63, new DateTime(2017, 3, 31, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 11, "Emerson Q Peck", 4, "Aenean", "Winter", 489, 58, new DateTime(2006, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 12, "Nissim Church", 7, "Aliquam", "Lucian", 62, 47, new DateTime(2012, 6, 6, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 13, "Fiona Landry", 8, "ipsum", "Quintessa", 168, 53, new DateTime(2001, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 14, "Avram W C Tillman", 5, "egestas.", "Stacey", 301, 76, new DateTime(2015, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 15, "Walker X Browning", 3, "et,", "Hilel", 245, 37, new DateTime(2015, 4, 9, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 16, "Travis L I Houston", 1, "augue", "Yasir", 195, 68, new DateTime(2010, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 17, "Bradley W Fuller", 4, "pede", "Austin", 488, 79, new DateTime(2002, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 18, "Sean V L Gross", 5, "Pellentesque", "Leigh", 259, 61, new DateTime(2017, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 19, "Rajah D E Richmond", 9, "tristique", "Omar", 328, 43, new DateTime(2015, 5, 27, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 20, "Paul Church", 5, "tempus", "Maia", 311, 78, new DateTime(2007, 7, 23, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 21, "Carol Powers", 4, "nec", "Ferdinand", 67, 59, new DateTime(2017, 12, 29, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 22, "Quamar T B Mcdaniel", 3, "ante", "Ulysses", 184, 45, new DateTime(2014, 2, 7, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 23, "Ramona O S Crosby", 9, "Aenean", "Alfreda", 103, 79, new DateTime(2007, 5, 7, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 24, "Perry Q Combs", 8, "nunc", "Malik", 486, 75, new DateTime(2012, 12, 29, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 25, "Dean N Caldwell", 9, "Quisque", "Chanda", 341, 26, new DateTime(2004, 10, 4, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 26, "Quinlan B Holder", 9, "Nam", "Lynn", 455, 31, new DateTime(2016, 12, 13, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 27, "Marvin Q H Mcknight", 6, "neque", "Phyllis", 179, 73, new DateTime(2016, 9, 22, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 28, "Benjamin Trujillo", 6, "metus", "Leroy", 140, 26, new DateTime(2016, 7, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 29, "Dawn C J Workman", 5, "a,", "Yetta", 70, 54, new DateTime(1999, 10, 9, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 30, "Gareth Fuentes", 6, "aliquam,", "Garrett", 340, 44, new DateTime(2004, 5, 13, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 31, "Eric Thornton", 6, "luctus", "Regina", 234, 58, new DateTime(2004, 9, 21, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 32, "Martin R S Adkins", 6, "lectus", "Denise", 182, 43, new DateTime(2002, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 33, "Jacob D Mcguire", 10, "lorem.", "Vernon", 82, 45, new DateTime(2021, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 34, "Mason Z Sherman", 4, "egestas", "Dieter", 483, 64, new DateTime(2009, 5, 22, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 35, "Jonah X K Guerrero", 10, "faucibus", "Hermione", 142, 47, new DateTime(1997, 11, 8, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 36, "India Cummings", 9, "arcu", "Lucius", 400, 55, new DateTime(2019, 9, 27, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 37, "Lee I Montoya", 8, "Proin", "Ivy", 132, 74, new DateTime(2018, 3, 7, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 38, "Germaine L M Wells", 7, "auctor", "Connor", 471, 74, new DateTime(2009, 9, 22, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 39, "Kelsie Castillo", 4, "nonummy", "Mariam", 222, 76, new DateTime(2003, 9, 22, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 40, "Jillian Salas", 10, "rhoncus", "Ursa", 245, 78, new DateTime(2012, 5, 21, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 41, "Kennan W Sexton", 5, "aliquet", "Jenna", 467, 76, new DateTime(2001, 4, 26, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 42, "Ina Romero", 5, "sollicitudin", "Samantha", 252, 64, new DateTime(2021, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 43, "Wynne Everett", 8, "senectus", "Evelyn", 384, 70, new DateTime(2011, 2, 26, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 44, "Dustin E Gonzales", 5, "faucibus", "Constance", 462, 74, new DateTime(2002, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 45, "Aristotle Tillman", 2, "non", "Ginger", 154, 57, new DateTime(2005, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 46, "Brett D Hess", 10, "eu", "Chadwick", 177, 31, new DateTime(2021, 7, 30, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 47, "Jocelyn S G Lott", 8, "magna.", "Brennan", 296, 70, new DateTime(2013, 3, 21, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 48, "Fritz L I Good", 9, "et", "Velma", 133, 66, new DateTime(2007, 1, 21, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 49, "Stuart R Fulton", 6, "feugiat", "Bradley", 352, 47, new DateTime(2012, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 50, "Dai R Barton", 1, "Nunc", "Eve", 206, 28, new DateTime(2017, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "admin",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "book",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "book",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "book",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "book",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "book",
                keyColumn: "id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "book",
                keyColumn: "id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "book",
                keyColumn: "id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "book",
                keyColumn: "id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "book",
                keyColumn: "id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "book",
                keyColumn: "id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "book",
                keyColumn: "id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "book",
                keyColumn: "id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "book",
                keyColumn: "id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "book",
                keyColumn: "id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "book",
                keyColumn: "id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "book",
                keyColumn: "id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "book",
                keyColumn: "id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "book",
                keyColumn: "id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "book",
                keyColumn: "id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "book",
                keyColumn: "id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "book",
                keyColumn: "id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "book",
                keyColumn: "id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "book",
                keyColumn: "id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "book",
                keyColumn: "id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "book",
                keyColumn: "id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "book",
                keyColumn: "id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "book",
                keyColumn: "id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "book",
                keyColumn: "id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "book",
                keyColumn: "id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "book",
                keyColumn: "id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "book",
                keyColumn: "id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "book",
                keyColumn: "id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "book",
                keyColumn: "id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "book",
                keyColumn: "id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "book",
                keyColumn: "id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "book",
                keyColumn: "id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "book",
                keyColumn: "id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "book",
                keyColumn: "id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "book",
                keyColumn: "id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "book",
                keyColumn: "id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "book",
                keyColumn: "id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "book",
                keyColumn: "id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "book",
                keyColumn: "id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "book",
                keyColumn: "id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "book",
                keyColumn: "id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "book",
                keyColumn: "id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "book",
                keyColumn: "id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "book",
                keyColumn: "id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "book",
                keyColumn: "id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "book",
                keyColumn: "id",
                keyValue: 50);
        }
    }
}
