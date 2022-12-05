using Book.Application.Configuration;
using Book.Infra.Data.Context;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", true, true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
    .AddEnvironmentVariables();

// ConfigureServices

builder.Services.AddDbContext<BookApiDbContext>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddIdentityConfig(builder.Configuration);

builder.Services.AddApiConfig();

builder.Services.AddSwaggerConfig();

builder.Services.AddLoggingConfig(builder.Configuration);

builder.Services.ResolveDependencies();

var app = builder.Build();
var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

// Configure

app.UseApiConfig(app.Environment);

app.UseSwaggerConfig(apiVersionDescriptionProvider);

app.UseLoggingConfiguration();

app.Run();