namespace dorotec_backend_test.Classes.Pagination;

public class PageResult<T> where T : class
{
    public List<T> Data { get; }
    private readonly PageFilter Filter;
    public int PageIndex { get => this.Filter.PageIndex; }
    public int PageSize { get => this.Filter.Take; }
    public long TotalRecords { get; }
    public int TotalPages
    {
        get => Convert.ToInt32
            (
                Math.Ceiling(
                    ((double)TotalRecords / (double)this.PageSize))
            );
    }

    public PageResult(List<T> data, int index, byte size, long total)
    {
        this.Data = data;
        this.Filter = new PageFilter(index, size);

        this.TotalRecords = total;
    }
    public PageResult(List<T> data, PageFilter filter, long total)
    {
        this.Data = data;
        this.Filter = filter;

        this.TotalRecords = total;
    }
}
