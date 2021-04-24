using GS.Api.Application.Middleware;
using GS.Api.Configuration;
using GS.Api.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GS.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApiConfig(Configuration);

            services.AddIdentityConfiguration(Configuration);

            services.AddDependencyInjectionRepositorys();

            services.AddDependencyInjectionServices();

            services.AddSwaggerConfig();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {   
            app.UseApiConfig(env);

            app.UseSwaggerConfig();
        }
    }
}
