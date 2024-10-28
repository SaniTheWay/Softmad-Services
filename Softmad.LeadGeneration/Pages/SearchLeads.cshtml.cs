using Dapr.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Softmad.LeadGeneration.Models;
using Softmad.Services.Models;

namespace Softmad.LeadGeneration.Pages
{
    [Authorize]
    public class PrivacyModel : PageModel
    {
        private const string AppId = "Softmad-Services-LeadGeneration";
        private const string MethodURL = "api/LeadGeneration/GetAllLeads/";
        public List<Lead> leadList { get; set; }

        private readonly DaprClient _daprClient;
        
        public Lead Lead { get; set; }
        
        private readonly ILogger<PrivacyModel> _logger;
        
        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        public PrivacyModel(ILogger<PrivacyModel> logger, DaprClient dapr)
        {
            _logger = logger;
            _daprClient = dapr;
        }

        public async Task OnGet()
        {
            if (_daprClient != null && SearchString != null)
            {
                leadList = await GetSearchResultLeads(SearchString);
            }
            else
            {
                leadList = await GetLeads();
            }
        }
        public async Task<List<Lead>> GetLeads()
        {
                return await _daprClient.InvokeMethodAsync<List<Lead>>(HttpMethod.Get, AppId, MethodURL);
        }

        public async Task <List<Lead>> GetSearchResultLeads(string SearchString)
        {
            var searchLeads = await _daprClient.InvokeMethodAsync<List<Lead>>(HttpMethod.Get, AppId, $"api/LeadGeneration/GetSearchLeads/{SearchString}");
            return searchLeads;
        }



    }
}
