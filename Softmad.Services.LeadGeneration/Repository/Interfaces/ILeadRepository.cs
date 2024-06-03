using Softmad.Services.Models;

namespace Softmad.Services.LeadGeneration.Repository.Interfaces
{
    public interface ILeadRepository
    {
        public Task<bool> SaveLead(Lead leadEntry);
        public Task<List<Lead>> GetLeadsAsync();

        public Task<Lead> GetLeadByIdAsync(Guid id);

        public Task<List<Lead>> GetCurrentUserLeads(Guid currentUserId);


    }
}
