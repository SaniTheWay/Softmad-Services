using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Softmad.Services.Models;


namespace Softmad.LeadGeneration.Pages
{
    public class LeadDetailsModel : PageModel
    {
        private readonly DaprClient _daprClient;
        private const string AppId = "Softmad-Services-LeadGeneration";
        private const string MethodURL = "api/LeadGeneration/";
        public LeadDetailsModel(DaprClient daprClient)
        {
            _daprClient = daprClient;
        }

        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        public Lead Lead { get; set; }
        public Visit LatestVisit { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Lead = await _daprClient.InvokeMethodAsync<Lead>(HttpMethod.Get, AppId, MethodURL + $"{Id}");
            if (Lead == null)
            {
                return NotFound();
            }
            else
            {
                LatestVisit = await _daprClient.InvokeMethodAsync<Visit>(HttpMethod.Get, AppId, MethodURL + $"visit/latest/{Id}");
                if (LatestVisit == null)
                {
                    return NotFound();
                }
            }
            return Page();
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
