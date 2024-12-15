using FactApp.Application.DTOs;

namespace FactApp.Application.Interfaces
{
    public interface IFactService
    {
        Task<int> DeleteFacts(string fileName, int count);
        Task<FactsResponse> GetFacts(string fileName);
        Task<FactResponse?> SaveNewFact(string fileName);
    }
}
