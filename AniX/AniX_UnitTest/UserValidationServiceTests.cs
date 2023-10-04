using Microsoft.VisualStudio.TestTools.UnitTesting;
using AniX_BusinessLogic;

namespace AniX_UnitTests
{
    [TestClass]
    public class UserValidationServiceTests
    {
        private UserValidationService _userValidationService;

        [TestInitialize]
        public void SetUp()
        {
            _userValidationService = new UserValidationService();
        }

        [TestMethod]
        public void ValidateCredentials_ValidInput_ReturnsTrue()
        {
            string username = "ValidUser";
            string password = "ValidP@ss123";
            string validationMessage;

            bool result = _userValidationService.ValidateCredentials(username, password, out validationMessage);

            Assert.IsTrue(result);
            Assert.AreEqual(string.Empty, validationMessage);
        }

        [TestMethod]
        public void ValidateCredentials_EmptyInput_ReturnsFalse()
        {
            string username = "";
            string password = "";
            string validationMessage;

            bool result = _userValidationService.ValidateCredentials(username, password, out validationMessage);

            Assert.IsFalse(result);
            Assert.AreEqual("Username and password cannot be empty.", validationMessage);
        }

        [TestMethod]
        public void ValidateCredentials_UsernameTooShort_ReturnsFalse()
        {
            string username = "Us";
            string password = "ValidP@ss123";
            string validationMessage;

            bool result = _userValidationService.ValidateCredentials(username, password, out validationMessage);

            Assert.IsFalse(result);
            Assert.AreEqual("Username: at least 4 characters | Password: at least 4 characters", validationMessage);
        }

        [TestMethod]
        public void ValidateCredentials_PasswordTooShort_ReturnsFalse()
        {
            string username = "ValidUser";
            string password = "Sh0";
            string validationMessage;

            bool result = _userValidationService.ValidateCredentials(username, password, out validationMessage);

            Assert.IsFalse(result);
            Assert.AreEqual("Username: at least 4 characters | Password: at least 4 characters", validationMessage);
        }

        [TestMethod]
        //
        // NORMALLY PASSES, BUT I DISABLED SOME VALIDATION IN THE CLASS FOR THE SAKE OF DEVELOPING EASIER FOR NOW
        // NORMALLY PASSES, BUT I DISABLED SOME VALIDATION IN THE CLASS FOR THE SAKE OF DEVELOPING EASIER FOR NOW
        // NORMALLY PASSES, BUT I DISABLED SOME VALIDATION IN THE CLASS FOR THE SAKE OF DEVELOPING EASIER FOR NOW
        //
        public void ValidateCredentials_PasswordMissingUpperCase_ReturnsFalse()
        {
            string username = "ValidUser";
            string password = "missinguppercase1";
            string validationMessage;

            bool result = _userValidationService.ValidateCredentials(username, password, out validationMessage);

            Assert.IsFalse(result);
            Assert.AreEqual("Password must contain at least one upper case, one lower case, and one number.", validationMessage);
        }

        [TestMethod]
        //
        // NORMALLY PASSES, BUT I DISABLED SOME VALIDATION IN THE CLASS FOR THE SAKE OF DEVELOPING EASIER FOR NOW
        // NORMALLY PASSES, BUT I DISABLED SOME VALIDATION IN THE CLASS FOR THE SAKE OF DEVELOPING EASIER FOR NOW
        // NORMALLY PASSES, BUT I DISABLED SOME VALIDATION IN THE CLASS FOR THE SAKE OF DEVELOPING EASIER FOR NOW
        //
        public void ValidateCredentials_PasswordMissingLowerCase_ReturnsFalse()
        {
            string username = "ValidUser";
            string password = "MISSINGLOWERCASE1";
            string validationMessage;

            bool result = _userValidationService.ValidateCredentials(username, password, out validationMessage);

            Assert.IsFalse(result);
            Assert.AreEqual("Password must contain at least one upper case, one lower case, and one number.", validationMessage);
        }

        [TestMethod]
        //
        // NORMALLY PASSES, BUT I DISABLED SOME VALIDATION IN THE CLASS FOR THE SAKE OF DEVELOPING EASIER FOR NOW
        // NORMALLY PASSES, BUT I DISABLED SOME VALIDATION IN THE CLASS FOR THE SAKE OF DEVELOPING EASIER FOR NOW
        // NORMALLY PASSES, BUT I DISABLED SOME VALIDATION IN THE CLASS FOR THE SAKE OF DEVELOPING EASIER FOR NOW
        //
        public void ValidateCredentials_PasswordMissingNumber_ReturnsFalse()
        {
            string username = "ValidUser";
            string password = "MissingNumber";
            string validationMessage;

            bool result = _userValidationService.ValidateCredentials(username, password, out validationMessage);

            Assert.IsFalse(result);
            Assert.AreEqual("Password must contain at least one upper case, one lower case, and one number.", validationMessage);
        }
    }
}
