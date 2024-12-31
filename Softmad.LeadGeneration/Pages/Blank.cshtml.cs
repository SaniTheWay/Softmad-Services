using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Softmad.LeadGeneration.Pages
{
    public class BlankModel : PageModel
    {
        public IActionResult OnGet()
        {
            return Redirect("/generate-lead");
        }
    }
}
