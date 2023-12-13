namespace DTech.CityBookStore.Application.Books.Dto;

public class BookAddDto
{
    public string Title { get; set; }
    public string Author { get; set; }
    public string Language { get; set; }
    public int Edition { get; set; }
    public int Pages { get; set; }
    public string Publishing { get; set; }
    public string ISBN10 { get; set; }
    public string ISBN13 { get; set; }
    public decimal? DimensionLength { get; set; }
    public decimal? DimensionHeight { get; set; }
    public decimal? DimensionWidth { get; set; }
}
