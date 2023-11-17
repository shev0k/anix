using Xunit;
using AniX_BusinessLogic;
using System.Text.RegularExpressions;
using Assert = Xunit.Assert;

namespace AniX_UnitTest
{
    public class UserValidationServiceTests
    {
        private readonly UserValidationService _validationService;

        public UserValidationServiceTests()
        {
            _validationService = new UserValidationService(4, 4, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).+$");
        }

        [Fact]
        public void ValidateCredentials_ValidInputs_ReturnsTrue()
        {
            string validUsername = "TestUser";
            string validPassword = "Password123";

            bool result = _validationService.ValidateCredentials(validUsername, validPassword, out string validationMessage);

            Assert.True(result);
            Assert.Empty(validationMessage);
        }

        [Fact]
        public void ValidateCredentials_InvalidUsername_ReturnsFalse()
        {
            string invalidUsername = "Abc";
            string validPassword = "Password123";

            bool result = _validationService.ValidateCredentials(invalidUsername, validPassword, out string validationMessage);

            Assert.False(result);
            Assert.Equal("Username must be at least 4 characters.", validationMessage);
        }

        [Fact]
        public void ValidatePasswordComplexity_ValidPassword_ReturnsTrue()
        {
            string password = "Complex123";

            bool result = _validationService.ValidatePasswordComplexity(password);

            Assert.True(result);
        }

        [Fact]
        public void ValidatePasswordComplexity_InvalidPassword_ReturnsFalse()
        {
            string password = "simple";

            bool result = _validationService.ValidatePasswordComplexity(password);

            Assert.False(result);
        }

    }
}