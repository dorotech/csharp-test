using System.Text;
using DoroTech.BookStore.Application.Common;
using DoroTech.BookStore.Application.Common.Interfaces.Services;
using DoroTech.BookStore.Application.Repositories;
using DoroTech.BookStore.Infrastructure.Authentication;
using DoroTech.BookStore.Infrastructure.Persistence;
using DoroTech.BookStore.Infrastructure.Persistence.Repositories;
using DoroTech.BookStore.Infrastructure.Persistence.Seeds;
using DoroTech.BookStore.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace DoroTech.BookStore.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddAuth(configuration);
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        services.AddPersistence(configuration);
        services.AddRepositories();
        
        return services;
    }

    public static IServiceCollection AddAuth(this IServiceCollection services, ConfigurationManager configuration)
    {
        var JwtSettings = new JwtSettings();
        configuration.Bind(JwtSettings.SectionName, JwtSettings);

        services.AddSingleton(Options.Create(JwtSettings));
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddScoped<IPasswordEncrypter, PasswordEncrypter>();

        services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuers = new[] { JwtSettings.Issuer },
                ValidAudience = JwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSettings.Secret))
            });

        return services;
    }

    public static IServiceCollection AddPersistence(this IServiceCollection services, ConfigurationManager configuration)
    {
        var connection = configuration.GetConnectionString("Default");
        services.AddDbContext<BookStoreContext>(options =>
           options.UseSqlServer(connection,
               b => b.MigrationsAssembly(typeof(BookStoreContext).Assembly.FullName)));

        services.AddScoped<SeedGenerationService>();
        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IBookRepository, BookRepository>();
        return services;
    }
}
