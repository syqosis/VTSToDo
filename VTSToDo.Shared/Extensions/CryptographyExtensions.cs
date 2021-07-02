using System;
using System.Security.Cryptography;
using System.Text;

namespace VTSToDo.Shared.Extensions
{
    public static class CryptographyExtensions
    {
        public static string ComputeHash(string value)
        {
            var md5 = new MD5CryptoServiceProvider();
            var buffer = Encoding.Default.GetBytes(value);
            var hash = md5.ComputeHash(buffer);
            return Convert.ToBase64String(hash);
        }
    }
}
