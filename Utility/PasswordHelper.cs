using System.Security.Cryptography;
using System.Text;

namespace OnlineShop.Utility
{
    public class PasswordHelper
    {
        public static string EncodePasswordMd5(string pass)
        {
            if (string.IsNullOrEmpty(pass))
                return string.Empty;

            using (MD5 md5 = MD5.Create())
            {
                byte[] originalBytes = Encoding.UTF8.GetBytes(pass);
                byte[] encodedBytes = md5.ComputeHash(originalBytes);
                return BitConverter.ToString(encodedBytes).Replace("-", "").ToLower();
            }
        }

        public static string EncodeProSecurity(string pass)
        {
            var first = EncodePasswordMd5(pass);
            var second = EncodePasswordMd5(first);
            return EncodePasswordMd5(second);
        }
    }
}
