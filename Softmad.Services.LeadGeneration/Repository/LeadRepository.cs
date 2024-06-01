using Microsoft.EntityFrameworkCore;
using Softmad.Services.LeadGeneration.Data;
using Softmad.Services.LeadGeneration.Repository.Interfaces;
using Softmad.Services.Models;

namespace Softmad.Services.LeadGeneration.Repository
{
    public class LeadRepository : ILeadRepository
    {
        private readonly DataContext _dataContext;
        private readonly ILogger<LeadRepository> _logger;
        public LeadRepository(DataContext dataContext, ILogger<LeadRepository> logger)
        {
            _dataContext = dataContext;
            _logger = logger;
        }

        public async Task<List<Lead>> GetLeadsAsync()
        {
            try
            {

                ///Learn: https://www.linkedin.com/advice/3/how-do-you-avoid-ef-lazy-loading-pitfalls-n1-problem
                //Here '.Include()' do EAGER loading 
                var leads = _dataContext.Leads.Include(l => l.CustomerDetails)
                                                        .ThenInclude(cd => cd.HospitalDetails)
                                                        .Include(l => l.CustomerDetails)
                                                        .ThenInclude(cd => cd.DoctorDetails);
                return leads.OrderByDescending(x=>x.LastUpdated).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "One or more error occured while fetching Leads.");
                throw;
            }
        }

        public async Task<bool> SaveLead(Lead leadEntry)
        {
            try
            {
                await _dataContext.Leads.AddAsync(leadEntry);
                await SaveChanges();
                return true;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString(), "Error Occured while saving the new lead entry.");
                throw;
            }
        }

        private async Task SaveChanges()
        {
            await _dataContext.SaveChangesAsync();
        }
    }
}
