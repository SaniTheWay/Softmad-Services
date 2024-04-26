using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Softmad.Services.Models;

namespace Softmad.LeadGeneration.Pages
{
    public class MultiStepForm : PageModel
    {
        private const string AppId = "Softmad-Services-LeadGeneration";
        private const string MethodURL = "api/LeadGeneration";

        [BindProperty]
        public Lead? Lead { get; set; }
        private readonly DaprClient _daprClient;

        public MultiStepForm(DaprClient daprClient)
        {
            _daprClient = daprClient;
        }
        public async Task<IActionResult> OnPostSubmitForm()
        {
            if (!ModelState.IsValid)
            {
                // If model validation fails, return the page with validation errors
                return Page();
            }

            // Handle form submission here, e.g., save Lead to a database
            // For demonstration purposes, let's assume saving to a database
            // SaveLeadToDatabase(Lead);

            // Redirect to a success page or return a different view
            await _daprClient.InvokeMethodAsync<Lead>(HttpMethod.Post, AppId, MethodURL, Lead);
            return RedirectToPage("/Index");
        }
    }
}
