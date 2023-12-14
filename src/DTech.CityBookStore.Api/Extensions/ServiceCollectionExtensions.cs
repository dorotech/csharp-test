using DTech.CityBookStore.Api.Configurations;
using DTech.CityBookStore.Application.Users;
using DTech.CityBookStore.Domain.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DTech.CityBookStore.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSwaggerConfigs(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {            
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "City Book Store - API",
                Description = "Backend API RESTful for City Book Store."
            });
            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "Put your JWT in this format: Bearer {your token}",
                Name = "Authorization",
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                    Array.Empty<string>()
                }
            });
        });

        return services;
    }

    public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services,
            IConfiguration configuration)
    {
        var appSettingsSection = configuration.GetSection(IdentityConfiguration.CONFIGURATION_SECTION);
        services.Configure<IdentityConfiguration>(appSettingsSection);

        var appSettings = appSettingsSection.Get<IdentityConfiguration>();

        var fileKey = LoadKeyFromFile(appSettings.PublicKeyFilePath);

        var key = Encoding.ASCII.GetBytes(fileKey);

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(bearerOptions =>
        {
            bearerOptions.RequireHttpsMetadata = true;
            bearerOptions.SaveToken = true;
            bearerOptions.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidAudience = appSettings.ValidateAt,
                ValidIssuer = appSettings.Issuer
            };
            bearerOptions.Events = new JwtBearerEvents
            {
                OnAuthenticationFailed = context =>
                {
                    if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                    {
                        context.Response.Headers.Add("IS-TOKEN-EXPIRED", "true");
                        context.Response.StatusCode = 401;
                    }
                    return Task.CompletedTask;
                }
            };
        });

        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddScoped<IUser, AppUser>();

        return services;
    }

    public static IApplicationBuilder UseIdentityConfiguration(this IApplicationBuilder app)
    {
        app.UseAuthentication();
        app.UseAuthorization();

        return app;
    }

    private static string LoadKeyFromFile(string path)
    {
        var fileContextString = File.ReadAllText(path, Encoding.UTF8);
        return fileContextString;
    }
}
