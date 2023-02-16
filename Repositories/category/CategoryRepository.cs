using api.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace api.Repositories.category
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext _context;
        private readonly ILogger _logger;

        public CategoryRepository(DataContext context, ILogger<CategoryRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Category> Post(Category category)
        {
            if (string.IsNullOrWhiteSpace(category.Name))
            {
                _logger.LogWarning("a category with empty name was not created.");
                return null;
            }

            _logger.LogInformation($"creating category with name {category.Name}");
            _context.Category.Add(category);

            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation($"category {category.Name} created successfuly. :)");
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, $"error creating category with name{category.Name}");
                throw;
            }
            return category;
        }
    }
}
