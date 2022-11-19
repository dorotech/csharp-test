using BookManager.Model;

namespace BookManager.Repository.Interfaces
{
    public interface IBookRepository : IBaseRepository
    {
        public Task<IEnumerable<Book>> GetBooksAsync();
        public Task<Book> GetBooksByIdAsync(int id);
        public Task<bool> bookCheckExists(Book book);
    }
}