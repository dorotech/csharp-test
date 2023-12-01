using Domain.Common;

namespace Domain.Entities;

public class Category : BaseAuditableEntity
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public ICollection<Book> Books { get; private set; }
    
    
    public Category(string name, string description)
    {
        Name = name;
        Description = description;
    }

    public void Update(string name, string description)
    {
        Name = name;
        Description = description;
    }
}