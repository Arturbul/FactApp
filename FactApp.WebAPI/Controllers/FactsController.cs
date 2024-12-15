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

        /// <summary>
        /// Retrieves a list of facts from the specified file.
        /// </summary>
        /// <param name="fileName">The name of the file to read facts from. Defaults to "facts.txt".</param>
        /// <param name="top">The number of facts to retrieve. If null or 0, retrieves all available facts.</param>
        /// <returns>
        /// - **200 OK**: A list of facts from the file.
        /// - **400 BadRequest**: If there is an invalid argument passed.
        /// - **404 NotFound**: If no facts are found or the file does not exist.
        /// - **409 Conflict**: If there is a conflict while fetching the facts.
        /// - **500 Internal Server Error**: If an unexpected error occurs.
        /// </returns>
        [HttpGet]
        public async Task<IActionResult> GetFacts(string fileName = "facts.txt", int? top = 0)
        {
            try
            {
                var result = await _factService.GetFacts(fileName, top);
                Response.Headers.Location = _fileService.GetFilePath(fileName);
                return Ok(result);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
            catch (NullReferenceException e)
            {
                return NotFound(e.Message);
            }
            catch (InvalidOperationException e)
            {
                return Conflict(e.Message);
            }
            catch (InvalidDataException e)
            {
                return StatusCode(500, e.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Saves a new fact to the specified file.
        /// </summary>
        /// <param name="fileName">The name of the file to save the fact. Defaults to "facts.txt".</param>
        /// <param name="count">The number of facts to save. Defaults to 1.</param>
        /// <returns>
        /// - **201 Created**: A fact or multiple facts have been successfully saved to the file.
        /// - **400 BadRequest**: If an invalid argument is passed.
        /// - **404 NotFound**: If the file cannot be found.
        /// - **409 Conflict**: If a conflict arises while saving the fact.
        /// - **500 Internal Server Error**: If an unexpected error occurs.
        /// </returns>
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

        /// <summary>
        /// Saves a single fact to the specified file.
        /// </summary>
        /// <param name="fileName">The name of the file to save the fact.</param>
        /// <param name="filePath">The path of the file where the fact will be saved.</param>
        /// <returns>Returns a Created response with the file path and saved fact if successful, or a Problem response if no fact is available to save.</returns>
        private async Task<IActionResult> SaveSingleFact(string fileName, string filePath)
        {
            var result = await _factService.SaveNewFact(fileName);

            if (result == null)
            {
                return Problem("No fact to save.");
            }

            return Created(filePath, result);
        }

        /// <summary>
        /// Saves multiple facts to the specified file.
        /// </summary>
        /// <param name="fileName">The name of the file to save the facts.</param>
        /// <param name="count">The number of facts to save.</param>
        /// <param name="filePath">The path of the file where the facts will be saved.</param>
        /// <returns>Returns a Created response with the file path and saved facts if successful, or a Problem response if no facts are available to save.</returns>
        private async Task<IActionResult> SaveMultipleFacts(string fileName, int count, string filePath)
        {
            var facts = await _factService.SaveNewFacts(fileName, count);

            if (facts == null || facts.Facts == null || !facts.Facts.Any())
            {
                return Problem("No facts to save.");
            }

            return Created(filePath, facts);
        }

        /// <summary>
        /// Deletes a specified number of facts from the file.
        /// </summary>
        /// <param name="count">The number of facts to delete. If null, deletes all facts.</param>
        /// <param name="fileName">The name of the file to delete facts from. Defaults to "facts.txt".</param>
        /// <returns>
        /// - **200 OK**: The facts were successfully deleted. The number of deleted facts is returned.
        /// - **400 BadRequest**: If there is an invalid argument or issue with the file name or count.
        /// - **404 NotFound**: If the file does not exist.
        /// - **409 Conflict**: If there is a conflict during deletion.
        /// - **500 Internal Server Error**: If an unexpected error occurs during the deletion process.
        /// </returns>
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
