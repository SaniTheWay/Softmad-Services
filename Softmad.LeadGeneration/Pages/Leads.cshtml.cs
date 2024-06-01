using Dapr.Client;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Softmad.Services.Models;

namespace Softmad.LeadGeneration.Pages
{
    public class LeadsModel : PageModel
    {
        private const string AppId = "Softmad-Services-LeadGeneration";
        private const string MethodURL = "api/LeadGeneration";

        public List<Lead> leadList { get; set; }

        private readonly DaprClient _daprClient;
        public LeadsModel(DaprClient dapr)
        {
            _daprClient = dapr;
        }
        public async Task OnGet()
        {
            if (_daprClient != null)
            {
                leadList = await GetAllLeads();
            }

        }
        private async Task<List<Lead>> GetAllLeads()
        {
            var result = await _daprClient.InvokeMethodAsync<IEnumerable<Lead>>(HttpMethod.Get, AppId, MethodURL);
            return result.ToList();
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
            if(leadStatus != null)
            {
                switch (leadStatus)
                {
                    case LeadStatus.Start:
                        bgColorClass = "bg-primary";
                        break;
                    case LeadStatus.Hold:
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
