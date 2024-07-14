namespace Softmad.Services.Models
{
    public class ReportResponse
    {
        public List<LeadsWithVists> Response { get; set; }        
    }

    public class LeadsWithVists
    {
        public Lead Lead { get; set; }
        public Visit Visit { get; set; }
    }
}
