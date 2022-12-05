using Book.Application.Extensions;
using Book.Domain.Interfaces;
using Book.Domain.Notifications;
using Book.Infra.Data.Context;
using Book.Infra.Data.Repository;
using Book.Service.Services;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Book.Application.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<BookApiDbContext>();
            services.AddScoped<IBookRepository, BookRepository>();

            services.AddScoped<INotifier, Notifier>();
            services.AddScoped<IBookService, BookService>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUser, AspNetUser>();

            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

            return services;
        }
    }
}