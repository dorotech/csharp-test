using api.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace api.Repositories.book
{
    public class BookRepository : IBookRepository
    {
        private readonly DataContext _context;
        private readonly ILogger _logger;

        public BookRepository(DataContext context, ILogger<BookRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Book>> Get()
        {
            return await _context.Books.OrderBy(b => b.Title).ToListAsync();
        }

        public async Task<Book> Get(int Id)
        {
            return await _context.Books.FindAsync(Id);
        }

        public async Task<Book> Post(Book book)
        {
            if (string.IsNullOrWhiteSpace(book.Title))
            {
                _logger.LogWarning("a book with empty title was not created");
                return null;
            }

            _logger.LogInformation($"creating book with name{book.Title}");
            _context.Books.Add(book);

            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation($"a book {book.Title} is created successfully. :)");
            }
            catch (DbException ex)
            {
                _logger.LogError(ex, "error creating book.");
                return null;
            }

            return book;
        }

        public async Task Put(Book book)
        {
            if (string.IsNullOrWhiteSpace(book.Title))
            {
                _logger.LogWarning("a book with empty title was not updated");
                return;
            }

            _logger.LogInformation($"updating book with name{book.Title}");
            _context.Entry(book).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation($"book with name{book.Title} updated");
            }
            catch (DbException ex)
            {
                _logger.LogInformation(ex, $"erro updated book {book.Title}");
                return;
            }
        }
        public async Task Delete(int Id)
        {
            var bookId = await _context.Books.FindAsync(Id);

            if (bookId == null)
            {
                _logger.LogWarning($"book not found");
                return;
            }

            _logger.LogInformation($"deleting book...");
            _context.Books.Remove(bookId);

            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation($"book removed");
            }
            catch (DbException ex)
            {
                _logger.LogError(ex, $"error with remove book");
                return;
            }
        }
    }
}
