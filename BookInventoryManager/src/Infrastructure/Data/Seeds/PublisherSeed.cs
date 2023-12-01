using Domain.Entities;

namespace Infrastructure.Data.Seeds;

public class PublisherSeed
{
    internal async static Task CreatePublishers(ApplicationDbContext context)
    {
        if (!context.Publishers.Any())
        {
            var publishers = new List<Publisher>
            {
                new Publisher("Editora 1"),
                new Publisher("Editora 2"),
                new Publisher("Editora 3"),
                new Publisher("Editora 4"),
                new Publisher("Editora 5"),
                new Publisher("Editora 6"),
                new Publisher("Editora 7"),
                new Publisher("Editora 8"),
                new Publisher("Editora 9"),
                new Publisher("Editora 10"),
                new Publisher("Editora 11"),
                new Publisher("Editora 12")
            };

            context.Publishers.AddRange(publishers);
            await context.SaveChangesAsync();
        }
    }
}