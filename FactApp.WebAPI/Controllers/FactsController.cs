using FactApp.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FactApp.Controllers
{
    [ApiController]
    [Route("api/facts")]
    public class FactsController : ControllerBase
    {
        private readonly IFactService _factService;

        public FactsController(IFactService factService)
        {
            _factService = factService;
        }

        [HttpGet]
        public IActionResult GetFacts()
        {
            try
            {
                return Ok();
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
        public IActionResult SaveNewFact()
        {
            try
            {
                _factService.SaveNewFact();
                return Ok();
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

        [HttpDelete]
        public IActionResult DeleteFact()
        {
            try
            {
                return Ok();
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
