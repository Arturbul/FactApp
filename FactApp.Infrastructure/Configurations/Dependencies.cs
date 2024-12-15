using FactApp.Domain.Interfaces.Repositories;
using FactApp.Domain.Interfaces.Services;
using FactApp.Infrastructure.Repositories;
using FactApp.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FactApp.Infrastructure.Configurations
{
    public class Dependencies
    {
        private static void RepositoriesRegister(IServiceCollection services)
        {
            services.AddHttpClient<IFactRepository, FactRepository>();
            services.AddSingleton<IFileService>(prov => new FileService("Saved"));
        }

        public static void Register(IServiceCollection services)
        {
            RepositoriesRegister(services);
        }
    }
}
