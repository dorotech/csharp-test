using Domain.Entities;
using Domain.Enums;

namespace Infrastructure.Data.Seeds;

public static class BookSeed
{
    internal async static Task CreateBooks(ApplicationDbContext context)
    {
        if (!context.Books.Any())
        {
            var authors = context.Authors.Take(10).ToArray();
            var categories = context.Categories.Take(10).ToArray();
            var publishers = context.Publishers.Take(10).ToArray();
            var rnd = new Random();

            var books = new List<Book>
            {
                new Book(
                    "Não tem nada de errado com você: Aprenda a aceitar suas imperfeições e tire proveito delas",
                    "1",
                    "Português",
                    DateTime.UtcNow.AddMonths(-12),
                    authors[rnd.Next(authors.Count())].Id,
                    categories[rnd.Next(categories.Count())].Id,
                    publishers[rnd.Next(publishers.Count())].Id,
                    rnd.Next(999),
                    rnd.Next(20)
                ),
                new Book(
                    "Mindset: A nova psicologia do sucesso",
                    "1",
                    "Português",
                    DateTime.UtcNow.AddMonths(-12),
                    authors[rnd.Next(authors.Count())].Id,
                    categories[rnd.Next(categories.Count())].Id,
                    publishers[rnd.Next(publishers.Count())].Id,
                    rnd.Next(999),
                    rnd.Next(20)
                ),
                new Book(
                    "Atitude mental positiva",
                    "1",
                    "Português",
                    DateTime.UtcNow.AddMonths(-12),
                    authors[rnd.Next(authors.Count())].Id,
                    categories[rnd.Next(categories.Count())].Id,
                    publishers[rnd.Next(publishers.Count())].Id,
                    rnd.Next(999),
                    rnd.Next(20)
                ),
                new Book(
                    "Os axiomas de Zurique",
                    "1",
                    "Português",
                    DateTime.UtcNow.AddMonths(-12),
                    authors[rnd.Next(authors.Count())].Id,
                    categories[rnd.Next(categories.Count())].Id,
                    publishers[rnd.Next(publishers.Count())].Id,
                    rnd.Next(999),
                    rnd.Next(20)
                ),
                new Book(
                    "A ciência de ficar rico",
                    "1",
                    "Português",
                    DateTime.UtcNow.AddMonths(-12),
                    authors[rnd.Next(authors.Count())].Id,
                    categories[rnd.Next(categories.Count())].Id,
                    publishers[rnd.Next(publishers.Count())].Id,
                    rnd.Next(999),
                    rnd.Next(20)
                ),
                new Book(
                    "A arte da negociação",
                    "1",
                    "Português",
                    DateTime.UtcNow.AddMonths(-12),
                    authors[rnd.Next(authors.Count())].Id,
                    categories[rnd.Next(categories.Count())].Id,
                    publishers[rnd.Next(publishers.Count())].Id,
                    rnd.Next(999),
                    rnd.Next(20)
                ),
                new Book(
                    "Aprendizados: Minha caminhada para uma vida com mais significado",
                    "1",
                    "Português",
                    DateTime.UtcNow.AddMonths(-12),
                    authors[rnd.Next(authors.Count())].Id,
                    categories[rnd.Next(categories.Count())].Id,
                    publishers[rnd.Next(publishers.Count())].Id,
                    rnd.Next(999),
                    rnd.Next(20)
                ),
                new Book(
                    "Desculpe o exagero, mas não sei sentir pouco",
                    "1",
                    "Português",
                    DateTime.UtcNow.AddMonths(-12),
                    authors[rnd.Next(authors.Count())].Id,
                    categories[rnd.Next(categories.Count())].Id,
                    publishers[rnd.Next(publishers.Count())].Id,
                    rnd.Next(999),
                    rnd.Next(20)
                )
            };

            context.Books.AddRange(books);

            foreach (var book in books)
            {
                context.StockMovements.Add(new StockMovement(book.Id, book.CurrentInventory, EMovementType.Incoming));
            }

            await context.SaveChangesAsync();
        }
    }
}