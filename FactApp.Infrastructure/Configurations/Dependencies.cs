using FactApp.Domain.Interfaces.Repositories;
using FactApp.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace FactApp.Infrastructure.Configurations
{
    public class Dependencies
    {
        private static void RepositoriesRegister(IServiceCollection services)
        {
            services.AddHttpClient<IFactRepository, FactRepository>();
        }

        public static void Register(IServiceCollection services)
        {
            RepositoriesRegister(services);
        }
    }
}
