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


        public ISessionService SessionService { get; set; }

        public AuthenticationService(IUserManagement userDal, IAzureBlobService blobService)
        {
            _userDal = userDal;
            _blobService = blobService;
        }

        public async Task<User> AuthenticateUserAsync(string username, string password)
        {
            try
            {
                return await _userDal.AuthenticateUserAsync(username, password);
            }
            catch (Exception e)
            {
                await ExceptionHandlingService.HandleExceptionAsync(e);
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

        public async Task<bool> RegisterUserAsync(string username, string email, string password, Stream profileImageStream = null, string contentType = null)
        {
            string salt = HashPassword.GenerateSalt();
            string hashedPassword = HashPassword.GenerateHashedPassword(password, salt);
            User newUser = new User
            {
                Username = username,
                Email = email,
                RegistrationDate = DateTime.Now,
                Banned = false,
                IsAdmin = false
            };
            newUser.UpdatePassword(hashedPassword, salt);

            bool isCreated = await _userDal.CreateAsync(newUser, profileImageStream, contentType);
            if (!isCreated)
            {
                return false;
            }

            return true;
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