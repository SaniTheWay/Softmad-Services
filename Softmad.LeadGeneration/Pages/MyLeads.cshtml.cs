using Dapr.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Softmad.Services.Models;

namespace Softmad.LeadGeneration.Pages
{
    [Authorize]
    public class MyLeadsModel : PageModel
    {
        private const string AppId = "Softmad-Services-LeadGeneration";
        private const string MethodURL = "api/LeadGeneration/GetUserLeads/";
        private Guid CurrentUserId => new Guid(User.Claims.ToList()[0].Value);
        public List<Lead> leadList { get; set; }

        private readonly DaprClient _daprClient;
        public Lead Lead { get; set; }

        
        public MyLeadsModel(DaprClient dapr)
        {
            _daprClient = dapr;
        }
        public async Task OnGet()
        {
            if (_daprClient != null)
            {
                leadList = await GetCurrentUserLeads();
            }

        }
        private async Task<List<Lead>> GetCurrentUserLeads()
        {
            var result = await _daprClient.InvokeMethodAsync<IEnumerable<Lead>>(HttpMethod.Get, AppId, MethodURL +$"{CurrentUserId}");
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
