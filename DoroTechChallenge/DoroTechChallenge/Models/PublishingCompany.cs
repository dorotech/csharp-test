namespace DoroTechChallenge.Models;

public class PublishingCompany
{
    public int Id { get; set; }
    public string CompanyName { get; set; }
    public virtual List<Book> Books { get; set; }
    public virtual List<BookPublishingCompany> BookPublishingCompany { get; set; }

    public PublishingCompany(string name)
    {
        CompanyName = name;
    }
}
