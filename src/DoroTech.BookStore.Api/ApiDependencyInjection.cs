namespace DoroTech.BookStore.Api;

public static class ApiDependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services
            .AddControllers()
            .AddJsonOptions(x =>
            {
                x.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
                x.JsonSerializerOptions.Converters.Add(new NullableDateOnlyJsonConverter());
            })
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
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });

            swaggwerOptions.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });

            swaggwerOptions.MapType<DateOnly>(() => new OpenApiSchema
            {
                Type = "string",
                Format = "date"
            });
        });
        
        return services;
    }
}
