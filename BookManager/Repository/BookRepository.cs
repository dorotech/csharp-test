using BookManager.Data;
using BookManager.Model;
using Microsoft.EntityFrameworkCore;

namespace BookManager.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly DataContext _dataContext;

        public BookRepository(DataContext dataContext) //: base(dataContext)
        {
            _dataContext = dataContext;
        }

        public void Add<T>(T entity) where T : class
        {
            throw new NotImplementedException();
        }

        public void Delete<T>(T entity) where T : class
        {
            throw new NotImplementedException();
        }

        public Task<bool> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public void Update<T>(T entity) where T : class
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Book>> GetBookAsync()
        {
            return await _dataContext.Books
                .Select(x => new Book { id = x.id, title = x.title })
                .ToListAsync();
        }

        public async Task<Book> GetBooksByIdAsync(int id)
        {
            var ret = await _dataContext.Books.Where(x => x.id == id).FirstOrDefaultAsync();
            if (ret != null)
            {
                return ret;
            }
            return new Book();

        }

        Task IBookRepository.GetBooksByIdAsync(int id)
        {
            throw new NotImplementedException();
        }


        // public async Task<Book> GetBooksByIdAsync(int id)
        // {
        //     return await _dataContext.Books.Include(x => x.Books)
        //                  .ThenInclude(c => c.Categorys)
        //                  .ThenInclude(c => c.Publishers)
        //                  .ThenInclude(c => c.Stocks)
        //                 .Where(x => x.id == id).FirstOrDefaultAsync();
        // }
    }
}