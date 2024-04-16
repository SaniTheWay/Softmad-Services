using Softmad.Services.Models;

namespace Softmad.Services.LeadGeneration.Repository.Interfaces
{
    public interface ILeadRepository
    {
        public bool SaveChanges();
        public Task<List<Lead>> GetLeadsAsync();

    }
}
