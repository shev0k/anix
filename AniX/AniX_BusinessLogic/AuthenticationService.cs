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

        public ISessionService SessionService { get; set; }
        public AuthenticationService(IUserManagement userDal)
        {
            _userDal = userDal;
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
                SessionService.SetSessionAndCookie(user.Id.ToString(), user.Username, "some_session_id_here");
            }
        }

        public async Task<bool> RegisterUserAsync(string username, string email, string password)
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

            return await _userDal.CreateAsync(newUser);
        }

        public async Task<bool> DoesUsernameExistAsync(string username)
        {
            return await _userDal.DoesUsernameExistAsync(username);
        }

        public async Task<bool> DoesEmailExistAsync(string email)
        {
            return await _userDal.DoesEmailExistAsync(email);
        }

        public bool ValidateUser(string email, string password)
        {
            var user = AuthenticateUserAsync(email, password).Result;
            return user != null;
        }
    }
}