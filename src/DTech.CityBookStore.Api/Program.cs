using DTech.CityBookStore.Data.Extensions;
using DTech.CityBookStore.Application.Extensions;

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
