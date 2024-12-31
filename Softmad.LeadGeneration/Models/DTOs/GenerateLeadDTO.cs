using Softmad.Services.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Softmad.LeadGeneration.Models.DTOs
{
    public class LeadDTO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public CustomerDetails CustomerDetails { get; set; }

        public AdditionalDetails AdditionalDetails { get; set; }    
    }
    public class AdditionalDetails
    {
        [Required]
        public LeadType Type { get; set; }
        [Required]
        public LeadStatus Status { get; set; } = LeadStatus.Active;
        [Required]
        public string Budget { get; set; }
        [Required]
        public string Requirements { get; set; }
        [Required]
        public string ClosurePlan { get; set; }
        [Required]
        [MaxLength(100)]
        public string ExistingMachines { get; set; }
        public bool Competitor { get; set; } //fixed now
        public string? CompetitorName { get; set; }
        public string? CompetitorModel { get; set; }
        [MaxLength(1000)]
        public string? Remarks { get; set; }
    }
    public enum LeadType
    {
        Hot,
        Mild,
        Cold
    }

    public enum LeadStatus
    {
        Active,
        Passive,
        Lost,
        Completed
    }

    public class CustomerDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public HospitalDetails HospitalDetails { get; set; }
        [Required]
        public DoctorDetails DoctorDetails { get; set; }
        [Required]
        public string CustomerType { get; set; }
    }

    public class HospitalDetails : Address
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required] [MaxLength(100)]
        public string Name { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        [MaxLength(10,ErrorMessage ="Phone should not be more than 10 digits")]
        [MinLength(10,ErrorMessage ="Phone should be strict 10 digits")]
        public string OwnerPhone { get; set; }
        public string OwnerName { get; set; }
        public string BioMedicalPhone { get; set; } = "NA";
        public string BioMedicalName { get; set; } = "NA";
        public string PurchaseHeadPhone { get; set; } = "NA";
        public string PurchaseHeadName { get; set; } = "NA";
        //public ICollection<HospitalContact> HospitalContacts { get; set; }
    }

    public class DoctorDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Specialization { get; set; }
        [Phone]
        [MaxLength(10, ErrorMessage = "Should not be more than 10 digits")]
        [MinLength(10, ErrorMessage = "Should be strict 10 digits")]
        public string Phone { get; set; }
        [EmailAddress]
        public string Email { get; set; }
    }
}
