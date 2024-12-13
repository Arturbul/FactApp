using FactApp.Application.Interfaces;
using FactApp.Domain.Interfaces.Repositories;

namespace FactApp.Application.Services
{
    internal class FactService : IFactService
    {
        private readonly IFactRepository _factRepository;

        public FactService(IFactRepository factRepository)
        {
            _factRepository = factRepository;
        }

        public void SaveNewFact()
        {
            _factRepository.GetNewFact();
        }
    }
}
