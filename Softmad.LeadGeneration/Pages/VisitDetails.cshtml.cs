using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Softmad.Services.Models;

namespace Softmad.LeadGeneration.Pages
{
    public class VisitDetailsModel : PageModel
    {
        public int VisitCount = 1;
        private const string AppId = "Softmad-Services-LeadGeneration";
        private const string MethodURL = "api/LeadGeneration/GetVisitById/";
        private readonly DaprClient _daprClient;
        public VisitDetailsModel(DaprClient daprClient)
        {
            _daprClient = daprClient;
        }
        [BindProperty]
        public Guid LeadId { get; set; }
        public List<Visit> VisitList { get; set; }


        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            LeadId = id;
            VisitList = await _daprClient.InvokeMethodAsync<List<Visit>>(HttpMethod.Get, AppId, MethodURL + $"{id}");
            if (VisitList == null)
            {
                return NotFound();
            }
            return Page();
        }
        public void OnPost()
        {

        }

        
    }
}
