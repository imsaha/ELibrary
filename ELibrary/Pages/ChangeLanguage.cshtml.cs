using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using ELibrary.Application.Operations.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ELibrary.Pages
{
    public class ChangeLanguageModel : PageModel
    {
        private readonly IMediator _mediator;

        public ChangeLanguageModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        [BindProperty(SupportsGet = true, Name = "returnUrl")]
        public string ReturnUrl { get; set; }

        [BindProperty(SupportsGet = true, Name = "lang")]
        public string LanguageCode { get; set; }


        public async Task<IActionResult> OnGet()
        {

            var supportedLangResponse = await _mediator.Send(new GetSupportedLanguageByLanguageCodeQuery(LanguageCode));

            if (!supportedLangResponse.IsSuccess || supportedLangResponse.Data == null)
                throw new ArgumentException("Invalid language code.");

            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(supportedLangResponse.Data.Code)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            var returnUrl = ReturnUrl ?? "/Index";
            return LocalRedirect(HttpUtility.UrlDecode(returnUrl));
        }
    }
}
