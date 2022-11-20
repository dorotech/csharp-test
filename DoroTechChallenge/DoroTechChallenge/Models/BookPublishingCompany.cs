namespace DoroTechChallenge.Models;

public class BookPublishingCompany
{
    public int Id { get; set; }
    public int BookId { get; set; }
    public int PublishingCompanyId { get; set; }

    public virtual Book Book { get; set; }
    public virtual PublishingCompany PublishingCompany { get; set; }
}
