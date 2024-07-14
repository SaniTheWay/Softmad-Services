using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Softmad.Services.Models
{
    public class ReportResponse
    {
        public List<LeadsWithVists> Response { get; set; }
        public ReportResponse(List<LeadsWithVists> leadswithvisits)
        {
            Console.WriteLine("Setting data leadswithvisits to response");
            Response = leadswithvisits;
        }
    }

    public class LeadsWithVists
    {
        public Lead Lead { get; set; }
        public Visit Visit { get; set; }
    }
}
