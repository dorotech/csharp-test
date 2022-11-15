using BookstoreManager.Repository.Interface;
using BookstoreManager.Repository.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System.Linq.Expressions;

namespace BookstoreManager.Repository
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastruture(
           this IServiceCollection services)
        {
           services.AddTransient<IBookRepository, BookRepository>();
           services.AddTransient<IUserRepository, UserRepository>();
           services.AddScoped<IAuthentication, JwtServicesRepository>();
            return services;
        }
    }
}