using FactApp.Application.Interfaces;
using FactApp.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace FactApp.Controllers
{
    [ApiController]
    [Route("api/facts")]
    public class FactsController : ControllerBase
    {
        private readonly IFactService _factService;
        private readonly IFileService _fileService;
        public FactsController(IFactService factService, IFileService fileService)
        {
            _factService = factService;
            _fileService = fileService;
        }

        [HttpGet]
        public async Task<IActionResult> GetFacts(string fileName = "facts.txt", int? top = 0)
        {
            try
            {
                var result = await _factService.GetFacts(fileName, top);
                Response.Headers.Location = _fileService.GetFilePath(fileName);
                return Ok(result);
            }
            catch (NullReferenceException e)
            {
                return NotFound(e.Message);
            }
            catch (InvalidOperationException e)
            {
                return Conflict(e.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> SaveNewFact(string fileName = "facts.txt", int count = 1)
        {
            try
            {
                var filePath = _fileService.GetFilePath(fileName);

                if (count == 1)
                {
                    return await SaveSingleFact(fileName, filePath);
                }
                else
                {
                    return await SaveMultipleFacts(fileName, count, filePath);
                }
            }
            catch (NullReferenceException e)
            {
                return NotFound(e.Message);
            }
            catch (InvalidOperationException e)
            {
                return Conflict(e.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        private async Task<IActionResult> SaveSingleFact(string fileName, string filePath)
        {
            var result = await _factService.SaveNewFact(fileName);

            if (result == null)
            {
                return Problem("No fact to save.");
            }

            return Created(filePath, result);
        }

        private async Task<IActionResult> SaveMultipleFacts(string fileName, int count, string filePath)
        {
            var facts = await _factService.SaveNewFacts(fileName, count);

            if (facts == null || facts.Facts == null || !facts.Facts.Any())
            {
                return Problem("No facts to save.");
            }

            return Created(filePath, facts);
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteFact(int? count, string fileName = "facts.txt")
        {
            try
            {
                var deleted = await _factService.DeleteFacts(fileName, count);
                Response.Headers.Location = _fileService.GetFilePath(fileName);
                return Ok($"Deleted {deleted} facts.");
            }
            catch (NullReferenceException e)
            {
                return NotFound(e.Message);
            }
            catch (InvalidOperationException e)
            {
                return Conflict(e.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
