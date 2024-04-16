using System.ComponentModel.DataAnnotations;

namespace Softmad.Services.Models
{
    public class Lead
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public LeadType Type { get; set; }
        [Required]
        public decimal Budget { get; set; }
        [Required]
        public CustomerDetails CustomerDetails { get; set; }
        
        [Required]
        public string Requirements { get; set; }
        [Required]
        public string ClosurePlan { get; set; }
        [Required]
        public string ExistingMachines { get; set; }
        
        public string Remarks { get; set; }
    }

    public enum LeadType
    {
        Hot,
        Mild,
        Cold
    }

    public class CustomerDetails
    {
        [Key]
        public Guid Id { get; set;}
        [Required]
        public HospitalDetails HospitalDetails { get; set; }
        [Required]
        public DoctorDetails DoctorDetails { get; set; }
    }

    public class HospitalDetails:Address
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }

    public class DoctorDetails
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Specialization { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }


}
