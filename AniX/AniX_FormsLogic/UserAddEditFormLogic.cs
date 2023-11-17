using System.Threading.Tasks;
using AniX_FormsLogic;
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

        public async Task<OperationResult> AddNewUserAsync(User user, string password)
        {
            OperationResult result = new OperationResult();
            string salt = HashPassword.GenerateSalt();
            string hashedPassword = HashPassword.GenerateHashedPassword(password, salt);
            user.UpdatePassword(hashedPassword, salt);
            user.RegistrationDate = DateTime.Now;

            result = await _appModel.UserDal.CreateAsync(user);

            return result;
        }

        public async Task<OperationResult> UpdateExistingUserAsync(User user, string newPassword)
        {
            OperationResult operationResult = new OperationResult();
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

            try
            {
                bool updateResult = await _appModel.UserDal.UpdateAsync(user, updateProfileImage: false);
                if (updateResult)
                {
                    operationResult.Success = true;
                    operationResult.Message = "User updated successfully.";
                }
                else
                {
                    operationResult.Success = false;
                    operationResult.Message = "User could not be updated.";
                }
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "An error occurred while updating the user.";
            }
            return operationResult;
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
