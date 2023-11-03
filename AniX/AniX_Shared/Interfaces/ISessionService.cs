using Microsoft.AspNetCore.Http;

namespace AniX_Shared.Interfaces{
    public interface ISessionService
    {
        void SetSessionAndCookie(string userId, string username, string sessionId, string profileImagePath);
        string GetUserId();
        string GetUsername();
        void SetUserName(string userName);
        string GetProfileImagePath();
        bool IsAuthenticated();
        void SignOut();
    }
}