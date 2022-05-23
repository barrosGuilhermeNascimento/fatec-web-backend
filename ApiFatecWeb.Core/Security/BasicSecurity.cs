using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using BCrypt.Net;

namespace ApiFatecWeb.Core.Security
{
    public class BasicSecurity
    {
        private static string SECRET = "f1341dsds#%^$*&^fgd1431gdfs";

        public static string HashPassword(string str)
        {
            str = (str + SECRET);
            using var sha = new SHA256Managed();
            var bytes = sha.ComputeHash(Encoding.Default.GetBytes(str));
            var sb = new StringBuilder();
            foreach (var b in bytes)
                sb.Append(b.ToString("x2"));
            return sb.ToString();
        }
    }
}
