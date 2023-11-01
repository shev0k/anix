using System;
using AniX_Utility;

namespace GenerateUserCredentials
{
    class Program
    {
        static void Main(string[] args)
        {
            string rawPassword = "user";
            string salt = HashPassword.GenerateSalt();
            string hashedPassword = HashPassword.GenerateHashedPassword(rawPassword, salt);

            Console.WriteLine($"Generated Salt: {salt}");
            Console.WriteLine($"Generated Hashed Password: {hashedPassword}");
        }
    }
}