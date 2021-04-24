using GS.Api.Bussines.Interfaces.Repositorys.Authorization;
using GS.Api.Bussines.Interfaces.Services.Authorization;
using GS.Api.Bussines.Repositorys.Authorization;
using GS.Api.Bussines.Services.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace GS.Api.Extensions
{
    public static class DependencyInjectionExtension
    {
        public static void AddDependencyInjectionServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthorizationServices, AuthorizationServices>();
        }

        public static void AddDependencyInjectionRepositorys(this IServiceCollection services)
        {
            services.AddScoped<IAuthorizationRepository, AuthorizationRepository>();
        }
    }
}
