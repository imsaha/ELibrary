using ELibrary.Application;
using Microsoft.AspNetCore.DataProtection;

namespace ELibrary.API
{
    public class DataProtectionService : IDataProtectionService
    {
        private readonly IDataProtector _protector;

        public DataProtectionService(IDataProtectionProvider dataProtectionProvider)
        {
            _protector = dataProtectionProvider.CreateProtector("authentication");
        }
        public string Protect(string plainText)
        {
            return _protector.Protect(plainText);
        }

        public string UnProtect(string protectedText)
        {
            return _protector.Unprotect(protectedText);
        }
    }
}
