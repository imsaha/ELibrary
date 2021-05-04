using ELibrary.Application;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELibrary
{
    public class AppContext : IAppContext
    {
        private readonly HttpContext _httpContext;

        public AppContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContext = httpContextAccessor?.HttpContext;
        }

        public long OperationCountryId => long.TryParse(_httpContext.Request.Cookies["opc_id"], out long result) ? result : throw new Exception("Operation country is invalid");
    }
}
