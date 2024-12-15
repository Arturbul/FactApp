using FactApp.Application.Interfaces;
using FactApp.Application.Mappers;
using FactApp.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FactApp.Application.Configurations
{
    /// <summary>
    /// Class responsible for registering dependencies in the dependency injection container.
    /// </summary>
    public class Dependencies
    {
        /// <summary>
        /// Registers application services into the dependency injection container.
        /// </summary>
        /// <param name="services">The service collection where the dependencies will be registered.</param>
        private static void EntitiesServiciesRegister(IServiceCollection services)
        {
            services.AddScoped<IFactService, FactService>();

        }

        /// <summary>
        /// Registers AutoMapper profiles into the dependency injection container.
        /// </summary>
        /// <param name="services">The service collection where the AutoMapper profiles will be registered.</param>
        private static void MapperProfilesRegister(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(FactServiceProfile));
        }

        /// <summary>
        /// Registers all dependencies required by the application in the service collection.
        /// </summary>
        /// <param name="services">The service collection where the dependencies will be registered.</param>
        public static void Register(IServiceCollection services)
        {
            EntitiesServiciesRegister(services);
            MapperProfilesRegister(services);

            // Registering dependencies from the Infrastructure layer
            Infrastructure.Configurations.Dependencies.Register(services);
        }
    }
}
