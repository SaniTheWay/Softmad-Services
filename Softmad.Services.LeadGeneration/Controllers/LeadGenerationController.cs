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

        public LeadGenerationController(ILeadGenerationService leadGenerationService)
        {
            _leadGenerationService = leadGenerationService;
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
        public string Get(int id)
        {
            return "value";
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
        public void Put(int id, [FromBody] string value)
        {
            return;
        }

        // DELETE api/<LeadGenerationController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
