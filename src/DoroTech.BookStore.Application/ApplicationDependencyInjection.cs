using Mapster;

namespace DoroTech.BookStore.Application;

public static class ApplicationDependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddMediatR(config => config.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly(), typeof(RegisterCommandHandler).GetTypeInfo().Assembly));
        serviceCollection.AddScoped(typeof(IPipelineBehavior<,>), typeof(ExceptionPipelineBehavior<,>));
        serviceCollection.AddScoped(typeof(IPipelineBehavior<,>), typeof(FailFastRequestPipelineBehavior<,>));
        
        serviceCollection.AddMappings();

        serviceCollection.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return serviceCollection;
    }

    public static IServiceCollection AddMappings(this IServiceCollection services)
    {
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(Assembly.GetExecutingAssembly());

        services.AddSingleton(config);
        services.AddScoped<IMapper, ServiceMapper>();
        return services;
    }
}
