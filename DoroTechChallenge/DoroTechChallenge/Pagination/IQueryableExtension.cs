using System.Linq;

namespace Kumbajah.Services.Pagination;

public static class IQueryableExtension
{
    public static IQueryable<T> Paginate<T>(this IQueryable<T> queryable, PagedResponse pagination)
    {
        if (pagination == null || pagination.ItemsPerPage == 0 || pagination.PageNumber == 0)
        {
            return queryable;
        }
        var skip = (pagination.PageNumber - 1) * pagination.ItemsPerPage;
        return queryable.Skip(skip).Take(pagination.ItemsPerPage);
    }
}
