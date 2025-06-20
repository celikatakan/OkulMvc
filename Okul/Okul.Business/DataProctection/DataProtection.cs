using System;
using Microsoft.AspNetCore.DataProtection;

namespace Okul.Business.DataProctection
{
    public class DataProtection : IDataProtection
    {
        private readonly IDataProtector _protector;
        // Constructor to create a data protector using the IDataProtectionProvider
        public DataProtection(IDataProtectionProvider provider)
        {
            // Create a protector with a specific purpose (string identifier)
            _protector = provider.CreateProtector("Okul-security-v1");
        }
        // Method to protect sensitive text by encrypting it
        public string Protect(string text)
        {
            return _protector.Protect(text);
        }
        // Method to unprotect previously protected text by decrypting it
        public string UnProtect(string protectedText)
        {
            return _protector.Unprotect(protectedText);
        }
    }
}
