using System;
using System.Text.RegularExpressions;

namespace AniX_BusinessLogic
{
    public class UserValidationService
    {
        // Configuration options for rules
        private readonly int minUsernameLength;
        private readonly int minPasswordLength;
        private readonly string passwordRegexPattern;

        public UserValidationService(int minUsernameLength = 4, int minPasswordLength = 4, string passwordRegexPattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).+$")
        {
            this.minUsernameLength = minUsernameLength;
            this.minPasswordLength = minPasswordLength;
            this.passwordRegexPattern = passwordRegexPattern;
        }

        public bool ValidateCredentials(string username, string password, out string validationMessage)
        {
            validationMessage = string.Empty;

            if (!ValidateUsername(username, out validationMessage) || !ValidatePassword(password, out validationMessage))
            {
                return false;
            }

            //if (!ValidatePasswordComplexity(password))
            //{
            //    validationMessage = "Password must contain at least one upper case, one lower case, and one number.";
            //    return false;
            //}

            return true;
        }

        private bool ValidateUsername(string username, out string validationMessage)
        {
            validationMessage = string.Empty;

            if (string.IsNullOrEmpty(username))
            {
                validationMessage = "Username cannot be empty.";
                return false;
            }

            if (username.Length < minUsernameLength)
            {
                validationMessage = $"Username must be at least {minUsernameLength} characters.";
                return false;
            }

            return true;
        }

        private bool ValidatePassword(string password, out string validationMessage)
        {
            validationMessage = string.Empty;

            if (string.IsNullOrEmpty(password))
            {
                validationMessage = "Password cannot be empty.";
                return false;
            }

            if (password.Length < minPasswordLength)
            {
                validationMessage = $"Password must be at least {minPasswordLength} characters.";
                return false;
            }

            return true;
        }

        public bool ValidatePasswordComplexity(string password)
        {
            return Regex.IsMatch(password, passwordRegexPattern);
        }
    }
}
