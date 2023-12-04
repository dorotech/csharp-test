using Bookstore.Domain.Dtos.v1.Request.Book;
using Bookstore.Domain.Dtos.v1.Response.Book;

namespace Bookstore.Infrastructure.Data.Repositories.v1;

public class BookRepository(BookstoreContext bookstoreContext) : IBookRepository
{
    public async Task AddAsync(Book book)
    {
        await bookstoreContext.Books.AddAsync(book);
        await bookstoreContext.SaveChangesAsync();

        bookstoreContext.Entry(book).State = EntityState.Detached;
    }

    public async Task<PaginatedBooksResponseDto> GetAllAsync(PaginatedBooksRequestDto paginatedBooksRequestDto)
    {
        var skip = (paginatedBooksRequestDto.Page - 1) * paginatedBooksRequestDto.Limit;

        var booksQueryable = bookstoreContext.Books.AsNoTracking()
            .AsQueryable();

        var totalCount = await booksQueryable.CountAsync();
        var result = await booksQueryable
            .OrderBy(x => x.Title)
            .Skip(skip)
            .Take(paginatedBooksRequestDto.Limit)
            .ToListAsync();

        return new PaginatedBooksResponseDto(result, totalCount, paginatedBooksRequestDto.Page);
    }

    public async Task<Book?> GetAsync(Guid id)
    {
        return await bookstoreContext
            .Books
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Book?> GetAsync(Book book)
    {
        return await bookstoreContext
            .Books
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Status == book.Status
                && x.Title == book.Title
                && x.Author == book.Author
                && x.Year == book.Year
                && x.Genre == book.Genre
                && x.Publisher == book.Publisher
                && x.Pages == book.Pages
            );
    }

    public async Task UpdateAsync(Book book)
    {
        bookstoreContext.Books.Update(book);

        await bookstoreContext.SaveChangesAsync();
    }
}