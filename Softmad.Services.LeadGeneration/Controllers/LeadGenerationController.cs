using ClosedXML.Excel;
using ClosedXML.Extensions;
using Microsoft.AspNetCore.Http.HttpResults;
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
        public async Task<Lead> Get(Guid id)
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

        [HttpGet("GetSearchLeads/{SearchString}")]
        public async Task<IActionResult> GetSearchResult(string SearchString)
        {
            var searchleads = await _leadGenerationService.GetSearchResultLeads(SearchString);
            if(searchleads == null)
            {
                return NotFound($"No leads found for {SearchString}");
            }
            return Ok(searchleads);
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
        [HttpPost("visit/add")]
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

        [HttpGet("GetVisitById/{leadId}")]
        public async Task<IActionResult> GetVisitById(Guid leadId)
        {
            try
            {
                var visit = await _leadGenerationService.GetVisitByIdAsync(leadId);
                return Ok(visit);
            }
            catch (Exception ex)
            {
                _logger.LogError("No Visits Found for Id");
                return StatusCode(500, ex);
            }

        }
        [HttpGet("visit/latest/{leadId}")]
        public async Task<IActionResult> GetLatestVisit(Guid leadId)
        {
            try
            {
                var visit = new Visit();
                visit = await _leadGenerationService.GetLatestVisit(leadId);
                return Ok(visit);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"One or more exception occured while fetching latest visit");
                return StatusCode(500, ex);
            }

        }

        [HttpGet("visit/latest")]
        public async Task<IActionResult> GetAllLatestVisits()
        {
            try
            {
                var visit = await _leadGenerationService.GetAllLatestVisitsAsync();
                return Ok(visit);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"One or more exception occured while fetching latest visit");
                return StatusCode(500, ex);
            }

        }
        
        [HttpGet("GetVisitsCount/{leadId}")]
        public async Task<IActionResult> GetVisitsCount(Guid leadId)
        {
            try
            {
                var visit = await _leadGenerationService.GetVisitByIdAsync(leadId);
                return Ok(visit.Count);
            }
            catch (Exception ex)
            {
                _logger.LogError("No Visits Found for Id");
                return Ok(0);
            }

        }
        #endregion


        #region Documents APIs
        [HttpGet("report")]
        public async Task<IActionResult> GetReport([FromQuery] string type)
        {
            switch(type)
            {
                case "weekly":
                    var result1 = _leadGenerationService.GetCurrentWeekReport();
                    return Ok(result1);
                case "monthly":
                    var result = _leadGenerationService.GetCurrentMonthReport();
                    return Ok(result);
                default:
                    throw new Exception($"Report type : {type} does not exist.");
            }
        }
        #endregion

    }
}
