using Dapr.Client;
using Microsoft.AspNetCore.Components;
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
        public List<int> WeeklyData { get; set; }
        protected override async Task OnInitializedAsync()
        {
            await GetData();
            var active = visitWithStatus(LeadStatus.Active, LatestVisits.ToList());
            var passive = visitWithStatus(LeadStatus.Passive, LatestVisits.ToList());
            var lost = visitWithStatus(LeadStatus.Lost, LatestVisits.ToList());
            var completed = visitWithStatus(LeadStatus.Completed, LatestVisits.ToList());
            PieChartData = [active.Count(), passive.Count(),  lost.Count(), completed.Count()];
            var week = LatestVisits.Where(c => c.VisitDate.AddDays(7).Date >= DateTime.Now.Date).ToList();
            WeeklyData = [visitWithStatus(LeadStatus.Active, week.ToList()).Count(),
                          visitWithStatus(LeadStatus.Passive, week.ToList()).Count(),
                          visitWithStatus(LeadStatus.Lost, week.ToList()).Count(),
                          visitWithStatus(LeadStatus.Completed, week.ToList()).Count()];
            chartData = new List<SeriesData>
            {
                new SeriesData
                {
                    name = "Active",
                    data = new List<int> { 44, 55, 41, 37, 22, 43, 21 }
                },
                new SeriesData
                {
                    name = "Passive",
                    data = new List<int> { 53, 32, 33, 52, 13, 43, 32 }
                },
                new SeriesData
                {
                    name = "Lost",
                    data = new List<int> { 12, 17, 11, 9, 15, 11, 20 }
                },
                new SeriesData
                {
                    name = "Completed",
                    data = new List<int> { 9, 7, 5, 8, 6, 9, 4 }
                }
            };

            //foreach (Lead lead in LeadList)
            //{

            //    chartData.Add(new ())
            //}
        }
        private async Task GetData()
        {
            //var result = await _daprClient.InvokeMethodAsync<IEnumerable<Lead>>(HttpMethod.Get, AppId, MethodURL);
            LatestVisits = await _daprClient.InvokeMethodAsync<IEnumerable<Visit>>(HttpMethod.Get, AppId, MethodURL + $"/visit/latest");
        }

        private IEnumerable<Visit> visitWithStatus(LeadStatus status,List<Visit> Visits)
        {
            switch (status)
            { 
                case LeadStatus.Active:
                    return Visits?.Where(v=>v.Status == LeadStatus.Active)!;
                case LeadStatus.Passive:
                    return Visits?.Where(v=>v.Status == LeadStatus.Passive)!;
                case LeadStatus.Lost:
                    return Visits?.Where(v=>v.Status == LeadStatus.Lost)!;
                case LeadStatus.Completed:
                    return Visits?.Where(v=>v.Status == LeadStatus.Completed)!;
                default:
                    throw new Exception($"Does not impliment status type - \"{status}\"");
            }
        }
    }

    public class SeriesData
    {
        public string name { get; set; }
        public List<int> data { get; set; }

    }

}
