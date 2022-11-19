using DoroTechChallenge.Models;
using Kumbajah.Services.Pagination;

namespace DoroTechChallenge.Repositories;

public interface IBookRepository
{
    Book FetchBook(int id);
    List<Book> List();
    Task<Book> Insert(Book book);
    Task<Book> Update(Book book);
    Task<bool> TryDelete(int id);
    public IQueryable<Book> Filter(Filter filter);
}
