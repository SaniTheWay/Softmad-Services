using Softmad.Services.Models;

namespace Softmad.Services.LeadGeneration.Services.Interfaces
{
    public interface ILeadGenerationService
    {
        /// <summary>
        /// Get method to list all the Leads
        /// </summary>
        /// <returns>List of <see cref="Lead"/></returns>
        public Task<List<Lead>> GetLeads();

        /// <summary>
        /// Post method to add a new entry of <see cref="Lead"/>
        /// </summary>
        /// <param name="lead"></param>
        /// <returns>true if succeed, else false</returns>
        public Task<Guid> PostLeadAsync(Lead lead);
        /// <summary>
        /// Get Lead By LeadId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<Lead> GetLeadByIdAsync(Guid id);

        public Task<List<Lead>> GetCurrentUserLeads(Guid currentUserId);

        public Task UpdateLeadAsync(Lead lead);

        /// <summary>
        /// Create a new visit for a lead 
        /// </summary>
        /// <param name="visit"></param>
        /// <returns></returns>
        public Task CreateVisitAsync(Visit visit);

        public Task<List<Visit>> GetVisitByIdAsync(Guid leadId);
        /// <summary>
        /// Returns latest lead for <seealso cref="Lead.Id"/>
        /// </summary>
        /// <param name="leadId"></param>
        /// <returns></returns>
        public Task<Visit> GetLatestVisit(Guid leadId);

    }
}
