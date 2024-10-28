using AutoMapper;
using Dapr.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Softmad.Services.Models;
using System.Text.Json;

namespace Softmad.LeadGeneration.Pages
{
    [Authorize]
    public class AddVisitModel : PageModel
    {

        #region Constants
        private const string AppId = "Softmad-Services-LeadGeneration";
        private const string MethodBaseURL = "api/LeadGeneration/";
        #endregion
        #region Service Injection
        private readonly HttpClient _httpClient;
        private readonly DaprClient _daprClient;
        private readonly ILogger<AddVisitModel> _logger;
        #endregion
        public AddVisitModel(DaprClient _daprClient, IHttpClientFactory httpClientFactory, ILogger<AddVisitModel> logger)
        {
            this._daprClient = _daprClient;
            _httpClient = httpClientFactory.CreateClient("MyApiClient");
            _logger = logger;
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
                //latestVisit = await _daprClient.InvokeMethodAsync<Visit?>(HttpMethod.Get, AppId, MethodBaseURL + $"visit/latest/{id}");
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                latestVisit = await _httpClient.GetFromJsonAsync<Visit>(MethodBaseURL + $"visit/latest/{id}", options);
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

            //await _daprClient.InvokeMethodAsync<Visit>(HttpMethod.Post, AppId, MethodBaseURL + "visit/add", Visit);
            var Visit_response = await _httpClient.PostAsJsonAsync<Visit>(MethodBaseURL + "visit/add", Visit);
            if (!Visit_response.IsSuccessStatusCode)
            {
                _logger.LogError($"Visit not created for Lead Id - {Visit.LeadId} because of some error. See visit/add API for more details.");
                return Page();
            }
            return Redirect($"/lead/{Visit.LeadId}/visitdetails");
        }
    }
}
