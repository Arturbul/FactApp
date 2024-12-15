using FactApp.Domain.Interfaces.Repositories;
using FactApp.Domain.Interfaces.Services;
using FactApp.Infrastructure.Repositories;
using FactApp.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FactApp.Infrastructure.Configurations
{
    /// <summary>
    /// Class responsible for registering dependencies in the dependency injection container.
    /// </summary>
    public class Dependencies
    {
        private static void RepositoriesRegister(IServiceCollection services)
        {
            services.AddHttpClient<IFactRepository, FactRepository>();
            services.AddSingleton<IFileService>(prov => new FileService(Path.Combine(Directory.GetCurrentDirectory(), "Saved")));
        }

        /// <summary>
        /// Registers all necessary dependencies in the service collection.
        /// </summary>
        /// <param name="services">The service collection where the dependencies will be registered.</param>
        public static void Register(IServiceCollection services)
        {
            RepositoriesRegister(services);
        }
    }
}
