using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using AniX_BusinessLogic.Controllers;
using AniX_Shared.DomainModels;
using AniX_Shared.Interfaces;
using AniX_BusinessLogic;
using AniX_Utility;
using System;
using Anix_Shared.DomainModels;

namespace AniX_UnitTests
{
    [TestClass]
    public class UserControllerTests
    {
        private UserController _userController;
        private Mock<IAuthenticationService> _authenticationServiceMock;
        private UserValidationService _userValidationService;
        private AuditService _auditService;

        [TestInitialize]
        public void Setup()
        {
            _authenticationServiceMock = new Mock<IAuthenticationService>();
            _userValidationService = new UserValidationService();
            _auditService = new AuditService();
            _userController = new UserController(_authenticationServiceMock.Object, _userValidationService, _auditService);
        }

        [TestMethod]
        //
        // NORMALLY PASSES, BUT I DISABLED SOME VALIDATION IN THE CLASS FOR THE SAKE OF DEVELOPING EASIER FOR NOW
        // NORMALLY PASSES, BUT I DISABLED SOME VALIDATION IN THE CLASS FOR THE SAKE OF DEVELOPING EASIER FOR NOW
        // NORMALLY PASSES, BUT I DISABLED SOME VALIDATION IN THE CLASS FOR THE SAKE OF DEVELOPING EASIER FOR NOW
        //
        public void Login_ValidCredentials_ReturnsUser()
        {
            // Arrange
            var mockUser = new User { Username = "admin", IsAdmin = true };
            _authenticationServiceMock.Setup(x => x.AuthenticateUser("admin", "admin")).Returns(mockUser);

            // Act
            var result = _userController.Login("admin", "admin");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("admin", result.Username);
        }

        [TestMethod]
        public void Login_AuthenticationServiceThrowsException_HandlesException()
        {
            // Arrange
            _authenticationServiceMock.Setup(x => x.AuthenticateUser(It.IsAny<string>(), It.IsAny<string>())).Throws(new Exception());

            // Act & Assert
            Exception exception = null;
            try
            {
                _userController.Login("admin", "admin");
            }
            catch (Exception e)
            {
                exception = e;
                ExceptionHandlingService.HandleException(e);
            }

            Assert.IsNotNull(exception);
        }
    }
}
