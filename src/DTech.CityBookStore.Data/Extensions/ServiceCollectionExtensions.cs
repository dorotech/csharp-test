using DTech.CityBookStore.Data.Context;
using DTech.CityBookStore.Data.Repositories.Books;
using DTech.CityBookStore.Domain.Books.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DTech.CityBookStore.Data.Extensions;

public static class ServiceCollectionExtensions
{
    private const string CITYBOOK_CONNECTION_STRING_NAME = "CITYBOOK";

    public static IServiceCollection AddDbCityBookStoreContext(this IServiceCollection services, 
        IConfiguration configuration)
    {
        var cityBookConnectionString = configuration.GetConnectionString(CITYBOOK_CONNECTION_STRING_NAME);

        services.AddDbContext<CityBookStoreContext>(options =>
        {
            options.UseSqlServer(cityBookConnectionString);
        });

        return services;
    }

    public static IServiceCollection AddRepositores(this IServiceCollection services)
        => services.AddScoped<IBookRepository, BookRepository>();
}
