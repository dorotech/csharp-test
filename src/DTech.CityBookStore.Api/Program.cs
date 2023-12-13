using DTech.CityBookStore.Data.Extensions;
using DTech.CityBookStore.Application.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DTech.CityBookStore.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbCityBookStoreContext(configuration);
builder.Services.AddRepositores();
builder.Services.AddCityBookStoreAutoMapper();
builder.Services.AddCityBookStoreServices();
builder.Services.AddSwaggerConfigs();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
