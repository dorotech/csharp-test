namespace DoroTechChallenge.Models;

public class PublishingCompany
{
    public int Id { get; set; }
    public string CompanyName { get; set; }
    public string Founder { get; set; }
    public int FoundationAge { get; set; }

    public virtual Address Address { get; set; }
    public virtual List<Book> Books { get; set; }
}
