using System.Text;
using dorotec_backend_test.Classes.Mapping;
using dorotec_backend_test.Interfaces;
using dorotec_backend_test.Models;
using dorotec_backend_test.Services;
using dorotec_backend_test.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Services.AddW3CLogging(options =>
{
    // options.FileName = "";
    options.LogDirectory = "Logs";
});
builder.Services.AddLogging(loggingBuilder =>
{
    loggingBuilder.AddFile(Path.Join("Logs", "Error", "{0:yyyy}-{0:MM}-{0:dd}.log"), configure =>
    {
        configure.FormatLogFileName = fName =>
            String.Format(fName, DateTime.UtcNow);
        configure.Append = true;
        configure.MinLevel = LogLevel.Error;
        configure.FileSizeLimitBytes = 10240;
    });
});

// Add services to the container.
builder.Services.AddControllers().AddNewtonsoftJson();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.ASCII.GetBytes(Secret.Key)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo() { Title = "Volo Librorum - dorotec challange", Version = "v1", Contact = new OpenApiContact() { Name = "gabriel-panz", Url = new Uri("https://github.com/gabriel-panz") } });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        In = ParameterLocation.Header,
        Description = "Por favor informe um Token JWT válido, seguindo o padrão 'Bearer [seu token]'",
        Name = "Authorization",
        BearerFormat = "JWT",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme()
            {
                Reference = new OpenApiReference()
                {
                    Type = ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new List<string>()
        }
    });
});

builder.Services.AddDbContext<BookstoreDbContext>(options =>
{
    options.UseSqlite("Data Source=test_db.sqlite");
});

builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddAutoMapper(typeof(BookProfile));

builder.WebHost.UseKestrel(options =>
{
    options.ListenAnyIP(5000);
});

var app = builder.Build();

app.UseW3CLogging();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
