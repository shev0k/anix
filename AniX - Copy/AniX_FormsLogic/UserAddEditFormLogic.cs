using System.Threading.Tasks;
using Anix_Shared.DomainModels;
using AniX_Utility;

namespace AniX_FormsLogic
{
    public class UserAddEditFormLogic
    {
        private readonly ApplicationModel _appModel;

        public UserAddEditFormLogic(ApplicationModel appModel)
        {
            _appModel = appModel;
        }

        public async Task<bool> AddNewUserAsync(User user, string password)
        {
            string salt = HashPassword.GenerateSalt();
            string hashedPassword = HashPassword.GenerateHashedPassword(password, salt);
            user.UpdatePassword(hashedPassword, salt);
            user.RegistrationDate = DateTime.Now;

            return await _appModel.UserDal.CreateAsync(user);
        }

        public async Task<bool> UpdateExistingUserAsync(User user, string newPassword)
        {
            user.Id = _appModel.UserToEdit.Id;
            if (!string.IsNullOrEmpty(newPassword))
            {
                string salt = HashPassword.GenerateSalt();
                string hashedPassword = HashPassword.GenerateHashedPassword(newPassword, salt);
                user.UpdatePassword(hashedPassword, salt);
            }
            else
            {
                (string existingPassword, string existingSalt) = _appModel.UserToEdit.RetrieveCredentials();
                user.UpdatePassword(existingPassword, existingSalt);
            }

            return await _appModel.UserDal.UpdateAPPAsync(user);
        }

        public async Task<(bool IsValid, string Message)> ValidateFormAsync(string username, string email, string password, bool isEditMode)
        {
            if (string.IsNullOrEmpty(username))
            {
                return (false, "Username cannot be empty");
            }

            if (await _appModel.UserController.DoesUsernameExistAsync(username) && !isEditMode)
            {
                return (false, "Username already exists");
            }

            if (string.IsNullOrEmpty(email))
            {
                return (false, "Email cannot be empty");
            }

            if (!email.Contains("@"))
            {
                return (false, "Please use a valid email!");
            }

            if (await _appModel.UserController.DoesEmailExistAsync(email) && !isEditMode)
            {
                return (false, "Email already exists");
            }

            if (!isEditMode && string.IsNullOrEmpty(password))
            {
                return (false, "Password cannot be empty for new users");
            }
            return (true, "Validation succeeded");
        }
    }
}
