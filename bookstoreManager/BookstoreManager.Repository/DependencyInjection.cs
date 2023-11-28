using BookstoreManager.Repository.Interface;
using BookstoreManager.Repository.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace BookstoreManager.Repository
{
    public static class DependencyInjection
    {        public static IServiceCollection AddInfrastruture(
           this IServiceCollection services)
        {
           services.AddTransient<IBookRepository, BookRepository>();
           services.AddTransient<IUserRepository, UserRepository>();
           services.AddTransient<ILogErrorRepository, LogErrorRepository>();
           services.AddScoped<IAuthentication, JwtServicesRepository>();
            return services;
        }
    }
}