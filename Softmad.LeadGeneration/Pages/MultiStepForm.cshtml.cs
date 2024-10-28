using AutoMapper;
using Dapr.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Softmad.LeadGeneration.Models.DTOs;
using Softmad.Services.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Softmad.LeadGeneration.Pages
{
    [Authorize]
    public class MultiStepForm : PageModel
    {
        #region City Pincode
        //public List<City> Cities { get; } = new List<City>
        //{
        //    new City { CityName = "Raipur GPO", Pincode = "492001" },
        //    new City { CityName = "Tatibandh", Pincode = "492099" },
        //    new City { CityName = "Shankar Nagar", Pincode = "492007" },
        //    new City { CityName = "Samta Colony", Pincode = "492002" },
        //    new City { CityName = "Pandri", Pincode = "492004" },
        //    new City { CityName = "New Rajendra Nagar", Pincode = "492015" },
        //    new City { CityName = "Byron Bazar", Pincode = "492003" },
        //    new City { CityName = "Telibandha", Pincode = "492006" },
        //    new City { CityName = "Vidhan Sabha Road", Pincode = "493111" },
        //    new City { CityName = "Sejbahar", Pincode = "492015" },
        //    new City { CityName = "Mana Camp", Pincode = "492016" },
        //    new City { CityName = "Daldal Seoni", Pincode = "492006" },
        //    new City { CityName = "Mowa", Pincode = "492005" },
        //    new City { CityName = "Mathpurena", Pincode = "492013" },
        //    new City { CityName = "Amanaka", Pincode = "492010" },
        //    new City { CityName = "Gudhiyari", Pincode = "492009" }
        //};
        #endregion

        private const string AppId = "Softmad-Services-LeadGeneration";
        private const string MethodURL = "api/LeadGeneration/";
        
        #region Service Injection
        private readonly HttpClient _httpClient;
        private readonly DaprClient _daprClient;
        private readonly IMapper _mapper;
        private readonly ILogger<MultiStepForm> _logger;
        #endregion
        
        [BindProperty]
        public LeadDTO? Lead { get; set; }
        
        public MultiStepForm(DaprClient daprClient, IMapper mapper, IHttpClientFactory httpClientFactory, ILogger<MultiStepForm> logger)
        {
            _daprClient = daprClient;
            _mapper = mapper;
            _httpClient = httpClientFactory.CreateClient("MyApiClient");
            _logger = logger;
        }
        public async Task<IActionResult> OnPostSubmitForm()
        {
            var userId = new Guid(User.Claims.ToList()[0].Value);
            if (!ModelState.IsValid)
            {
                // If model validation fails, return the page with validation errors
                return Page();
            }
            //Lead.CustomerDetails.HospitalDetails.PinCode = Cities.FirstOrDefault(x => x.CityName == Lead.CustomerDetails.HospitalDetails.City).Pincode;
            var request_Lead = _mapper.Map<Lead>(Lead);
            request_Lead.EmployeeId = userId;

            var request_Visit0 = _mapper.Map<Visit>(Lead.AdditionalDetails);
            request_Visit0.VisitId = new Guid();
            request_Visit0.VisitDate = DateTimeOffset.Now;
            request_Visit0.VisitType = VisitTypeEnum.VisitZero;
            request_Visit0.SalesPersonId = userId;

            #region DaprClient Version (Obselete)
            //var leadId = await _daprClient.InvokeMethodAsync<Lead, Guid>(HttpMethod.Post, AppId, MethodURL, request_Lead);
            //request_Visit0.LeadId = leadId;
            //await _daprClient.InvokeMethodAsync<Visit>(HttpMethod.Post, AppId, MethodURL + "/visit/add", request_Visit0);
            #endregion

            #region HTTPClient Version
            var Lead_response = await _httpClient.PostAsJsonAsync<Lead>(MethodURL, request_Lead) ;
            if (Lead_response.IsSuccessStatusCode)
            {
                // fetching the response of API (i.e. Lead Id) from the Json
                var jsonString = await Lead_response.Content.ReadAsStringAsync();
                //Converting the LeadId (string to GUID)
                var leadId = JsonSerializer.Deserialize<Guid>(jsonString);
                request_Visit0.LeadId = leadId;

                // Create Visit - 0 for the latest lead created using its Id
                var Visit_response = await _httpClient.PostAsJsonAsync<Visit>(MethodURL + "visit/add", request_Visit0) ;
                if(!Visit_response.IsSuccessStatusCode)
                {
                    _logger.LogError($"Visit not created for Lead Id - {leadId} because of some error. See visit/add API for more details.");
                    return Page();
                }
            }
            #endregion

            return RedirectToPage("/MyLeads");
        }
    }
}





