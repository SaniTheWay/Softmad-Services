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
        private const string MethodBaseURL = "api/LeadGeneration";
        #endregion
        private readonly DaprClient _daprClient;
        public AddVisitModel(DaprClient _daprClient)
        {
            this._daprClient = _daprClient;
        }

        public Guid LeadId { get; set; }
        [BindProperty]
        public Visit Visit { get; set; } = new();
        
        public void OnGet(Guid id)
        {
            LeadId = id;
            Visit.LeadId = LeadId;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                // If model validation fails, return the page with validation errors
                return Page();
            }            
            Visit.SalesPersonId = new Guid(User.Claims.ToList()[0].Value);
            
            await _daprClient.InvokeMethodAsync<Visit>(HttpMethod.Post, AppId, MethodBaseURL+ "/addvisit", Visit);
            return Redirect($"/lead/{Visit.LeadId}/visitdetails");
        }
    }
}
