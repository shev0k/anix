using AniX_DAL;
using Anix_Shared.DomainModels;
using AniX_Shared.Interfaces;
using AniX_Utility;
using System;
using System.Threading.Tasks;

namespace AniX_BusinessLogic
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserManagement _userDal;
        private readonly IAzureBlobService _blobService;
        private readonly IExceptionHandlingService _exceptionHandlingService;
        private readonly IErrorLoggingService _errorLoggingService;

        public ISessionService SessionService { get; set; }

        public AuthenticationService(
            IUserManagement userDal,
            IAzureBlobService blobService,
            IExceptionHandlingService exceptionHandlingService,
            IErrorLoggingService errorLoggingService)
        {
            _userDal = userDal;
            _blobService = blobService;
            _exceptionHandlingService = exceptionHandlingService;
            _errorLoggingService = errorLoggingService;
        }

        public async Task<User> AuthenticateUserAsync(string username, string password)
        {
            try
            {
                return await _userDal.AuthenticateUserAsync(username, password);
            }
            catch (Exception e)
            {
                bool handled = await _exceptionHandlingService.HandleExceptionAsync(e);
                if (!handled)
                {
                    await _errorLoggingService.LogErrorAsync(e, LogSeverity.Critical);
                }
                return null;
            }
        }

        public void SetWebUserSession(User user)
        {
            if (user != null && SessionService != null)
            {
                string sessionId = Guid.NewGuid().ToString();
                SessionService.SetSessionAndCookie(user.Id.ToString(), user.Username, sessionId, user.ProfileImagePath);
            }
        }

        public async Task<OperationResult> RegisterUserAsync(string username, string email, string password, Stream profileImageStream = null, string contentType = null)
        {
            OperationResult result = new OperationResult();

            // Check if username or email already exists
            if (await DoesUsernameExistAsync(username))
            {
                result.Success = false;
                result.Message = "Username already exists.";
                return result;
            }
            if (await DoesEmailExistAsync(email))
            {
                result.Success = false;
                result.Message = "Email already exists.";
                return result;
            }

            // Generate salt and hashed password
            string salt = HashPassword.GenerateSalt();
            string hashedPassword = HashPassword.GenerateHashedPassword(password, salt);

            // Create new user object
            User newUser = new User
            {
                Username = username,
                Email = email,
                RegistrationDate = DateTime.Now,
                Banned = false,
                IsAdmin = false
            };
            newUser.UpdatePassword(hashedPassword, salt);

            // Attempt to create the new user
            OperationResult createResult = await _userDal.CreateAsync(newUser, profileImageStream, contentType);
            if (!createResult.Success)
            {
                return createResult; // Directly return the OperationResult from CreateAsync
            }

            // If the user was successfully created
            result.Success = true;
            result.Message = "User registered successfully.";
            return result;
        }




        public async Task<bool> DoesUsernameExistAsync(string username)
        {
            return await _userDal.DoesUsernameExistAsync(username);
        }

        public async Task<bool> DoesEmailExistAsync(string email)
        {
            return await _userDal.DoesEmailExistAsync(email);
        }

        public async Task<bool> ValidateUserAsync(string email, string password)
        {
            var user = await AuthenticateUserAsync(email, password);
            return user != null;
        }
    }
}