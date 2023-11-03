using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AniX_Shared.Interfaces;
using Microsoft.AspNetCore.Http;

namespace AniX_BusinessLogic
{
    public class SessionService : ISessionService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SessionService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void SetSessionAndCookie(string userId, string username, string sessionId, string profileImagePath)
        {
            _httpContextAccessor.HttpContext.Session.SetString("UserId", userId);
            _httpContextAccessor.HttpContext.Session.SetString("Username", username);
            _httpContextAccessor.HttpContext.Session.SetString("ProfileImagePath", profileImagePath);

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTimeOffset.Now.AddHours(1)
            };

            _httpContextAccessor.HttpContext.Response.Cookies.Append("SessionID", sessionId, cookieOptions);
        }

        public string GetUserId()
        {
            return _httpContextAccessor.HttpContext.Session.GetString("UserId");
        }

        public string GetUsername()
        {
            return _httpContextAccessor.HttpContext.Session.GetString("Username");
        }

        public string GetProfileImagePath()
        {
            return _httpContextAccessor.HttpContext.Session.GetString("ProfileImagePath");
        }

        public void SetUserName(string userName)
        {
            _httpContextAccessor.HttpContext.Session.SetString("UserName", userName);
        }

        public bool IsAuthenticated()
        {
            return !string.IsNullOrEmpty(_httpContextAccessor.HttpContext.Session.GetString("UserId"));
        }
        public void SignOut()
        {
            _httpContextAccessor.HttpContext.Session.Clear();
        }
    }
}
