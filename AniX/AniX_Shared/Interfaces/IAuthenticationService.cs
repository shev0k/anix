using Anix_Shared.DomainModels;
using AniX_Utility;
using System.Threading.Tasks;

namespace AniX_Shared.Interfaces
{
    public interface IAuthenticationService
    {
        Task<User> AuthenticateUserAsync(string username, string password);

        Task<OperationResult> RegisterUserAsync(string username, string email, string password, Stream profileImageStream = null, string contentType = null);
        Task<bool> DoesUsernameExistAsync(string username);
        Task<bool> DoesEmailExistAsync(string email);
        void SetWebUserSession(User user);
        Task<bool> ValidateUserAsync(string email, string password);
    }
}