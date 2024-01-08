using DoroTech.BookStore.Application.Exceptions;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace DoroTech.BookStore.Api.Mappings;

public class ProblemDetailsMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<BookStoreException, ProblemDetails>()
            .Map(dest => dest.Type, src => src.Type ?? src.ResourceTitle.ToLower())
            .Map(dest => dest.Title, src => src.Type ?? src.ResourceTitle)
            .Map(dest => dest.Detail, src => src.ResourceDetail)
            .Map(dest => dest.Status, src => src.StatusCode);
    }
}
