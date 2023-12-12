using DoroTechCSharpTest.API.Configuration;

var builder = WebApplication.CreateBuilder(args);

#region Configure Services

builder.Services.AddJwtConfiguration(builder.Configuration);

builder.Services.AddApiConfiguration(builder.Configuration);

builder.Services.AddAutoMapperConfiguration();

builder.Services.AddSwaggerConfiguration();

builder.Services.AddSwaggerGen();

builder.Services.AddDependencyInjectionConfiguration();

#endregion

var app = builder.Build();

#region Configure app

DbMigrationHelpers.EnsureMigration(app).Wait();

app.UseJwtConfiguration();

app.UseSwaggerConfiguration();

app.UseApiConfiguration(app.Environment);

app.UseAuthorization();

app.Run();

#endregion