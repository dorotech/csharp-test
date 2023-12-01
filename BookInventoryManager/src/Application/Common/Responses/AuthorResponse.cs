using Domain.Entities;

namespace Application.Common.Responses;

public class AuthorResponse
{
    public Guid Id { get; set; }
    public string Name { get;  set; }
    public string Biography { get;  set; }
    
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Author, AuthorResponse>();
        }
    }
}