using Anix_Shared.DomainModels;
using System.Threading.Tasks;
using AniX_Shared.Interfaces;
using System.IO; 
using System.Text.RegularExpressions; 

namespace AniX_BusinessLogic
{
    public class UserValidationService
    {
        private readonly IUserManagement _userManagement;

        public UserValidationService(IUserManagement userManagement)
        {
            _userManagement = userManagement;
        }
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

        private bool ValidateImage(string imagePath, out string validationMessage)
        {
            validationMessage = string.Empty;
            string extension = Path.GetExtension(imagePath);
            string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif" };

            if (string.IsNullOrEmpty(extension))
            {
                validationMessage = "Image file must have an extension.";
                return false;
            }

            if (Array.IndexOf(allowedExtensions, extension) < 0)
            {
                validationMessage = "Only .jpg, .jpeg, .png, and .gif image types are supported.";
                return false;
            }

            if (!File.Exists(imagePath))
            {
                validationMessage = "File does not exist.";
                return false;
            }

            if (imagePath.Contains(".."))
            {
                validationMessage = "Invalid file path.";
                return false;
            }

            return true;
        }

        public bool ValidateImageExtension(string imagePath, out string validationMessage)
        {
            return ValidateImage(imagePath, out validationMessage);
        }


        public async Task<bool> UpdateProfileImagePathAsync(int userId, string imagePath)
        {
            if (!ValidateImage(imagePath, out string validationMessage))
            {
                return false;
            }

            var user = await _userManagement.GetUserFromIdAsync(userId);
            if (user == null)
            {
                return false;
            }

            return await _userManagement.UpdateProfileImagePathAsync(userId, imagePath);
        }
    }
}
