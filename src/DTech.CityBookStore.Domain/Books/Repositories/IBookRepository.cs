using DTech.Domain.Core.Pagination;

namespace DTech.CityBookStore.Domain.Books.Repositories;

/// <summary>
/// Repository contract for <see cref="Book"/>
/// </summary>
public interface IBookRepository
{
    Task<IEnumerable<Book>> GetAsync(Func<Book, bool> func);
    Task<Book> GetAsync(int id);
    Task<PagedResult<Book>> FindByFiltersAsync(int? id,
        string title,
        string author,
        string language,
        int? minEdition,
        int? maxEdition,
        int? minPages,
        int? maxPages,
        string publishing,
        string isbn10,
        string isbn13,
        int page = 1,
        int pageSize = 100);
    Task<bool> ExistsAsync(int id);
    Task<bool> ExistsAsync(string isbn10, string isbn13);
    Task<bool> ExistsAsync(int id, string isbn10, string isbn13);
    Task<Book> CreateAsync(Book book);
    Task<Book> UpdateAsync(Book book);
    Task<bool> DeleteAsync(int id);
}
