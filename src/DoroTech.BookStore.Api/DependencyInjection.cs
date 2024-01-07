using DoroTech.BookStore.Api.Infra;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.OData;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;

namespace DoroTech.BookStore.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddMappings(this IServiceCollection services)
    {
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(Assembly.GetExecutingAssembly());

        services.AddSingleton(config);
        services.AddScoped<IMapper, ServiceMapper>();
        return services;
    }

    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services
            .AddControllers()
            .AddOData(options => options.Select().Filter().Count().OrderBy().Expand().SetMaxTop(10));

        services.AddSwaggerGen(swaggwerOptions =>
        {
            swaggwerOptions.SwaggerDoc("v1", new OpenApiInfo { Title = "DoroTech.BookStore", Version = "v1" });
            swaggwerOptions.OperationFilter<SwaggerAddODataField>();
            swaggwerOptions.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme. Example: 'Bearer {token}'",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            swaggwerOptions.OperationFilter<SecurityRequirementsOperationFilter>();
        });
        services.AddMappings();
        return services;
    }
}
