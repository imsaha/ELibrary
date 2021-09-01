using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELibrary.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class V1Controller : ControllerBase
    {
        internal protected readonly IMediator _mediator;

        public V1Controller(IMediator mediator)
        {
            _mediator = mediator;
        }
    }
}
