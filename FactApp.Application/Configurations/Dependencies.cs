using FactApp.Application.Interfaces;
using FactApp.Application.Mappers;
using FactApp.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FactApp.Application.Configurations
{
    public class Dependencies
    {
        private static void EntitiesServiciesRegister(IServiceCollection services)
        {
            services.AddScoped<IFactService, FactService>();

        }
        private static void MapperProfilesRegister(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(FactServiceProfile));
        }

        public static void Register(IServiceCollection services)
        {
            EntitiesServiciesRegister(services);
            MapperProfilesRegister(services);

            //DI for DataAccess 
            Infrastructure.Configurations.Dependencies.Register(services);
        }
    }
}
