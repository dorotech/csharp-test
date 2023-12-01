using Domain.Entities;

namespace Infrastructure.Data.Seeds;

public class AuthorSeed
{
    internal async static Task CreateAuthors(ApplicationDbContext context)
    {
        if (!context.Authors.Any())
        {
            var authors = new List<Author>
            {
                new Author("Autor A"),
                new Author("Autor B"),
                new Author("Autor C"),
                new Author("Autor D"),
                new Author("Autor E"),
                new Author("Autor F"),
                new Author("Autor G"),
                new Author("Autor H"),
                new Author("Autor I"),
                new Author("Autor J"),
                new Author("Autor K"),
                new Author("Autor L"),
                new Author("Autor M"),
                new Author("Autor N"),
            };

            context.Authors.AddRange(authors);
            await context.SaveChangesAsync();
        }
    }
}