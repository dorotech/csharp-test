using Domain.Entities;

namespace Infrastructure.Data.Seeds;

internal static class CategorySeed
{
    internal async static Task CreateCategories(ApplicationDbContext context)
    {
        if (!context.Categories.Any())
        {
            var categories = new List<Category>
            {
                new Category("Tecnologia", default),
                new Category("Terror", default),
                new Category("Romance", default),
                new Category("Infantil", default),
                new Category("HQs e Mangás", default),
                new Category("Autoajuda", default),
            };

            context.Categories.AddRange(categories);
            await context.SaveChangesAsync();
        }
    }
}