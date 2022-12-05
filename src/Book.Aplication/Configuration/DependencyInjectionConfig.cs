using BackendTest.Context;
using BackendTest.Extensions;
using BackendTest.Interfaces;
using BackendTest.Notifications;
using BackendTest.Repository;
using BackendTest.Services;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace BackendTest.Configuration
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