using DoroTechChallenge.Models;
using Kumbajah.Services.Pagination;

namespace DoroTechChallenge.Repositories;

public interface IBookRepository
{
    Book FetchBook(int id);
    Book FindByTitle(string book);
    IQueryable<Book> Filter(Filter filter);
    Task<Book> InsertAsync(Book book);
    Task<Tuple<bool, Book>> TryUpdate(Book book);
    Task<Tuple<bool, int>> TryDelete(int id);
}
