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
            return await _leadRepository.SaveLead(lead);
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
                await _leadRepository.UpdateLeadAsync(lead);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating the lead with Id: {lead.Id}");
                throw;
            }
        }

        public async Task CreateVisitAsync(Visit visit)
        {
            try
            {
                // Update last visit property - lastestVisit = false;
                var lastVisit = await GetLatestVisit(visit.LeadId);
                if (lastVisit != null)
                {
                    lastVisit.isLatestVisit = false;
                    await UpdateVisit(lastVisit);
                }
                // Create New Visit
                await _leadRepository.SaveVisit(visit);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"--- Error: While creating new Visit for Lead - {visit.LeadId}");
                throw;
            }
        }

        public async Task<List<Visit>> GetVisitByIdAsync(Guid leadId)
        {
            return await _leadRepository.GetVisitsByLeadIdAsync(leadId);
        }

        public async Task<Visit> GetLatestVisit(Guid leadId)
        {
            try
            {
                var latestVisit = await _leadRepository.GetLatestVisitAsync(leadId);
                return latestVisit;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<List<Visit>> GetAllLatestVisitsAsync()
        {
            try
            {
                var latestVisits = await _leadRepository.GetAllLatestVisitsAsync();
                return latestVisits;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task UpdateVisit(Visit visit)
        {
            try
            {
                await _leadRepository.UpdateVisitAsync(visit);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating visit with lead Id: {visit.LeadId}");
                throw;
            }
        }

        public ReportResponse GetCurrentMonthReport()
        {
            _logger.LogDebug("Generating report");
            var a = new ReportResponse()
            {
                Response = _leadRepository.GetCurrentMonthReport().ToList()
            };
            return a;
        }

        public ReportResponse GetCurrentWeekReport()
        {
            _logger.LogDebug("Generating Report");
            var b = new ReportResponse()
            {
                Response = _leadRepository.GetCurrentWeekReport().ToList()
            };
            return b;
        }

    }
}
