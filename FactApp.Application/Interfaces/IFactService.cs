using FactApp.Application.DTOs;

namespace FactApp.Application.Interfaces
{
    public interface IFactService
    {
        Task<int> DeleteFacts(string fileName, int? count);
        Task<FactsResponse> GetFacts(string fileName, int? top);
        Task<FactResponse?> SaveNewFact(string fileName);
        Task<FactsResponse?> SaveNewFacts(string fileName, int count = 1);
    }
}
