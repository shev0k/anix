using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using AniX_BusinessLogic;
using AniX_Shared.DomainModels;
using AniX_Shared.Interfaces;
using AniX_Utility;
using Anix_Shared.DomainModels;

namespace AniX_UnitTests { 
    [TestClass]
    public class AuthenticationServiceTests
    {
        private AuthenticationService _authenticationService;
        private Mock<IUserManagement> _userDalMock;

        [TestInitialize]
        public void Setup()
        {
            _userDalMock = new Mock<IUserManagement>();
            _authenticationService = new AuthenticationService(_userDalMock.Object);
        }

        [TestMethod]
        public void AuthenticateUser_ValidCredentials_ReturnsUser()
        {
            // Arrange
            var mockUserManagement = new Mock<IUserManagement>();
            var mockUser = new User { Username = "admin", IsAdmin = true };
            mockUser.UpdatePassword("X0/NlIglGavadmD+ccWAg9fMe1XamXSlg1XVugsBSOw=", "67wrdXCIXl+aRIaO80gjX6ZMPje9Mv3B3DB++adAs2s=");

            mockUserManagement.Setup(um => um.AuthenticateUser("admin", "admin")).Returns(mockUser);

            var authenticationService = new AuthenticationService(mockUserManagement.Object);

            // Act
            var result = authenticationService.AuthenticateUser("admin", "admin");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("admin", result.Username);
        }


        [TestMethod]
        public void AuthenticateUser_InvalidUsername_ReturnsNull()
        {
            // Arrange
            _userDalMock.Setup(x => x.AuthenticateUser("InvalidUser", "Password123")).Returns((User)null);

            // Act
            var result = _authenticationService.AuthenticateUser("InvalidUser", "Password123");

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void AuthenticateUser_InvalidPassword_ReturnsNull()
        {
            // Arrange
            _userDalMock.Setup(x => x.AuthenticateUser("John", "WrongPassword")).Returns((User)null);

            // Act
            var result = _authenticationService.AuthenticateUser("John", "WrongPassword");

            // Assert
            Assert.IsNull(result);
        }
    }
}