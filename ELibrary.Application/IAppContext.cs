using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELibrary.Application
{
    public interface IAppContext
    {
        public long OperationCountryId { get; }
    }

    public interface IDataProtectionService
    {
        string Protect(string plainText);
        string UnProtect(string protectedText);
    }
}
