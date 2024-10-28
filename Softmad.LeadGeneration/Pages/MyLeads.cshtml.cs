using Dapr.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Softmad.LeadGeneration.HelperMethods;
using Softmad.Services.Models;
using System.Net.Http;
using System.Text.Json;

namespace Softmad.LeadGeneration.Pages
{
    [Authorize]
    public class MyLeadsModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private const string AppId = "Softmad-Services-LeadGeneration";
        private const string MethodURL = "api/LeadGeneration/GetUserLeads/";
        private Guid CurrentUserId => new Guid(User.Claims.ToList()[0].Value);
        public List<Lead>? leadList { get; set; }

        private readonly DaprClient _daprClient;
        public Lead? Lead { get; set; }


        public MyLeadsModel(DaprClient dapr, IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("MyApiClient");
            _daprClient = dapr;
        }
        public async Task OnGet()
        {

            if (_daprClient != null)
            {
                leadList = await HTTPClient_GetCurrentUserLeads();
            }
        }
        //private async Task<List<Lead>> GetCurrentUserLeads()
        //{
        //    var result = await _daprClient.InvokeMethodAsync<IEnumerable<Lead>>(HttpMethod.Get, AppId, MethodURL + $"{CurrentUserId}");
        //    return result.ToList();
        //}

        private async Task<List<Lead>?> HTTPClient_GetCurrentUserLeads()
        {
            // Call the API using the configured HttpClient
            List<Lead>? result = new();
            var response = await _httpClient.GetAsync(MethodURL + $"{CurrentUserId}");
            if (response.IsSuccessStatusCode)
            {
                result = await HelperMethod.HttpResponseConverter(result, response);
            }
            return result;
        }

        public async Task<int> GetVisitsCount(Guid leadId)
        {
            int result = 0;
            var response = await _httpClient.GetAsync($"api/LeadGeneration/GetVisitsCount/{leadId}");
            if (response.IsSuccessStatusCode)
            {
                result = await HelperMethod.HttpResponseConverter(result, response);
            }
            return result;
        }

        internal string GetCSSBadgeColor(LeadType? leadType = null!, LeadStatus? leadStatus = null!)
        {
            string bgColorClass = string.Empty;
            if (leadType != null)
            {
                switch (leadType)
                {
                    case LeadType.Hot:
                        bgColorClass = "bg-danger";
                        break;
                    case LeadType.Cold:
                        bgColorClass = "bg-info";
                        break;
                    case LeadType.Mild:
                        bgColorClass = "bg-warning";
                        break;
                    default:
                        break;
                }
            }
            if (leadStatus != null)
            {
                switch (leadStatus)
                {
                    case LeadStatus.Active:
                        bgColorClass = "bg-primary";
                        break;
                    case LeadStatus.Passive:
                        bgColorClass = "bg-secondary";
                        break;
                    case LeadStatus.Lost:
                        bgColorClass = "bg-danger";
                        break;
                    case LeadStatus.Completed:
                        bgColorClass = "bg-success";
                        break;
                    default:
                        break;
                }
            }

            return bgColorClass;
        }

    }
}
