using System;
using System.Security.Cryptography;
using System.Text;

namespace AniX_ClassLib.Utility
{
    public static class HashPassword
    {
        public static string GenerateSalt()
        {
            byte[] saltBytes = new byte[32];
            using (var provider = new RNGCryptoServiceProvider())
            {
                provider.GetBytes(saltBytes);
            }
            return Convert.ToBase64String(saltBytes);
        }

        public static string GenerateHashedPassword(string password, string salt)
        {
            using (var sha256 = SHA256.Create())
            {
                var saltedPassword = password + salt;
                byte[] saltedPasswordBytes = Encoding.UTF8.GetBytes(saltedPassword);
                byte[] hashBytes = sha256.ComputeHash(saltedPasswordBytes);

                return Convert.ToBase64String(hashBytes);
            }
        }
    }
}
