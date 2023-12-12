using DoroTechCSharpTest.Application.Interfaces;
using DoroTechCSharpTest.Application.Services;
using DoroTechCSharpTest.Domain.Interfaces;
using DoroTechCSharpTest.Domain.Notifications;
using DoroTechCSharpTest.Infra.Data.Context;
using DoroTechCSharpTest.Infra.Data.Repository;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;

namespace DoroTechCSharpTest.Infra.CrossCutting.IoC
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<AppDbContext>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<INotifier, Notifier>();

            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IUserService, UserService>();
        }
    }
}
