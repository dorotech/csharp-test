using APIBook.Model;
using APIBook.Repository;
using APIBook.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;

namespace APIBook
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            
            Log.Logger = (Serilog.ILogger)new LoggerConfiguration()
            .WriteTo.File("programlogs/log.txt", Serilog.Events.LogEventLevel.Information)
                //.WriteTo.Console()
                .CreateLogger();



            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("V0.0.1 Dirty", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "YOUNG BOOKS API", Version = "v0.0.1 Dirty" });
            });
            builder.Services.AddScoped<IBookRepository, BookRepository>();
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<BookContext>(db => db.UseSqlServer(connectionString));


            string key = "$$os_seguredos_dos_admins1337_iaiaiaiaiaiii$$"; // chave
            
            builder.Services.AddAuthentication(c => {
                c.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                c.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(c =>
            {
                c.RequireHttpsMetadata = false;
                c.SaveToken = true;
                c.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });


            builder.Services.AddSingleton<JwtAuth>(new JwtAuth(key)); // adiciona a chave para a cripotgrafia para o token

            builder.Host.UseSerilog(Log.Logger); // adicionar o Serilog como sistema de logs

            var app = builder.Build();

            //create db
            DataBaseMigrateService.MigrationInitialsation(app); // criar a o banco de dado caso não tenha

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
            }
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/V0.0.1 Dirty/swagger.json", "YOUNG BOOKS API v0.0.1 Dirty"));


            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}