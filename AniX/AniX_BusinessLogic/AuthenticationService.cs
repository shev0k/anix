using AniX_DAL;
using Anix_Shared.DomainModels;
using AniX_Shared.Interfaces;
using AniX_Utility;
using System;

namespace AniX_BusinessLogic
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserManagement _userDal;

        public AuthenticationService(IUserManagement userDal)
        {
            _userDal = userDal;
        }

        public User AuthenticateUser(string username, string password)
        {
            try
            {
                return _userDal.AuthenticateUser(username, password);
            }
            catch (Exception e)
            {
                ExceptionHandlingService.HandleException(e);
                return null;
            }
        }
    }
}
