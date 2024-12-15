using FactApp.Domain.Models;

namespace FactApp.Domain.Interfaces.Repositories
{
    public interface IFactRepository
    {
        Task<Fact?> GetNewFact();
    }
}
