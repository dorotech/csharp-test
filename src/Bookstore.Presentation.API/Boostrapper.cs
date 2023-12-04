using Bookstore.Domain.Commands.Base;
using Bookstore.Domain.Contracts.v1.Repositories;
using Bookstore.Infrastructure.Data;
using Bookstore.Infrastructure.Data.Repositories.v1;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using System.Reflection;

namespace Bookstore.API;

public static class Boostrapper
{
    private static readonly Assembly domainAssembly = typeof(Command<>).Assembly;

    public static IServiceCollection ConfigureRepositories(this IServiceCollection services)
    {
        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }

    public static IServiceCollection ConfigureExternalServices(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(domainAssembly);
        services.AddAutoMapper(domainAssembly);
        services.AddMediatR(x => x.RegisterServicesFromAssembly(domainAssembly));
        services.AddFluentValidationAutoValidation();

        return services;
    }

    public static IServiceCollection ConfigureExceptionHandlers(this IServiceCollection services)
    {
        services.AddExceptionHandler<ApiExceptionHandler>();

        return services;
    }

    public static IServiceCollection ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        #if DEBUG
        services.AddDbContextPool<BookstoreContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), x =>
            {
                x.EnableRetryOnFailure(5);
            })
        );
        #else
        services.AddDbContextPool<BookstoreContext>(options =>
        options.UseInMemoryDatabase("BookstoreDatabase")
        );
        #endif

        return services;
    }
}
