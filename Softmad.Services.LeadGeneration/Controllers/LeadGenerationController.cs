using Microsoft.AspNetCore.Mvc;
using Softmad.Services.LeadGeneration.Services.Interfaces;
using Softmad.Services.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Softmad.Services.LeadGeneration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeadGenerationController : ControllerBase
    {

        private readonly ILeadGenerationService _leadGenerationService;
        private readonly ILogger<LeadGenerationController> _logger;

        public LeadGenerationController(ILeadGenerationService leadGenerationService, ILogger<LeadGenerationController> logger)
        {
            _leadGenerationService = leadGenerationService;
            _logger = logger;
        }
        // GET: api/<LeadGenerationController>
        [HttpGet]
        public async Task<IEnumerable<Lead>> GetLeads()
        {
            var result = await _leadGenerationService.GetLeads();
            return result;
        }

        // GET api/<LeadGenerationController>/5
        [HttpGet("{id}")]
        public async Task <Lead> Get(Guid id)
        {
            var value = await _leadGenerationService.GetLeadByIdAsync(id);
            return value;
        }

        [HttpGet("GetUserLeads/{currentUserId}")]
        public async Task<IActionResult> GetByUser(Guid currentUserId)
        {
                var leads = await _leadGenerationService.GetCurrentUserLeads(currentUserId);
                if (leads == null)
                {
                    return NotFound($"No leads found for the current employee {currentUserId}");
                }
                return Ok(leads);
        }

        // POST api/<LeadGenerationController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Lead lead)
        {
            try
            {
                var result = await _leadGenerationService.PostLeadAsync(lead);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<LeadGenerationController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] Lead newLead)
        {
            try
            {
                if (newLead != null)
                {
                    await _leadGenerationService.UpdateLeadAsync(newLead);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);   
                throw;
            }
            return Ok();
        }
        // DELETE api/<LeadGenerationController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {

        }

        #region Visit APIs

        // POST api/<LeadGenerationController>
        [HttpPost("addvisit")]
        public async Task<IActionResult> CreateVisit([FromBody] Visit visit)
        {
            try
            {
                await _leadGenerationService.CreateVisitAsync(visit);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError("One or more error occured.");
                return BadRequest(ex.Message);
            }
        }

        #endregion
    }
}
