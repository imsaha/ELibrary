using ELibrary.Application;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELibrary.API
{
    public class AppContext : IAppContext
    {
        private readonly HttpContext _httpContext;

        public AppContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContext = httpContextAccessor?.HttpContext;
        }

        public long OperationCountryId
        {
            get
            {
                if (_httpContext.Request.Cookies.Count == 0)
                    return default;

                var operationIdCookie = _httpContext.Request.Cookies["opc_id"];
                if (string.IsNullOrEmpty(operationIdCookie))
                    return default;

                return long.TryParse(operationIdCookie, out long result) ? result : throw new Exception("Operation country is invalid");
            }
        }
    }
}
