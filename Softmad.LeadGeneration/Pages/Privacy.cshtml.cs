using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Softmad.LeadGeneration.Models;
using Softmad.Services.Models;

namespace Softmad.LeadGeneration.Pages
{
    public class PrivacyModel : PageModel
    {
        private const string AppId = "Softmad-Services-LeadGeneration";
        private const string MethodURL = "api/LeadGeneration/GetLeads";
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
            if (_daprClient != null)
            {
                leadList = await GetLeads();
            }
        }
        private async Task<List<Lead>> GetLeads()
        {
            var result = await _daprClient.InvokeMethodAsync<IEnumerable<Lead>>(HttpMethod.Get, AppId, MethodURL);
            return result.ToList();



                    }
        }

    }
