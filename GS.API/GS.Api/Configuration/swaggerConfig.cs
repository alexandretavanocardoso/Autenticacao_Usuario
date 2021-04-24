using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace GS.Api.Configuration
{
    public static class swaggerConfig
    {
        public static void AddSwaggerConfig(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { 
                    Version = "v1",
                    Title = "Game Store",
                    Description = "Sistema Web desenvolvido em AspNet.Core 5.0",
                    Contact = new OpenApiContact { Name = "Game Store", Email = "gameStore@outlook.com"}
                });

                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    BearerFormat = "Entre com o token disponível",
                    Name = "Autorização",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme {
                          Reference = new OpenApiReference
                          {
                              Id="Bearer",
                              Type = ReferenceType.SecurityScheme
                          }
                      }, new List<string>()
                    }
                });
            });
        }

        public static IApplicationBuilder UseSwaggerConfig(this IApplicationBuilder app) {

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Game Store - V1"));

            return app;
        }
    }
}
