using Microsoft.AspNetCore.Identity;

namespace Softmad.Service.Account.Models
{
    public class AppUser : IdentityUser
    {
        // Additional user-related properties
        public string? UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmployeeID { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        // Add more properties as needed
    }
}
