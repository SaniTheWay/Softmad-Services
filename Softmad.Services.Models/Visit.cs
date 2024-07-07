using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Softmad.Services.Models
{
    public class Visit : AdditionalDetails
    {
        // Primary key
        [Key]
        public Guid VisitId { get; set; }

        // Foreign key referencing the Lead
        [ForeignKey("Lead")]
        public Guid LeadId { get; set; }

        // Date and time of the visit
        [Required]
        public DateTimeOffset VisitDate { get; set; } = DateTime.Now;

        [AllowNull]
        public int? Duration { get; set; }

        // Type of visit (e.g., follow-up, technical support, sales pitch, etc.)
        [Required]
        public VisitTypeEnum VisitType { get; set; } = VisitTypeEnum.FollowUp;

        // Foreign key referencing the SalesPerson(EmpId)
        [ForeignKey("SalesPerson")]
        public Guid SalesPersonId { get; set; }

        // Any additional feedback from the client
        [MaxLength(1000)]
        [AllowNull]
        public string? ClientFeedback { get; set; }

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
        public bool isLatestVisit { get; set; } = true;
    }
    public enum VisitTypeEnum
    {
        VisitZero,
        FollowUp,
        TechnicalSupport,
        SalesPitch,
        Training,
        Other
    }

    public enum VisitModeEnum
    {
        Online,
        Offline
    }
}
