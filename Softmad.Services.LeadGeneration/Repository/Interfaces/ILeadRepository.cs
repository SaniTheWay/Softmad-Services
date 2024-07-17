using Softmad.Services.Models;

namespace Softmad.Services.LeadGeneration.Repository.Interfaces
{
    public interface ILeadRepository
    {
        public Task<Guid> SaveLead(Lead leadEntry);
        public Task<List<Lead>> GetLeadsAsync();

        public Task<Lead> GetLeadByIdAsync(Guid id);

        public Task<List<Lead>> GetCurrentUserLeads(Guid currentUserId);

        public Task UpdateLeadAsync(Lead lead);

        public Task SaveVisit(Visit visit);

        public Task<List<Visit>> GetVisitsByLeadIdAsync(Guid leadId);
        public Task<Visit?> GetLatestVisitAsync(Guid leadId);
        public Task<List<Visit>> GetAllLatestVisitsAsync();
        public Task UpdateVisitAsync(Visit visit);
        public IQueryable<LeadsWithVists> GetCurrentMonthReport();

        public IQueryable<LeadsWithVists> GetCurrentWeekReport();

    }
}
