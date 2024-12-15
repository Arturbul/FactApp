using FactApp.Application.DTOs;

namespace FactApp.Application.Interfaces
{
    public interface IFactService
    {
        /// <summary>
        /// Deletes a specified number of facts from the beginning of the file. If no count is specified, deletes all facts.
        /// </summary>
        /// <param name="fileName">The name of the file containing the facts.</param>
        /// <param name="count">The number of facts to delete. If null, all facts are deleted.</param>
        /// <returns>The number of facts deleted.</returns>
        Task<int> DeleteFacts(string fileName, int? count);

        /// <summary>
        /// Retrieves a specified number of facts from the file.
        /// </summary>
        /// <param name="fileName">The name of the file containing the facts.</param>
        /// <param name="top">The number of facts to retrieve. If null, retrieves all facts.</param>
        /// <returns>A <see cref="FactsResponse"/> containing the retrieved facts.</returns>
        Task<FactsResponse> GetFacts(string fileName, int? top);

        /// <summary>
        /// Fetches a new fact from the repository, saves it to a file, and returns it as a response.
        /// </summary>
        /// <param name="fileName">The name of the file to save the fact to.</param>
        /// <returns>A <see cref="FactResponse"/> containing the saved fact, or null if fetching failed.</returns>
        Task<FactResponse?> SaveNewFact(string fileName);

        /// <summary>
        /// Fetches multiple new facts from the repository, saves them to a file, and returns them as a response.
        /// </summary>
        /// <param name="fileName">The name of the file to save the facts to.</param>
        /// <param name="count">The number of facts to fetch and save. Default is 1.</param>
        /// <returns>A <see cref="FactsResponse"/> containing the saved facts, or null if fetching failed.</returns>
        Task<FactsResponse?> SaveNewFacts(string fileName, int count = 1);
    }
}
