using Softmad.Services.LeadGeneration.Data;
using Softmad.Services.LeadGeneration.Repository.Interfaces;
using Softmad.Services.LeadGeneration.Services.Interfaces;
using Softmad.Services.Models;

namespace Softmad.Services.LeadGeneration.Services
{
    public class LeadGenerationService : ILeadGenerationService
    {
        private readonly ILeadRepository _leadRepository;
        public LeadGenerationService(ILeadRepository leadRepository)
        {
            _leadRepository = leadRepository; 
        }

        /// <inheritdoc/>
        public async Task<List<Lead>> GetLeads()
        {
            var LeadsList = await _leadRepository.GetLeadsAsync();  
            return LeadsList;
        }

        /// <inheritdoc/>
        public async Task<bool> PostLeadAsync(Lead lead)
        {
            await _leadRepository.SaveLead(lead);
            return true;
        }
    }
}
