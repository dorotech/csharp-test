using DoroTechChallenge.Context;
using DoroTechChallenge.Models;
using DoroTechChallenge.Repositories;
using DoroTechChallenge.Services;
using DoroTechChallenge.Services.Auth;
using DoroTechChallenge.Services.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Data;
using System.Data.SqlClient;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//database config
string connectionString = builder.Configuration.GetConnectionString("DoroTech");
builder.Services.AddTransient<IDbConnection>(conn => new SqlConnection(connectionString));
builder.Services.AddDbContext<DoroTechContext>(options =>
{
    options.UseSqlServer(connectionString);
    options.UseLazyLoadingProxies();
});

//JWT config
var provider = builder.Services.BuildServiceProvider();
var configuration = provider.GetRequiredService<IConfiguration>();

var secretKey = configuration.GetValue<string>("JWT:Key");
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

//dependency injection into entity/service
builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddTransient<IBookRepository, BookRepository>();
builder.Services.AddTransient<IBookService, BookService>();
builder.Services.AddTransient<IValidator<Book>, BookValidator>();
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});

//swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "DoroTech API",
        Version = "v1",
        Description = "API construída para o desafio da DoroTech // use EntityFrameworkCore\\Update-Database para atualizar o banco e comecar os testes !!",
        Contact = new OpenApiContact
        {
            Name = "Ramon Barbosa",
            Email = "ramonmfb777@gmail.com",
            Url = new Uri("https://github.com/ramonfbarbosa")
        },
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Por favor utilize Bearer <TOKEN>",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            new string[] { }
        }
    });
});

//server config
builder.Services.AddEndpointsApiExplorer();
var app = builder.Build();
app.UseCors(x => x.AllowAnyMethod().AllowAnyHeader().SetIsOriginAllowed(origin => true).AllowCredentials());
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
