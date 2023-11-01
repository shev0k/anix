using Anix_Shared.DomainModels;
using AniX_Utility;
using System;
using System.Text.RegularExpressions;

namespace AniX_BusinessLogic
{
    public class UserValidationService
    {
        public bool ValidateCredentials(string username, string password, out string validationMessage)
        {
            validationMessage = string.Empty;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                validationMessage = "Username and password cannot be empty.";
                return false;
            }

            if (username.Length < 4 || password.Length < 4)
            {
                validationMessage = "Username: at least 4 characters | Password: at least 4 characters";
                return false;
            }

            //if (!Regex.IsMatch(password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).+$"))
            //{
            //    validationMessage = "Password must contain at least one upper case, one lower case, and one number.";
            //    return false;
            //}
            
            // WILL ENABLE LATER

            return true;
        }
    }
}
