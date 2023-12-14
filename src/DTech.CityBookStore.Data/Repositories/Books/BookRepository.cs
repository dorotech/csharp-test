using DTech.CityBookStore.Data.Context;
using DTech.CityBookStore.Data.Extensions;
using DTech.CityBookStore.Domain.Books;
using DTech.CityBookStore.Domain.Books.Repositories;
using DTech.Domain.Core.Pagination;
using Microsoft.EntityFrameworkCore;

namespace DTech.CityBookStore.Data.Repositories.Books;

public class BookRepository : IBookRepository
{
    private readonly CityBookStoreContext _context;

    public BookRepository(CityBookStoreContext context) => _context = context;

    public async Task<Book> CreateAsync(Book book)
    {
        await _context.Books.AddAsync(book);
        await _context.SaveChangesAsync();
        return book;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var model = await GetAsync(id);
        _context.Remove(model);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> ExistsAsync(int id)
        => await _context.Books.Where(b => b.Id.Equals(id))
                               .AnyAsync();

    public async Task<bool> ExistsAsync(string isbn10, string isbn13)
        => await _context.Books.Where(b => b.ISBN10.Equals(isbn10) 
                                        || b.ISBN13.Equals(isbn13))
                               .AnyAsync();

    /// <summary>
    /// Check if there is a book with different ID for the same value of isbn10 and isbn13.
    /// </summary>
    /// <param name="id">Book Id</param>
    /// <param name="isbn10">Book ISNB10.</param>
    /// <param name="isbn13">Book ISNB13.</param>
    /// <returns>If Exists.</returns>
    public async Task<bool> ExistsAsync(int id, string isbn10, string isbn13)
        => await _context.Books.Where(b => !b.Id.Equals(id)
                                        && (b.ISBN10.Equals(isbn10)
                                        || b.ISBN13.Equals(isbn13)))
                               .AnyAsync();

    public async Task<PagedResult<Book>> FindByFiltersAsync(int? id,
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
        int pageSize = 100)
    {
        var query = _context.Books.AsQueryable();

        if (id.HasValue)
        {
            query = query.Where(b => b.Id.Equals(id));
        }

        if (!string.IsNullOrEmpty(title))
        {
            query = query.Where(b => EF.Functions.Like(b.Title, title.MakeLikeOutput()));
        }

        if (!string.IsNullOrEmpty(author))
        {
            query = query.Where(b => EF.Functions.Like(b.Author, author.MakeLikeOutput()));
        }

        if (!string.IsNullOrEmpty(language))
        {
            query = query.Where(b => EF.Functions.Like(b.Language, language.MakeLikeOutput()));
        }

        if (!string.IsNullOrEmpty(publishing))
        {
            query = query.Where(b => EF.Functions.Like(b.Publishing, publishing.MakeLikeOutput()));
        }

        if (!string.IsNullOrEmpty(isbn10))
        {
            query = query.Where(b => EF.Functions.Like(b.ISBN10, isbn10));
        }

        if (!string.IsNullOrEmpty(isbn13))
        {
            query = query.Where(b => EF.Functions.Like(b.ISBN13, isbn13));
        }

        if (minEdition.HasValue)
        {
            query = query.Where(b => b.Edition >= minEdition.Value);
        }

        if (maxEdition.HasValue)
        {
            query = query.Where(b => b.Edition <= maxEdition.Value);
        }

        if (minPages.HasValue)
        {
            query = query.Where(b => b.Pages >= minPages.Value);
        }

        if (maxPages.HasValue)
        {
            query = query.Where(b => b.Pages <= maxPages.Value);
        }

        query = query.OrderBy(b => b.Title);

        return await query.GetPagedAsync(page, pageSize);
    }
        

    public async Task<IEnumerable<Book>> GetAsync(Func<Book, bool> func)
        => await Task.FromResult(_context.Books.Where(func).ToList());

    public async Task<Book> GetAsync(int id)
        => await _context.Books.Where(b => b.Id.Equals(id)).FirstOrDefaultAsync();

    public async Task<Book> UpdateAsync(Book book)
    {
        _context.Books.Update(book);
        await _context.SaveChangesAsync();
        return book;
    }
}
