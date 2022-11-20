using System.Reflection;
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

// Logging
builder.Logging.ClearProviders();

builder.Logging.AddConsole();

builder.Services.AddW3CLogging(options =>
{
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

// Controllers
builder.Services.AddControllers().AddNewtonsoftJson();

// Authentication Configuration
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

// Swagger Configuration
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo()
    {
        Title = "Volo Librorum - dorotec challenge",
        Version = "v1",
        Contact = new OpenApiContact() { Name = "gabriel-panz", Url = new Uri("https://github.com/gabriel-panz") },
        Description = "<b>API básica</b> de uma livraria com autenticação para o desafio de <b>back-end</b> da <b>Dorotec</b>. \r\n\r\nAs rotas de <b>pesquisa</b> dos livros são <b>livres</b> para uso <b>público</b>, sem token de autenticação. As demais rotas <b>necessitam</b> de um <b>token JWT</b> que pode ser informado abaixo no botão <b>'Authorize'</b>. \r\n\r\nAs <b>credenciais de acesso</b> padrões são: <b>{ \"login\":\"admin\", \"password\":\"12345\" }</b>"
    });

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        In = ParameterLocation.Header,
        Description = "Por favor informe um <b>Token JWT</b> válido no campo abaixo.",
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

// Persistance
builder.Services.AddDbContext<BookstoreDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
});

// Services
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<ITokenService, TokenService>();

// DTO - Model mapping
builder.Services.AddAutoMapper(typeof(BookProfile), typeof(AdminProfile));

// Host Configuration
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
