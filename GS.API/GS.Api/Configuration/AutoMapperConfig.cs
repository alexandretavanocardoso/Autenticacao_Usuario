using AutoMapper;
using GS.Bussines.Mappings;
using Microsoft.Extensions.DependencyInjection;

namespace GS.Api.Configuration
{
    public static class AutoMapperConfig
    {
        public static void AddAutoMapperConfig(this IServiceCollection services)
        {
            var configMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DtoUser());
            });

            IMapper mapper = configMapper.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
