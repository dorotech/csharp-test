namespace DoroTechChallenge.Services.Requests;

public class InsertOrUpdateBookRequest
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string PublishedDate { get; set; }
    public string Author { get; set; }
    public string Genre { get; set; }
    public List<string> PublishingCompanies { get; set; }
}
