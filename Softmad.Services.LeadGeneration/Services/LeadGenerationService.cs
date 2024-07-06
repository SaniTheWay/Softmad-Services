using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Softmad.Services.LeadGeneration.Data;
using Softmad.Services.LeadGeneration.Repository.Interfaces;
using Softmad.Services.LeadGeneration.Services.Interfaces;
using Softmad.Services.Models;

namespace Softmad.Services.LeadGeneration.Services
{
    public class LeadGenerationService : ILeadGenerationService
    {
        private readonly ILeadRepository _leadRepository;
        private readonly ILogger<LeadGenerationService> _logger;
        public LeadGenerationService(ILeadRepository leadRepository, ILogger<LeadGenerationService> logger)
        {
            _leadRepository = leadRepository;
            _logger = logger;
        }

        /// <inheritdoc/>
        public async Task<List<Lead>> GetLeads()
        {
            var LeadsList = await _leadRepository.GetLeadsAsync();
            return LeadsList;
        }

        /// <inheritdoc/>
        public async Task<Guid> PostLeadAsync(Lead lead)
        {
            return  await _leadRepository.SaveLead(lead);
        }

        public async Task<Lead> GetLeadByIdAsync(Guid id)
        {
            var lead = await _leadRepository.GetLeadByIdAsync(id);
            if (lead != null)
            {
                return lead;
            }
            throw new Exception($"No Lead Detials Found for {id}");
        }

        public async Task<List<Lead>> GetCurrentUserLeads(Guid currentUserId)
        {
            var leads = await _leadRepository.GetCurrentUserLeads(currentUserId);
            if (leads != null)
            {
                return leads;
            }
            throw new Exception($"No leads found for the current employee {currentUserId}");
        }

        public async Task UpdateLeadAsync(Lead lead)
        {
            try
            {
                var updatedLead = _leadRepository.UpdateLeadAsync(lead);
                if (updatedLead == null)
                {
                    throw new Exception($"Lead not found for Id: {lead.Id}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating the lead with Id: {lead.Id}");
                throw;
            }
        }

        public async Task CreateVisitAsync(Visit visit)
        {
            await _leadRepository.SaveVisit(visit);
        }

        public async Task<List<Visit>> GetVisitByIdAsync(Guid leadId)
        {
            return await _leadRepository.GetVisitByIdAsync(leadId);
        }

        public async Task<Visit> GetLatestVisit(Guid leadId)
        {
            var visits = await _leadRepository.GetVisitByIdAsync(leadId);
            if (visits != null)
                return visits.OrderByDescending(v => v.VisitDate).ToList()[0];
            _logger.LogError($"Can not find any latest visit for Lead Id {leadId}");
            throw new Exception("One or more exception occured");
        }
    }
}
