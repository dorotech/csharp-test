using LibraryApi.Domain.Repositories;
using LibraryApi.Domain.Services;
using LibraryApi.Persistence.Repositories;
using LibraryApi.Services;

namespace LibraryApi.Extensions
{
    public static class ServiceExtensions
    {
        public static void RegisterRepositories(this IServiceCollection collection)
        {
            collection.AddScoped<IBooksRepository, BooksRepository>();
            collection.AddScoped<IUsersRepository, UsersRepository>();
        }

        public static void RegisterServices(this IServiceCollection collection)
        {
            collection.AddScoped<IUnitOfWork, UnitOfWork>();
            collection.AddScoped<IBooksService, BooksService>();
            collection.AddScoped<IUsersService, UsersService>();
        }
    }
}