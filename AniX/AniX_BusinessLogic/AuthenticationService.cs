using AniX_DAL;
using Anix_Shared.DomainModels;
using System;

namespace AniX_BusinessLogic
{
    public class AuthenticationService
    {
        public User AuthenticateUser(string username, string password)
        {
            try
            {
                UserDAL userDal = new UserDAL();
                return userDal.AuthenticateUser(username, password);
            }
            catch (Exception e)
            {
                ExceptionHandlingService.HandleException(e);
                return null;
            }
        }
    }
}