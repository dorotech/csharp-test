namespace dorotec_backend_test.Classes.Pagination;

public class PageResult<T> where T : class
{
    /// <summary> Registros contidos na página. </summary>
    public List<T> Data { get; }
    private readonly PageFilter Filter;

    /// <summary> Índice da página. </summary>
    public int PageIndex { get => this.Filter.PageIndex; }

    /// <summary> Quantidade de registros por página. </summary>
    public int PageSize { get => this.Filter.Take; }
    
    /// <summary> Quantidade total de registros em todas as páginas. </summary>
    public long TotalRecords { get; }
    
    /// <summary> Quantidade total de páginas. </summary>
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
