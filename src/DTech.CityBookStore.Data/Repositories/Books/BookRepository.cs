using DTech.CityBookStore.Data.Context;
using DTech.CityBookStore.Domain.Books;
using DTech.CityBookStore.Domain.Books.Repositories;
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

    public async Task DeleteAsync(int id)
    {
        var model = await GetAsync(id);
        _context.Remove(model);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(string isbn10, string isbn13)
        => await _context.Books.Where(b => b.ISBN10.Equals(isbn10) 
                                        || b.ISBN13.Equals(isbn13))
                               .AnyAsync();

    public async Task<IEnumerable<Book>> GetAllAsync()
        => await _context.Books.OrderBy(b => b.Title).ToListAsync();

    public async Task<IEnumerable<Book>> GetAsync(Func<Book, bool> func)
        => await Task.FromResult(_context.Books.Where(func).ToList());

    public async Task<Book> GetAsync(int id)
    {
        var model = await _context.Books.FindAsync(id);
        return model;
    }

    public async Task<Book> UpdateAsync(Book book)
    {
        _context.Books.Update(book);
        await _context.SaveChangesAsync();
        return book;
    }
}
