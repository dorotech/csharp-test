using Domain.Entities;

namespace Application.Common.Responses;

public class StockMovementResponse
{
    public Guid Id { get; set; }
    public Guid BookId { get; set; }
    public int Quantity { get; set; }
    public string Description { get; set; }
    public string Type { get; set; }
    public DateTimeOffset CreatedAt { get; set; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<StockMovement, StockMovementResponse>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToString()));
        }
    }
}