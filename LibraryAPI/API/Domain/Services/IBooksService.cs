using LibraryApi.Domain.Entities;
using LibraryApi.Domain.Services.Communication;
using LibraryApi.Models;

namespace LibraryApi.Domain.Services
{
    public interface IBooksService
    {
        Task<IEnumerable<Book>> ListAsync(ListBooksModel model);
        Task<BookResponse> SaveAsync(Book Book);
        Task<BookResponse> UpdateAsync(int id, Book Book);
        Task<BookResponse> DeleteAsync(int id);
    }
}