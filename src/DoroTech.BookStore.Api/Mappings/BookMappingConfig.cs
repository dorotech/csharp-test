using DoroTech.BookStore.Contracts;
using DoroTech.BookStore.Domain.Aggregates;
using Mapster;

namespace DoroTech.BookStore.Api.Mappings;

public class BookMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Book, BookDetailsViewModel>();
    }
}
