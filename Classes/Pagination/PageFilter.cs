namespace dorotec_backend_test.Classes.Pagination;

public class PageFilter
{
    public int PageIndex { get; set; }
    public byte Take { get; set; }
    public int Skip => (this.PageIndex - 1) * Take;

    public PageFilter(int index, byte size)
    {
        // Nunca menos que 1
        this.PageIndex = index < 1
            ? 1
            : index;

        // Nunca mais que 20
        this.Take = size > (byte)20
            ? (byte)20
            : size;
    }
}
