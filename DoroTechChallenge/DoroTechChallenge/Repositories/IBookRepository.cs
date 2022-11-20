using DoroTechChallenge.Models;
using Kumbajah.Services.Pagination;

namespace DoroTechChallenge.Repositories;

public interface IBookRepository
{
    Book FetchBook(int id);
    Book FindByName(string book);
    Task<Book> InsertAsync(Book book);
    Task<Book> UpdateAsync(Book book);
    Task<bool> TryDelete(int id);
    IQueryable<Book> Filter(Filter filter);
}
