using Softmad.Services.Models;

namespace Softmad.Services.LeadGeneration.Repository.Interfaces
{
    public interface ILeadRepository
    {
        public Task<bool> SaveLead(Lead leadEntry);
        public Task<List<Lead>> GetLeadsAsync();

    }
}
