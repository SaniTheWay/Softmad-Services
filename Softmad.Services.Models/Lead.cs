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
        public string Budget { get; set; }      //
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

    public class CustomerDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set;}
        [Required]
        public HospitalDetails HospitalDetails { get; set; }
        [Required]
        public DoctorDetails DoctorDetails { get; set; }
        public string CustomerType { get; set; }
    }

    public class HospitalDetails:Address
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Type {  get; set; }
        public string TypeName { get; set; }
        public string TypePhone { get; set; }
        public string Email { get; set; }
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


}
