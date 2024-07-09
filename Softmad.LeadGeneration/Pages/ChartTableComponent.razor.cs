using Microsoft.AspNetCore.Components;
using Softmad.Services.Models;

namespace Softmad.LeadGeneration.Pages
{
    public partial class ChartTableComponent:ComponentBase
    {
        [Parameter]
        public string TableCaption { get; set; }
        [Parameter]
        public List<Visit> TableData { get; set; }
        [Parameter]
        public string TableBodyId { get; set; }
        [Parameter]
        public string? Display { get; set; } = string.Empty;

        private string GetStatusIcon(LeadStatus leadStatus)
        {
            string bgColorClass = string.Empty;

            switch (leadStatus)
            {
                case LeadStatus.Active:
                    bgColorClass = "✅";
                    break;
                case LeadStatus.Passive:
                    bgColorClass = "⏸";
                    break;
                case LeadStatus.Lost:
                    bgColorClass = "❌";
                    break;
                case LeadStatus.Completed:
                    bgColorClass = "🏁";
                    break;
                default:
                    break;
            }

            return bgColorClass;
        }

    }
}
