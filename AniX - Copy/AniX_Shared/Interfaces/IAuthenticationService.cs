using Anix_Shared.DomainModels;
using System.Threading.Tasks;

namespace AniX_Shared.Interfaces
{
    public interface IAuthenticationService
    {
        Task<User> AuthenticateUserAsync(string username, string password);
        Task<bool> RegisterUserAsync(string username, string email, string password);
        Task<bool> DoesUsernameExistAsync(string username);
        Task<bool> DoesEmailExistAsync(string email);
        void SetWebUserSession(User user);
        bool ValidateUser(string email, string password);
    }
}