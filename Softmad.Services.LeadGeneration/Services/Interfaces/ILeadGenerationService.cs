using Softmad.Services.Models;

namespace Softmad.Services.LeadGeneration.Services.Interfaces
{
    public interface ILeadGenerationService
    {
        public Task<bool> PostLead();
        public Task<List<Lead>> GetLeads();
    }
}
