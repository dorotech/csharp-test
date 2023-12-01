using System.Globalization;
using API;
using API.Extensions;
using Application;
using FluentValidation;
using Infrastructure;
using Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);
builder.AddSerilog(builder.Configuration, "API Book Inventory Manager");

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddControllers();

builder.Services.AddApiServices(builder.Configuration);

builder.Services.AddHttpLogging(o => { });

ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("en");

var app = builder.Build();

app.UseHttpLogging();

if(builder.Configuration.GetValue<bool>("InitialiseDatabase"))
    await app.InitialiseDatabaseAsync();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();