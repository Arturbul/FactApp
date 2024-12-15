﻿using AutoMapper;
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
            var newFact = await FetchNewFactContent();

            var response = _mapper.Map<FactResponse?>(newFact);
            return response;
        }

        public async Task<FactsResponse?> SaveNewFacts(string fileName, int count = 1)
        {
            var newFacts = new List<string>();
            for (int i = 0; i < count; i++)
            {
                var factToSave = await FetchNewFactContent();
                if (factToSave == null)
                {
                    break;
                }
                newFacts.Add(factToSave);
            }

            await _fileService.SaveToFileAsync(fileName, newFacts);

            var response = _mapper.Map<FactsResponse>(newFacts);
            return response;
        }

        private async Task<string?> FetchNewFactContent()
        {
            var newFact = await _factRepository.GetNewFact();
            if (newFact == null)
            {
                return null;
            }

            var factToSave = _mapper.Map<NewFactCommand>(newFact);
            return factToSave.ToString();
        }

        public async Task<FactsResponse> GetFacts(string fileName, int? top)
        {
            var fileLines = await _fileService.GetFileContent(fileName, top);

            var result = _mapper.Map<FactsResponse>(fileLines);
            return result;
        }

        public async Task<int> DeleteFacts(string fileName, int? count)
        {
            var deleted = await _fileService.DeleteLines(fileName, count);
            return deleted;
        }
    }
}
