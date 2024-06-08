using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Softmad.Services.Models;

namespace Softmad.LeadGeneration.Pages
{
    public class EditDetailsModel : PageModel
    {
        private readonly DaprClient _daprClient;
        //private readonly NavigationManager _navManager;

        private const string AppId = "Softmad-Services-LeadGeneration";
        private const string MethodURL = "api/LeadGeneration/";
        public EditDetailsModel(DaprClient daprClient)
        {
            _daprClient = daprClient;
            //_navManager = navigationManager;
        }
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }
        public Lead Lead { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            Lead = await _daprClient.InvokeMethodAsync<Lead>(HttpMethod.Get, AppId, MethodURL + $"{Id}");
            if (Lead == null)
            {
                return NotFound();
            }
            return Page();
        }

        public void CancelEdit()
        {
            Response.Redirect("/lead/" + Id + "/details");
            //_navManager.NavigateTo(_navManager.BaseUri + "/lead/" + Id + "/details");
        }

        public async Task<IActionResult> OnPostSubmitForm()
        {
            var userId = new Guid(User.Claims.ToList()[0].Value);
            if (!ModelState.IsValid)
            {
                // If model validation fails, return the page with validation errors
                return Page();
            }

            // Handle form submission here, e.g., save Lead to a database
            // For demonstration purposes, let's assume saving to a database
            // SaveLeadToDatabase(Lead);

            // Redirect to a success page or return a different view
            Lead.EmployeeId = userId;
            await _daprClient.InvokeMethodAsync<Lead>(HttpMethod.Post, AppId, MethodURL, Lead);
            return RedirectToPage("/Index");
        }
    }
}
