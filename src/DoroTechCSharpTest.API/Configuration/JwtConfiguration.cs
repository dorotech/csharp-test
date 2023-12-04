using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace DoroTechCSharpTest.API.Configuration
{
    public static class JwtConfiguration
    {
        public static void AddJwtConfiguration(this IServiceCollection services, IConfiguration configuration)
        {

            var secretKey = "ZWRpw6fDo28gZW0gY29tcHV0YWRvcmE=WHO.WictorHuggo";

            services.AddAuthorization();
            services.AddAuthentication(x =>
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
                                    ValidIssuer = "ForLogic.DevTest.API",
                                    ValidAudience = "Api",
                                    ValidateIssuer = false,
                                    ValidateAudience = false
                                };
                            });
        }

        public static void UseJwtConfiguration(this WebApplication app)
        {
            app.UseAuthentication();

            app.UseAuthorization();
        }
    }
}