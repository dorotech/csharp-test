using DTech.CityBookStore.Application.Books;
using DTech.CityBookStore.Application.Core.Notifications;
using Microsoft.Extensions.DependencyInjection;

namespace DTech.CityBookStore.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCityBookStoreAutoMapper(this IServiceCollection services)
        => services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

    public static IServiceCollection AddCityBookStoreServices(this IServiceCollection services)
        => services.AddScoped<INotifier, Notifier>()
                   .AddScoped<IBookService, BookService>();
}
