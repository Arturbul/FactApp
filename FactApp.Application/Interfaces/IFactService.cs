using FactApp.Application.DTOs;

namespace FactApp.Application.Interfaces
{
    public interface IFactService
    {
        Task<FactResponse?> SaveNewFact();
    }
}
