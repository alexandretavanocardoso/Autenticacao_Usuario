using GS.Api.Extensions;
using GS.Data.Data.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace GS.Api.Configuration
{
    public static class IdentityConfig
    {
        public static void AddIdentityConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            // Context
            services.AddDbContext<GameStoreContext>(
                options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // Configurando o Identity
            services.AddDefaultIdentity<IdentityUser>() // Usuario
                .AddRoles<IdentityRole>() // Roles - perfis
                .AddErrorDescriber<IdentityMensagemPortugues>() // Convertendo mensagens de erros em portugues
                .AddEntityFrameworkStores<GameStoreContext>() // Context
                .AddDefaultTokenProviders(); // Servico token

            var appSettingsSection = configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(jwtOptions => {
                jwtOptions.RequireHttpsMetadata = false;
                jwtOptions.SaveToken = true;
                jwtOptions.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true, // Valida se o emitor tem que ser o mesmo do token com base na chave
                    IssuerSigningKey = new SymmetricSecurityKey(key), // Configurando chave
                    ValidateIssuer = true, // Valida o emitor
                    ValidateAudience = true, // Valida onde o token é valido
                    ValidAudience = appSettings.ValidoEm, // configurando Audience
                    ValidIssuer = appSettings.Emissor // Configurando emissor
                };
            });
        }
    }
}
