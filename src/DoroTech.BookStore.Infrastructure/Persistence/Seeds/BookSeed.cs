namespace DoroTech.BookStore.Infrastructure.Persistence.Seeds;

public static class BookSeed
{
    internal async static Task Generate(BookStoreContext context)
    {
        if (context.Books.Any())
            return;

        var books = new List<Book>
        {
            Book.Create("The Great Gatsby", 1, "English", 10, 15, new DateOnly(1925, 4, 10), "978-0-7432-7356-5"),
            Book.Create("To Kill a Mockingbird", 2, "English", 10, 15, new DateOnly(1960, 7, 11), "978-0-06-112008-4", description: "A novel by Harper Lee", pages: 281),
            Book.Create("The Lord of the Rings", 5, "English", 10, 15, new DateOnly(1954, 7, 29), "978-0-618-62412-0", description: "A trilogy by J.R.R. Tolkien", pages: 1178),
            Book.Create("1984", 1, "English", 10, 15, new DateOnly(1949, 6, 8), "978-0-452-28423-4", description: "A dystopian novel by George Orwell", pages: 326),
            Book.Create("Dom Casmurro", 2, "Portuguese", 10, 15, new DateOnly(1899, 1, 1), "978-0-06-112008-4", description: "A novel by Machado de Assis", pages: 281),
            Book.Create("Harry Potter and the Sorcerer's Stone", 1, "English", 10, 15, new DateOnly(1997, 6, 26), "978-0-590-35340-3", description: "A novel by J.K. Rowling", pages: 309),
            //Book.Create(7L, "The Great Gatsby", 1, "English", new DateTime(1925, 4, 10), "978-0-7432-7356-5", null, 180),
            //Book.Create(8L, "The Great Gatsby", 1, "English", new DateTime(1925, 4, 10), "978-0-7432-7356-5", null, 180),
            //Book.Create(9L, "The Great Gatsby", 1, "English", new DateTime(1925, 4, 10), "978-0-7432-7356-5", null, 180),
            //Book.Create(10L, "The Great Gatsby", 1, "English", new DateTime(1925, 4, 10), "978-0-7432-7356-5", null, 180),
        };

        context.Books.AddRange(books);

        await context.SaveChangesAsync();
    }
}
