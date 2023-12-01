using System.Linq.Expressions;
using Domain.Entities;

namespace Application.Features.Books.Queries;

internal static class BookPredicate
{
    internal static Expression<Func<Book, bool>> GetPredicateBySearch(string search)
    {
        if (string.IsNullOrEmpty(search))
            return null;
        
        return category => category.Title.Contains(search)
                           || category.Id.ToString().Contains(search)
                           || category.Author.Name.Contains(search)
                           || category.Publisher.Name.Contains(search)
                           || category.Category.Name.Contains(search)
                           || category.Category.Description.Contains(search);
    }
}