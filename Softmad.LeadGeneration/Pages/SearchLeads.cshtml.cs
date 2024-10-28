using Dapr.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Softmad.LeadGeneration.Models;
using Softmad.Services.Models;
using System.Net.Http;

namespace Softmad.LeadGeneration.Pages
{
    [Authorize(Policy = "admin")]
    public class PrivacyModel : PageModel
    {
        private const string AppId = "Softmad-Services-LeadGeneration";
        private const string MethodURL = "api/LeadGeneration/GetAllLeads/";

        private readonly DaprClient _daprClient;
        private readonly ILogger<PrivacyModel> _logger;
        private readonly HttpClient _httpClient;

        public List<Lead>? leadList { get; set; }
        public Lead Lead { get; set; }        
        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        public PrivacyModel(ILogger<PrivacyModel> logger, DaprClient dapr, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _daprClient = dapr;
            _httpClient = httpClientFactory.CreateClient("MyApiClient");
        }

        public async Task OnGet()
        {
            if (_httpClient != null && SearchString != null)
            {
                leadList = await GetSearchResultLeads(SearchString);
            }
            else
            {
                leadList = await GetLeads();
            }
        }
        public async Task<List<Lead>?> GetLeads()
        {
            //return await _daprClient.InvokeMethodAsync<List<Lead>>(HttpMethod.Get, AppId, MethodURL);
            return await _httpClient.GetFromJsonAsync<List<Lead>>(MethodURL);
        }

        public async Task<List<Lead>?> GetSearchResultLeads(string SearchString)
        {
            //var searchLeads = await _daprClient.InvokeMethodAsync<List<Lead>>(HttpMethod.Get, AppId, $"api/LeadGeneration/GetSearchLeads/{SearchString}");
            var searchLeads = await _httpClient.GetFromJsonAsync<List<Lead>>($"api/LeadGeneration/GetSearchLeads/{SearchString}");
            return searchLeads;
        }
    }
}
