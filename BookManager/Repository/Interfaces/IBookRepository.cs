using BookManager.Model;

namespace BookManager.Repository.Interfaces
{
    public interface IBookRepository : IBaseRepository
    {
        public Task<IEnumerable<Book>> GetBooksAsync();
        public Task<Book> GetBooksByIdAsync(int id);
        public Task<bool> bookCheckExists(Book book);

        public Task<IEnumerable<Book>> GetFilter(int skip, int take, string name);

        public Task<int> getMaxIdBook();
        public int getCountBook(string name);
    }
}