using Domain.Entities;

namespace Application.Common.Responses;

public class BookWithPurchasePriceResponse: BookResponse
{
    public decimal? PurchasePrice { get; private set; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Book, BookWithPurchasePriceResponse>();
        }
    }
}