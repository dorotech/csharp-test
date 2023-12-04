using Bookstore.API;
using Bookstore.Infrastructure.Security;
using Microsoft.OpenApi.Models;
using Bookstore.API.Workers;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

services.AddSingleton(x => new CryptographyKeys(configuration[$"{nameof(CryptographyKeys)}:Key"]!
    , configuration[$"{nameof(CryptographyKeys)}:IV"]!));
services.AddScoped<Cryptography>();
services.AddAuthentication().AddBearerToken();
services.AddAuthorization();
services.AddHostedService<SeedDatabaseWorker>();
services.AddControllers();
services.AddEndpointsApiExplorer();
services.ConfigureRepositories();
services.ConfigureDatabase(configuration);
services.ConfigureExceptionHandlers();
services.ConfigureExternalServices();

services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Description = "Authorization based in Json Web Token (JWT)",
        In = ParameterLocation.Header,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseExceptionHandler(opt => { });

await app.RunAsync();