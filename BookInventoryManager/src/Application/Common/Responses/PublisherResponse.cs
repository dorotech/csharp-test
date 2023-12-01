using Domain.Entities;

namespace Application.Common.Responses;

public class PublisherResponse
{
    public Guid Id { get;  set; }
    public string Name { get;  set; }
    
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Publisher, PublisherResponse>();
        }
    }
}