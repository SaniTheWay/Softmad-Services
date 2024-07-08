using Softmad.Services.Models;

namespace Softmad.LeadGeneration.Models
{
    public class ChartData
    {
        public List<Visit> Active { get; set; }
        public List<Visit> Passive { get; set; }
        public List<Visit> Lost { get; set; }
        public List<Visit> Completed { get; set; }
    }
}
