using Softmad.Services.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Contracts;

namespace Softmad.Services.Models
{
    public class Lead
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required]
        public LeadType Type { get; set; }
        [Required]
        public LeadStatus Status { get; set; } = LeadStatus.Start;
        public DateTimeOffset? LastUpdated { get; set; } = DateTimeOffset.UtcNow;
        [Required]
        public string Budget { get; set; }      
        [Required]
        public CustomerDetails CustomerDetails { get; set; }
        
        [Required]
        public string Requirements { get; set; }
        [Required]
        public string ClosurePlan { get; set; }
        [Required]
        public string ExistingMachines { get; set; }
        public bool Competitor { get; set; } //fixed now
        public string? CompetitorName { get; set; }
        public string? CompetitorModel { get; set; }
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
        Start,
        Hold,
        Lost,
        Completed
    }

    public class CustomerDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set;}
        [Required]
        public HospitalDetails HospitalDetails { get; set; }
        [Required]
        public DoctorDetails DoctorDetails { get; set; }
        [Required]
        public string CustomerType { get; set; }
    }

    public class HospitalDetails:Address
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string OwnerPhone { get; set; }
        public string OwnerName { get; set; }
        public string BioMedicalPhone { get; set; }
        public string BioMedicalName { get; set; }
        public string PurchaseHeadPhone { get; set; }
        public string PurchaseHeadName { get; set; }
        //public ICollection<HospitalContact> HospitalContacts { get; set; }
    }

    public class DoctorDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Specialization { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }

    //public class HospitalContact
    //{
    //    [Key]
    //    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    //    public Guid Id { get; set; }
    //    public Guid HospitalDetailsId { get; set; }
    //    [ForeignKey("HospitalDetailsId")]
    //    public HospitalDetails HospitalDetails { get; set; }
    //    public ContactRoleEnum Role { get; set; }
    //    public string Name { get; set; }
    //    public string Phone { get; set; }
    //}

}
