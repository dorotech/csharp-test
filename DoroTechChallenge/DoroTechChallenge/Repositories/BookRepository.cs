using DoroTechChallenge.Context;
using DoroTechChallenge.Models;
using Kumbajah.Services.Pagination;
using Microsoft.EntityFrameworkCore;

namespace DoroTechChallenge.Repositories;

public class BookRepository : IBookRepository
{
    public DoroTechContext Context { get; }

    public BookRepository(DoroTechContext context)
    {
        Context = context;
    }

    public IQueryable<Book> BooksWithInclude() =>
        Context.Books
        .Include(x => x.Author)
        .Include(x => x.PublishingCompanies)
        .Include(x => x.Genre);

    public Book FindByTitle(string bookName) =>
            BooksWithInclude().FirstOrDefault(x => x.Title.ToLower() == bookName.ToLower());

    public Book FetchBook(int id) =>
        BooksWithInclude().AsNoTracking()
            .FirstOrDefault(x => x.Id == id);

    public IQueryable<Book> Filter(Filter filter)
    {
        IQueryable<Book> books = BooksWithInclude();
        if (filter is null || filter.Value is null) return books;
        return books.Where(x => x.Title.Contains(filter.Value.ToLower()));
    }

    public async Task<Book> InsertAsync(Book book)
    {
        using var transaction = Context.Database.BeginTransaction();
        try
        {
            var proxy = Context.Books.Add(book);
            await Context.SaveChangesAsync();
            transaction.Commit();
            return proxy.Entity;
        }
        catch (Exception)
        {
            try
            {
                transaction.Rollback();
            }
            catch (Exception) { }
            throw;
        }
    }

    public async Task<Tuple<bool, Book>> TryUpdate(Book book)
    {
        using var transaction = Context.Database.BeginTransaction();
        try
        {
            var existingBook = FetchBook(book.Id);
            if (existingBook is not null)
            {
                var proxy = Context.Books.Attach(book);
                proxy.State = EntityState.Modified;
                Context.Update(book);
                await Context.SaveChangesAsync();
                transaction.Commit();
                return Tuple.Create(true, proxy.Entity);
            }
            else
            {
                await Context.SaveChangesAsync();
                transaction.Commit();
                return Tuple.Create(false, book);
            }
        }
        catch (Exception)
        {
            try
            {
                transaction.Rollback();
            }
            catch (Exception) { }
            throw;
        }
    }

    public async Task<Tuple<bool, int>> TryDelete(int id)
    {
        using var transaction = Context.Database.BeginTransaction();
        try
        {
            var book = FetchBook(id);
            if (book is not null)
            {
                var proxy = Context.Books.Remove(book);
                await Context.SaveChangesAsync();
                transaction.Commit();
                return Tuple.Create(true, proxy.Entity.Id);
            }
            return Tuple.Create(false, 0);
        }
        catch (Exception)
        {
            try
            {
                transaction.Rollback();
            }
            catch (Exception) { }
            throw;
        }
    }
}
