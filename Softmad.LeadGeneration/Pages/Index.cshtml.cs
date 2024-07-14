using Dapr.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient.DataClassification;
using OfficeOpenXml;
using Softmad.LeadGeneration.Models;
using Softmad.LeadGeneration.Models.DTOs;
using Softmad.Services.Models;

namespace Softmad.LeadGeneration.Pages
{
    [Authorize(Policy = "admin")]
    public class IndexModel : PageModel
    {
        private const string AppId = "Softmad-Services-LeadGeneration";
        private const string MethodURL = "api/LeadGeneration";

        private readonly ILogger<IndexModel> _logger;
        private readonly DaprClient _daprClient;
        [BindProperty]
        internal ChartTypeEnum FileSelection { get; set; }
        public IEnumerable<Lead>? LeadList { get; set; }

        public IndexModel(ILogger<IndexModel> logger, DaprClient daprClient)
        {
            _logger = logger;
            _daprClient = daprClient;
        }

        public async Task<IActionResult> OnPostDowloadReport()
        {
            var request = new HttpRequestMessage();
            var stream = new MemoryStream();

            string filename = "Error-File";
            if (FileSelection == ChartTypeEnum.WeeklyChart)
            {
                //data = LatestVisits.Where(c => c.VisitDate.AddDays(7).Date >= DateTime.Now.Date).ToList();
                request = _daprClient.CreateInvokeMethodRequest(HttpMethod.Get, AppId, MethodURL + $"/report?type=weekly");
                filename = $"WeekReport {DateTime.Now.Date.AddDays(-7).ToString("dd-MM-yy")} to {DateTime.Now.Date.ToString("dd-MM-yy")}.xlsx";
            }
            else if (FileSelection == ChartTypeEnum.MonthlyChart)
            {
                request = _daprClient.CreateInvokeMethodRequest(HttpMethod.Get, AppId, MethodURL + $"/report?type=monthly");
                filename = $"MonthReport - {DateTime.Now.ToString("MMMM")}.xlsx";
            }
            else
            {
                _logger.LogDebug("Invalid chart type selection.");
                return BadRequest("Invalid chart type selection.");
            }
            try
            {
                var data = await _daprClient.InvokeMethodAsync<ReportResponse>(request);
                using (var package = new ExcelPackage(stream))
                {
                    var worksheet = package.Workbook.Worksheets.Add(filename);

                    // Setting Column Names:
                    setColumns(worksheet);

                    var responseList = data.Response;
                    // Setting Values
                    for (var i = 2; i < responseList.Count; i++)
                    {
                        var row = responseList[i - 2];
                        setRows(worksheet, row, i);
                    }
                    package.Save();
                }
                stream.Position = 0;
                var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                return File(stream, contentType, filename);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "------- Error: one or more error occured while fetching report");
                throw;
            }
        }

        private void setColumns(ExcelWorksheet worksheet)

        {
            worksheet.Cells[1, 1].Value = "Lead Id";
            worksheet.Cells[1, 2].Value = "Employee Id";
            worksheet.Cells[1, 3].Value = "Customer Type";
            worksheet.Cells[1, 4].Value = "Customer Name";
            worksheet.Cells[1, 5].Value = "Customer Email";
            worksheet.Cells[1, 6].Value = "Address";
            worksheet.Cells[1, 7].Value = "City";
            worksheet.Cells[1, 8].Value = "State";
            worksheet.Cells[1, 9].Value = "Pincode";
            worksheet.Cells[1, 10].Value = "Country";
            worksheet.Cells[1, 11].Value = "Owner Name";
            worksheet.Cells[1, 12].Value = "Owner Phone";
            worksheet.Cells[1, 13].Value = "Purchase Head Name";
            worksheet.Cells[1, 14].Value = "Purchase Head Phone";
            worksheet.Cells[1, 15].Value = "Bio Medical Name";
            worksheet.Cells[1, 16].Value = "Bio Medical Phone";
            worksheet.Cells[1, 17].Value = "Doctor Name";
            worksheet.Cells[1, 18].Value = "Doctor Specialization";
            worksheet.Cells[1, 19].Value = "Doctor Phone";
            worksheet.Cells[1, 20].Value = "Doctor Email";
            worksheet.Cells[1, 21].Value = "Visit Id";
            worksheet.Cells[1, 22].Value = "Visit Date";
            worksheet.Cells[1, 23].Value = "Lead Type";
            worksheet.Cells[1, 24].Value = "Status";
            worksheet.Cells[1, 25].Value = "Budget";
            worksheet.Cells[1, 26].Value = "Requirements";
            worksheet.Cells[1, 27].Value = "Closure Plan";
            worksheet.Cells[1, 28].Value = "Existing Machines";
        }

