using Domain.Entities;

namespace Application.Common.Responses;

public class CategoryResponse()
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Category, CategoryResponse>();
        }
    }
}