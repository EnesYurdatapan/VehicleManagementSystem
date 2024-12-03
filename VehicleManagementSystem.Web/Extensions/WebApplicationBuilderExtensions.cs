using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace VehicleManagementSystem.Web.Extensions
{
    public static class WebApplicationBuilderExtensions
    {
        public static WebApplicationBuilder AddAppAuthentication(this WebApplicationBuilder builder)
        {
            var settingsSection = builder.Configuration.GetSection("JwtOptions");
            var secret = settingsSection.GetValue<string>("Secret");
            var issuer = settingsSection.GetValue<string>("Issuer");
            var audience = settingsSection.GetValue<string>("Audience");
            var key = Encoding.ASCII.GetBytes(secret);

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true, // Token'in güvenlik anahtarını doğrula
                        IssuerSigningKey = new SymmetricSecurityKey(key), // Güvenlik anahtarı
                        ValidateIssuer = true, // Token'i oluşturanı doğrula
                        ValidIssuer = issuer, // Kabul edilecek Issuer
                        ValidateAudience = true, // Token'in hedef kitlesini doğrula
                        ValidAudience = audience, // Kabul edilecek Audience
                        ValidateLifetime = true, // Token'in süresini doğrula
                        ClockSkew = TimeSpan.Zero // Süre farkını ortadan kaldır
                    };
                });

            return builder;
        }
    }
}
