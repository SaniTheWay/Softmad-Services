using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Softmad.LeadGeneration.Pages
{
    [Authorize(Policy = "admin")]
    public class BlankModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
