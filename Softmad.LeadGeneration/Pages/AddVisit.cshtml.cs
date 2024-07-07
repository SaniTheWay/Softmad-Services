using Dapr.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Softmad.Services.Models;

namespace Softmad.LeadGeneration.Pages
{
    [Authorize]
    public class AddVisitModel : PageModel
    {

        #region Constants
        private const string AppId = "Softmad-Services-LeadGeneration";
        private const string MethodBaseURL = "api/LeadGeneration/";
        #endregion
        private readonly DaprClient _daprClient;
        public AddVisitModel(DaprClient _daprClient)
        {
            this._daprClient = _daprClient;
        }

        public Guid LeadId { get; set; }
        [BindProperty]
        public Visit Visit { get; set; } = new();
        public bool CanCreateVisit { get; set; } = true;
        public async Task<IActionResult> OnGet(Guid id)
        {
            LeadId = id;
            Visit? latestVisit;
            try
            {
                latestVisit = await _daprClient.InvokeMethodAsync<Visit?>(HttpMethod.Get, AppId, MethodBaseURL + $"visit/latest/{id}");
            }
            catch (Exception ex)
            {
                latestVisit = null;
            }
            Visit.LeadId = LeadId;
            if (latestVisit != null)
            {
                if (latestVisit.Status == LeadStatus.Completed || latestVisit.Status == LeadStatus.Lost)
                {
                    CanCreateVisit = false;
                }
                Visit.Type = latestVisit.Type;
                Visit.Status = latestVisit.Status;
                Visit.Budget = latestVisit.Budget;
                Visit.Requirements = latestVisit.Requirements;
                Visit.ClosurePlan = latestVisit.ClosurePlan;
                Visit.ExistingMachines = latestVisit.ExistingMachines;
                Visit.Competitor = latestVisit.Competitor;
                Visit.CompetitorName = latestVisit.CompetitorName;
                Visit.CompetitorModel = latestVisit.CompetitorModel;
                Visit.Remarks = latestVisit.Remarks;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                // If model validation fails, return the page with validation errors
                return Page();
            }
            Visit.SalesPersonId = new Guid(User.Claims.ToList()[0].Value);

            await _daprClient.InvokeMethodAsync<Visit>(HttpMethod.Post, AppId, MethodBaseURL + "visit/add", Visit);
            return Redirect($"/lead/{Visit.LeadId}/visitdetails");
        }
    }
}
