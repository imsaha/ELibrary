using ELibrary.Application;
using ELibrary.Application.BookManagement.Command;
using ELibrary.Application.BookManagement.Dtos;
using ELibrary.Application.BookManagement.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELibrary.API.Controllers
{
    public class BooksController : V1Controller
    {
        public BooksController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        [Route("")]
        [Produces(typeof(ListResult<BookDto>))]
        public async Task<IActionResult> GetBooks([FromQuery] GetBooksQuery query)
        {
            return Ok(await _mediator.Send(query, Request.HttpContext.RequestAborted));
        }

        [HttpPost]
        [Route("")]
        [Produces(typeof(ListResult<BookDto>))]
        public async Task<IActionResult> CreateBook([FromBody] SaveBookCommand command)
        {
            return Ok(await _mediator.Send(command, Request.HttpContext.RequestAborted));
        }


        [HttpPut]
        [Route("{id}")]
        [Produces(typeof(ListResult<BookDto>))]
        public async Task<IActionResult> UpdateBook([FromRoute] long id, [FromBody] SaveBookCommand command)
        {
            command.Id = id;
            return Ok(await _mediator.Send(command, Request.HttpContext.RequestAborted));
        }
    }
}
