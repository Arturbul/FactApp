using AutoMapper;
using FactApp.Application.Commands;
using FactApp.Application.DTOs;
using FactApp.Application.Interfaces;
using FactApp.Domain.Interfaces.Repositories;
using FactApp.Domain.Interfaces.Services;

namespace FactApp.Application.Services
{
    internal class FactService : IFactService
    {
        private readonly IFactRepository _factRepository;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        public FactService(IFactRepository factRepository, IMapper mapper, IFileService fileService)
        {
            _factRepository = factRepository;
            _mapper = mapper;
            _fileService = fileService;
        }

        public async Task<FactResponse?> SaveNewFact(string fileName)
        {
            var newFact = await _factRepository.GetNewFact();
            if (newFact == null)
            {
                return null;
            }

            var factToSave = _mapper.Map<NewFactCommand>(newFact);
            await _fileService.SaveToFileAsync(fileName, factToSave.ToString());

            var response = _mapper.Map<FactResponse>(newFact);
            return response;
        }

        public async Task<FactsResponse> GetFacts(string fileName)
        {
            var fileLines = await _fileService.GetFileContent(fileName);
            var result = _mapper.Map<FactsResponse>(fileLines);
            return result;
        }

        public async Task<int> DeleteFacts(string fileName, int count)
        {
            var deleted = await _fileService.DeleteLines(fileName, count);
            return deleted;
        }
    }
}
