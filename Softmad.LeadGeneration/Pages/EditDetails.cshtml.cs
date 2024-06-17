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
        public List<City> Cities { get; } = new List<City>
        {
            new City { CityName = "Raipur GPO", Pincode = "492001" },
            new City { CityName = "Tatibandh", Pincode = "492099" },
            new City { CityName = "Shankar Nagar", Pincode = "492007" },
            new City { CityName = "Samta Colony", Pincode = "492002" },
            new City { CityName = "Pandri", Pincode = "492004" },
            new City { CityName = "New Rajendra Nagar", Pincode = "492015" },
            new City { CityName = "Byron Bazar", Pincode = "492003" },
            new City { CityName = "Telibandha", Pincode = "492006" },
            new City { CityName = "Vidhan Sabha Road", Pincode = "493111" },
            new City { CityName = "Sejbahar", Pincode = "492015" },
            new City { CityName = "Mana Camp", Pincode = "492016" },
            new City { CityName = "Daldal Seoni", Pincode = "492006" },
            new City { CityName = "Mowa", Pincode = "492005" },
            new City { CityName = "Mathpurena", Pincode = "492013" },
            new City { CityName = "Amanaka", Pincode = "492010" },
            new City { CityName = "Gudhiyari", Pincode = "492009" }
        };

        public EditDetailsModel(DaprClient daprClient)
        {
            _daprClient = daprClient;
            //_navManager = navigationManager;
        }
        private Guid Id { get; set; }
        [BindProperty]
        public Lead Lead { get; set; }
        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            Id = id;
            Lead = await _daprClient.InvokeMethodAsync<Lead>(HttpMethod.Get, AppId, MethodURL + $"{id}");
            if (Lead == null)
            {
                return NotFound();
            }
            return Page();
        }

        public void CancelEdit()
        {
            //Response.Redirect("/lead/" + id + "/details");
            //_navManager.NavigateTo(_navManager.BaseUri + "/lead/" + Id + "/details");
        }

        public async Task<IActionResult> OnPostSubmitForm()
        {
            try
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
                await _daprClient.InvokeMethodAsync<Lead>(HttpMethod.Put, AppId, MethodURL + $"{Lead.Id}", Lead);
                return RedirectToPage($"~/lead/{Lead.Id}/details");
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                throw;
            }
        }
    }
}
