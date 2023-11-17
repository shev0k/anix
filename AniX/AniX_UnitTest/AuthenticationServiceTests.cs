using Xunit;
using Moq;
using AniX_Shared.Interfaces;
using AniX_Shared.DomainModels;
using System;
using System.Threading.Tasks;
using System.IO;
using Anix_Shared.DomainModels;
using AniX_Utility;
using Assert = Xunit.Assert;

namespace AniX_UnitTest
{
    public class AuthenticationServiceTests
    {
        private readonly Mock<IAuthenticationService> _mockAuthenticationService;

        public AuthenticationServiceTests()
        {
            _mockAuthenticationService = new Mock<IAuthenticationService>();
        }

        [Fact]
        public async Task AuthenticateUserAsync_ReturnsUser()
        {
            string username = "testuser";
            string password = "password";
            var user = new User { Username = username };

            _mockAuthenticationService.Setup(service => service.AuthenticateUserAsync(username, password))
                .ReturnsAsync(user);

            var result = await _mockAuthenticationService.Object.AuthenticateUserAsync(username, password);

            Assert.NotNull(result);
            Assert.Equal(username, result.Username);
        }
        [Fact]
        public async Task RegisterUserAsync_Success()
        {
            string username = "newuser";
            string email = "newuser@example.com";
            string password = "newpassword";
            var operationResult = new OperationResult { Success = true };

            _mockAuthenticationService.Setup(service => service.RegisterUserAsync(username, email, password, null, null))
                .ReturnsAsync(operationResult);

            var result = await _mockAuthenticationService.Object.RegisterUserAsync(username, email, password);

            Assert.True(result.Success);
        }
        [Fact]
        public async Task DoesUsernameExistAsync_ReturnsTrueOrFalse()
        {
            string username = "existinguser";
            _mockAuthenticationService.Setup(service => service.DoesUsernameExistAsync(username))
                .ReturnsAsync(true);

            var result = await _mockAuthenticationService.Object.DoesUsernameExistAsync(username);

            Assert.True(result);
        }

        [Fact]
        public async Task DoesEmailExistAsync_ReturnsTrueOrFalse()
        {
            string email = "existing@example.com";
            _mockAuthenticationService.Setup(service => service.DoesEmailExistAsync(email))
                .ReturnsAsync(true);

            var result = await _mockAuthenticationService.Object.DoesEmailExistAsync(email);

            Assert.True(result);
        }

        [Fact]
        public void SetWebUserSession_SetsSession()
        {
            var user = new User { Username = "testuser" };

            _mockAuthenticationService.Object.SetWebUserSession(user);
        }

        [Fact]
        public async Task ValidateUserAsync_ReturnsTrueOrFalse()
        {
            string email = "user@example.com";
            string password = "password";
            _mockAuthenticationService.Setup(service => service.ValidateUserAsync(email, password))
                .ReturnsAsync(true);

            var result = await _mockAuthenticationService.Object.ValidateUserAsync(email, password);

            Assert.True(result);
        }
    }
}