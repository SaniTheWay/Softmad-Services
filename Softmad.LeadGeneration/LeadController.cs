using Microsoft.AspNetCore.Mvc;
using Softmad.Services.Models;

namespace WebApplication1.Controllers
{
    public class LeadController : Controller
    {
        [HttpPost]
        public IActionResult SubmitLead(Lead lead, UserProfile userProfile)
        {
            // Handle lead submission
            // You can access lead properties like lead.Type, lead.Budget, etc.
            // You can also access related properties like lead.CustomerDetails.HospitalDetails.Name

            // Handle user profile submission
            // You can access userProfile properties like userProfile.Username, userProfile.FirstName, etc.
            // You can also access address properties like userProfile.AddressLine1, userProfile.City, etc.

            // For demonstration purposes, let's just return a success message
            return Content("Lead and User Profile submitted successfully!");
        }
    }
}
