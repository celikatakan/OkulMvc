using System;
namespace Okul.Business.DataProctection
{
	public interface IDataProtection
	{
        string Protect(string text);
        string UnProtect(string protectedText);
    }
}

