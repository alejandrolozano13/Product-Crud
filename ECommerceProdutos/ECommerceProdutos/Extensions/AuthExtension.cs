using Domain.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ECommerceProdutos.Extensions
{
    public static class AuthExtension
    {
        public static void AddJwtAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication()
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = EnviromentsVariables.Jwt_Issuer,
                        ValidAudience = EnviromentsVariables.Jwt_Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(EnviromentsVariables.Jwt_Key))
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context =>
                        {
                            Console.WriteLine($"Falha na autenticação: {context.Exception.Message}");
                            Console.WriteLine($"Stack Trace: {context.Exception.StackTrace}");
                            return Task.CompletedTask;
                        }
                    };
                });
        }
    }
}