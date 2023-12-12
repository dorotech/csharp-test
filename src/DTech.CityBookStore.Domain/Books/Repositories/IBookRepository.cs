namespace DTech.CityBookStore.Domain.Books.Repositories;

/// <summary>
/// Repository contract for <see cref="Book"/>
/// </summary>
public interface IBookRepository
{
    Task<IEnumerable<Book>> GetAsync(Func<Book, bool> func);
    Task<Book> GetAsync(int id);
    Task<IEnumerable<Book>> GetAllAsync();
    Task<bool> ExistsAsync(string isbn10, string isbn13);
    Task<Book> CreateAsync(Book book);
    Task<Book> UpdateAsync(Book book);
    Task DeleteAsync(int id);
}
