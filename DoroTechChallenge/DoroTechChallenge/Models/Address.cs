namespace DoroTechChallenge.Models;

public class Address
{
    public int Id { get; set; }
    public string Cep { get; set; }
    public string Street { get; set; }
    public string State { get; set; }
    public string City { get; set; }
    public string District { get; set; }
    public int Number { get; set; }
    public string Complement { get; set; }
    public string Reference { get; set; }
    public int PublishingCompanyId { get; set; }

    public virtual PublishingCompany PublishingCompany { get; set; }
}
