using Domain.Common;

namespace Domain.Entities;

public class Publisher : BaseAuditableEntity
{
    public string Name { get; private set; }
    public ICollection<Book> Books { get; private set; }

    public Publisher(string name)
    {
        Name = name;
    }

    public void Update(string name)
    {
        Name = name; }
}