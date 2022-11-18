using LibraryApi.Domain.Entities;
using LibraryApi.Models;

namespace LibraryApi.Domain.Repositories
{
    public interface IBooksRepository
    {
        Task<IEnumerable<Book>> ListAsync(ListBooksModel model);
        Task AddAsync(Book book);
        Task<Book?> FindByIdAsync(int id);
        Task<Book?> FindByNameAsync(string name);
        void Update(Book book);
        void Remove(Book book);
    }
}