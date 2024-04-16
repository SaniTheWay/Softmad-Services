using Softmad.Services.LeadGeneration.Data;
using Softmad.Services.LeadGeneration.Repository.Interfaces;
using Softmad.Services.Models;

namespace Softmad.Services.LeadGeneration.Repository
{
    public class LeadRepository:ILeadRepository
    {
        private readonly DataContext _dataContext;
        public LeadRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<List<Lead>> GetLeadsAsync()
        {
            var leads = _dataContext.Leads.ToList();

            return leads;
        }

        public bool SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
