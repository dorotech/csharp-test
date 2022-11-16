using BookManager.Model;

namespace BookManager.Repository
{
    public interface IBookRepository : IBaseRepository
    {
        public Task<IEnumerable<Book>> GetBooksAsync();
        public Task<Book> GetBooksByIdAsync(int id);
    }
}