        private void setRows(ExcelWorksheet worksheet, LeadsWithVists row, int i)
        {
            worksheet.Cells[i, 1].Value = row.Lead.Id;
            worksheet.Cells[i, 2].Value = row.Lead.EmployeeId;
            worksheet.Cells[i, 3].Value = row.Lead.CustomerDetails.CustomerType;
            worksheet.Cells[i, 4].Value = row.Lead.CustomerDetails.HospitalDetails.Name;
            worksheet.Cells[i, 5].Value = row.Lead.CustomerDetails.HospitalDetails.Email;
            worksheet.Cells[i, 6].Value = row.Lead.CustomerDetails.HospitalDetails.AddressLine1 + "\n" + row.Lead.CustomerDetails.HospitalDetails.AddressLine2;
            worksheet.Cells[i, 7].Value = row.Lead.CustomerDetails.HospitalDetails.City;
            worksheet.Cells[i, 8].Value = row.Lead.CustomerDetails.HospitalDetails.State;
            worksheet.Cells[i, 9].Value = row.Lead.CustomerDetails.HospitalDetails.PinCode;
            worksheet.Cells[i, 10].Value = row.Lead.CustomerDetails.HospitalDetails.Country;
            worksheet.Cells[i, 11].Value = row.Lead.CustomerDetails.HospitalDetails.OwnerName;
            worksheet.Cells[i, 12].Value = row.Lead.CustomerDetails.HospitalDetails.OwnerPhone;
            worksheet.Cells[i, 13].Value = row.Lead.CustomerDetails.HospitalDetails.PurchaseHeadName;
            worksheet.Cells[i, 14].Value = row.Lead.CustomerDetails.HospitalDetails.PurchaseHeadPhone;
            worksheet.Cells[i, 15].Value = row.Lead.CustomerDetails.HospitalDetails.BioMedicalName;
            worksheet.Cells[i, 16].Value = row.Lead.CustomerDetails.HospitalDetails.BioMedicalPhone;
            worksheet.Cells[i, 17].Value = row.Lead.CustomerDetails.DoctorDetails.Name;
            worksheet.Cells[i, 18].Value = row.Lead.CustomerDetails.DoctorDetails.Specialization;
            worksheet.Cells[i, 19].Value = row.Lead.CustomerDetails.DoctorDetails.Phone;
            worksheet.Cells[i, 20].Value = row.Lead.CustomerDetails.DoctorDetails.Email;
            worksheet.Cells[i, 21].Value = row.Visit.VisitId;
            worksheet.Cells[i, 22].Value = row.Visit.VisitDate;
            worksheet.Cells[i, 23].Value = row.Visit.Type;
            worksheet.Cells[i, 24].Value = row.Visit.Status;
            worksheet.Cells[i, 25].Value = row.Visit.Budget;
            worksheet.Cells[i, 26].Value = row.Visit.Requirements;
            worksheet.Cells[i, 27].Value = row.Visit.ClosurePlan;
            worksheet.Cells[i, 28].Value = row.Visit.ExistingMachines;
        }
    }
}
