using DoroTech.BookStore.Domain.Entities;

namespace DoroTech.BookStore.Infrastructure.Persistence.Seeds;

public static class BookSeed
{
    public async static Task Generate(BookStoreContext context)
    {
        if (context.Books.Any())
            return;

        var books = new List<Book>
        {
            Book.Create("The Holy Bible", "God", 1, "English", 0, 9.99m, new DateOnly(30, 1, 1), "N/A", isDonation: true, description: "A sacred text of Christianity", pages: 31102),
            Book.Create("The Great Gatsby", "Scott Fitzgerald", 1, "English", 20.95m, 35, new DateOnly(1925, 4, 10), "978-0-7432-7356-5"),
            Book.Create("To Kill a Mockingbird", "Harper Lee", 2, "English", 20.95m, 59.99m, new DateOnly(1960, 7, 11), "978-0-06-112008-4", description: "A novel by Harper Lee", pages: 281),
            Book.Create("The Lord of the Rings", "J.R.R. Tolkien", 5, "English", 20.95m, 38.83m, new DateOnly(1954, 7, 29), "978-0-618-62412-0", description: "A trilogy by J.R.R. Tolkien", pages: 1178),
            Book.Create("1984", "George Orwell", 1, "English", 15.55m, 35, new DateOnly(1949, 6, 8), "978-0-452-28423-4", description: "A dystopian novel by George Orwell", pages: 326),
            Book.Create("Dom Casmurro", "Machado de Assis", 2, "Portuguese", 10, 15, new DateOnly(1899, 1, 1), "978-0-06-112008-4", description: "A novel by Machado de Assis", pages: 281),
            Book.Create("O Alquimista", "Paulo Coelho", 1, "Portuguese", 9.35m, 15, new DateOnly(1988, 1, 1), "978-0-06-112008-4", description: "A novel by Paulo Coelho", pages: 281),
            Book.Create("Harry Potter and the Sorcerer's Stone", "J.K. Rowling", 1, "English", 10, 85.79m, new DateOnly(1997, 6, 26), "978-0-590-35340-3", description: "A novel by J.K. Rowling", pages: 309),
            Book.Create("Memórias Póstumas de Brás Cubas", "Machado de Assis", 1, "Portuguese", 21.99m, 55.67m, new DateOnly(1881, 1, 1), "978-85-325-1940-9", description: "A novel by Machado de Assis", pages: 176),
            Book.Create("The Hobbit", "J.R.R. Tolkien", 5, "English", 9.99m, 15, new DateOnly(1937, 9, 21), "978-0-261-10295-2", description: "A fantasy novel by J.R.R. Tolkien", pages: 1178),
            Book.Create("The Da Vinci Code", "Dan Brown", 5, "English", 11.44m, 15, new DateOnly(2003, 3, 18), "978-0-385-50420-1", description: "A mystery thriller novel by Dan Brown", pages: 454),
            Book.Create("The Chronicles of Narnia", "C.S. Lewis", 7, "English", 29.99m, 49.99m, new DateOnly(1950, 10, 19), "978-0-385-50420-1", description: "A mystery thriller novel by Dan Brown", pages: 767),
        };

        context.Books.AddRange(books);

        await context.SaveChangesAsync();
    }
}
