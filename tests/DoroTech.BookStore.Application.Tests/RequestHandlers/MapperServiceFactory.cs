using Mapster;
using Microsoft.Extensions.DependencyInjection;

namespace DoroTech.BookStore.Application.Tests.RequestHandlers;

public class MapperServiceFactory
{
    protected readonly IMapper _mapper;

    public MapperServiceFactory()
    {
        var serviceCollection = new ServiceCollection();

        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(typeof(ApplicationDependencyInjection).Assembly);
        serviceCollection.AddSingleton(config);
        serviceCollection.AddScoped<IMapper, ServiceMapper>();
        var serviceProvider = serviceCollection.BuildServiceProvider(); ;

        _mapper = serviceProvider.GetRequiredService<IMapper>();
    }
}
