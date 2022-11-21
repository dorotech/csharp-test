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
            return await _context.Books.OrderBy(b => b.name).Take(1000).ToListAsync();
        }

        public async Task<Book> GetBooksByIdAsync(int id)
        {

            Book? ret = await _context.Books.Where(x => x.id == id).FirstOrDefaultAsync();
            if (ret == null)
            {
                ret = new Book();
            }
            return ret;

        }

        public async Task<IEnumerable<Book>> GetFilter(int skip, int take, string name)
        {

            name = string.IsNullOrWhiteSpace(name) ? string.Empty : name.Trim().ToLower();

            if (!string.IsNullOrWhiteSpace(name))
            {
                return await _context.Books.Where(
                              x => x.name.Contains(name)
                              ).AsNoTracking().Skip(skip).Take(take).OrderBy(b => b.name).ToListAsync();
            }
            else
            {
                return await _context.Books.AsNoTracking().Skip(skip).Take(take).OrderBy(b => b.name).ToListAsync();
            }
        }





        public async Task<bool> bookCheckExists(Book book)
        {
            var ret = await _context.Books.Where(x => x.isnb == book.isnb).ToListAsync();
            return (ret != null && ret.Any());

        }


        public async Task<int> getMaxIdBook()
        {
            int maxId = 0;
            var lst = await _context.Books.OrderByDescending(b => b.id).Take(1).ToListAsync();
            if (lst != null && lst.Any())
            {
                var obj = lst.FirstOrDefault();
                if (obj != null)
                {
                    maxId = obj.id;
                }

            }
            return maxId;

        }



    }
}