using BookManager.Data;
using BookManager.Model;
using BookManager.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookManager.Repository
{
    public class BookRepository : BaseRepository, IBookRepository
    {
        private readonly DataContext _context;
        public BookRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Book>> GetBooksAsync()
        {
            return await _context.Books
                .Select(x => new Book { id = x.id, title = x.title })
                .ToListAsync();
        }

        public async Task<Book> GetBooksByIdAsync(int id)
        {
            var ret = await _context.Books.Where(x => x.id == id).FirstOrDefaultAsync();
            if (ret != null)
            {
                return ret;
            }
            return new Book();

        }



        public async Task<bool> bookCheckExists(Book book)
        {
            var ret = await _context.Books.Where(x => x.isnb == book.isnb).ToListAsync();
            return (ret != null && ret.Any());

        }

    }
}