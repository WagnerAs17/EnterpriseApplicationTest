using Microsoft.Extensions.DependencyInjection;
using Volvo.Frota.API.Business;
using Volvo.Frota.API.Business.Interfaces;
using Volvo.Frota.API.Data.Repositories;
using Volvo.Frota.API.Data.Repositories.Interfaces;
using Volvo.Frota.API.Extensions;
using Volvo.Frota.API.Utils.AutoMapper;

namespace Volvo.Frota.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterDependencyInjection(this IServiceCollection services)
        {
            RegisterRepositoriesInjection(services);

            RegisterBusinessInjection(services);

            RegisterAutoMapper(services);
        }

        private static void RegisterRepositoriesInjection(IServiceCollection services)
        {
            services.AddScoped<ICaminhaoRepository, CaminhaoRepository>();
            services.AddScoped(typeof(Entities));
        }

        private static void RegisterBusinessInjection(IServiceCollection services)
        {
            services.AddScoped<ICaminhaoBusiness, CaminhaoBusiness>();

            services.AddTransient<ExceptionMiddleware>();
        }

        private static void RegisterAutoMapper(IServiceCollection services)
        {
            var config = AutoMapperUtils.GetConfigurationMappings();

            var mapper = config.CreateMapper();

            services.AddSingleton(mapper);
        }
    }
}
