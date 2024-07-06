using AutoMapper;
using Dapr.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Softmad.LeadGeneration.Models.DTOs;
using Softmad.Services.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Softmad.LeadGeneration.Pages
{
    [Authorize]
    public class MultiStepForm : PageModel
    {
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
        [BindProperty]
        public LeadDTO? Lead { get; set; }

        private const string AppId = "Softmad-Services-LeadGeneration";
        private const string MethodURL = "api/LeadGeneration";

        private readonly DaprClient _daprClient;
        private readonly IMapper _mapper;
        public MultiStepForm(DaprClient daprClient, IMapper mapper)
        {
            _daprClient = daprClient;
            _mapper = mapper;
        }
        public async Task<IActionResult> OnPostSubmitForm()
        {
            var userId = new Guid(User.Claims.ToList()[0].Value);
            if (!ModelState.IsValid)
            {
                // If model validation fails, return the page with validation errors
                return Page();
            }
            Lead.CustomerDetails.HospitalDetails.PinCode = Cities.FirstOrDefault(x => x.CityName == Lead.CustomerDetails.HospitalDetails.City).Pincode;
            var request_Lead = _mapper.Map<Lead>(Lead);
            request_Lead.EmployeeId = userId;

            var request_Visit0 = _mapper.Map<Visit>(Lead.AdditionalDetails);
            request_Visit0.VisitId = new Guid();
            request_Visit0.VisitDate = DateTimeOffset.Now;
            request_Visit0.VisitType = VisitTypeEnum.VisitZero;
            request_Visit0.SalesPersonId = userId;

            var leadId = await _daprClient.InvokeMethodAsync<Lead, Guid>(HttpMethod.Post, AppId, MethodURL, request_Lead);
            request_Visit0.LeadId = leadId;
            await _daprClient.InvokeMethodAsync<Visit>(HttpMethod.Post, AppId, MethodURL + "/addvisit", request_Visit0);

            return RedirectToPage("/MyLeads");
        }
    }
}





