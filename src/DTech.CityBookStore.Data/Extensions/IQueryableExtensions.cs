using DTech.Domain.Core.Pagination;
using Microsoft.EntityFrameworkCore;

namespace DTech.CityBookStore.Data.Extensions;

internal static class IQueryableExtensions
{
    public static async Task<PagedResult<T>> GetPagedAsync<T>(this IQueryable<T> query, int page, int pageSize) where T : class
    {
        pageSize = pageSize > 100 ? 100 : pageSize;

        var result = new PagedResult<T>();
        result.CurrentPage = page;
        result.PageSize = pageSize;
        result.TotalItems = await query.CountAsync();

        var pageCount = (double)result.TotalItems / pageSize;
        result.TotalPages = (int)Math.Ceiling(pageCount);

        var skip = (page - 1) * pageSize;
        result.Items = await query.Skip(skip).Take(pageSize).ToListAsync();

        return result;
    }
}
