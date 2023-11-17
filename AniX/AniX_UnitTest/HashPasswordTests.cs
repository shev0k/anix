using Xunit;
using AniX_Utility;
using System;
using Assert = Xunit.Assert;

namespace AniX_UnitTest
{
    public class HashPasswordTests
    {
        [Fact]
        public void GenerateHashedPassword_CreatesConsistentHash()
        {
            string password = "TestPassword";
            string salt = HashPassword.GenerateSalt();

            var hash1 = HashPassword.GenerateHashedPassword(password, salt);
            var hash2 = HashPassword.GenerateHashedPassword(password, salt);

            Assert.Equal(hash1, hash2);
        }

        [Fact]
        public void GenerateHashedPassword_DifferentSaltsProduceDifferentHashes()
        {
            string password = "TestPassword";
            string salt1 = HashPassword.GenerateSalt();
            string salt2 = HashPassword.GenerateSalt();

            var hash1 = HashPassword.GenerateHashedPassword(password, salt1);
            var hash2 = HashPassword.GenerateHashedPassword(password, salt2);

            Assert.NotEqual(hash1, hash2);
        }

    }
}