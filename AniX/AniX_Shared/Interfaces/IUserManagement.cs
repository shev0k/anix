using Anix_Shared.DomainModels;
using System.Threading.Tasks;

namespace AniX_Shared.Interfaces
{
    public interface IUserManagement
    {
        Task<bool> CreateAsync(User user);
        Task<bool> UpdateAsync(User user);
        Task<bool> DeleteAsync(int id);
        Task<User> GetUserFromUsernameAsync(string username);
        Task<User> GetUserFromIdAsync(int id);
        Task<User> AuthenticateUserAsync(string username, string rawPassword);
        Task<List<User>> GetUsersInBatchAsync(int startIndex, int batchSize);
        //Task<List<User>> SearchUsersAsync(string searchText);
        //Task<List<User>> FetchFilteredUsersAsync(string filter);
        Task<List<User>> FetchFilteredAndSearchedUsersAsync(string filter, string searchTerm);
        Task<bool> DoesUsernameExistAsync(string username);
        Task<bool> DoesEmailExistAsync(string email);
    }
}