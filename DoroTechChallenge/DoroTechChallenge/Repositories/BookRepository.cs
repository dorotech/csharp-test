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

    public List<Book> List() =>
        Context.Books
        .Include(x => x.Author)
        .Include(x => x.PublishingCompanies)
        .Include(x => x.Genre)
        .ToList();

    public Book FetchBook(int id) =>
        Context.Books.AsNoTracking()
            .FirstOrDefault(x => x.Id == id);

    public IQueryable<Book> Filter(Filter filter)
    {
        IQueryable<Book> books = Context.Books;
        if (filter is null || filter.Value is null) return books;
        return books.Where(x => x.Author.AuthorName.Contains(filter.Value.ToLower())
                || x.Title.Contains(filter.Value.ToLower())
                || x.Genre.GenreName.Contains(filter.Value.ToLower()));
    }

    public async Task<Book> Insert(Book book)
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

    public async Task<Book> Update(Book book)
    {
        using var transaction = Context.Database.BeginTransaction();
        try
        {
            var attachedBook = Context.Attach(book);
            attachedBook.State = EntityState.Modified;
            var proxy = Context.Books.Update(book);
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

    public async Task<bool> TryDelete(int id)
    {
        using var transaction = Context.Database.BeginTransaction();
        try
        {
            var book = FetchBook(id);
            if (book is not null)
            {
                var proxy = Context.Books.Add(book);
                await Context.SaveChangesAsync();
                transaction.Commit();
                return true;
            }
            return false;
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
