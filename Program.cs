using dorotec_backend_test.Classes.Mapping;
using dorotec_backend_test.Interfaces;
using dorotec_backend_test.Models;
using dorotec_backend_test.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<BookstoreDbContext>(options => {
    options.UseSqlite("Data Source=test_db.sqlite");
});
builder.Services.AddScoped<IBookService, BookService>();

builder.Services.AddAutoMapper(typeof(BookProfile));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
