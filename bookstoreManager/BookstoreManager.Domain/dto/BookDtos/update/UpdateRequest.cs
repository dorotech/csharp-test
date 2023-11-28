namespace BookstoreManager.Domain.dto.update
{
    public record UpdateRequest(int Id ,
        string Name, 
        string Description, 
        string Genre, 
        string Author,
        bool Active);
    
}
