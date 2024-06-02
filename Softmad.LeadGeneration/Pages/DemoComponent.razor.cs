using Dapr.Client;
using Microsoft.AspNetCore.Components;
using Softmad.Services.Models;
using System.ComponentModel;

namespace Softmad.LeadGeneration.Pages
{
    public partial class DemoComponent : ComponentBase
    {
        private const string AppId = "Softmad-Services-LeadGeneration";
        private const string MethodURL = "api/LeadGeneration";

        [Inject]
        private ILogger<IndexModel> _logger { get; set; }
        [Inject]
        private DaprClient _daprClient { get; set; }

        public IEnumerable<Lead>? LeadList { get; set; }

        protected override async Task OnInitializedAsync()
        {
            LeadList = await CallAPI();
        }
        private async Task<IEnumerable<Lead>> CallAPI()
        {
            var result = await _daprClient.InvokeMethodAsync<IEnumerable<Lead>>(HttpMethod.Get, AppId, MethodURL);
            return result;
        }
    }
}

