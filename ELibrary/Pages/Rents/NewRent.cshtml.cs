using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ELibrary.Application.BookManagement.Queries;
using ELibrary.Application.RentManagement.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ELibrary.Pages.Rents
{
    public class NewRentModel : PageModel
    {
        private readonly IMediator _mediator;

        public NewRentModel(IMediator mediator)
        {
            _mediator = mediator;
        }


        [BindProperty]
        public InitiateNewRentCommand Command { get; set; }


        public void OnGet()
        {
        }


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var result = await _mediator.Send(Command);
            TempData["RentSaved"] = result.IsSuccess;
            return RedirectToPage("/Rents/Index");
        }


        public async Task<SelectList> GetBooksMiniAsync()
        {
            var response = await _mediator.Send(new GetAvailableBooksAsMiniQuery());
            return new SelectList(response.Data, "Id", "Text");
        }

    }
}
