using FactApp.Domain.Interfaces.Repositories;

namespace FactApp.Infrastructure.Repositories
{
    internal class FactRepository : IFactRepository
    {
        private readonly HttpClient _httpClient;

        public FactRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.Timeout = TimeSpan.FromSeconds(30);
        }

        public async void GetNewFact()
        {
            try
            {
                var json = await _httpClient.GetStringAsync("https://catfact.ninja/fact");
            }
            catch (InvalidOperationException)
            {
                throw new InvalidOperationException("Cannot fetch data.");
            }
            catch (Exception)
            {
                throw new Exception("Error while fetching data.");
            }
        }
    }
}
