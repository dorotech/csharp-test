using BookstoreManager.Domain.Entities;

namespace BookstoreManager.Repository.Interface
{
    public interface IBookRepository 
    {
        Task Add(Book book);
        void Delete(Book book);
        void Update(Book book);
        IEnumerable<Book> GetAll();
        Task<Book> GetByIdAsync(int id);
        Book GetById(int id);
        Book? GetByName(string name);

    }
}
