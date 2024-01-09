using DoroTech.BookStore.Contracts.Responses.Book;
using DoroTech.BookStore.Domain.Entities;

namespace DoroTech.BookStore.Api.Mappings;

public class BookMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Book, BookDetailsViewModel>();
    }
}
