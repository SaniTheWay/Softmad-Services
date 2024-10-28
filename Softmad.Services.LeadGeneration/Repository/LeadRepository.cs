using Microsoft.EntityFrameworkCore;
using Softmad.Services.LeadGeneration.Data;
using Softmad.Services.LeadGeneration.Repository.Interfaces;
using Softmad.Services.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
                return leads.OrderByDescending(x => x.LastUpdated).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "One or more error occured while fetching Leads.");
                throw;
            }
        }

        public async Task<Guid> SaveLead(Lead leadEntry)
        {
            try
            {
                var leadEntity = await _dataContext.Leads.AddAsync(leadEntry);
                await SaveChanges();
                return leadEntity.Entity.Id;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString(), "Error Occured while saving the new lead entry.");
                throw;
            }
        }

        public async Task<Lead> GetLeadByIdAsync(Guid id)
        {
            return await _dataContext.Leads
                                 .Include(l => l.CustomerDetails)
                                 .ThenInclude(cd => cd.HospitalDetails)
                                 .Include(l => l.CustomerDetails.DoctorDetails)
                                 .FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task<List<Lead>> GetCurrentUserLeads(Guid currentUserId)
        {
            return await _dataContext.Leads
                                 .Include(l => l.CustomerDetails)
                                 .ThenInclude(cd => cd.HospitalDetails)
                                 .Include(l => l.CustomerDetails.DoctorDetails)
                                 .Where(l => l.EmployeeId == currentUserId)
                                 .ToListAsync();
        }

        public async Task UpdateLeadAsync(Lead lead)
        {
            _dataContext.Leads.Update(lead);
            await SaveChanges();
        }
        public List<Lead> GetSearchResultLeadsAsync(string SearchString)
        {
            var sleads = new List<Lead>();
            if (!string.IsNullOrEmpty(SearchString))
            {
                try
                {
                    sleads = _dataContext.Leads.Include(l => l.CustomerDetails)
                                 .ThenInclude(cd => cd.HospitalDetails)
                                 .Include(l => l.CustomerDetails.DoctorDetails).Where(l => l.CustomerDetails.HospitalDetails.Name.Contains(SearchString)).ToList();
                                                    
                    return sleads;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.ToString(), ex.Message);
                    throw;
                }
            }
            return sleads;
        }

        public async Task SaveVisit(Visit visitEntry)
        {
            try
            {
                await _dataContext.Visits.AddAsync(visitEntry);
                await SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString(), $"Error Occured while saving a new visit for lead {visitEntry.LeadId}.");
                throw;
            }
        }

        public async Task<List<Visit>> GetVisitsByLeadIdAsync(Guid leadId)
        {
            try
            {
                var visit = _dataContext.Visits.Where(v => v.LeadId == leadId);
                return visit.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString(), $"No Visit were found or this Lead Id {leadId}");
                throw;
            }
        }

        public async Task<Visit?> GetLatestVisitAsync(Guid leadId)
        {
            try
            {
                var visit = await _dataContext.Visits.FirstOrDefaultAsync(v => v.LeadId == leadId && v.isLatestVisit == true);
                return visit;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString(), ex.Message);
                throw;
            }
        }

        public async Task<List<Visit>> GetAllLatestVisitsAsync()
        {
            try
            {
                var visit = _dataContext.Visits.Where(v => v.isLatestVisit == true);
                if (visit is not null)
                    return visit.ToList();
                throw new Exception($"No latest Visits found");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString(), ex.Message);
                throw;
            }
        }

        public async Task UpdateVisitAsync(Visit visit)
        {
            _dataContext.Visits.Update(visit);
            await SaveChanges();
        }

        public IQueryable<LeadsWithVists> GetCurrentMonthReport()
        {
            var currentMonth = DateTime.Now.Month;
            var currentYear = DateTime.Now.Year;

            var leadsWithLatestVisits = _dataContext.Visits
                .Where(visit => visit.VisitDate.Month == currentMonth
                                && visit.VisitDate.Year == currentYear
                                && visit.isLatestVisit)
                .Join(
                    _dataContext.Leads.Include(l => l.CustomerDetails)
                                                        .ThenInclude(cd => cd.HospitalDetails)
                                                        .Include(l => l.CustomerDetails)
                                                        .ThenInclude(cd => cd.DoctorDetails),
                    visit => visit.LeadId,
                    lead => lead.Id,
                    (visit, lead) => new { Visit = visit, Lead = lead }
                )
                .Select(row => new LeadsWithVists
                {
                    Lead = row.Lead,
                    Visit = row.Visit
                });
            return leadsWithLatestVisits;
        }

        public IQueryable<LeadsWithVists> GetCurrentWeekReport()
        {
            var leadsWithLatestVisits = _dataContext.Visits
                .Where(visit => visit.VisitDate.AddDays(7).Date >= DateTime.Now.Date
                                && visit.isLatestVisit)
                .Join(
                    _dataContext.Leads.Include(l => l.CustomerDetails)
                                                        .ThenInclude(cd => cd.HospitalDetails)
                                                        .Include(l => l.CustomerDetails)
                                                        .ThenInclude(cd => cd.DoctorDetails),
                    visit => visit.LeadId,
                    lead => lead.Id,
                    (visit, lead) => new { Visit = visit, Lead = lead }
                )
                .Select(row => new LeadsWithVists
                {
                    Lead = row.Lead,
                    Visit = row.Visit
                });
            return leadsWithLatestVisits;
        }

        private async Task SaveChanges()
        {
            await _dataContext.SaveChangesAsync();
        }
    }
}
