using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ELibrary.Pages
{
    public class ChangeOperationCountryModel : PageModel
    {
        [BindProperty(SupportsGet = true, Name = "returnUrl")]
        public string ReturnUrl { get; set; }

        [BindProperty(SupportsGet = true, Name = "opc_id")]
        public long OperationCountryId { get; set; }

        public IActionResult OnGet()
        {
            Response.Cookies.Append("opc_id", OperationCountryId.ToString(),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) });

            var returnUrl = ReturnUrl ?? "/Index";
            return LocalRedirect(HttpUtility.UrlDecode(returnUrl));
        }
    }
}
