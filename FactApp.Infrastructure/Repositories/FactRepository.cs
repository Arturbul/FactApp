using FactApp.Domain.Interfaces.Repositories;
using FactApp.Domain.Models;
using Newtonsoft.Json;

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

        /// <summary>
        /// Fetches a new fact from the external API (https://catfact.ninja/fact).
        /// </summary>
        /// <returns>A <see cref="Fact"/> object containing the fetched fact, or null if deserialization fails.</returns>
        /// <exception cref="InvalidOperationException">Thrown when there is an issue with the HTTP request.</exception>
        /// <exception cref="Exception">Thrown when an unexpected error occurs while fetching or processing the data.</exception>
        public async Task<Fact?> GetNewFact()
        {
            try
            {
                var json = await _httpClient.GetStringAsync("https://catfact.ninja/fact");
                var result = JsonConvert.DeserializeObject<Fact>(json);
                return result;
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
