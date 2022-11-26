using dorotec_backend_test.Classes.DTOs;
using dorotec_backend_test.Models;

namespace dorotec_backend_test.Utils.Extensions;

public static class BookFilterExtensions
{
    public static IQueryable<Book> WhereFilterMatches(this IQueryable<Book> source, BookFilterDTO filter)
    {
        if (filter.MaxPrice is not null)
            source = source.Where(book => book.Price <= filter.MaxPrice);

        if (filter.MinPrice is not null)
            source = source.Where(book => book.Price >= filter.MinPrice);

        if (filter.Author is not null)
            source = source.Where(book => book.Author == filter.Author);

        if (filter.Edition is not null)
            source = source.Where(book => book.Edition == filter.Edition);

        if (filter.Genre is not null)
            source = source.Where(book => book.Genre == filter.Genre);

        if (filter.Name is not null)
            source = source.Where(book => book.Name == filter.Name);

        if (filter.Pages is not null)
            source = source.Where(book => book.Pages == filter.Pages);

        return source;
    }
}
