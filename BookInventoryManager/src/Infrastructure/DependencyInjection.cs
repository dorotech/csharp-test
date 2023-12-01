using Application.Common.Interfaces;
using Application.Common.Interfaces.UnitOfWork;
using Infrastructure.Data;
using Infrastructure.Data.Interceptors;
using Infrastructure.Data.UnitOfWork;
using Infrastructure.Identity;
using Infrastructure.Identity.Interfaces;
using Infrastructure.Identity.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureDatabase(configuration);

        services.AddSingleton(TimeProvider.System);
        
        services.ConfigureIdentity(configuration);

        return services;
    }

    private static void ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddScoped<ISaveChangesInterceptor, EntityInterceptor>();

        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
            options.UseSqlServer(connectionString);
        });
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ApplicationDbContextInitialiser>();
    }

    private static void ConfigureIdentity(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IAuthenticationService, AuthenticationService>();
        services.AddTransient<IJwtService, JwtService>();
        services.AddScoped<IUser, CurrentUser>();

        services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
    }
}