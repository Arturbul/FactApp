using FactApp.Domain.Models;

namespace FactApp.Domain.Interfaces.Repositories
{
    public interface IFactRepository
    {
        /// <summary>
        /// Retrieves a new fact from an external API.
        /// </summary>
        /// <returns>A <see cref="Fact"/> object containing the fetched fact, or null if the operation fails.</returns>
        /// <exception cref="InvalidOperationException">Thrown when there is an issue with fetching data from the API.</exception>
        /// <exception cref="Exception">Thrown when an unexpected error occurs while fetching data.</exception>
        Task<Fact?> GetNewFact();
    }
}
