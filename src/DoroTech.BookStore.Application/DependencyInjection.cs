using System.Reflection;
using DoroTech.BookStore.Application.RequestHandlers.CommandHandlers;
using Microsoft.Extensions.DependencyInjection;

namespace DoroTech.BookStore.Application;

public static class ApplicationDependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddMediatR(config => config
            .RegisterServicesFromAssemblies(
                Assembly.GetExecutingAssembly(),
                typeof(RegisterCommandHandler).GetTypeInfo().Assembly
            )
        );

        return serviceCollection;
    }
}
