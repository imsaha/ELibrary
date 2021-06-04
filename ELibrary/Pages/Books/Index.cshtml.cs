using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ELibrary.Application.BookManagement.Dtos;
using ELibrary.Application.BookManagement.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ELibrary.Pages.Books
{
    public class IndexModel : PageModel
    {
        private readonly IMediator _mediator;

        public IndexModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        [BindProperty(SupportsGet = true)]
        public string Search { get; set; }

        public List<BookDto> Books { get; set; }

        public async Task OnGet()
        {
            var result = await _mediator.Send(new GetBooksQuery()
            {
                Search = this.Search,
            });

            Books = result.Data?.ToList();
        }
    }
}
