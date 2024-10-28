using AutoMapper;
using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Softmad.Services.Models;
using System.Text.Json;

namespace Softmad.LeadGeneration.Pages
{
    public class VisitDetailsModel : PageModel
    {
        public int VisitCount = 0;
        private const string AppId = "Softmad-Services-LeadGeneration";
        private const string MethodURL = "api/LeadGeneration/GetVisitById/";
        #region Service Injection
        private readonly HttpClient _httpClient;
        private readonly DaprClient _daprClient;
        private readonly ILogger<VisitDetailsModel> _logger;
        #endregion
        public VisitDetailsModel(DaprClient daprClient, IHttpClientFactory httpClientFactory, ILogger<VisitDetailsModel> logger)
        {
            _daprClient = daprClient;
            _httpClient = httpClientFactory.CreateClient("MyApiClient");
            _logger = logger;
        }
        [BindProperty]
        public Guid LeadId { get; set; }
        public List<Visit>? VisitList { get; set; }


        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            LeadId = id;
            //VisitList = await _daprClient.InvokeMethodAsync<List<Visit>>(HttpMethod.Get, AppId, MethodURL + $"{id}");
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            VisitList = await _httpClient.GetFromJsonAsync<List<Visit>>(MethodURL + $"{LeadId}", options);
            if (VisitList == null)
            {
                return NotFound();
            }
            return Page();
        }
        public void OnPost()
        {

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
