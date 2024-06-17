using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Softmad.Services.Models
{
    public class Visit
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

        // Duration of the visit (in minutes)
        [Required]
        public int Duration { get; set; }

        // Remarks or notes about the visit
        [MaxLength(1000)]
        public string Remarks { get; set; }

        // Type of visit (e.g., follow-up, technical support, sales pitch, etc.)
        [Required]
        public VisitTypeEnum VisitType { get; set; } = VisitTypeEnum.FollowUp;

        // Foreign key referencing the SalesPerson(EmpId)
        [ForeignKey("SalesPerson")]
        public Guid SalesPersonId { get; set; }

        // Any additional feedback from the client
        [MaxLength(1000)]
        public string ClientFeedback { get; set; }

        // Status of the visit (e.g., completed, cancelled, rescheduled)
        [Required]
        public VisitStatusEnum Status { get; set; } = VisitStatusEnum.Completed;

        [Required]
        public VisitModeEnum Mode { get; set; } = VisitModeEnum.Offline;

        // Potential next steps after the visit
        [MaxLength(1000)]
        public string NextSteps { get; set; }
    }

    public enum VisitTypeEnum
    {
        FollowUp,
        TechnicalSupport,
        SalesPitch,
        Training,
        Other
    }

    public enum VisitStatusEnum
    {
        Completed,
        Cancelled,
        Rescheduled,
        Pending
    }

    public enum VisitModeEnum
    {
        Online,
        Offline
    }
}
