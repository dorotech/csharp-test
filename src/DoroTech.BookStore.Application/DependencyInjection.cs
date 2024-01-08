using System.Reflection;
using DoroTech.BookStore.Application.PipelineBehaviors;
using DoroTech.BookStore.Application.RequestHandlers.CommandHandlers;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace DoroTech.BookStore.Application;

public static class ApplicationDependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddMediatR(config => config.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly(), typeof(RegisterCommandHandler).GetTypeInfo().Assembly));
        serviceCollection.AddScoped(typeof(IPipelineBehavior<,>), typeof(ExceptionPipelineBehavior<,>));
        serviceCollection.AddScoped(typeof(IPipelineBehavior<,>), typeof(FailFastRequestPipelineBehavior<,>));


        serviceCollection.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return serviceCollection;
    }
}
