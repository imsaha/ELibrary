using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ELibrary.Application.RentManagement.Commands;
using ELibrary.Application.RentManagement.Dtos;
using ELibrary.Application.RentManagement.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ELibrary.Pages.Rents
{
    public class IndexModel : PageModel
    {
        private readonly IMediator _mediator;

        public IndexModel(IMediator mediator)
        {
            _mediator = mediator;
        }


        [BindProperty]
        public long DeleteRentId { get; set; }


        public IEnumerable<ActiveRentDto> ActiveRents { get; set; }

        public async Task OnGet()
        {
            var result = await _mediator.Send(new GetActiveRentsQuery());
            ActiveRents = result.Data;
        }


        public async Task<IActionResult> OnPostDelete()
        {
            var result = await _mediator.Send(new DeleteRentByIdCommand(DeleteRentId));
            TempData["Deleted"] = result.IsSuccess;
            return RedirectToPage();
        }
    }
}
