using Dapr.Client;
using Microsoft.AspNetCore.Components;
using Softmad.LeadGeneration.Models;
using Softmad.Services.Models;

namespace Softmad.LeadGeneration.Pages
{
    public partial class Charts : ComponentBase
    {
        private const string AppId = "Softmad-Services-LeadGeneration";
        private const string MethodURL = "api/LeadGeneration";

        [Inject]
        private DaprClient _daprClient { get; set; }

        public IEnumerable<Visit>? LatestVisits { get; set; }
        public ChartData? WeeklyDataByStatus { get; set; }
        public ChartData? MonthlyDataByStatus { get; set; }
        public List<Visit>? WeeklyData { get; set; }
        public List<Visit>? MonthData { get; set; }
        protected override async Task OnInitializedAsync()
        {
            await SetData();
        }
        private async Task SetData()
        {
            LatestVisits = await _daprClient.InvokeMethodAsync<IEnumerable<Visit>>(HttpMethod.Get, AppId, MethodURL + $"/visit/latest");
            
            // set past 7-days data
            WeeklyData = LatestVisits.Where(c => c.VisitDate.AddDays(7).Date >= DateTime.Now.Date).ToList();
            WeeklyDataByStatus = new()
            {
                Active = visitWithStatus(LeadStatus.Active, WeeklyData),
                Passive = visitWithStatus(LeadStatus.Passive, WeeklyData),
                Lost = visitWithStatus(LeadStatus.Lost, WeeklyData),
                Completed = visitWithStatus(LeadStatus.Completed, WeeklyData)
            };

            // set current month data
            MonthData = LatestVisits.Where(c => c.VisitDate.Month == DateTime.Now.Month).ToList();
            MonthlyDataByStatus = new()
            {
                Active = visitWithStatus(LeadStatus.Active, MonthData),
                Passive = visitWithStatus(LeadStatus.Passive, MonthData),
                Lost = visitWithStatus(LeadStatus.Lost, MonthData),
                Completed = visitWithStatus(LeadStatus.Completed, MonthData)
            };
        }

        private List<Visit> visitWithStatus(LeadStatus status, List<Visit> Visits)
        {
            switch (status)
            {
                case LeadStatus.Active:
                    return Visits?.Where(v => v.Status == LeadStatus.Active).ToList()!;
                case LeadStatus.Passive:
                    return Visits?.Where(v => v.Status == LeadStatus.Passive).ToList()!;
                case LeadStatus.Lost:
                    return Visits?.Where(v => v.Status == LeadStatus.Lost).ToList()!;
                case LeadStatus.Completed:
                    return Visits?.Where(v => v.Status == LeadStatus.Completed).ToList()!;
                default:
                    throw new Exception($"Does not impliment status type - \"{status}\"");
            }
        }
    }
}
