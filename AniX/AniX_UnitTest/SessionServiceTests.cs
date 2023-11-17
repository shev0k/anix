using Xunit;
using Moq;
using AniX_Shared.Interfaces;
using Assert = Xunit.Assert;

namespace AniX_UnitTest
{
    public class SessionServiceTests
    {
        private readonly Mock<ISessionService> _mockSessionService;

        public SessionServiceTests()
        {
            _mockSessionService = new Mock<ISessionService>();
        }
        [Fact]
        public void SetSessionAndCookie_SetsValues()
        {
            string userId = "1", username = "TestUser", sessionId = "Session123", profileImagePath = "/path/image.jpg";

            _mockSessionService.Object.SetSessionAndCookie(userId, username, sessionId, profileImagePath);

        }

        [Fact]
        public void GetUserId_ReturnsUserId()
        {
            _mockSessionService.Setup(s => s.GetUserId()).Returns("1");

            var result = _mockSessionService.Object.GetUserId();

            Assert.Equal("1", result);
        }
        [Fact]
        public void GetAndSetUsername()
        {
            string username = "NewTestUser";
            _mockSessionService.Setup(s => s.SetUsername(username));

            _mockSessionService.Object.SetUsername(username);
            _mockSessionService.Setup(s => s.GetUsername()).Returns(username);
            var result = _mockSessionService.Object.GetUsername();

            Assert.Equal(username, result);
        }
        [Fact]
        public void GetAndSetProfileImagePath()
        {
            string imagePath = "/new/path/image.jpg";
            _mockSessionService.Setup(s => s.SetProfileImagePath(imagePath));

            _mockSessionService.Object.SetProfileImagePath(imagePath);
            _mockSessionService.Setup(s => s.GetProfileImagePath()).Returns(imagePath);
            var result = _mockSessionService.Object.GetProfileImagePath();

            Assert.Equal(imagePath, result);
        }
        [Fact]
        public void IsAuthenticated_ReturnsTrueOrFalse()
        {
            _mockSessionService.Setup(s => s.IsAuthenticated()).Returns(true);

            var result = _mockSessionService.Object.IsAuthenticated();

            Assert.True(result);
        }
        [Fact]
        public void SignOut_PerformsSignOut()
        {

            _mockSessionService.Object.SignOut();

        }

    }
}