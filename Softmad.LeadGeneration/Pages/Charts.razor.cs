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
        private ILogger<IndexModel> _logger { get; set; }
        [Inject]
        private DaprClient _daprClient { get; set; }

        public IEnumerable<Lead>? LeadList { get; set; }
        public IEnumerable<Visit>? LatestVisits { get; set; }
        public List<SeriesData> chartData { get; set; }
        public List<int> PieChartData { get; set; }
        public ChartData? WeeklyDataByStatus { get; set; }
        public ChartData? MonthlyDataByStatus { get; set; }
        public List<Visit>? WeeklyData { get; set; }
        public List<Visit>? MonthData { get; set; }
        private ChartTypeEnum SelectedChart { get; set; } = ChartTypeEnum.WeeklyChart;
        protected override async Task OnInitializedAsync()
        {
            await GetData();
            WeeklyData = LatestVisits.Where(c => c.VisitDate.AddDays(7).Date >= DateTime.Now.Date).ToList();
            WeeklyDataByStatus = new()
            {
                Active = visitWithStatus(LeadStatus.Active, WeeklyData.ToList()).ToList(),
                Passive = visitWithStatus(LeadStatus.Passive, WeeklyData.ToList()).ToList(),
                Lost = visitWithStatus(LeadStatus.Lost, WeeklyData.ToList()).ToList(),
                Completed = visitWithStatus(LeadStatus.Completed, WeeklyData.ToList()).ToList()
            };
            MonthData = LatestVisits.Where(c => c.VisitDate.Month == DateTime.Now.Month).ToList();
            MonthlyDataByStatus = new()
            {
                Active = visitWithStatus(LeadStatus.Active, WeeklyData.ToList()).ToList(),
                Passive = visitWithStatus(LeadStatus.Passive, WeeklyData.ToList()).ToList(),
                Lost = visitWithStatus(LeadStatus.Lost, WeeklyData.ToList()).ToList(),
                Completed = visitWithStatus(LeadStatus.Completed, WeeklyData.ToList()).ToList()
            };
            //var active = visitWithStatus(LeadStatus.Active, LatestVisits.ToList());
            //var passive = visitWithStatus(LeadStatus.Passive, LatestVisits.ToList());
            //var lost = visitWithStatus(LeadStatus.Lost, LatestVisits.ToList());
            //var completed = visitWithStatus(LeadStatus.Completed, LatestVisits.ToList());
            //PieChartData = [active.Count(), passive.Count(), lost.Count(), completed.Count()];
        }
        private async Task GetData()
        {
            //var result = await _daprClient.InvokeMethodAsync<IEnumerable<Lead>>(HttpMethod.Get, AppId, MethodURL);
            LatestVisits = await _daprClient.InvokeMethodAsync<IEnumerable<Visit>>(HttpMethod.Get, AppId, MethodURL + $"/visit/latest");
        }

        private IEnumerable<Visit> visitWithStatus(LeadStatus status, List<Visit> Visits)
        {
            switch (status)
            {
                case LeadStatus.Active:
                    return Visits?.Where(v => v.Status == LeadStatus.Active)!;
                case LeadStatus.Passive:
                    return Visits?.Where(v => v.Status == LeadStatus.Passive)!;
                case LeadStatus.Lost:
                    return Visits?.Where(v => v.Status == LeadStatus.Lost)!;
                case LeadStatus.Completed:
                    return Visits?.Where(v => v.Status == LeadStatus.Completed)!;
                default:
                    throw new Exception($"Does not impliment status type - \"{status}\"");
            }
        }

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

        private Object SelectReport(ChartTypeEnum selectedChart)
        {
            this.SelectedChart = selectedChart;
            StateHasChanged();
            return "ABC";
        }
    }

    public class SeriesData
    {
        public string name { get; set; }
        public List<int> data { get; set; }

    }

}
