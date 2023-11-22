using Anix_Shared.DomainModels;
using System.Threading.Tasks;
using AniX_Utility;


namespace AniX_Shared.Interfaces
{
    public interface IUserManagement
    {
        Task<OperationResult> CreateAsync(User user, Stream profileImageStream = null, string contentType = null);
        Task<bool> UpdateAsync(User user, bool updateProfileImage);
        Task<OperationResult> DeleteAsync(int id);
        Task<User> GetUserFromIdAsync(int id);
        Task<User> AuthenticateUserAsync(string username, string rawPassword);
        Task<List<User>> FetchFilteredAndSearchedUsersAsync(string filter, string searchTerm);
        Task<bool> DoesUsernameExistAsync(string username);
        Task<bool> DoesEmailExistAsync(string email);
        Task<User> GetUserByEmailAsync(string email);
    }
}
