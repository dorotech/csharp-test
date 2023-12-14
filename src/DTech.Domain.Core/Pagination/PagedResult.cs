namespace DTech.Domain.Core.Pagination;

public class PagedResult<T> where T : class
{
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int PageSize { get; set; }
    public int TotalItems { get; set; }
    public int FirstItemOnPage => (CurrentPage - 1) * PageSize + 1;
    public int LastItemOnPage => Math.Min(CurrentPage * PageSize, TotalItems);
    public List<T> Items { get; set; }

    public PagedResult<TN> CopyToDtoMapping<TN>(List<TN> newResult) where TN : class
    {
        return new PagedResult<TN>
        {
            CurrentPage = this.CurrentPage,
            TotalPages = this.TotalPages,
            PageSize = this.PageSize,
            TotalItems = this.TotalItems,
            Items = newResult
        };
    }

    public PagedResult()
    {
        Items = new List<T>();
        CurrentPage = 0;
        TotalPages = 0;
        PageSize = 0;
        TotalItems = 0;
    }
}
